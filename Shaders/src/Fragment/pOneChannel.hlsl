#include <Lights.hlsl>
#include <StandardPixelInput.hlsl>
#include <TextureConstantBuffer.hlsl>

// Ce shader va récupérer le premier composant (rouge donc) de la texture et la transformer en niveau de gris
// Utile pour débugger la texture retourné par le DepthStencilBuffer
Texture2D textureMap : register(t0);

SamplerState textureSampler
{
	Filter = D3D11_FILTER_MIN_MAG_MIP_POINT;
	AddressU = Wrap;
	AddressV = Wrap;
};

float4 main(PixelInput input) : SV_Target
{
	if (HasTexture)
	{
		float2 uvs = input.uv;

			uvs.x = uvs.x * TilingWidth;
		uvs.y = uvs.y * TilingHeight;
		float4 colorlol = textureMap.Sample(textureSampler, float2(XOffset, YOffset) + uvs);
		return float4(colorlol.x, colorlol.x, colorlol.x,1.0f);
	}
	else
	{
		return MainColor;
	}
}