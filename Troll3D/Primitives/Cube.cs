using System;
using System.Collections.Generic;
using SharpDX;

namespace Troll3D{

    public class Cube{

        // Public

            // Static Methods
 
                public static Mesh GetMesh(){

                    if (CubeMesh == null) {

                        StandardMesh mesh = new StandardMesh();

                        Front(mesh);
                        Back(mesh);
                        Left(mesh);
                        Right(mesh);
                        Bottom(mesh);
                        Top(mesh);
                        Triangles(mesh);

                        mesh.ComputeNormals();
                        mesh.ComputeTangents();
                        CubeMesh = mesh.ReturnMesh();
                    }
                    return CubeMesh;
                }

            // Static Datas

                public static Mesh CubeMesh;

            // Lifecycle

            // Methods

        // Private

            // Static Methods

                public static void Front(StandardMesh mesh)
                {

                    mesh.AddVertex(
                        new StandardVertex(
                            new Vector3(-0.5f, -0.5f, -0.5f),
                            new Vector3(0.0f, 0.0f, -1.0f),
                            new Vector2(0.0f, 0.0f)
                        )
                    );

                    mesh.AddVertex(
                        new StandardVertex(
                            new Vector3(0.5f, -0.5f, -0.5f),
                            new Vector3(0.0f, 0.0f, -1.0f),
                            new Vector2(1.0f, 0.0f)
                        )
                    );

                    mesh.AddVertex(
                        new StandardVertex(
                            new Vector3(-0.5f, 0.5f, -0.5f),
                            new Vector3(0.0f, 0.0f, -1.0f),
                            new Vector2(0.0f, 1.0f)
                        )
                    );

                    mesh.AddVertex(
                        new StandardVertex(
                            new Vector3(0.5f, 0.5f, -0.5f),
                            new Vector3(0.0f, 0.0f, -1.0f),
                            new Vector2(1.0f, 1.0f)
                        )
                    );
                }

                public static void Back(StandardMesh mesh)
                {
                    mesh.AddVertex(
                        new StandardVertex(
                            new Vector3(-0.5f, -0.5f, 0.5f),
                            new Vector3(0.0f, 0.0f, 1.0f),
                            new Vector2(1.0f, 0.0f)
                        )
                    );

                    mesh.AddVertex(
                        new StandardVertex(
                            new Vector3(0.5f, -0.5f, 0.5f),
                            new Vector3(0.0f, 0.0f, 1.0f),
                            new Vector2(0.0f, 0.0f)
                        )
                    );

                    mesh.AddVertex(
                        new StandardVertex(
                            new Vector3(-0.5f, 0.5f, 0.5f),
                            new Vector3(0.0f, 0.0f, 1.0f),
                            new Vector2(1.0f, 1.0f)
                        )
                    );

                    mesh.AddVertex(
                        new StandardVertex(
                            new Vector3(0.5f, 0.5f, 0.5f),
                            new Vector3(0.0f, 0.0f, 1.0f),
                            new Vector2(0.0f, 1.0f)
                        )
                    );

                }

                public static void Left(StandardMesh mesh)
                {
                    mesh.AddVertex(
                              new StandardVertex(
                                  new Vector3(-0.5f, -0.5f, -0.5f),
                                  new Vector3(-1.0f, 0.0f, 0.0f),
                                  new Vector2(1.0f, 0.0f)
                              )
                          );

                    mesh.AddVertex(
                        new StandardVertex(
                            new Vector3(-0.5f, -0.5f, 0.5f),
                            new Vector3(-1.0f, 0.0f, 0.0f),
                            new Vector2(0.0f, 0.0f)
                        )
                    );

                    mesh.AddVertex(
                        new StandardVertex(
                            new Vector3(-0.5f, 0.5f, -0.5f),
                            new Vector3(-1.0f, 0.0f, 0.0f),
                            new Vector2(1.0f, 1.0f)
                        )
                    );

                    mesh.AddVertex(
                        new StandardVertex(
                            new Vector3(-0.5f, 0.5f, 0.5f),
                            new Vector3(-1.0f, 0.0f, 0.0f),
                            new Vector2(0.0f, 1.0f)
                        )
                    );
                }

