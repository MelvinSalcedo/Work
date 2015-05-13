
// J'ai un peu bataillé avec ce truc, apparemment si je met COLOR après SV_POSITION,
// ca ne fonctionne pas
struct PixelInput
{
	float4 stuff		: COLOR;
	float4 position		: SV_POSITION;	// Position "transformé donc pas trop trop utilisable

};