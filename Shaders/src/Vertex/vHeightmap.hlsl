#include <transform.hlsl>
#include <Lights.hlsl>
#include <StandardVertexInput.hlsl>
#include <StandardPixelInput.hlsl>


// Filter Possible Val : MIN_MAG_MIP_POINT
//texture
Texture2D textureMap;
SamplerState textureSampler
{
	Filter = MIN_MAG_MIP_POINT;
	AddressU = Wrap;
	AddressV = Wrap;
};


PixelInput main(VertexInput input){

	PixelInput output = (PixelInput)0;

	float4 offset = textureMap.SampleLevel(textureSampler, input.uv, 1);
	input.position.y += offset.r * 1.0f;

	output.position = mul(mul(projection, mul(view, model)), input.position);

	output.uv = input.uv;

	return output;
}
