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
            return new ShaderResourceView( ApplicationDX11.Instance.device_, resource );
        }

        public static ShaderResourceView GetShaderResourceViewFromFile( string path )
        {
            return new ShaderResourceView( ApplicationDX11.Instance.device_,
                    Texture2D.FromFile( ApplicationDX11.Instance.device_, GetRealPath( path ) ) );
        }

        public static Texture2D GetTexture2DFromFile( string path )
        {
            return ( Texture2D )Texture2D.FromFile( ApplicationDX11.Instance.device_, GetRealPath( path ) );
        }

        public static string GetFontRealPath( string path )
        {
            string totalpath;

#if DEBUG
            totalpath = DebugFontPath + path;
#else
                        totalpath = ReleaseFontPath+ path;
#endif
            return totalpath;

        }


        private static string GetRealPath( string path )
        {
            string totalpath;

            #if DEBUG
            totalpath = DebugPath + path;
            #else
                        totalpath = ReleasePath+ path;
            #endif
            return totalpath;
        }


        // Static Datas

        /// <summary>
        /// Petits raccourcis pour récupérer l'endroit ou sont stocké les images et faciliter un peu tout
        /// </summary>
        private static string DebugPath = "C:\\NewWorkbench\\DotNet\\Work\\Resources\\";
        private static string ReleasePath = ".\\";

        private static string DebugFontPath = "C:\\NewWorkbench\\DotNet\\Work\\Resources\\Fonts\\";
        private static string ReleaseFontPath = ".\\Fonts";



    }
}
