#include <transform.hlsl>
#include <StandardVertexInput.hlsl>
#include <SimplePixelInput.hlsl>
#include <Lights.hlsl>
#include <TextureConstantBuffer.hlsl>

PixelInput main(VertexInput input){

	PixelInput output	=	(PixelInput )0;
	output.uv			=	input.uv;

	// Pour éviter les déformations de la normale et tangentes en fonction des transformations de type Scaling et Translation
	//		// On doit utiliser la matrice de transformation Inverse transposé
	output.normal = float4(normalize(mul(transpose(inverse), float4(input.normal.xyz, 0.0f)).xyz), 0.0f);


	// Position dans l'espace transformé de la caméra/projection
	output.position		= mul(mul(projection, mul(view, model)), float4(input.position, 1.0f));

	// Position dans l'espace
	output.realpos		= mul(model, float4(input.position, 1.0f));

	// On transforme le vecteur représentant la direction de la caméra dans l'espace tangent
	
	return output;

}
