using System;
using System.Collections.Generic;
using SharpDX;
using SharpDX.Direct3D11;

namespace Troll3D{

    /// <summary>
    ///  Permet de représenter graphiquement une projection    
    /// </summary>
    public class ProjectionMesh{

        // Public

            // Static Methods

                public static Vector3 RealVertex(Vector4 vec){
                    return new Vector3(vec.X / vec.W, vec.Y / vec.W, vec.Z / vec.W);
                }

                public static Mesh GetModel(Projection projection){

                    Mesh model = new Mesh();
                    Vector3[] vertices = new Vector3[8];

                    Matrix mat = projection.Data;
                    Matrix inverse = Matrix.Invert(mat);

                    // On utilise la transformation inverse pour transformer les points de l'espace d'origine vers l'espace d'arrivé (et non l'inverse)
                    
                    // Near Plane


                    vertices[0] = RealVertex(Vector4.Transform(new Vector4(-1.0f, -1.0f, -1.0f, 1.0f), inverse));
                    vertices[1] = RealVertex(Vector4.Transform(new Vector4( 1.0f, -1.0f, -1.0f, 1.0f), inverse));
                    vertices[2] = RealVertex(Vector4.Transform(new Vector4(-1.0f, 1.0f, -1.0f, 1.0f), inverse));
                    vertices[3] = RealVertex(Vector4.Transform(new Vector4( 1.0f, 1.0f, -1.0f, 1.0f), inverse));

                    // Far Plane
                    vertices[4] = RealVertex(Vector4.Transform(new Vector4(-1.0f, -1.0f, 1.0f, 1.0f), inverse));
                    vertices[5] = RealVertex(Vector4.Transform(new Vector4(1.0f, -1.0f, 1.0f, 1.0f), inverse));
                    vertices[6] = RealVertex(Vector4.Transform(new Vector4(-1.0f, 1.0f, 1.0f, 1.0f), inverse));
                    vertices[7] = RealVertex(Vector4.Transform(new Vector4(1.0f, 1.0f, 1.0f, 1.0f), inverse));

                    FrontFace(model,    vertices);
                    BackFace(model, vertices);
                    LeftFace(model, vertices);
                    RightFace(model, vertices);
                    BottomFace(model,   vertices);
                    TopFace(model,      vertices);

                    MakeTriangles(model);
                    model.UpdateMesh();

                    return model;


                }

            // Lifecycle

        // Private

            // Static Methods

                private static void FrontFace(Mesh model, Vector3[] vertices){
                    model.AddVertex( new StandardVertex(
                        vertices[0],
                        new Vector3(0.0f,0.0f,-1.0f),
                        new Vector2(0.0f, 0.0f)
                        
                        ));

                    model.AddVertex(new StandardVertex(
                        vertices[1],
                        new Vector3(0.0f, 0.0f, -1.0f),
                        new Vector2(0.0f, 0.0f)

                        ));

                    model.AddVertex(new StandardVertex(
                        vertices[2],
                        new Vector3(0.0f, 0.0f, -1.0f),
                        new Vector2(0.0f, 0.0f)

                        ));

                    model.AddVertex(new StandardVertex(
                        vertices[3],
                        new Vector3(0.0f, 0.0f, -1.0f),
                        new Vector2(0.0f, 0.0f)
                        ));
                }

                private static void BackFace(Mesh model, Vector3[]vertices){
                    model.AddVertex(new StandardVertex(
                        vertices[4],
                        new Vector3(0.0f, 0.0f, 1.0f),
                        new Vector2(0.0f, 0.0f)

                        ));

                    model.AddVertex(new StandardVertex(
                        vertices[5],
                        new Vector3(0.0f, 0.0f, 1.0f),
                        new Vector2(0.0f, 0.0f)

                        ));

                    model.AddVertex(new StandardVertex(
                        vertices[6],
                        new Vector3(0.0f, 0.0f, 1.0f),
                        new Vector2(0.0f, 0.0f)

                        ));

                    model.AddVertex(new StandardVertex(
                        vertices[7],
                        new Vector3(0.0f, 0.0f, 1.0f),
                        new Vector2(0.0f, 0.0f)
                        ));
                }

