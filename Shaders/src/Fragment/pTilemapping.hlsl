#include <Lights.hlsl>
#include <StandardPixelInput.hlsl>

// Filter Possible Val : MIN_MAG_MIP_POINT
//texture
Texture2D tileset : register(t0);
Texture2D tilemap : register(t1);

SamplerState textureSampler{
	Filter = MIN_MAG_MIP_POINT;
	AddressU = Wrap;
	AddressV = Wrap;
};

cbuffer tilemapping : register(b2){

	float tilesetwidth;
	float tilesetheight;

	float tilewidth;
	float tileheight;

	float tilemapwidth;
	float tilemapheight;
}



float4 main(PixelInput input) : SV_Target{

	// Je récupère le pixel de la tilemap sur lequel on se trouve

	int u = input.uv.x*tilemapwidth;
	int v = input.uv.y*tilemapheight;

	// Je récupère la valeur du pixel pour savoir quelle tuile afficher 

	float4 val = tilemap.Sample(textureSampler, input.uv);

	// Je récupère les coordonnées de la tuile à afficher dans le tileset

	float stuff = round(val.x*1000.0);

	int tilesety = ceil(stuff / (tilesetwidth / tilewidth));
	int tilesetx = round(fmod(stuff, (tilesetwidth / tilewidth)));

	float offsetx = tilesetx * (tilewidth / tilesetwidth);
	float offsety = tilesety * (tileheight / tilesetheight);

	float newx = offsetx + (input.uv.x*tilemapwidth - u)*(tilewidth / tilesetwidth);
	float newy = offsety + (input.uv.y*tilemapheight - v)*(tileheight / tilesetheight);

	return  tileset.Sample(textureSampler, float2(newx, newy));
}