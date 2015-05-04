#include <StandardPixelInput.hlsl>

// Ce shader transforme une texture en niveau de gris en faisant la composant maximale et minimale, le tout 
// divisé par 2


// Filter Possible Val : MIN_MAG_MIP_POINT
//texture
Texture2D tex0 : register(t0);
SamplerState textureSampler{
	Filter = D3D11_FILTER_MIN_MAG_MIP_POINT;
	AddressU = Wrap;
	AddressV = Wrap;
};

float4 main(PixelInput input) : SV_Target{


	float4 color = tex0.Sample(textureSampler, input.uv);

	float greyValue = (max(max(color.r, color.g), color.b) + min(color.r, min( color.g, color.b)))/2.0f;

	return float4(greyValue, greyValue, greyValue, color.a);
}