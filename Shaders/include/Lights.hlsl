struct Light{

	float4x4 Projection;		// La projection sera utilisé pour la gestion des ombres
	float4x4 Transformation;	// Matrice contenant les transformation de la lumière. On l'utilisera pour déduire
								// la position ainsi que l'orientation de la lumière

	float4x4 Inverse;

	float4 Ambiantcolor;
	float4 LightColor;

	float	SpecularIntensity;	// Détermine l'intensité de la valeur spéculaire, plus elle est élevé, plus l'effet est faible
	// une valeur de -1 désactive la composante spéculaire

	
	float Intensity;			// S'utilise conjointement avec la variable Range pour déterminer l'illumination d'un pixel pour
								// SpotLight et PointLight

	float Range;			// Détermine la portée de la lumière (valable pour PointLight et SpotLight) Plus la portée diminue, plus la 
							// "puissance" de la couleur diffuse diminue en fonction de la variable "intensity"

	int Type;				// 0 = PointLight, 1 = DirectionalLight, 2 = SpotLight
	float Angle;			// Utilisé pour déterminer "l'ouverture" d'une SpotLight
	bool IsCastingShadow;	// Permet d'activer ou désactiver l'utilisation d'ombres pour un lumière donnée

	float shadowmapWidth; // On a besoin de connaitre la taille de la shadowmap pour réaliser une PCF (percentage filtering)
	float shadowmapHeight;
};

cbuffer datalol:register(b4)
{
	float AmbiantCoef;
	float DiffuseCoef;
	float SpecularCoef;
}

// Dans mon bordel perso, l'adresse "1" devra être reservé aux lumières
// Contient 10 lumières maximum pour l'instant
cbuffer data : register(b1){
	int		lightCount;
	float3	eyePosition;
	float	acneBias;
	bool	activatePCF;
	Light	lights[10] ;
}


// En théorie on peut bind jusque 128 textures à l'intérieur des shaders directx11, alors on va en réservé une petite dizaine 
// pour les shadow Maps

Texture2D shadowmap0: register(t20);
Texture2D shadowmap1: register(t21);
Texture2D shadowmap2: register(t22);
Texture2D shadowmap3: register(t23);
Texture2D shadowmap4: register(t24);
Texture2D shadowmap5: register(t25);
Texture2D shadowmap6: register(t26);
Texture2D shadowmap7: register(t27);
Texture2D shadowmap8: register(t28);
Texture2D shadowmap9: register(t29);



float3 LightPosition(Light light){
	return	mul(light.Inverse, float4(0.0f, 0.0f, 0.0f, 1.0f)).xyz;
}

float3 LightDirection(Light light){
	return (mul(transpose(light.Transformation), float4(0.0f, 0.0f, 1.0f, 0.0f)).xyz);
}

float3 ComputeColor(Light light, float3 eye, float3 normal, float3 lightDirection, float3 halfvector, float3 lightposition, float3 pixelPosition){

		float3 Is = float3(0.0f, 0.0f, 0.0f);

		float	lightdistance = distance(lightposition, pixelPosition);

		// On commence par calculer la composante ambiante
		float3 Ia = saturate(AmbiantCoef*light.Ambiantcolor);

		float coefdistanceintensity = min(
		((1 - lightdistance / light.Range)*(light.Intensity)),
		1.0f
		);


		float3 Id = float3(0.0, 0.0, 0.0);

		if (max(dot(normal, lightDirection), 0.0)>0.0){


			// Composante Diffuse
			Id = (light.LightColor)* max(dot(normal, lightDirection), 0.0)* coefdistanceintensity;
			Id = saturate(DiffuseCoef*Id);

			if (dot(normal, lightDirection) < 0.0)
				// light source on the wrong side?
			{
				Is = float3(0.0, 0.0, 0.0);
				// no specular reflection
			}
			else{

				float specularangle = max(dot(normal, halfvector), 0.0);
				//// Composante Spéculaire
				Is = (light.LightColor)*pow(specularangle, light.SpecularIntensity) *coefdistanceintensity;
				Is = saturate(SpecularCoef*Is);
			}
		}
	// Addition des différentes composantes
	return (Ia + Id + Is);

}

float3 DirectionalLight(Light light,float3 diffuseColor, float3 eye, float3 normal, float3 lightDirection){

	eye				=	normalize(eye);
	normal			=	normalize(normal);
	lightDirection	= -	normalize(lightDirection);

	float3	halfvector = normalize(eye + lightDirection);

	float3 Is = float3(0.0f, 0.0f, 0.0f);

	// On commence par calculer la composante ambiante
	float3 Ia = saturate(light.Ambiantcolor);

	float3 Id = float3(0.0, 0.0, 0.0);

		if (max(dot(normal, lightDirection), 0.0)>0.0){

			float coefdistanceintensity = 1.0f;

			// Composante Diffuse
			Id = (diffuseColor)* max(dot(normal, lightDirection), 0.0)* coefdistanceintensity;
			Id = saturate(Id);

			if (dot(normal, lightDirection) < 0.0)
				// light source on the wrong side?
			{
				Is = float3(0.0, 0.0, 0.0);
				// no specular reflection
			}
			else{

				float specularangle = max(dot(normal, halfvector), 0.0);
				//// Composante Spéculaire
				Is = pow(specularangle, light.SpecularIntensity) *coefdistanceintensity;
				Is = saturate(Is);
			}

		

		}
	// Addition des différentes composantes
	return (Ia + Id +  Is);
}




float4 SpotLight(Light light, float3 eye, float3 normal, float3 pixelPosition){


		float3 position		= (LightPosition(light));
		float3 direction	= (LightDirection(light));

		float3	incidentvector	= normalize(position - pixelPosition);
		float	lightdistance	= distance(position , pixelPosition);
		float3	halfvector		= normalize(eye + incidentvector);

		float	specularangle = max(dot(normal, halfvector), 0.0);

		float4 Ia = clamp(light.Ambiantcolor, 0.0, 1.0);
		float4 Id = float4(0.0, 0.0, 0.0, 0.0);

		float angle = acos(dot(direction, -incidentvector));

		if (angle < light.Angle){

			if (max(dot(normal, incidentvector), 0.0)>0.0){

			float coefdistanceintensity = min(
				(1 - angle / light.Angle)*((1 - lightdistance / light.Range)*(light.Intensity)),
				1.0f
				);

			if (light.Type == 1){
				coefdistanceintensity = 1.0f;
			}
			// Composante Diffuse
			Id = (light.LightColor) * max(dot(normal, incidentvector), 0.0)* coefdistanceintensity;
			Id = clamp(Id, 0.0, 1.0);

		}
		// Addition des différentes composantes
	}

	return (Ia + Id);


}


float3 PointLight(Light light, float3 eye, float3 normal, float4 pixelPosition)
{
	float3 position = LightPosition(light);
	eye = normalize(-eye);
	normal = normalize(normal);
	float3 lightDirection = normalize(position - pixelPosition.xyz);
	float3	halfvector = normalize(eye + lightDirection);

	return ComputeColor(light, eye, normal, lightDirection, halfvector, position, pixelPosition.xyz);
}

