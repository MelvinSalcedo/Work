//using System;
//using System.Collections.Generic;
//using SharpDX;

//namespace IA{

//    public class Flee : SteeringBehavior {

//        // Public 

//            // Lifecycle 

//                public Flee(Vector3 fleepoint, float maxdesiredspeed, float weight) {
//                    fleepoint_ = fleepoint;
//                    maxdesiredspeed_ = maxdesiredspeed;
//                    weight_ = weight;
//                }

//            // Virtual Methods 

//                public override Vector3 Compute() {
//                    Vector3 desiredvelocity_ = -(fleepoint_ - GetPosistion());
//                    desiredvelocity_.Normalize();
//                    desiredvelocity_ = desiredvelocity_ * maxdesiredspeed_;
//                    return desiredvelocity_ * weight_;
//                }

//            // Datas 

//                public Vector3  fleepoint_;
//                public float    maxdesiredspeed_;
                
//    }
//}
