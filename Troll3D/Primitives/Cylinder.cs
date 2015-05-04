using System;
using System.Collections.Generic;
using SharpDX;

namespace Troll3D{

    public class Cylinder{

        // Public

            // Static Methods

                /// <summary>
                /// Retourne un cylindre, la valeur passé en paramètre détermine le niveau de 
                /// détail du cylindre
                /// </summary>
                public static Mesh GetMesh(int discretisation){

                    StandardMesh smesh = new StandardMesh();

                    // On commence par crée les points du cercle qui forme la base du cylindre
                    for (int i = 0; i < discretisation; i++){

                        float angleVal = 2.0f * 3.141592f * (float)i / (float)discretisation;

                        StandardVertex svertex = new StandardVertex(
                            new Vector3(
                            (float)Math.Cos(angleVal),
                            0.0f,
                            (float)Math.Sin(angleVal)

                            ));

                        smesh.AddVertex(svertex);

                    }

                    for (int i = 0; i < discretisation - 1; i++)
                    {
                        smesh.AddFace(0, i, i + 1);
                    }

                    int meshOffset = smesh.GetVertexCount();
                    // On commence par crée un deuxième cercle en haut du cylindre
                    for (int i = 0; i < discretisation; i++)
                    {

                        float angleVal = 2.0f * 3.141592f * (float)i / (float)discretisation;

                        StandardVertex svertex = new StandardVertex(
                            new Vector3(
                            (float)Math.Cos(angleVal),
                            1.0f,
                            (float)Math.Sin(angleVal)

                            ));

                        smesh.AddVertex(svertex);

                    }

                    for (int i = 0; i < discretisation - 1; i++)
                    {
                        smesh.AddFace(meshOffset, meshOffset + i + 1 , meshOffset + i );
                    }

                    meshOffset = smesh.GetVertexCount();

                    // On Crée encore une fois le cercle à la base et en haut du cylindre pour éviter les souscis de normales
                    for (int i = 0; i < discretisation; i++){

                        float angleVal = 2.0f * 3.141592f * (float)i / (float)discretisation;

                        StandardVertex svertex = new StandardVertex(
                            new Vector3(
                            (float)Math.Cos(angleVal),
                            0.0f,
                            (float)Math.Sin(angleVal)

                            ));

                        smesh.AddVertex(svertex);

                    }
                    // On Crée encore une fois le cercle à la base et en haut du cylindre pour éviter les souscis de normales
                    for (int i = 0; i < discretisation; i++)
                    {

                        float angleVal = 2.0f * 3.141592f * (float)i / (float)discretisation;

                        StandardVertex svertex = new StandardVertex(
                            new Vector3(
                            (float)Math.Cos(angleVal),
                            1.0f,
                            (float)Math.Sin(angleVal)

                            ));

                        smesh.AddVertex(svertex);

                    }


                    for (int i = 0; i < discretisation - 1; i++){
                        smesh.AddFace(meshOffset+ i, meshOffset + discretisation+ i, meshOffset + i + 1  );
                        smesh.AddFace(meshOffset +i +1, meshOffset + discretisation + i, meshOffset + discretisation + i + 1);
                    }

                    smesh.AddFace(meshOffset + discretisation -1, meshOffset + discretisation + discretisation-1, meshOffset);
                    smesh.AddFace(meshOffset, meshOffset + discretisation + discretisation - 1, meshOffset + discretisation);
                    //smesh.AddFace(meshOffset + i + 1, meshOffset + discretisation + i, meshOffset + discretisation + i + 1);

                    smesh.ComputeNormals();
                    smesh.ComputeTangents();
                    return smesh.ReturnMesh();

                }
            // Lifecycle
    }
}
