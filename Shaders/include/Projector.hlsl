
// On va donc désormais partir du principe que l'indice 3 sera utilisé pour gérer les projecteurs
struct Projector{
	float4x4	Projection;
	float4x4	Transformation;
	float4x4	Inverse;
};

// On va pour le moment partir du principe qu'il n'y a qu'une liste de 10 projecteurs
cbuffer ProjectorsCBuffer : register(b3){
	int projectorCount;
	Projector projectors[10];
}

// En théorie on peut bind jusque 128 textures à l'intérieur des shaders directx11, alors on va en réservé une petite dizaine 
// pour les textures projetés

Texture2D projectorTexture0: register(t10);
Texture2D projectorTexture1: register(t11);
Texture2D projectorTexture2: register(t12);
Texture2D projectorTexture3: register(t13);
Texture2D projectorTexture4: register(t14);
Texture2D projectorTexture5: register(t15);
Texture2D projectorTexture6: register(t16);
Texture2D projectorTexture7: register(t17);
Texture2D projectorTexture8: register(t18);
Texture2D projectorTexture9: register(t19);
