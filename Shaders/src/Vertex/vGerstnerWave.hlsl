#include <transform.hlsl>
#include <StandardVertexInput.hlsl>
#include <StandardPixelInput.hlsl>
#include <Lights.hlsl>


struct Wave{
	float Steepness;	// Controle la "pente" de la vague. Doit être compris entre 0 et 1/(amplitude*wavelength)
	float amplitude;
	float wavelength;
	float speed;
	float2 direction;
};

cbuffer datastuff : register(b2){
	float4 maincolor;
	int wavecount;
	float time;
	Wave waves[40];
};




float3 ComputeBitangent(float A, float L, float F, float S, float Q, float2 dir, float xy){

	return float3(
		Q* pow(dir.x, 2)*F*A* sin(F* dot(dir,xy + time*S)),
		Q* dir.x*dir.y*F*A* sin(F* dot(dir, xy + time*S)),
		dir.x*F*A* cos(F* dot(dir, xy + time*S))
		);
}

float3 ComputeTangent(float A, float L, float F, float S, float Q, float2 dir, float xy){
	return float3(
		Q*dir.x*dir.y*F*A* sin(F* dot(dir, xy + time*S)),
		Q* pow(dir.y,2)*F*A* sin(F* dot(dir, xy + time*S)),
		dir.y*F*A* cos(F* dot(dir, xy + time*S))
		);
}

float3 ComputeNormal(float A, float L, float F, float S, float Q, float2 dir, float2 xy){
	return float3(
		dir.x*F*A* cos(F* dot(dir, xy) + time*S),
		dir.y*F*A* cos(F* dot(dir, xy) + time*S),
		Q*F*A* sin(F* dot(dir, xy) + time*S)
	);
}


// A = amplitude
// L = WaveLength
// S = Speed
// Dir = direction de la vague
// xy : position actuelle du sommet en cours d'édition
// Q : steepness, pente
float3 ComputeWave(float A, float L, float F, float S, float Q, float2 dir, float2 xy){
	return float3(

		Q*A*dir.x * cos(F* dot(dir, xy) + time*S),
		Q*A*dir.y * cos(F* dot(dir, xy) + time*S),
		A*sin(F* dot(dir, xy) + time*S)
		);
}

PixelInput main(VertexInput input){

	PixelInput output = (PixelInput)0;

	float3 sum = float3(0.0f, 0.0f, 0.0f);
	float3 sumTangent = float3(0.0f, 0.0f, 0.0f);
	float3 sumNormal = float3(0.0f, 0.0f, 0.0f);
	float3 sumBitangent = float3(0.0f, 0.0f, 0.0f);

	for (int i = 0; i< wavecount; i++){

		float A = waves[i].amplitude;
		float L = waves[i].wavelength;
		float F = (2 * 3.141592) / L;
		float S = waves[i].speed*F;
		float2 dir = normalize(waves[i].direction);
		float Q = waves[i].Steepness;

		
		sum				+= ComputeWave(		A, L, F, S, Q, dir, input.position.xz);
		sumTangent		+= ComputeTangent(	A, L, F, S, Q, dir, input.position.xz);
		sumBitangent	+= ComputeBitangent(A, L, F, S, Q, dir, input.position.xz);
		sumNormal		+= ComputeNormal(	A, L, F, S, Q, dir, input.position.xz);
	}

	output.tangent = float4(
		-sumTangent.x,
		1 - sumTangent.z,
		sumTangent.z,
		1.0f
	);

	output.bitangent = float3(
		1- sumBitangent.x,
		-sumBitangent.z,
		sumBitangent.y
	);

	output.normal = float4(
		-sumNormal.x,
		1 - sumNormal.z,
		-sumNormal.y,
		1.0f
	);

	input.position = float3(
		input.position.x + sum.x,
		sum.z,
		input.position.z + sum.y
		);


	float3 lightDirection = (LightDirection(lights[0]));
	output.view		= normalize(output.realpos - cameraPosition);
	output.light0	= normalize(lightDirection);

	output.uv = input.uv;
	//output.tangent = input.tangent;

	// Position dans l'espace transformé de la caméra/projection
	output.position = mul(mul(projection, mul(view, model)), float4(input.position, 1.0f));

	// Position dans l'espace
	output.realpos = mul(model, input.position);


	return output;

}
