#include <Lights.hlsl>
#include <StandardPixelInput.hlsl>

cbuffer data2 : register(b2){
	int ImageWidth;
	int BlurSize;		// Correspond aux nombre de pixel qu'on compare pour effectuer le blur
}

// Filter Possible Val : MIN_MAG_MIP_POINT
//texture
Texture2D textureMap : register(t0);
SamplerState textureSampler{
	Filter = MIN_MAG_MIP_POINT;
	AddressU = Wrap;
	AddressV = Wrap;
};

float4 main(PixelInput input) : SV_Target{

	// On récupère la taille d'un pixel, les texels étant sur l'intervale [0;1]
	float texelSizeWidth = 1.0 / (float)ImageWidth;

	float4 sum = float4(0.0f, 0.0f, 0.0f, 0.0F);

	float weight1 = 0.109317;
	float weight2 = 0.107159;
	float weight3 = 0.100939;
	float weight4 = 0.091364;
	float weight5 = 0.079465;
	float weight6 = 0.066414;


	sum += textureMap.Sample(textureSampler, input.uv + float2(-5.0*texelSizeWidth, 0.0f)) * weight6;
	sum += textureMap.Sample(textureSampler, input.uv + float2(-4.0*texelSizeWidth, 0.0f)) * weight5;
	sum += textureMap.Sample(textureSampler, input.uv + float2(-3.0*texelSizeWidth, 0.0f)) * weight4;
	sum += textureMap.Sample(textureSampler, input.uv + float2(-2.0*texelSizeWidth, 0.0f)) * weight3;
	sum += textureMap.Sample(textureSampler, input.uv + float2(-texelSizeWidth, 0.0f)) * weight2;
	sum += textureMap.Sample(textureSampler, input.uv + float2(0.0f, 0.0f)) * weight1;
	sum += textureMap.Sample(textureSampler, input.uv + float2(texelSizeWidth, 0.0f)) * weight2;
	sum += textureMap.Sample(textureSampler, input.uv + float2(2.0*texelSizeWidth, 0.0f)) * weight3;
	sum += textureMap.Sample(textureSampler, input.uv + float2(3.0*texelSizeWidth, 0.0f)) * weight4;
	sum += textureMap.Sample(textureSampler, input.uv + float2(4.0*texelSizeWidth, 0.0f)) * weight5;
	sum += textureMap.Sample(textureSampler, input.uv + float2(5.0*texelSizeWidth, 0.0f)) * weight6;
	
	return sum;

}