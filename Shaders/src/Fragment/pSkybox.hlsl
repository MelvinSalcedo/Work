#include<SkyboxPixelInput.hlsl>

// Filter Possible Val : MIN_MAG_MIP_POINT
//MIN_MAG_MIP_LINEAR
//texture
TextureCube  textureMap : register(t0);

SamplerState textureSampler
{
	Filter = MIN_MAG_MIP_LINEAR;
	AddressU = Wrap;
	AddressV = Wrap;
};

float4 main(PixelInput input) : SV_Target
{
	return textureMap.Sample(textureSampler, input.stuff.zyx);
}