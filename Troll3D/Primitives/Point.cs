using System;
using System.Collections.Generic;
using SharpDX;
using SharpDX.Direct3D11;

namespace Troll3D {


    public class T3DPoint: Primitive{

        // Public 

            // Lifecycle 

                public T3DPoint(Vector3 pos, float radius, Color col) {
                    color_  = col;
                    pos_    = pos;
                    radius_ = radius;
                }

            // Virtual Methods

                public override void Draw() {

                    //PrimitiveRenderer.Instance.material_.Send();
                    //Transform transform = new Transform();
                    //transform.Translation(pos_);
                    //transform.SetScale(radius_, radius_, radius_);
                    //transform.Update();

                    //int fillmode = Cube.GetMesh().fillmode_;
                    //transform.Send(PrimitiveRenderer.Instance.material_.effect_);

                    //Cube.GetMesh().SetFillMode((int)FillMode.Solid);
                    //DirectxApplication.GetDevice().SetRenderState(RenderState.FillMode, FillMode.Solid);

                    //PrimitiveRenderer.Instance.material_.effect_.Begin();
                    //for (int i = 0; i < PrimitiveRenderer.Instance.material_.passCounts; i++) {
                    //    PrimitiveRenderer.Instance.material_.effect_.BeginPass(i);

                    //    Cube.GetMesh().Draw();
                    //    PrimitiveRenderer.Instance.material_.effect_.EndPass();
                    //}
                    //PrimitiveRenderer.Instance.material_.effect_.End();
                    //DirectxApplication.GetDevice().SetRenderState(RenderState.FillMode, fillmode); 
                    //Cube.GetMesh().SetFillMode(fillmode);

                }

        // Datas 

        public Vector3 pos_;
        public float radius_;
        public Color color_;
    }
}