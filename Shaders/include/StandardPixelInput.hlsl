
struct PixelInput{
	float4 realpos		: POSITION;		// Permet de déterminer la position spatial d'un pixel en cours de traitement par le pixel Shader
	float4 position		: SV_POSITION;	// Position "transformé donc pas trop trop utilisable
	float2 uv			: TEXCOORD0;
	float4 normal		: NORMAL;
	float4 tangent		: TANGENT0;
	float3 bitangent	: TANGENT1;
	float3 view			: TEXCOORD1;		// Vecteur entre le pixel le plant du texel en cours d'évaluation
	float3 light0		: TEXCOORD2;		// Vecteur entre la lumière et le plan du texel en cours d'évaluation
};