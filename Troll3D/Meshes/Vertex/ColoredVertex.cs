using System;
using System.Collections.Generic;
using SharpDX;
using SharpDX.Direct3D11;
using SharpDX.DXGI;

namespace Troll3D{

    public class ColoredVertex : AbstractVertex{

        // Public

                /// <summary>
                /// Retourne la description du sommet
                /// </summary>
                public static  InputElement[] GetInfos(){

                    return new InputElement[] {
                        new InputElement(){
                            SemanticName = "POSITION",
                            SemanticIndex = 0,
                            Format = Format.R32G32B32_Float,
                            Slot    = 0,
                            AlignedByteOffset  = 0,
                            Classification = InputClassification.PerVertexData,
                            InstanceDataStepRate = 0
                        }, 
                        new InputElement(){
                            SemanticName = "COLOR",
                            SemanticIndex = 0,
                            Format = Format.R32G32B32A32_Float,
                            Slot= 0,
                            AlignedByteOffset = 12,
                            Classification = InputClassification.PerVertexData,
                            InstanceDataStepRate = 0
                        }};
                }

                public static int GetSize()
                {
                    return 7 * sizeof(float);
                }


            // Lifecycle

                public ColoredVertex(Vector3 pos, Color4 color) {
                    Initialize(pos, color);
                }

                public ColoredVertex(Vector3 pos) {
                    Initialize(pos, new Color4(0.0f, 0.0f, 1.0f, 1.0f));
                }

                public void Initialize(Vector3 position, Color4 color) {
                    Position    = position;
                    Color       = color;
                    type_       = VertexTypeD11.COLOR_VERTEX;
                }

            // Methods

                public override byte[] Datas() {
                    byte[] bytes = new byte[7 * sizeof(float)];

                    //Position.ToArray().CopyTo(bytes,0);

                    System.Buffer.BlockCopy(Position.ToArray(), 0, bytes, 0,                3 * sizeof(float));
                    System.Buffer.BlockCopy(Color.ToArray(),    0, bytes, 3 * sizeof(float), 4 * sizeof(float));

                    return bytes;
                }

            // Datas

                public Vector3  Position;
                public Color4   Color;

        // Protected


           
    }
}
