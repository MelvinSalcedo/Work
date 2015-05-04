
// L'adresse 0 est réservé aux transformations 
cbuffer modeldata : register(b0){
	float4x4 model;
	float4x4 view;
	float4x4 projection;
	float4x4 inverse;
	float3   cameraPosition;
};