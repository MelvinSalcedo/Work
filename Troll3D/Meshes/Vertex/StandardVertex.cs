using System;
using System.Collections.Generic;
using SharpDX;
using SharpDX.Direct3D11;
using SharpDX.DXGI;

namespace Troll3D {

    public class StandardVertex : AbstractVertex {

        // Public

            // Static Methods

                /// <summary>
                /// Retourne la description du sommet
                /// </summary>
                public static InputElement[] GetInfos()
                {
                    return new InputElement[] {
                                    new InputElement(){
                                        SemanticName            = "POSITION",
                                        SemanticIndex           = 0,
                                        Format                  = Format.R32G32B32_Float,
                                        Slot                    = 0,
                                        AlignedByteOffset       = 0,
                                        Classification          = InputClassification.PerVertexData,
                                        InstanceDataStepRate    = 0
                                    }, 
                                    new InputElement(){
                                        SemanticName            = "NORMAL",
                                        SemanticIndex           = 0,
                                        Format                  = Format.R32G32B32_Float,
                                        Slot                    = 0,
                                        AlignedByteOffset       = 12,
                                        Classification          = InputClassification.PerVertexData,
                                        InstanceDataStepRate    = 0
                                    },
                                    new InputElement(){
                                        SemanticName            ="TANGENT",
                                        SemanticIndex           = 0,
                                        Format                  = Format.R32G32B32A32_Float,
                                        Slot                    = 0,
                                        AlignedByteOffset       = 24,
                                        Classification          = InputClassification.PerVertexData,
                                        InstanceDataStepRate    = 0
                                    },
                                    new InputElement(){
                                        SemanticName            = "TEXCOORD",
                                        SemanticIndex           = 0,
                                        Format                  = Format.R32G32_Float,
                                        Slot                    = 0,
                                        AlignedByteOffset       = 40,
                                        Classification          = InputClassification.PerVertexData,
                                        InstanceDataStepRate    = 0
                                    }};
                }

                public static int GetSize(){
                    return 12 * sizeof(float);
                }

            // Lifecycle

                public StandardVertex(Vector3 pos){
                    Initialize(pos, new Vector3(0.0f, 0.0f, 0.0f), new Vector2(0.0f, 0.0f));
                }

                public StandardVertex(Vector3 pos, Vector3 normal){
                    Initialize(pos, normal, new Vector2(0.0f, 0.0f));
                }

                public StandardVertex(Vector3 pos, Vector3 normal, Vector2 uv) {
                    Initialize(pos, normal, uv);
                }

                public void Initialize(Vector3 pos, Vector3 normal, Vector2 uv) {
                    Position    = pos;
                    Normal      = normal;
                    Uv          = uv;
                    type_       = VertexTypeD11.STANDARD_VERTEX;
                }

            // Methods

                public override byte[] Datas() {
                    byte[] bytes = new byte[GetSize()];

                    System.Buffer.BlockCopy(Position.ToArray(), 0, bytes, 0,                  3 * sizeof(float));
                    System.Buffer.BlockCopy(Normal.ToArray(),   0, bytes, 3 * sizeof(float),  3 * sizeof(float));
                    System.Buffer.BlockCopy(Tangent.ToArray(),  0, bytes, 6 * sizeof(float),  4 * sizeof(float));
                    System.Buffer.BlockCopy(Uv.ToArray(),       0, bytes, 10 * sizeof(float), 2 * sizeof(float));
                    return bytes;
                }

            // Datas

                public Vector3 Position;
                public Vector3 Normal;
                public Vector4 Tangent; // On utilise une 4 eme dimension pour stocker le déterminant de la matrice de manière à retrouver
                                        // la binormale directement dans le vertex shader et ainsi éviter de passer 3 attributs 
                                        // supplémentaire dans le shader
                public Vector3 Binormal;
                public Vector2 Uv;
                
    }
}
