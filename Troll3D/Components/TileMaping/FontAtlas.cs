using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Troll3D
{
    /// <summary>
    /// Lis un fichier fnt pour construire un AtlasTexture utilisable pour afficher des caractères
    /// </summary>
    public class FontAtlas
    {
        public FontAtlas( string fontname )
        {
            string fontpath = ResourceManager.GetFontRealPath( fontname + ".fnt" );
            atlas = new TextureAtlas( fontname + ".png" );

            StreamReader reader = new StreamReader( fontpath );

            int lineCount = 0;

            while ( !reader.EndOfStream )
            {
                // Je passe les 5 premières lignes sans rien faire
                if ( lineCount > 3 )
                {
                    string line = reader.ReadLine();
                    string[] lines = line.Split( ' ' );

                    int width = 0;
                    int height = 0;
                    int id = -1;
                    int xoffset = 0;
                    int yoffset = 0;
                    int x = 0;
                    int y = 0;
                    int xadvance = 0;

                    for ( int i = 0; i < lines.Length; i++ )
                    {
                        string[] linessplitted = lines[i].Split( '=' );

                        if ( linessplitted.Length > 0 )
                        {
                            if ( linessplitted[0] == "id" )
                            {
                                id = int.Parse( linessplitted[1] );
                            }
                            if ( linessplitted[0] == "x" )
                            {
                                x = int.Parse( linessplitted[1] );
                            }
                            if ( linessplitted[0] == "y" )
                            {
                                y = int.Parse( linessplitted[1] );
                            }
                            if ( linessplitted[0] == "xoffset" )
                            {
                                xoffset = int.Parse( linessplitted[1] );
                            }
                            if ( linessplitted[0] == "yoffset" )
                            {
                                yoffset = int.Parse( linessplitted[1] );
                            }
                            if ( linessplitted[0] == "width" )
                            {
                                width = int.Parse( linessplitted[1] );
                            }
                            if ( linessplitted[0] == "height" )
                            {
                                height = int.Parse( linessplitted[1] );
                            }
                            if ( linessplitted[0] == "xadvance" )
                            {
                                xadvance = int.Parse( linessplitted[1] );
                            }
                        }
                    }
                    if ( id != -1 )
                    {
                        atlas.AddAtlasNode( new AtlasNode( xoffset, yoffset, width, height, x, y, id, xadvance ), id );
                    }
                }
                else
                {
                    string line = reader.ReadLine();
                }
                lineCount++;
            }
            reader.Close();
        }

        
        public TextureAtlas atlas;

    }
}
