using System;
using System.Collections.Generic;
using SharpDX;

namespace Troll3D{

    public class Grid {

        // Public

            // Static Method

                /// <summary>
                /// Retourne un Mesh de (dimension+1)² sommets et de (dimension)² de triangles
                /// </summary>
                /// <param name="dimension"></param>
                /// <returns></returns>
                public static Mesh GetMesh(int dimension) {

                    Mesh model = new Mesh(VertexTypeD11.STANDARD_VERTEX);

                    for (int i = 0; i < dimension + 1; i++) {
                        for (int j = 0; j < dimension+1; j++) {
                            model.AddVertex(
                                new StandardVertex(
                                    new Vector3( (float)(j)/(float)dimension - 0.5f,0.0f, (float)(i)/(float)dimension -0.5f) ,
                                    new Vector3(0.0f, 1.0f, 0.0f),
                                    new Vector2((float)(j)/(float)dimension ,(float)(i)/(float)dimension ))
                                    );
                        }
                    }

                    for (int i = 0; i < dimension; i++) {
                        for (int j = 0; j < dimension; j++) {
                            model.AddFace(i * (dimension + 1) + j, (i + 1) * (dimension + 1) + j, (i + 1) * (dimension + 1) + j + 1);
                            model.AddFace(i * (dimension + 1) + j, (i + 1) * (dimension + 1) + j + 1, (i) * (dimension + 1) + j + 1);
                        }
                    }

                    model.UpdateMesh();
                    return model;

                }
            
            // Lifecycle

            // Methods

    }
}
