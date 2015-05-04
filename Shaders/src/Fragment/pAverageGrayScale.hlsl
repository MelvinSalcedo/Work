#include "StandardPixelInput.hlsl"

// Ce shader se contente de transformer une image en niveau de gris en faisant la moyenne de la somme
// de ses composantes RGB

// Filter Possible Val : MIN_MAG_MIP_POINT
//texture
Texture2D tex0 : register(t0);
SamplerState textureSampler{
	Filter = D3D11_FILTER_MIN_MAG_MIP_POINT;
	AddressU = Wrap;
	AddressV = Wrap;
};

float4 main(PixelInput input) : SV_Target{

	float4	color		=	tex0.Sample(textureSampler, input.uv);
	float	greyValue	=	(color.r + color.g + color.b) / 3.0f;
	
	return float4(greyValue, greyValue, greyValue, color.a);
}