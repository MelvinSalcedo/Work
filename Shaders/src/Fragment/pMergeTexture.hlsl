#include <Lights.hlsl>
#include <StandardPixelInput.hlsl>

cbuffer data2 : register(b2){
	float intensity; // compris entre 0 et 1, valeur utilisé pour lerp les deux textures
}

Texture2D tex0 : register(t0);
Texture2D tex1 : register(t1);


// Filter Possible Val : MIN_MAG_MIP_POINT
//texture
SamplerState textureSampler{
	Filter = D3D11_FILTER_MIN_MAG_MIP_POINT;
	AddressU = Wrap;
	AddressV = Wrap;
};


float4 main(PixelInput input) : SV_Target{

	float4 source	= tex0.Sample(textureSampler, input.uv);
	float4 dest		= tex1.Sample(textureSampler, input.uv);

	// Equation de transparence
	return  float4(source.xyz * source.w + (dest.xyz* 1.0)* (1 - source.w), source.w*source.w + dest.w*(1-source.w));
	return dest;
	
}