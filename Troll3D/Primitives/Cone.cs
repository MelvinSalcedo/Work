using System;
using System.Collections.Generic;
using SharpDX;

namespace Troll3D{

    /// <summary>
    /// Modélise un cone. La valeur de discrétisation détermine le nombre de points
    /// utilisé pour déterminer le cercle
    /// </summary>
    public class Cone{

        // Public

            // Static Methods

                /// <summary>
                /// Crée un nouveau cône dont le cercle de base est composé du nombre
                /// de sommet passé en paramètre
                /// </summary>
                /// <param name="discretisation"></param>
                /// <returns></returns>
                public static Mesh GetMesh(int discretisation){

                    StandardMesh smesh = new StandardMesh();

                    // On commence par crée les points du cercle qui forme la base du cône
                    for (int i = 0; i < discretisation; i++){

                        float angleVal =  2.0f *3.141592f * (float)i/(float)discretisation;

                        StandardVertex svertex = new StandardVertex(
                            new Vector3(
                            (float)Math.Cos(angleVal),
                            0.0f,
                            (float)Math.Sin(angleVal)
                            
                            ));

                        smesh.AddVertex(svertex);

                    }

                    smesh.AddVertex(new StandardVertex(new Vector3(0.0f, 1.0f, 0.0f)));

                    
                    for (int i = 0; i < discretisation -1 ; i++)
                    {
                        smesh.AddFace(i + 1,i , smesh.GetVertexCount() - 1);
                    }

                    smesh.AddFace( 0, smesh.GetVertexCount() - 2, smesh.GetVertexCount() - 1);

                    // On construit la base


                    int startSecondCircle = smesh.GetVertexCount();
                    // On Crée une deuxième fois les sommets à la base du cone pour éviter les problèmes
                    // de normales (comme le cube par exemple)
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


                    for(int i=0; i< discretisation -1; i++ ){
                        smesh.AddFace(startSecondCircle, startSecondCircle + i, startSecondCircle+i + 1);
                    }

                    smesh.ComputeNormals();
                    smesh.ComputeTangents();

                    return smesh.ReturnMesh();
                }


            // Lifecycle

                public Cone() { }
    }
}
