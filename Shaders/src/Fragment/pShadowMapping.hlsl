#include <Lights.hlsl>
#include <Projector.hlsl>
#include <StandardPixelInput.hlsl>
#include <transform.hlsl>

// Filter Possible Val : MIN_MAG_MIP_POINT
//texture

SamplerComparisonState samShadow
{
	Filter = COMPARISON_MIN_MAG_LINEAR_MIP_POINT;
	// Return 0 for points outside the light frustum
	// to put them in shadow.
	AddressU = BORDER;
	AddressV = BORDER;
	AddressW = BORDER;
	BorderColor = float4(0.0f, 0.0f, 0.0f, 0.0f);
	ComparisonFunc = LESS_EQUAL;
};

SamplerState textureSampler{
	Filter = D3D11_FILTER_MIN_MAG_MIP_POINT;
	AddressU = Wrap;
	AddressV = Wrap;
};

float4 main(PixelInput input) : SV_Target{

	float percentageLight = 0.0f;

	for (int i = 0; i < lightCount; i++)
	{
		// On vérifie si la lumière est sensé projeter des ombres
		if (lights[i].IsCastingShadow)
		{
			// On récupère les informations concernant la projection
			float4x4	viewproj = mul(lights[i].Projection, lights[i].Transformation);
			float3		position	= LightPosition(lights[i]);
			float3		direction	= LightDirection(lights[i]);


			// On multiplie la position du pixel par la matrice du projecteur . Si x et y 
			// sont compris entre [-1 et 1] alors on peut les utiliser comme coordonnées de texture (je crois)
			float4 val = (mul(viewproj, input.realpos));

			// On divise par w pour trouver les coordonnées en NDC space ([-1;+1])
			val.x = (val.x / val.w / 2.0f + 0.5f);
			val.y =  1-(val.y / val.w / 2.0f + 0.5f);
			val.z = val.z / val.w ;
			

			/*if 
			(
				val.x >= 0.0f && val.x <= 1.0f &&
				val.y >= 0.0f && val.y <= 1.0f &&
				val.z >= 0.0f && val.z <= 1.0f &&
				val.w >= 0.0f && val.w <=1.0f 
			)
			{*/
				// On est dans un cas ou il peut y avoir une ombre
				float4 pixelValue = shadowmap0.Sample(textureSampler, val.xy);
				//return float4(val.z, 0.0f, 0.0f, 1.0f);

				// PCF Filtering 
				float deltaWidth = 1.0f / lights[i].shadowmapWidth;
				float deltaHeight = 1.0f / lights[i].shadowmapHeight;

				float4 pixels[4];

				// On récupère 4 texels voisin du texel du depth buffer projeté
				pixels[0] = shadowmap0.Sample(textureSampler, val.xy + float2(0.0f, 0.0f));
				pixels[1] = shadowmap0.Sample(textureSampler, val.xy + float2(deltaWidth, 0.0f));
				pixels[2] = shadowmap0.Sample(textureSampler, val.xy + float2(-deltaWidth, -deltaHeight));
				pixels[3] = shadowmap0.Sample(textureSampler, val.xy + float2(0.0f, -deltaHeight));

				bool results[4];

				float bias = 0.0f;

				if (activatePCF)
				{
					float cosTheta = abs(dot(input.normal.xyz, -normalize(input.realpos - position)));
					//return float4(acos(cosTheta), acos(cosTheta), acos(cosTheta), 1.0f);
						bias = acneBias* clamp(tan(acos(cosTheta)), 0.0f, 1.0f); // cosTheta is dot( n,l ), clamped between 0 and 1
						//bias = clamp(bias, 0, 0.01f);
				}
				else
				{
					bias = acneBias;
				}
						
				// On effectue les test pour savoir si le pixel est dans une ombre ou non
				for (int i = 0; i < 4; i++)
				{
					results[i] = pixels[i].r < val.z - bias;
				}

				// Transform to texel space.
				//float2 texelPos = float2(val.x*lights[i].shadowmapWidth, val.y*lights[i].shadowmapHeight);

				// Determine the interpolation amounts.
				//float2 t = frac(texelPos);

				// Interpolate results.
						
				percentageLight =  lerp(lerp(results[0], results[1], val.x), lerp(results[2], results[3], val.x), val.y);

				float dx = 1.0f / 2000.0f;

				const float2 offsets[9] =
				{
					float2(-dx, -dx), float2(0.0f, -dx), float2(dx, -dx),
					float2(-dx, 0.0f), float2(0.0f, 0.0f), float2(dx, 0.0f),
					float2(-dx, +dx), float2(0.0f, +dx), float2(dx, +dx)
				};

				float offset = 1.0 / 9.0f;

				for (int j = 0; j < 9; j++)
				{
					if (shadowmap0.Sample(textureSampler, val.xy + offsets[j]).r < (val.z - bias))
					{
						percentageLight += offset;
					}
				}

				
//					else{
//						 percentageLight = shadowmap0.SampleCmpLevelZero(samShadow, val.xy, val.z).r;
///*
//						if (pixel.r < val.z - acnebias){
//							pixelValue =  float4(0.0f, 0.0f, 0.0f, 1.0f);
//						}
//						else{
//							pixelValue =  float4(0.0f, 0.0f, 0.0f, 0.0f);
//						}*/
//					}
			//}
		}
	}
	
	float3 normal = normalize(input.normal.xyz);
	float3 eye = normalize(eyePosition - input.realpos.xyz);

	float3 finalcolor = ComputeLight(input.realpos, input.normal.xyz, cameraPosition);

	return lerp(float4(finalcolor,1.0f), float4(0.0f, 0.0f, 0.0f, 1.0f), (percentageLight));

}