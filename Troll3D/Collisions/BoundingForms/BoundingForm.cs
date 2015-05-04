using System;
using System.Collections.Generic;
using SharpDX;

namespace Troll3D{

    public enum BoundingType{
        OBB,
        Bounding_Sphere
    }

    // Classe de base pour les différentes boites englobantes servant aux collisions
    public abstract class BoundingForm : Entity{

        // Public

            // Lifecycle

                public BoundingForm(Entity entity) : base(entity) {
                    Color_ = new Vector4(0.0f, 1.0f, 0.0f, 1.0f);
                    CollisionManager.Instance.boundingforms_.Add(this);
                    active_ = true;
                }

            // Methods

                //public abstract bool Collide(BoundingForm b);
                public abstract bool Collide(OBB box);
                public abstract bool Collide(BoundingSphere sphere);


                //public override void Draw() {
                //    base.Draw();
                //    //if (show_) {

                //    //    Vector4 matcolor = material_.maincolor;
                //    //    material_.maincolor = Color_;
                //    //    material_.Send();
                //    //    material_.maincolor = matcolor;

                //    //    transform_.Send(material_.effect_);
                //    //    material_.effect_.Begin();
                //    //    for (int i = 0; i < material_.passCounts; i++) {
                //    //        material_.effect_.BeginPass(i);
                //    //        mesh_.Draw();
                //    //        material_.effect_.EndPass();
                //    //    }
                //    //    material_.effect_.End();
                //    //}
                //}

                public Entity link {
                    get { return linkedto_; }
                }

            // Datas

                public  bool            active_;
                public  Vector4         Color_;
                public  BoundingType    type_;
                public  Entity          linkedto_;


    }
}
