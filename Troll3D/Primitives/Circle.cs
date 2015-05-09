using System;
using System.Collections.Generic;
using SharpDX;


namespace Troll3D
{
    public class Circle
    {
        public static Mesh GetMesh( int discretisation )
        {
            StandardMesh smesh = new StandardMesh();

            for ( int i = 0; i < discretisation; i++ )
            {
                float angleVal = 2.0f * 3.141592f * ( float )i / ( float )discretisation;

                StandardVertex svertex = new StandardVertex(
                    new Vector3(
                    ( float )Math.Cos( angleVal ),
                    0.0f,
                    ( float )Math.Sin( angleVal )

                    ) );

                smesh.AddVertex( svertex );
            }

            for ( int i = 0; i < discretisation - 1; i++ )
            {
                smesh.AddFace( 0, i, i + 1 );
            }

            smesh.ComputeNormals();
            smesh.ComputeTangents();
            return smesh.ReturnMesh();
        }

        /// <summary>
        /// Retourne une liste de sommet situé sur le cercle et utilisable en affichage par 
        /// "ligne"
        /// </summary>
        /// <param name="discretisation">nombre de lignes</param>
        /// <returns></returns>
        public static List<StandardVertex> GetLines(int discretisation)
        {
            List<StandardVertex> lines = new List<StandardVertex>();

            float angleVal = 0.0f;

            for ( int i = 0; i < discretisation ; i++ )
            {
                angleVal = 2.0f * 3.141592f * ( float )i / ( float )discretisation;

                lines .Add( new StandardVertex(
                    new Vector3(
                    ( float )Math.Cos( angleVal ),
                    0.0f,
                    ( float )Math.Sin( angleVal )

                    ) ));

                angleVal = 2.0f * 3.141592f * ( float )(i+1) / ( float )(discretisation);

                lines.Add( new StandardVertex(
                    new Vector3(
                    ( float )Math.Cos( angleVal ),
                    0.0f,
                    ( float )Math.Sin( angleVal )

                    ) ) );

            }


            return lines;
        }
    }
}