                private static void Right(StandardMesh mesh)
                {
                    mesh.AddVertex(
                           new StandardVertex(
                               new Vector3(0.5f, -0.5f, -0.5f),
                               new Vector3(1.0f, 0.0f, 0.0f),
                               new Vector2(0.0f, 0.0f)
                           )
                       );

                    mesh.AddVertex(
                        new StandardVertex(
                            new Vector3(0.5f, -0.5f, 0.5f),
                            new Vector3(1.0f, 0.0f, 0.0f),
                            new Vector2(1.0f, 0.0f)
                        )
                    );

                    mesh.AddVertex(
                        new StandardVertex(
                            new Vector3(0.5f, 0.5f, -0.5f),
                            new Vector3(1.0f, 0.0f, 0.0f),
                            new Vector2(0.0f, 1.0f)
                        )
                    );

                    mesh.AddVertex(
                        new StandardVertex(
                            new Vector3(0.5f, 0.5f, 0.5f),
                            new Vector3(1.0f, 0.0f, 0.0f),
                            new Vector2(1.0f, 1.0f)
                        )
                    );
                }

                private static void Top(StandardMesh mesh)
                {
                    mesh.AddVertex(
                              new StandardVertex(
                                  new Vector3(-0.5f, 0.5f, -0.5f),
                                  new Vector3(0.0f, 1.0f, 0.0f),
                                  new Vector2(0.0f, 0.0f)
                              )
                          );

                    mesh.AddVertex(
                        new StandardVertex(
                            new Vector3(0.5f, 0.5f, -0.5f),
                            new Vector3(0.0f, 1.0f, 0.0f),
                            new Vector2(1.0f, 0.0f)
                        )
                    );

                    mesh.AddVertex(
                        new StandardVertex(
                            new Vector3(-0.5f, 0.5f, 0.5f),
                            new Vector3(0.0f, 1.0f, 0.0f),
                            new Vector2(0.0f, 1.0f)
                        )
                    );

                    mesh.AddVertex(
                        new StandardVertex(
                            new Vector3(0.5f, 0.5f, 0.5f),
                            new Vector3(0.0f, 1.0f, 0.0f),
                            new Vector2(1.0f, 1.0f)
                        )
                    );
                }

                private static void Bottom(StandardMesh mesh)
                {

                    mesh.AddVertex(
                      new StandardVertex(
                          new Vector3(-0.5f, -0.5f, 0.5f),
                          new Vector3(0.0f, -1.0f, 0.0f),
                          new Vector2(0.0f, 1.0f)
                      )
                  );

                    mesh.AddVertex(
                        new StandardVertex(
                            new Vector3(0.5f, -0.5f, 0.5f),
                            new Vector3(0.0f, -1.0f, 0.0f),
                            new Vector2(0.0f, 0.0f)
                        )
                    );

                    mesh.AddVertex(
                        new StandardVertex(
                            new Vector3(-0.5f, -0.5f, -0.5f),
                            new Vector3(0.0f, -1.0f, 0.0f),
                            new Vector2(0.0f, 1.0f)
                        )
                    );

                    mesh.AddVertex(
                        new StandardVertex(
                            new Vector3(0.5f, -0.5f, -0.5f),
                            new Vector3(0.0f, -1.0f, 0.0f),
                            new Vector2(1.0f, 1.0f)
                        )
                    );
                }

                private static void Triangles(StandardMesh mesh)
                {
                    // front
                    mesh.AddFace(0, 2, 3);
                    mesh.AddFace(0, 3, 1);

                    // Back
                    mesh.AddFace(5, 7, 6);
                    mesh.AddFace(5, 6, 4);

                    // Left
                    mesh.AddFace(9, 10, 8);
                    mesh.AddFace(9, 11, 10);

                    // Right
                    mesh.AddFace(12, 14, 15);
                    mesh.AddFace(12, 15, 13);

                    // Bottom
                    mesh.AddFace(16, 18, 19);
                    mesh.AddFace(16, 19, 17);

                    // top

                    mesh.AddFace(20, 22, 23);
                    mesh.AddFace(20, 23, 21);

                }
    }
}
