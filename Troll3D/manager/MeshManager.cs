using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.Direct3D11;
using SharpDX;
using System.IO;
using System.Text.RegularExpressions;

namespace Troll3D
{
    public class MeshManager
    {

        public static MeshManager Instance;


        public static Entity LoadObj( string file )
        {
            string filename = "C:\\NewWorkbench\\DotNet\\Work\\Resources\\" + file;

            Entity root = new Entity();

            StreamReader stream = new StreamReader( filename );



            List<float> Vertices = new List<float>();
            List<float> Normals = new List<float>();
            List<float> Textures = new List<float>();

            List<int> Faces = new List<int>();

            bool isTriangle = true;

            bool faceLoading = false;

            while ( !stream.EndOfStream )
            {
                string line = stream.ReadLine();

                if ( line.Count() > 0 )
                {
                    if ( line[0] != ' ' && line[0] != '#' )
                    {
                        char[] chars = {
                                    ' ',
                                    '\t'
                                };

                        line = line.Trim();
                        RegexOptions options = RegexOptions.None;

                        // { 2,} signifie 2 fois au moins
                        // [] est un ensemble de caractère
                        Regex regex = new Regex( @"[ ]{2,}", options );
                        line = regex.Replace( line, @" " );

                        string[] splits = line.Split( ' ' );

                        if ( splits[0] == "v" )
                        {
                            for ( int i = 1; i < splits.Count(); i++ )
                            {
                                Vertices.Add( float.Parse( splits[i].Replace( '.', ',' ) ) );
                            }
                        }

                        if ( splits[0] == "vn" )
                        {
                            for ( int i = 1; i < splits.Count(); i++ )
                            {
                                Normals.Add( float.Parse( splits[i].Replace( '.', ',' ) ) );
                            }
                        }

                        if ( splits[0] == "vt" )
                        {
                            for ( int i = 1; i < splits.Count(); i++ )
                            {
                                Textures.Add( float.Parse( splits[i].Replace( '.', ',' ) ) );
                            }
                        }

                        if ( splits[0] == "f" )
                        {
                            if ( splits.Count() > 4 )
                            {
                                isTriangle = false;
                            }
                            for ( int i = 1; i < splits.Count(); i++ )
                            {

                                string[] secondsplit = splits[i].Split( '/' );

                                for ( int j = 0; j < secondsplit.Count(); j++ )
                                {
                                    Faces.Add( int.Parse( secondsplit[j] ) );
                                }
                            }
                        }
                    }
                }

                
                    


            }

            faceLoading = false;

            Mesh mesh = new Mesh();


            for ( int i = 0; i < Faces.Count; i += 3 )
            {
                mesh.AddVertex(
                    new StandardVertex(
                    new Vector3( Vertices[( Faces[i] - 1 ) * 3], Vertices[( Faces[i] - 1 ) * 3 + 1], Vertices[( Faces[i] - 1 ) * 3 + 2] ),
                    new Vector3( Normals[( Faces[i + 2] - 1 ) * 3], Normals[( Faces[i + 2] - 1 ) * 3 + 1], Normals[( Faces[i + 2] - 1 ) * 3 + 2] ),
                    new Vector2( Textures[( Faces[i + 1] - 1 ) * 2], Textures[( Faces[i + 1] - 1 ) * 2 + 1] )

                    ) );
            }

            int countplus = 3*3;
            if ( isTriangle == false )
            {
                countplus = 4*3;
            }
            for ( int i = 0; i < Faces.Count; i += countplus )
            {
                mesh.AddFace( i/3, i/3 + 1, i/3 + 2 );
                if ( !isTriangle )
                {
                    mesh.AddFace( i / 3 + 3, i / 3, i / 3 + 2 );
                }
            }

            mesh.UpdateMesh();


            Components.MeshRenderer mr = ( Components.MeshRenderer )root.AddComponent( new Components.MeshRenderer( new MaterialDX11(), mesh ) );


            return root;
        }



    }
}
