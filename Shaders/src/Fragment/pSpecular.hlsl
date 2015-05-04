#include <Lights.hlsl>
#include <StandardPixelInput.hlsl>
#include <TextureConstantBuffer.hlsl>

float4 main(PixelInput input) : SV_Target{


	float3 normallol = normalize(input.normal.xyz);
	float3 finalcolor = float3(0.0f,0.0f,0.0F);
	float3 color = MainColor;


	for (int i = 0; i< lightCount; i++){

		if (lights[i].Type == 0){ 
			//finalcolor += PointLight(lights[i], eye, normal, input);
		}
		else if (lights[i].Type == 1){
			//finalcolor += DirectionalLight(lights[i], color, -input.view, normallol, input.light0);
		}
		else{
			//finalcolor += SpotLight(lights[i], eye, normal, input);
		}
	}

	return float4(finalcolor, 1.0f);
}

