#include <Lights.hlsl>
#include <StandardPixelInput.hlsl>
#include <TextureConstantBuffer.hlsl>

// Filter Possible Val : MIN_MAG_MIP_POINT
//texture
Texture2D textureMap : register(t0);

cbuffer lsystem : register(b2){
	float t;
}

SamplerState textureSampler{
	Filter = D3D11_FILTER_MIN_MAG_MIP_POINT;
	AddressU = Wrap;
	AddressV = Wrap;
};

float4 main(PixelInput input) : SV_Target{

	float2 uvs = input.uv;

	uvs.x = uvs.x * TilingWidth;
	uvs.y = uvs.y * TilingHeight;

	if (HasTexture){
		return textureMap.Sample(textureSampler, float2(XOffset, YOffset) + uvs);
	}
	else{
		if (uvs.x < t){

			return MainColor;
		}
		else{
			return  float4(0.0f,0.0f,0.0F,0.0F);
		}
	}
}