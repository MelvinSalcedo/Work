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
    /// <summary>
    /// Petite classe utilitaire pour enregistrer les indices des informations de sommets
    /// qui ont été précédemment chargé
    /// </summary>
    public class FaceObj
    {
        public FaceObj() { }
        public FaceObj( int[] vertices, int[] normals, int [] textures)
        {
            IndexesVertice.AddRange(vertices);
            IndexesNormal.AddRange( normals);
            IndexesTexture.AddRange( textures );
        }

        public void AddVertex( int vertice, int normal, int texture )
        {
            IndexesVertice.Add(vertice);
            if ( normal != -1 )
            {
                IndexesNormal.Add( normal );
            }
            if ( texture != -1 )
            {
                IndexesTexture.Add( texture );
            }
        }

        public List<int> IndexesVertice = new List<int>();
        public List<int> IndexesNormal  = new List<int>();
        public List<int> IndexesTexture = new List<int>();
        /// <summary>
        /// RealIndex renvoie vers l'index de sommet que l'on calcule plus tardivement
        /// </summary>
        public List<int> RealIndex      = new List<int>();
    }

    /// <summary>
    /// J'utilise une classe pour enregistrer les indices qui forment un sommet unique
    /// </summary>
    public class MeshVertex
    {
        /// <summary>
        /// Vérifie si le MeshVertex passé en paramètre existe déjà et renvoie son indice
        /// </summary>
        public static int Exist( List<MeshVertex> vertices, MeshVertex mv )
        {
            for ( int i = 0; i < vertices.Count; i++ )
            {
                if
                ( 
                    vertices[i].positionIndex== mv.positionIndex &&
                    vertices[i].textureIndex== mv.textureIndex &&
                    vertices[i].normalIndex== mv.normalIndex 
                ){
                    return i;
                }
            }
            return -1;
        }
        public MeshVertex( int pos, int tex, int norm )
        {
            positionIndex = pos;
            textureIndex = tex;
            normalIndex = norm;
        }
        public int positionIndex;
        public int textureIndex;
        public int normalIndex;
    }
   

    public class MeshManager
    {

        public static MeshManager Instance;

        public static Mesh LoadObj( string file )
        {
            string filename = "D:\\Work\\Resources\\" + file;

            StreamReader stream = new StreamReader( filename );

            List<float> Vertices = new List<float>();
            List<float> Normals = new List<float>();
            List<float> Textures = new List<float>();
            List<MeshVertex> meshVertices = new List<MeshVertex>();

            

            
            List<FaceObj> ObjFaces = new List<FaceObj>();


            // On commence par lire l'intégrité du fichier. Dans le processus, on récupère les informations
            // sur les sommets et les faces
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

                        // Si la ligne débute par v, on ajoute un sommet
                        if ( splits[0] == "v" )
                        {
                            for ( int i = 1; i < splits.Count(); i++ )
                            {
                                Vertices.Add( float.Parse( splits[i].Replace( '.', ',' ) ) );
                            }
                        }
                        // Si la ligne débute par vn, on ajoute une normale
                        if ( splits[0] == "vn" )
                        {
                            for ( int i = 1; i < splits.Count(); i++ )
                            {
                                Normals.Add( float.Parse( splits[i].Replace( '.', ',' ) ) );
                            }
                        }

                        // Si la ligne débute par vt, on ajoute une coordonnée de texture
                        if ( splits[0] == "vt" )
                        {
                            for ( int i = 1; i < splits.Count(); i++ )
                            {
                                Textures.Add( float.Parse( splits[i].Replace( '.', ',' ) ) );
                            }
                        }

                        // Si la ligne débute par f, on ajoute une face
                        if ( splits[0] == "f" )
                        {
                            FaceObj fobj = new FaceObj();
                            ObjFaces.Add(fobj);

                            for ( int i = 1; i < splits.Count(); i++ )
                            {
                                string[] secondsplit = splits[i].Split( '/' );
                                int[] vertex = new int[secondsplit.Length];

                                for ( int j = 0; j < secondsplit.Count(); j++ )
                                {
                                    // Position
                                    if ( j == 0 )
                                    {
                                        int result;
                                        if ( int.TryParse( secondsplit[j], out result ) )
                                        {
                                            fobj.IndexesVertice.Add( result );
                                        }
                                    }
                                    // Textures
                                    if ( j == 1 )
                                    {
                                        int result;
                                        if ( int.TryParse( secondsplit[j], out result ) )
                                        {
                                            fobj.IndexesTexture.Add( result );
                                        }
                                        else
                                        {
                                            //fobj.IndexesNormal.Add( -1 );
                                        }
                                    } 
                                    // Normals
                                    if ( j == 2 )
                                    {
                                        int result;
                                        if ( int.TryParse( secondsplit[j], out result ) )
                                        {
                                            fobj.IndexesNormal.Add( result );
                                        }
                                        else
                                        {
                                           // fobj.IndexesTexture.Add( -1 );
                                        }
                                    }
                                }

                            }
                        }
                    }
                }
            }

            StandardMesh mesh = new StandardMesh();

            for ( int i = 0; i < ObjFaces.Count; i ++ )
            {
                // On va construire les sommets à partir des indices
                // Tout en essayant de ne pas dupliquer les sommets
                for ( int j = 0; j < ObjFaces[i].IndexesVertice.Count; j++ )
                {   
                    
                    Vector3 normal  = new Vector3( 0.0f, 0.0f, 0.0F );
                    Vector2 tex     = new Vector2( 0.0f, 0.0f );

                    int vertexIndex  = ObjFaces[i].IndexesVertice[j]-1;
                    int normalIndex  = -1;
                    int textureIndex = -1;

                    if ( ObjFaces[i].IndexesNormal.Count> 0)
                    {
                        normalIndex = ObjFaces[i].IndexesNormal[j]-1;
                        normal = new Vector3( Normals[normalIndex*3], Normals[normalIndex*3+1], Normals[normalIndex*3+2]);
                    }

                    if ( ObjFaces[i].IndexesTexture.Count>0 )
                    {
                        textureIndex = ObjFaces[i].IndexesTexture[j]-1;
                        tex = new Vector2( Textures[textureIndex * 2], Textures[textureIndex*2 + 1] );
                    }
                    MeshVertex mv = new MeshVertex( vertexIndex, normalIndex, textureIndex );
                    int index = MeshVertex.Exist( meshVertices, mv );
                    // Si on a pas déjà eu cette combinaison d'indice, on ajoute un nouveau sommet
                    if ( index ==-1)
                    {
                        meshVertices.Add( mv );
                        mesh.AddVertex
                        (
                            new StandardVertex
                            (
                               new Vector3( Vertices[vertexIndex * 3], Vertices[vertexIndex * 3 + 1], Vertices[vertexIndex * 3 + 2] ),
                               normal,
                               tex
                            )
                        );
                        ObjFaces[i].RealIndex.Add( meshVertices.Count - 1 );
                    }
                    else
                    {
                        ObjFaces[i].RealIndex.Add(index);
                    }
                    
                }
            }

            // Pour le moment je ne gère que les obj contenant des triangles ou des carrés
            for ( int i = 0; i < ObjFaces.Count ; i++)
            {
                mesh.AddFace( ObjFaces[i].RealIndex[0], ObjFaces[i].RealIndex[1] , ObjFaces[i].RealIndex[2] );

                if ( ObjFaces[i].IndexesVertice.Count== 4 )
                {
                    mesh.AddFace( ObjFaces[i].RealIndex[0], ObjFaces[i].RealIndex[2], ObjFaces[i].RealIndex[3] );
                }
            }

            if ( Normals.Count == 0 )
            {
                mesh.ComputeNormals();
            }
            
            return mesh.ReturnMesh();
        }



    }
}
