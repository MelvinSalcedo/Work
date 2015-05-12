#include <transform.hlsl>
#include <StandardVertexInput.hlsl>
#include <SkyboxPixelInput.hlsl>

PixelInput main(VertexInput input)
{
	PixelInput output;

	output.position = mul(mul(projection, mul(view, model)), float4(input.position, 1.0f));
	output.stuff = float4( input.position,1.0f);
	return output;

}
