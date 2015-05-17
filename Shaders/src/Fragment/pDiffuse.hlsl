#include <Lights.hlsl>
#include <transform.hlsl>
#include <SimplePixelInput.hlsl>
#include <TextureConstantBuffer.hlsl>

// Filter Possible Val : MIN_MAG_MIP_POINT
//texture
Texture2D tex0 : register(t0);
SamplerState textureSampler{
	Filter = MIN_MAG_MIP_LINEAR;
	AddressU = Wrap;
	AddressV = Wrap;
};


float4 main(PixelInput input) : SV_Target{

	float2 uv = input.uv;
	float3 finalcolor = float3(0.0, 0.0, 0.0);
	float3 color;

	if (HasTexture)
	{
		float2 uvs = input.uv;

		uvs.x = uvs.x * TilingWidth;
		uvs.y = uvs.y * TilingHeight;

		color = tex0.Sample(textureSampler, float2(XOffset, YOffset) + uvs);
	}
	else
	{
		color = MainColor;
	}

	finalcolor =  ComputeLight(input.realpos, input.normal.xyz, cameraPosition);
	
	float4 lolilol = (float4(color, 1.0f)) + (float4(finalcolor, 1.0f));
		lolilol.w = 1.0f;
	return  lolilol;
	//return float4(color, 1.0f);
}

