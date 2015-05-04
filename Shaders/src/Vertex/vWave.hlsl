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
	float4	maincolor;
	int		wavecount;
	float	time;
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

// Retourne la bitangente
float3 WaveBitangent(float x, float y){

	float dx = 0.0;

	for (int i = 0; i< wavecount; i++){

		float A = waves[i].amplitude;
		float L = waves[i].wavelength;
		float F = (2 * 3.14159) / L;
		float S = waves[i].speed*F;
		float2 dir = normalize(waves[i].direction);

			dx += WaveXDerivative(A, L, F, S, dir, x, y);
	}

	return normalize(float3(1, 0, dx));
}

// Retourne la tangente
float3 WaveTangent(float x, float y){

	float dy = 0.0;

	for (int i = 0; i< wavecount; i++){

		float A = waves[i].amplitude;
		float L = waves[i].wavelength;
		float F = (2 * 3.14159) / L;
		float S = waves[i].speed*F;
		float2 dir = normalize(waves[i].direction);

			dy += WaveYDerivative(A, L, F, S, dir, x, y);
	}

	return normalize(float3(0, 1, dy));
}

// Retourne la normale, la tangete et la bitangente
float WaveNormal(float x, float y){

	float dx = 0.0;
	float dy = 0.0;

	for (int i = 0; i< wavecount; i++){

		float A = waves[i].amplitude;
		float L = waves[i].wavelength;
		float F = (2 * 3.14159) / L;
		float S = waves[i].speed*F;
		float2 dir = normalize(waves[i].direction);

			dx += WaveXDerivative(A, L, F, S, dir, x, y);
			dy += WaveYDerivative(A, L, F, S, dir, x, y);
	}
	return normalize(float3(-dx, 1.0, -dy));
}

PixelInput main(VertexInput input)
{
	PixelInput output = (PixelInput)0;

	output.uv = input.uv;

	for (int i = 0; i< wavecount; i++){

		float A = waves[i].amplitude;
		float L = waves[i].wavelength;
		float F = (2 * 3.14159) / L;
		float S = waves[i].speed*F;
		float2 dir = normalize(waves[i].direction);

		input.position.y += ComputeWave(A, L, F, S, dir, input.position.xz);
	}

	output.bitangent	= WaveTangent(input.position.x, input.position.y);
	output.tangent		= float4(WaveBitangent(input.position.x, input.position.y),0.0f);

	output.position = mul(mul(projection, mul(view, model)), float4(input.position, 1.0f));

	output.normal = WaveNormal(input.position.x, input.position.z);

	output.realpos = mul(model, input.position);


	if (lightCount > 0){
		float3 lightDirection = (LightDirection(lights[0]));
			//output.light0 = lightDirection;
			// On transforme la direction de la lumière dans l'espace tangent

			output.light0 = (float3(
			dot(output.tangent.xyz, lightDirection),
			dot(output.bitangent.xyz, lightDirection),
			dot(output.normal.xyz, lightDirection))

			);
	}

	// On transforme la direction de la caméra dans l'espace tangent (toujours pour gérer la lumière)

	output.view = (float3(
		dot(output.tangent.xyz, output.realpos - cameraPosition),
		dot(output.bitangent.xyz, output.realpos - cameraPosition),
		dot(output.normal.xyz, output.realpos - cameraPosition)));



	return output;
}