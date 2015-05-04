#include "StandardPixelInput.hlsl"
#include <TextureConstantBuffer.hlsl>

cbuffer modeldata : register(b2){
	bool	IsGlowing;
	float4	GlowColor;
};

// Filter Possible Val : MIN_MAG_MIP_POINT
//texture
Texture2D textureMap : register(t0);
SamplerState textureSampler{
	Filter = D3D11_FILTER_MIN_MAG_MIP_POINT;
	AddressU = Wrap;
	AddressV = Wrap;
};

float4 main(PixelInput input) : SV_Target{

	if (!IsGlowing){
		return float4(0.0f, 0.0f, 0.0f, 0.0f);
	}
	if (!HasTexture){
		return GlowColor;
	}

	float2 uvs = input.uv;

	uvs.x = uvs.x * TilingWidth;
	uvs.y = uvs.y * TilingHeight;

	return textureMap.Sample(textureSampler, float2(XOffset, YOffset) + uvs);
}