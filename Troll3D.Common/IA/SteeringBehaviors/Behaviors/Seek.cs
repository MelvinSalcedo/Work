//using System;
//using System.Collections.Generic;
//using SharpDX;

//namespace IA{

//    /// <summary>
//    /// Le comportement seek va diriger l'agent dans la direction souhaitée
//    /// </summary>
//    public class Seek : SteeringBehavior {

//        //  Public 

//            //Lifecycle 

//                public Seek(Vector3 seekpoint, float desiredmaxspeed, float weight) {
//                    seekpoint_          = seekpoint;
//                    weight_             = weight;
//                    desiredmaxspeed_    = desiredmaxspeed;
//                }

//            // Methods 

//                public override Vector3 Compute() {

//                    Vector3 desiredvelocity_ = seekpoint_ - GetPosistion();
//                    desiredvelocity_.Normalize();
//                    desiredvelocity_ *= desiredmaxspeed_;

//                    Vector3 steeringvelocity_ = desiredvelocity_ ;
//                    return steeringvelocity_ * weight_;

//                }

//            // Datas 

//                public Vector3 seekpoint_;
//                public float desiredmaxspeed_;
//    }
//}
