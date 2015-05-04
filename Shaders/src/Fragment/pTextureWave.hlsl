

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

float4 main(PixelInput input) : SV_Target{

	return float4(cos(2.0f*3.141592f*input.uv.x)* float3(0.0f, 1.0f, 0.0f), 1.0f);
	
	
}