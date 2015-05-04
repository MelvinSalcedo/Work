#include <Lights.hlsl>
#include <StandardPixelInput.hlsl>
#include <TextureConstantBuffer.hlsl>

// Filter Possible Val : MIN_MAG_MIP_POINT
//texture
Texture2D textureMap : register(t0);

SamplerState textureSampler{
	Filter = D3D11_FILTER_MIN_MAG_MIP_POINT;
	AddressU = Wrap;
	AddressV = Wrap;
};

float4 main(PixelInput input) : SV_Target{

	if (HasTexture){

		float2 uvs = input.uv;

		uvs.x = uvs.x * TilingWidth;
		uvs.y = uvs.y * TilingHeight;

		float4 texel = textureMap.Sample(textureSampler, float2(XOffset, YOffset) + uvs);
		float4 FinalColor = MainColor; 
		FinalColor.a = texel.a;
		return FinalColor;
	}
	else{
		return MainColor;
	}
}