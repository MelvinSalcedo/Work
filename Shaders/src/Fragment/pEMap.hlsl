#include <Lights.hlsl>
#include <transform.hlsl>
#include <StandardPixelInput.hlsl>
#include <TextureConstantBuffer.hlsl>

// Filter Possible Val : MIN_MAG_MIP_POINT
//texture
TextureCube tex0 : register(t0);

SamplerState textureSampler{
	Filter = MIN_MAG_MIP_LINEAR;
	AddressU = Wrap;
	AddressV = Wrap;
};


float4 main(PixelInput input) : SV_Target{

	float3 finalcolor = float3(0.0, 0.0, 0.0);
	float3 color;

	if (HasTexture)
	{
		float2 uvs = input.uv;

		uvs.x = uvs.x * TilingWidth;
		uvs.y = uvs.y * TilingHeight;

		color = tex0.Sample(textureSampler, input.normal);
	}
	else
	{
		color = MainColor;
	}


	for (int i = 0; i < lightCount; i++)
	{
		if (lights[i].Type == 0)
		{
			finalcolor += PointLight(lights[i], (normalize(input.realpos - cameraPosition).xyz), input.normal.xyz, input.realpos);
		}
		else if (lights[i].Type == 1){

			//finalcolor += DirectionalLight(lights[i], color.xyz, -input.view, input.normal, input.light0);
		}
		else
		{
			//finalcolor += SpotLight(lights[i], eye, normal, input.realpos.xyz);
		}
	}
	return  (float4(color, 1.0f)) + (float4(finalcolor, 1.0f));
	//return float4(color, 1.0f);
}

