#include "transform.hlsl"
#include "Lights.hlsl"
#include <StandardVertexInput.hlsl>
#include <StandardPixelInput.hlsl>

struct Wave{
	float amplitude;
	float wavelength;
	float speed;
	float2 direction;
};

cbuffer datastuff : register(b2){
	float4 maincolor;
	int wavecount;
	float time;
	Wave waves[10];
};


float ComputeWave(float A, float L, float F, float S, float2 dir, float2 xy){
	return A*sin(dot(dir, xy)*F + (time * S));
}

float WaveXDerivative(float A, float L, float F, float S, float2 dir, float x, float y){
	return A*dir.x*F*cos(dot(dir, float2(x, y))*F + time*S);
}

float WaveYDerivative(float A, float L, float F, float S, float2 dir, float x, float y){
	return (A* dir.y* F * cos(dot(dir, float2(x, y))*F + time*S));
}


float3 WaveNormal(float x, float y){

	float dx = 0.0;
	float dy = 0.0;

	for (int i = 0; i< wavecount; i++){

		float A = waves[i].amplitude;
		float L = waves[i].wavelength;
		float F = (2 * 3.14159) / L;
		float S = waves[i].speed*F;
		float2 dir = normalize(waves[i].direction);

			dx += WaveXDerivative(A, L, F, S, dir - float2(x, y), x, y);
		dy += WaveYDerivative(A, L, F, S, dir - float2(x, y), x, y);
	}
	return normalize(float3(-dx, 1.0, -dy));
}

PixelInput main(VertexInput input)
{
	PixelInput output = (PixelInput)0;

	for (int i = 0; i< wavecount; i++){

		float A = waves[i].amplitude;
		float L = waves[i].wavelength;
		float F = (2 * 3.14159) / L;
		float S = waves[i].speed*F;
		float2 dir = normalize(waves[i].direction);

			input.position.y += ComputeWave(A, L, F, S, dir - input.position.xz, input.position.xz);
	}


	output.position = mul(mul(projection, mul(view, model)), float4(input.position, 1.0f));
	output.uv = input.uv;
	output.realpos = mul(model, input.position);
	output.normal = float4(WaveNormal(input.position.x, input.position.z), 1.0f);
	output.normal = float4(wavecount, 0.0f, 0.0f, 1.0f);
	return output;
}
