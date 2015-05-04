struct PixelInput{
	float4 realpos		: POSITION;		// Permet de déterminer la position spatial d'un pixel en cours de traitement par le pixel Shader
	float4 position		: SV_POSITION;	// Position "transformé donc pas trop trop utilisable
	float2 uv			: TEXCOORD0;
	float4 normal		: NORMAL;
};