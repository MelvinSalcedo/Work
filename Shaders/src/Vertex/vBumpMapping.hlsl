#include <transform.hlsl>
#include <StandardVertexInput.hlsl>
#include <StandardPixelInput.hlsl>
#include <Lights.hlsl>
#include <TextureConstantBuffer.hlsl>

PixelInput main(VertexInput input){

	PixelInput output = (PixelInput)0;

	output.uv = input.uv;
	output.normal = (float4)(input.normal,1.0);
	//output.tangent = input.tangent;

	// Position dans l'espace transformé de la caméra/projection
	output.position = mul(mul(projection, mul(view, model)), float4(input.position, 1.0f));

	// Position dans l'espace
	output.realpos = mul(model, float4(input.position, 1.0f));
	//output.realpos = float4(0.0, 0.0, 0.0, 0.0) ;

	//float3 lightDirection = (LightDirection(lights[0]));


	//	// Attention cependant, si le material n'a pas de texture, alors le tangent/texture space ne fonctionnera pas,
	//	// on récupère et utilise donc la texture normalement et on désactive l'éventuel Normal Mapping

	//	if (HasTexture){

	//		// Pour éviter les déformations de la normale et tangentes en fonction des transformations de type Scaling et Translation
	//		// On doit utiliser la matrice de transformation Inverse transposé
	//		output.normal = float4(normalize(mul(transpose(inverse), float4(input.normal.xyz, 0.0f)).xyz), 0.0f);
	//		output.tangent = float4(normalize(mul(transpose(inverse), float4(input.tangent.xyz, 0.0f)).xyz), input.tangent.w);

	//		// On calcule la bitangente B = (N x T) * T.w.
	//		float3 bitangent = normalize(cross(output.normal.xyz, output.tangent.xyz) * input.tangent.w);
	//			output.bitangent = bitangent;

	//		//bitangent = float4(normalize(mul(transpose(inverse), float4(bitangent.xyz, 0.0f)).xyz), 0.0f);

	//		// On calcule la direction de la lumière (pour l'instant on va se contenter d'une et je 
	//		// vais voir pour faire les modifications nécessaire pour en gérer plusieurs

	//		if (lightCount > 0){

	//			//output.light0 = lightDirection;
	//			// On transforme la direction de la lumière dans l'espace tangent

	//			output.light0 = (float3(
	//				dot(output.tangent.xyz, lightDirection),
	//				dot(bitangent.xyz, lightDirection),
	//				dot(output.normal.xyz, lightDirection))

	//				);
	//		}

	//		// On transforme la direction de la caméra dans l'espace tangent (toujours pour gérer la lumière)

	//		output.view = (float3(
	//			dot(output.tangent.xyz, output.realpos - cameraPosition),
	//			dot(bitangent.xyz, output.realpos - cameraPosition),
	//			dot(output.normal.xyz, output.realpos - cameraPosition)));
	//	}
	//	else{

	//		output.view = normalize(output.realpos - cameraPosition);
	//		output.light0 = normalize(lightDirection);
	//		output.normal = float4(normalize(mul(transpose(inverse), float4(input.normal.xyz, 0.0f)).xyz), 0.0f);
	//	}



		// On transforme le vecteur représentant la direction de la caméra dans l'espace tangent

		return output;

}
