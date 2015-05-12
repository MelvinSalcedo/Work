using System;
using System.Collections.Generic;
using SharpDX;

namespace Troll3D
{

    public class Cube
    {
        public static Mesh Mesh
        {
            get
            {
                if ( m_cubeMesh == null )
                {
                    BuildMesh();
                }
                return m_cubeMesh;
            }
        }

        public static Mesh ReverseMesh
        {
            get
            {
                if ( m_reverseCubeMesh == null )
                {
                    BuildReverseMesh();
                }
                return m_reverseCubeMesh;
            }
        }

        private static void BuildMesh()
        {
            StandardMesh mesh = new StandardMesh();

            Front( mesh );
            Back( mesh );
            Left( mesh );
            Right( mesh );
            Bottom( mesh );
            Top( mesh );
            Triangles( mesh );

            mesh.ComputeNormals();
            mesh.ComputeTangents();
            m_cubeMesh = mesh.ReturnMesh();
        }

        /// <summary>
        /// Construit le mesh "inverse", c'est à dire celui dont les faces
        /// pointent vers l'intérieur du cube
        /// </summary>
        private static void BuildReverseMesh()
        {
            StandardMesh mesh = new StandardMesh();

            Front( mesh );
            Back( mesh );
            Left( mesh );
            Right( mesh );
            Bottom( mesh );
            Top( mesh );
            ReverseTriangles( mesh );

            mesh.ComputeNormals();
            mesh.ComputeTangents();
            m_reverseCubeMesh= mesh.ReturnMesh();
        }

        public static void Front( StandardMesh mesh )
        {
            mesh.AddVertex(
                new StandardVertex(
                    new Vector3( -0.5f, -0.5f, -0.5f ),
                    new Vector3( 0.0f, 0.0f, -1.0f ),
                    new Vector2( 0.0f, 0.0f )
                )
            );

            mesh.AddVertex(
                new StandardVertex(
                    new Vector3( 0.5f, -0.5f, -0.5f ),
                    new Vector3( 0.0f, 0.0f, -1.0f ),
                    new Vector2( 1.0f, 0.0f )
                )
            );

            mesh.AddVertex(
                new StandardVertex(
                    new Vector3( -0.5f, 0.5f, -0.5f ),
                    new Vector3( 0.0f, 0.0f, -1.0f ),
                    new Vector2( 0.0f, 1.0f )
                )
            );

            mesh.AddVertex(
                new StandardVertex(
                    new Vector3( 0.5f, 0.5f, -0.5f ),
                    new Vector3( 0.0f, 0.0f, -1.0f ),
                    new Vector2( 1.0f, 1.0f )
                )
            );
        }

        public static void Back( StandardMesh mesh )
        {
            mesh.AddVertex(
                new StandardVertex(
                    new Vector3( -0.5f, -0.5f, 0.5f ),
                    new Vector3( 0.0f, 0.0f, 1.0f ),
                    new Vector2( 1.0f, 0.0f )
                )
            );

            mesh.AddVertex(
                new StandardVertex(
                    new Vector3( 0.5f, -0.5f, 0.5f ),
                    new Vector3( 0.0f, 0.0f, 1.0f ),
                    new Vector2( 0.0f, 0.0f )
                )
            );

            mesh.AddVertex(
                new StandardVertex(
                    new Vector3( -0.5f, 0.5f, 0.5f ),
                    new Vector3( 0.0f, 0.0f, 1.0f ),
                    new Vector2( 1.0f, 1.0f )
                )
            );

            mesh.AddVertex(
                new StandardVertex(
                    new Vector3( 0.5f, 0.5f, 0.5f ),
                    new Vector3( 0.0f, 0.0f, 1.0f ),
                    new Vector2( 0.0f, 1.0f )
                )
            );

        }

        public static void Left( StandardMesh mesh )
        {
            mesh.AddVertex(
                      new StandardVertex(
                          new Vector3( -0.5f, -0.5f, -0.5f ),
                          new Vector3( -1.0f, 0.0f, 0.0f ),
                          new Vector2( 1.0f, 0.0f )
                      )
                  );

            mesh.AddVertex(
                new StandardVertex(
                    new Vector3( -0.5f, -0.5f, 0.5f ),
                    new Vector3( -1.0f, 0.0f, 0.0f ),
                    new Vector2( 0.0f, 0.0f )
                )
            );

            mesh.AddVertex(
                new StandardVertex(
                    new Vector3( -0.5f, 0.5f, -0.5f ),
                    new Vector3( -1.0f, 0.0f, 0.0f ),
                    new Vector2( 1.0f, 1.0f )
                )
            );

            mesh.AddVertex(
                new StandardVertex(
                    new Vector3( -0.5f, 0.5f, 0.5f ),
                    new Vector3( -1.0f, 0.0f, 0.0f ),
                    new Vector2( 0.0f, 1.0f )
                )
            );
        }

