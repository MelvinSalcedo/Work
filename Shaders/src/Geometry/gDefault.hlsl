// List des entrées possible du Geometry Shader
//point		VS_OUTPUT input[1]	// Because a point contains a single element, we do not have to specify [1] if we don't want
//line		VS_OUTPUT input[2]
//lineadj		VS_OUTPUT input[4]
//triangle	VS_OUTPUT input[3]
//triangleadj VS_OUTPUT input[6]


// Un geometry shader permet de supprimer, modifier ou rajouter des primitives. Il se situe entre le vertex Shader et le Pixel Shader
// Un Geometry shader ne retourne aucune donnée, au contraire, on précise dans le deuxième argument de la fonction main
// un "flux" dans lequel on rajoute des primitives. Ce flux peut être de type
//		*PointStream
//		*LineStream
//		*TriangleStream

#include <StandardPixelInput.hlsl>

// Dans le cas présent, ce shader se content de prendre un triangle, et de le remettre dans le flux de triangle, aucune opération n'est effectuée
// Le géométry shader récupères les sommets des primitives déjà modifié par le vertex shader. Les sommets de sorties du géometry shader vont donc
// ensuite vers le fragment/pixel shader (d'ou le "PS_IN")
[maxvertexcount(3)] // maxvertexcount définit le nombre maximum de sommet que l'on peut ajouter à la liste/flux
void main(triangle PixelInput input[3], inout TriangleStream<PixelInput> OutputStream){

	OutputStream.Append(input[0]);
	OutputStream.Append(input[1]);
	OutputStream.Append(input[2]);
}

