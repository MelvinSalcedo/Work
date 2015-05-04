#include <transform.hlsl>
#include <StandardPixelInput.hlsl>

cbuffer data2 : register(b2){
	int ImageHeight;
}

// Filter Possible Val : MIN_MAG_MIP_POINT
Texture2D textureMap : register(t0);
SamplerState textureSampler{
	Filter = MIN_MAG_MIP_POINT;
	AddressU = Wrap;
	AddressV = Wrap;
};

float4 main(PixelInput input) : SV_Target{

	float texelSizeHeight = 1.0f / ImageHeight;
	float4 val = float4(0.0f, 0.0f, 0.0f, 0.0f);

	
	return val;
}