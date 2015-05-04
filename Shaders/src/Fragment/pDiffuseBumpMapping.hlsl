#include <Lights.hlsl>
#include <StandardPixelInput.hlsl>
#include <Transform.hlsl>
// Filter Possible Val : MIN_MAG_MIP_POINT
//texture
Texture2D textureMap : register(t0);
Texture2D normalMap: register(t1);

SamplerState textureSampler : register(s0);

float4 main(PixelInput input) : SV_Target{
	
	float2 uv = input.uv ;

	float3 normal = (normalMap.Sample(textureSampler, uv).xyz);
	//return float4(normal, 1.0F);
	normal.x = normal.x*2.0f - 1.0f;
	normal.y = normal.y*2.0f - 1.0f;
	normal.z = normal.z*2.0f ;
	
	float4 color = textureMap.Sample(textureSampler, uv);
	float3 finalcolor = float3(0.0, 0.0, 0.0);

	for (int i = 0; i < lightCount; i++){

		if (lights[i].Type == 0){
			//finalcolor += PointLight(lights[i], eye, normal, input.realpos.xyz);
		}
		else if (lights[i].Type == 1){

			//finalcolor += DirectionalLight(lights[i], color.xyz, -input.view, normal, input.light0);
		}
		else{
			//finalcolor += SpotLight(lights[i], eye, normal, input.realpos.xyz);
		}
	}
	return float4(finalcolor, color.w);
}