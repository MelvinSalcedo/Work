using System;
using System.Collections.Generic;
using SharpDX;
using SharpDX.Direct3D11;

namespace Troll3D
{
    /// <summary>
    ///  Une Texture Atlas est tout simplement une texture qui va servir à stocker plusieurs textures en une seule
    ///  de manière à limiter les changements de textures. Il est possible d'initialiser et de se servir de cette classe de plusieurs
    ///  manière. La première (et unique pour l'instant) consiste à charger directement une Image (Texture2D), et ensuite utiliser diverses
    ///  fonctions pour créer les régions utilisables. Plus tard, il sera possible de créer une TextureAtlas vide, de rajouter manuellement
    ///  des textures à l'intérieur qui seront automatiquement positionné en fonction des valeurs d'offset
    /// </summary>
    public class TextureAtlas
    {
        public TextureAtlas( string texturepath )
        {
            m_dictionary    = new Dictionary<int, AtlasNode>();
            Texture2D tex   = ResourceManager.GetTexture2DFromFile( texturepath );
            Width           = tex.Description.Width;
            Height          = tex.Description.Height;
            SRV             = ResourceManager.GetSRVFromResource( tex );
        }

        /// <summary>
        /// Défini une nouvelle zone de la texture comme étant utilisable.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="id"></param>
        public void AddAtlasNode( AtlasNode node, int id )
        {
            m_dictionary.Add( id, node );
        }

        public AtlasNode GetNode( int id )
        {
            return m_dictionary[id];
        }

        public int Width    { get; private set; }
        public int Height   { get; private set; }
        public ShaderResourceView SRV { get; private set; }

        /// <summary>
        /// J'utilise un dictionnaire pour faciliter l'utilisation dans le cadre d'une 
        /// Texture contenant des charactères de manière à récupérer la texture d'un caractère directement
        /// à partir de son code (après faut voir les histoires d'encodage évidemment)
        /// </summary>
        private Dictionary<int, AtlasNode> m_dictionary;


    }
}