        private static void Right( StandardMesh mesh )
        {
            mesh.AddVertex(
                   new StandardVertex(
                       new Vector3( 0.5f, -0.5f, -0.5f ),
                       new Vector3( 1.0f, 0.0f, 0.0f ),
                       new Vector2( 0.0f, 0.0f )
                   )
               );

            mesh.AddVertex(
                new StandardVertex(
                    new Vector3( 0.5f, -0.5f, 0.5f ),
                    new Vector3( 1.0f, 0.0f, 0.0f ),
                    new Vector2( 1.0f, 0.0f )
                )
            );

            mesh.AddVertex(
                new StandardVertex(
                    new Vector3( 0.5f, 0.5f, -0.5f ),
                    new Vector3( 1.0f, 0.0f, 0.0f ),
                    new Vector2( 0.0f, 1.0f )
                )
            );

            mesh.AddVertex(
                new StandardVertex(
                    new Vector3( 0.5f, 0.5f, 0.5f ),
                    new Vector3( 1.0f, 0.0f, 0.0f ),
                    new Vector2( 1.0f, 1.0f )
                )
            );
        }

        private static void Top( StandardMesh mesh )
        {
            mesh.AddVertex(
                      new StandardVertex(
                          new Vector3( -0.5f, 0.5f, -0.5f ),
                          new Vector3( 0.0f, 1.0f, 0.0f ),
                          new Vector2( 0.0f, 0.0f )
                      )
                  );

            mesh.AddVertex(
                new StandardVertex(
                    new Vector3( 0.5f, 0.5f, -0.5f ),
                    new Vector3( 0.0f, 1.0f, 0.0f ),
                    new Vector2( 1.0f, 0.0f )
                )
            );

            mesh.AddVertex(
                new StandardVertex(
                    new Vector3( -0.5f, 0.5f, 0.5f ),
                    new Vector3( 0.0f, 1.0f, 0.0f ),
                    new Vector2( 0.0f, 1.0f )
                )
            );

            mesh.AddVertex(
                new StandardVertex(
                    new Vector3( 0.5f, 0.5f, 0.5f ),
                    new Vector3( 0.0f, 1.0f, 0.0f ),
                    new Vector2( 1.0f, 1.0f )
                )
            );
        }

        private static void Bottom( StandardMesh mesh )
        {

            mesh.AddVertex(
              new StandardVertex(
                  new Vector3( -0.5f, -0.5f, 0.5f ),
                  new Vector3( 0.0f, -1.0f, 0.0f ),
                  new Vector2( 0.0f, 1.0f )
              )
          );

            mesh.AddVertex(
                new StandardVertex(
                    new Vector3( 0.5f, -0.5f, 0.5f ),
                    new Vector3( 0.0f, -1.0f, 0.0f ),
                    new Vector2( 0.0f, 0.0f )
                )
            );

            mesh.AddVertex(
                new StandardVertex(
                    new Vector3( -0.5f, -0.5f, -0.5f ),
                    new Vector3( 0.0f, -1.0f, 0.0f ),
                    new Vector2( 0.0f, 1.0f )
                )
            );

            mesh.AddVertex(
                new StandardVertex(
                    new Vector3( 0.5f, -0.5f, -0.5f ),
                    new Vector3( 0.0f, -1.0f, 0.0f ),
                    new Vector2( 1.0f, 1.0f )
                )
            );
        }

        private static void Triangles( StandardMesh mesh )
        {
            // front
            mesh.AddFace( 0, 2, 3 );
            mesh.AddFace( 0, 3, 1 );

            // Back
            mesh.AddFace( 5, 7, 6 );
            mesh.AddFace( 5, 6, 4 );

            // Left
            mesh.AddFace( 9, 10, 8 );
            mesh.AddFace( 9, 11, 10 );

            // Right
            mesh.AddFace( 12, 14, 15 );
            mesh.AddFace( 12, 15, 13 );

            // Bottom
            mesh.AddFace( 16, 18, 19 );
            mesh.AddFace( 16, 19, 17 );

            // top

            mesh.AddFace( 20, 22, 23 );
            mesh.AddFace( 20, 23, 21 );
        }

        private static void ReverseTriangles(StandardMesh mesh)
        {
            // front
            mesh.AddFace( 0, 3, 2 );
            mesh.AddFace( 0, 1, 3 );

            // Back
            mesh.AddFace( 5, 6, 7 );
            mesh.AddFace( 5, 4, 6 );

            // Left
            mesh.AddFace( 9, 8, 10 );
            mesh.AddFace( 9, 10, 11 );

            // Right
            mesh.AddFace( 12, 15, 14 );
            mesh.AddFace( 12, 13, 15 );

            // Bottom
            mesh.AddFace( 16, 19, 18 );
            mesh.AddFace( 16, 17, 19 );

            // top

            mesh.AddFace( 20, 23, 22 );
            mesh.AddFace( 20, 21, 23 );
        }

        private static Mesh m_cubeMesh;
        private static Mesh m_reverseCubeMesh;
    }
}
