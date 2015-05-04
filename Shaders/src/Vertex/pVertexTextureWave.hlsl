#include <transform.hlsl>
#include <StandardVertexInput.hlsl>
//#include <Lights.hlsl>

//
//struct Wave{
//	float2 Direction;
//	float DirectionxXWavelengthXAmplitude;	// D.x*w*A		D = direction, A = Amplitude w = Wavelength
//	float DirectionyXWavelengthXAmplitude;	// D.y*w*A
//
//
//	float Steepness;	// Controle la "pente" de la vague. Doit être compris entre 0 et 1/(amplitude*wavelength)
//	float amplitude;
//	float wavelength;
//	float speed;
//	float2 direction;
//};
//
//cbuffer datastuff : register(b2){
//	float4 maincolor;
//	int wavecount;
//	float time;
//	Wave waves[40];
//};


// PixelInput pour la vague
struct PixelInput{

	float2 uv			: TEXCOORD;
	float4 position		: SV_POSITION;	// Position "transformé donc pas trop trop utilisable
};

PixelInput main(VertexInput input){

	PixelInput output = (PixelInput)0;

	output.uv		= input.uv;
	output.position = mul(mul(projection, mul(view, model)), float4(input.position, 1.0f));
	
	return output;

}
