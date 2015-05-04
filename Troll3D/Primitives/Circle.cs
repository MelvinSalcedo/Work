using System;
using System.Collections.Generic;
using SharpDX;


namespace Troll3D{

public class Circle  {
    
    // Public 

        // Static Methods

            public static Mesh GetMesh(int discretisation){

                StandardMesh smesh = new StandardMesh();

                for (int i = 0; i < discretisation; i++) {

                    float angleVal = 2.0f * 3.141592f * (float)i / (float)discretisation;

                    StandardVertex svertex = new StandardVertex(
                        new Vector3(
                        (float)Math.Cos(angleVal),
                        0.0f,
                        (float)Math.Sin(angleVal)

                        ));

                    smesh.AddVertex(svertex);

                }

                for (int i = 0; i < discretisation - 1; i++) {
                    smesh.AddFace(0, i, i + 1);
                }

                smesh.ComputeNormals();
                smesh.ComputeTangents();
                return smesh.ReturnMesh();

            }
    }
}
    