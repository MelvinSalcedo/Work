#include <Lights.hlsl>
#include <StandardPixelInput.hlsl>

// Filter Possible Val : MIN_MAG_MIP_POINT
//texture
Texture2D textureMap : register(t0);

SamplerState textureSampler : register(s0);

float4 main(PixelInput input) : SV_Target{
	float pixel = textureMap.Sample(textureSampler, input.uv).r;
	float gpixel = textureMap.Sample(textureSampler, input.uv).g;
	
	return float4(pixel, pixel, pixel, 1.0f);
}