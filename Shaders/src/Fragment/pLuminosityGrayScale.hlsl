#include <StandardPixelInput.hlsl>

// Ce shader transforme une texture en niveau de gris en prenant en compte la sensibilité de l'oeil humain
// aux différentes couleurs. L'oeil humain a plus de percepteur de vert que de bleu ou de rouge

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

	float greyValue = 0.21 * color.r + 0.72 * color.g + 0.07 * color.b;
	

	return float4(greyValue, greyValue, greyValue, color.a);
}