#include <transform.hlsl>
#include <StandardPixelInput.hlsl>

cbuffer data2 : register(b2){
	int ImageHeight;
	int BlurSize;		// Correspond aux nombre de pixel qu'on compare pour effectué le blur
}

// Filter Possible Val : MIN_MAG_MIP_POINT
Texture2D textureMap : register(t0);
SamplerState textureSampler{
	Filter = MIN_MAG_MIP_POINT;
	AddressU = Wrap;
	AddressV = Wrap;
};

float4 main(PixelInput input) : SV_Target{

	float texelSizeHeight = 1.0f / (float)ImageHeight;

	float4 sum = float4(0.0f,0.0f,0.0f,0.0f);

	float weight1 = 0.109317;
	float weight2 = 0.107159;
	float weight3 = 0.100939;
	float weight4 = 0.091364;
	float weight5 = 0.079465;
	float weight6 = 0.066414;

	sum += textureMap.Sample(textureSampler, input.uv + float2(0.0f, -5.0*texelSizeHeight)) * weight6;
	sum += textureMap.Sample(textureSampler, input.uv + float2(0.0f, -4.0*texelSizeHeight)) * weight5;
	sum += textureMap.Sample(textureSampler, input.uv + float2(0.0f, -3.0*texelSizeHeight)) * weight4;
	sum += textureMap.Sample(textureSampler, input.uv + float2(0.0f, -2.0*texelSizeHeight)) * weight3;
	sum += textureMap.Sample(textureSampler, input.uv + float2(0.0f, -texelSizeHeight)) * weight2;
	sum += textureMap.Sample(textureSampler, input.uv + float2(0.0f, 0.0f)) * weight1;
	sum += textureMap.Sample(textureSampler, input.uv + float2(0.0f, texelSizeHeight)) * weight2;
	sum += textureMap.Sample(textureSampler, input.uv + float2(0.0f, 2.0*texelSizeHeight)) * weight3;
	sum += textureMap.Sample(textureSampler, input.uv + float2(0.0f, 3.0*texelSizeHeight)) * weight4;
	sum += textureMap.Sample(textureSampler, input.uv + float2(0.0f, 4.0*texelSizeHeight)) * weight5;
	sum += textureMap.Sample(textureSampler, input.uv + float2(0.0f, 5.0*texelSizeHeight)) * weight6;

	return sum;
}