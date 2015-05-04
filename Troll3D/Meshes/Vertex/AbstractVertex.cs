using System;
using System.Collections.Generic;
using SharpDX;
using SharpDX.Direct3D11;
using SharpDX.DXGI;

namespace Troll3D{

    public enum VertexTypeD11 {
        STANDARD_VERTEX,
        COLOR_VERTEX
    }

    public abstract class AbstractVertex {

        // Public

            // Static Methods

                /// <summary>
                ///  Retourne les informations concernant le type sommet nécessaire à DirectX
                /// </summary>
                public static InputElement[] Infos(VertexTypeD11 type){

                    switch (type){
                        case VertexTypeD11.COLOR_VERTEX :
                            return ColoredVertex.GetInfos();
                        case VertexTypeD11.STANDARD_VERTEX:
                            return StandardVertex.GetInfos();
                    }

                    return null;
                }

                /// <summary>
                /// Retourne la taille en octet d'un sommet
                /// </summary>
                public static int Size(VertexTypeD11 type){
                    switch (type)
                    {
                        case VertexTypeD11.COLOR_VERTEX:
                            return ColoredVertex.GetSize();
                        case VertexTypeD11.STANDARD_VERTEX:
                            return StandardVertex.GetSize();
                    }
                    return -1;
                }

            // Methods

                public void AddFace(int faceIndex){
                    if (!IsFaceAlreadyAdded(faceIndex)){
                        Faces.Add(faceIndex);
                    }
                }

            // Datas

                public List<int> Faces = new List<int>(); // Liste d'indice des faces rattaché au sommet

        // Private

            // Methods

                /// <summary>
                /// Vérifie que la face que l'on souhaite insérer n'existe pas déjà
                /// </summary>
                /// <param name="index"></param>
                private bool IsFaceAlreadyAdded(int index){

                    for (int i = 0; i < Faces.Count; i++){
                        if (Faces[i] == index){
                            return true;
                        }
                    }
                    return false;
                }

            // Methods

                public abstract byte[] Datas();

            // Datas

                public VertexTypeD11 type_;


    }
}
