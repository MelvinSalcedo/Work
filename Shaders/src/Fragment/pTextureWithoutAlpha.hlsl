#include <Lights.hlsl>
#include <StandardPixelInput.hlsl>

// Filter Possible Val : MIN_MAG_MIP_POINT
//texture
Texture2D textureMap : register(t0);

SamplerState textureSampler{
	Filter = D3D11_FILTER_MIN_MAG_MIP_POINT;
	AddressU = Wrap;
	AddressV = Wrap;
};

float4 main(PixelInput input) : SV_Target{
	return float4(textureMap.Sample(textureSampler, input.uv).xyz,1.0f);
}