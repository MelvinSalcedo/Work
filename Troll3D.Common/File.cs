using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Troll3D.Common
{
    class Files
    {
        /// <summary>
        /// Récupère une liste de chemin correspondant aux fichiers contenus dans le dossier passé
        /// en paramètre, le tout récursivement
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string[] ScanDirectoryRecursivly( string path )
        {
            string[] files = Directory.GetFiles( path );
            string[] concatenateFiles;

            string[] directories = Directory.GetDirectories( path );
            for ( int i = 0; i < directories.Length; i++ )
            {
                string[] newFiles = ScanDirectoryRecursivly( directories[i] );

                concatenateFiles = new string[files.Length + newFiles.Length];
                files.CopyTo( concatenateFiles, 0 );
                newFiles.CopyTo( concatenateFiles, files.Length );
                files = concatenateFiles;
            }
            return files;
        }
    }
}
