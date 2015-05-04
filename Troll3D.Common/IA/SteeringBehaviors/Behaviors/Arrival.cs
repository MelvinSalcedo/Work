//using System;
//using System.Collections.Generic;
//using SharpDX;

//namespace IA{

//    public class Arrival : SteeringBehavior{

//        // Public 

//            // Lifecycle 

//                public Arrival(Vector3 arrivalpos, float radius, float desiredspeed, float weight) {
//                    end_            = false;
//                    arrivalpos_     = arrivalpos;
//                    radius_         = radius;
//                    weight_         = weight;
//                    desiredspeed_   = desiredspeed;
//                }

//            // Virtual Methods 

//                public override Vector3 Compute() {

//                    float distance = (arrivalpos_ - GetPosistion()).Length();
//                    Vector3 desiredvelocity = arrivalpos_ - GetPosistion();

//                    // manager_.desiredorientation_ = desiredvelocity.normalized;

//                    if (distance < radius_) {
//                        desiredvelocity = arrivalpos_ - GetPosistion();
//                        desiredvelocity = Vector3.Normalize(desiredvelocity);
//                        desiredvelocity = desiredvelocity * (distance / radius_);
//                    } else {
//                        desiredvelocity = Vector3.Normalize(desiredvelocity);
//                        desiredvelocity = desiredvelocity * desiredspeed_;
//                    }

//                    if (distance < 0.1 * radius_) {
//                        return -CurrentVelocity(); ;
//                    }
//                    return desiredvelocity * weight_;
//                }

//            // Datas 

//                public bool end_;
//                public Vector3 lastdir_;
//                public Vector3 lastlastdir_;

//                public Vector3 arrivalpos_;
//                public float radius_;
//                public float desiredspeed_;

//    }
//}
