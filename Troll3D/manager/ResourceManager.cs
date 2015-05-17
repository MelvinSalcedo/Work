using System;
using System.Collections.Generic;
using SharpDX;
using SharpDX.Direct3D11;

namespace Troll3D
{

    public class ResourceManager
    {

        public static ShaderResourceView GetSRVFromResource( Resource resource )
        {
            return new ShaderResourceView( ApplicationDX11.Instance.Device, resource );
        }

        /// <summary>
        /// Charge une image dans le dossier des resources
        /// </summary>
        /// <returns></returns>
        public static ShaderResourceView GetImageFromFile( string path )
        {
            return new ShaderResourceView( ApplicationDX11.Instance.Device,
                    Texture2D.FromFile( ApplicationDX11.Instance.Device, GetRealPath( path ) ) );
        }

        public static Texture2D GetTexture2DFromFile( string path )
        {
            return ( Texture2D )Texture2D.FromFile( ApplicationDX11.Instance.Device, GetRealPath( path ) );
        }

        public static string GetFontRealPath( string path ) { return FontPath + path; }


        private static string GetRealPath( string path ) { return Path + path; }

        /// <summary>
        /// Petits raccourcis pour récupérer l'endroit ou sont stocké les images et faciliter un peu tout
        /// </summary>
        private static string Path = "D:\\Work\\Resources\\";
        private static string FontPath = "D:\\Work\\Resources\\Fonts";
    }
}
