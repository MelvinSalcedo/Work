
// L'adresse 5 est réservé aux paramètres classique de réglage de la texture
cbuffer textureConstantBuffer: register(b5){
	float	XOffset;
	float	YOffset;
	float	TilingWidth;
	float	TilingHeight;
	float4	MainColor;
	bool	HasTexture;
};