                private static void TopFace(Mesh model, Vector3[] vertices){
                    model.AddVertex(new StandardVertex(
                        vertices[2],
                        new Vector3(0.0f, 1.0f, 0.0f),
                        new Vector2(0.0f, 0.0f)

                        ));

                    model.AddVertex(new StandardVertex(
                        vertices[3],
                        new Vector3(0.0f, 1.0f, 0.0f),
                        new Vector2(0.0f, 0.0f)
                        ));

                    model.AddVertex(new StandardVertex(
                        vertices[6],
                        new Vector3(0.0f, 1.0f, 0.0f),
                        new Vector2(0.0f, 0.0f)

                        ));

                    model.AddVertex(new StandardVertex(
                        vertices[7],
                        new Vector3(0.0f, 1.0f, 0.0f),
                        new Vector2(0.0f, 0.0f)
                        ));
                }

                private static void BottomFace(Mesh model, Vector3[] vertices){
                    model.AddVertex(new StandardVertex(
                      vertices[4],
                      new Vector3(0.0f, 1.0f, 0.0f),
                      new Vector2(0.0f, 0.0f)

                      ));

                    model.AddVertex(new StandardVertex(
                        vertices[5],
                        new Vector3(0.0f, 1.0f, 0.0f),
                        new Vector2(0.0f, 0.0f)
                        ));

                    model.AddVertex(new StandardVertex(
                        vertices[0],
                        new Vector3(0.0f, 1.0f, 0.0f),
                        new Vector2(0.0f, 0.0f)

                        ));

                    model.AddVertex(new StandardVertex(
                        vertices[1],
                        new Vector3(0.0f, 1.0f, 0.0f),
                        new Vector2(0.0f, 0.0f)
                        ));
                }

                private static void LeftFace(Mesh model, Vector3[] vertices){
                    model.AddVertex(new StandardVertex(
                      vertices[0],
                      new Vector3(0.0f, 1.0f, 0.0f),
                      new Vector2(0.0f, 0.0f)

                      ));

                    model.AddVertex(new StandardVertex(
                        vertices[4],
                        new Vector3(0.0f, 1.0f, 0.0f),
                        new Vector2(0.0f, 0.0f)
                        ));

                    model.AddVertex(new StandardVertex(
                        vertices[2],
                        new Vector3(0.0f, 1.0f, 0.0f),
                        new Vector2(0.0f, 0.0f)

                        ));

                    model.AddVertex(new StandardVertex(
                        vertices[6],
                        new Vector3(0.0f, 1.0f, 0.0f),
                        new Vector2(0.0f, 0.0f)
                        ));
                }

                private static void RightFace(Mesh model, Vector3[] vertices){
                    model.AddVertex(new StandardVertex(
                      vertices[1],
                      new Vector3(0.0f, 1.0f, 0.0f),
                      new Vector2(0.0f, 0.0f)

                      ));

                    model.AddVertex(new StandardVertex(
                        vertices[5],
                        new Vector3(0.0f, 1.0f, 0.0f),
                        new Vector2(0.0f, 0.0f)
                        ));

                    model.AddVertex(new StandardVertex(
                        vertices[3],
                        new Vector3(0.0f, 1.0f, 0.0f),
                        new Vector2(0.0f, 0.0f)

                        ));

                    model.AddVertex(new StandardVertex(
                        vertices[7],
                        new Vector3(0.0f, 1.0f, 0.0f),
                        new Vector2(0.0f, 0.0f)
                        ));
                }

                private static void MakeTriangles(Mesh model){
                    model.AddFace(0, 2, 3);
                    model.AddFace(0, 3, 1);

                    // Back
                    model.AddFace(5, 7, 6);
                    model.AddFace(5, 6, 4);

                    // Left
                    model.AddFace(9, 10, 8);
                    model.AddFace(9, 11, 10);

                    // Right
                    model.AddFace(12, 14, 15);
                    model.AddFace(12, 15, 13);

                    // Bottom
                    model.AddFace(16, 18, 19);
                    model.AddFace(16, 19, 17);

                    // top

                    model.AddFace(20, 22, 23);
                    model.AddFace(20, 23, 21);
                }




                
    }
}
