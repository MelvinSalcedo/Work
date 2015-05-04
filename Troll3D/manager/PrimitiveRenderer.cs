using System;
using System.Collections.Generic;
using SharpDX.Direct3D11;

namespace Troll3D {

    public class PrimitiveRenderer {

        // Public 

            // Static Data 

                public static PrimitiveRenderer Instance;

            // Lifecycle

                public PrimitiveRenderer() {
                    primitives_     = new List<Primitive>();
                    Instance        = this;
                 //   material_       = DirectxApplication.Instance.LoadMaterial("C:\\usr\\resources\\shaders\\basic.fx");
                }

            // Methods 

                public void Draw() {
                    DrawPrimitives();
                    primitives_.Clear();
                }

                public void AddPrimitive(Primitive primitive) {
                    primitives_.Add(primitive);
                }

            // Datas 

                public MaterialDX11 material_;

        // Private 

            // Methods 

                private void DrawPrimitive(Primitive prim) {
                    //DirectxApplication.GetDevice().BeginScene();

                    //prim.Draw();

                    //DirectxApplication.GetDevice().EndScene();

                    //DirectxApplication.GetDevice().Present();

                   
                }

                private void DrawPrimitives() {
                    for (int i = 0; i < primitives_.Count; i++) {
                        DrawPrimitive(primitives_[i]);
                    }
                }

            // Datas 

                private List<Primitive> primitives_;
    }

}
