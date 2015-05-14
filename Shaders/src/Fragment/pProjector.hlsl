#include <StandardPixelInput.hlsl>
#include <Lights.hlsl>
#include <Projector.hlsl>

// Filter Possible Val : MIN_MAG_MIP_POINT
//texture

SamplerState textureSampler : register(s0);

float4 main(PixelInput input) : SV_Target
{
	float4 pixelValue = float4(0.0f, 0.0f, 0.0f, 0.0f);

	for (int i = 0; i < projectorCount; i++)
	{
		float4x4	viewproj	= mul(projectors[i].Projection, projectors[i].Transformation);
		float3		position	= mul(projectors[i].Inverse, float4(0.0f, 0.0f, 0.0f, 1.0f)).xyz;
		float3		direction	= float4(normalize(mul(transpose(projectors[i].Transformation), float4(0.0f, 0.0f, 1.0f, 0.0f)).xyz), 0.0f).xyz;

		// En directx, la caméra regarde les Z positifs
		// contrairement à Opengl (ta race)

		//return float4(direction,1.0f);

		// On multiplie la position du pixel par la matrice du projecteur . Si x et y 
		// sont compris entre [-1 et 1] alors on peut les utiliser comme coordonnées de texture (je crois)
		float4 val = (mul(viewproj, input.realpos));

		// Bon apparemment faut toucher aux coordonnées homogènes ... faudrait que je me mette un peu à jour sur ce truc
		// La projection touche aux coordonnées homogènes, il faut donc diviser les coordoonées x/y/z par w pour 
		// retrouver la bonne valeur
		val.x = 1 - (val.x / val.w / 2.0f + 0.5f);
		val.y = 1 - (val.y / val.w / 2.0f + 0.5f);
		val.z = val.z / val.w / 2.0f + 0.5f;

		return float4(direction,1.0f);
		//return float4(acos(dot(normalize(direction), input.normal.xyz)) /3.141592f , 0.0f, 0.0f, 1.0f);

		//return float4(val.z, 0.0f, 0.0f, 1.0f);
		
		if (acos(dot(normalize(direction), input.normal.xyz)) > 3.141592f / 2.0f)
		{
			if (val.x >= 0.0f && val.x <= 1.0f && val.y >= 0.0f && val.y <= 1.0f && val.w>0 && val.z>0.0f && val.z<1.0f){
				pixelValue = projectorTexture0.Sample(textureSampler, val.xy);
			}
			else{
				pixelValue = float4(0.0f, 0.0f, 0.0f, 0.0f);
			}
		}
	}

	float3 normal = normalize(input.normal.xyz);
	float3 eye = normalize(eyePosition - input.realpos.xyz);

	float4 finalcolor = float4(0.0, 0.0, 0.0, 0.0);
		
	for (int i = 0; i < lightCount; i++)
	{
		if (lights[i].Type == 0){
			//finalcolor += PointLight(lights[i], eye, normal, input.realpos.xyz);
		}
		else if (lights[i].Type == 1){
			//finalcolor += DirectionalLight(lights[i], eye, normal, input.realpos.xyz);
		}
		else{
			finalcolor += SpotLight(lights[i], eye, normal, input.realpos.xyz);
		}
	}

	if (pixelValue.w > 0.0f)
	{
		return lerp(pixelValue, finalcolor, 0.5f);
	}
	return (finalcolor);
}