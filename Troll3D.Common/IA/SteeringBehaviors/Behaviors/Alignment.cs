//using System;
//using System.Collections.Generic;
//using SharpDX;

//namespace IA.SteeringBehaviors.Behaviors {

//    public class Alignment {

//        // Public 

//            // Lifecycle 

//                public Alignment(SteeringManager manager, float radius, float weight) {
//                    manager_ = manager;
//                    weight_ = weight;
//                    radius_ = radius;
//                }

//            // Virtual Methods 

//                public override Vector3 Compute() {
//                    SteeringManager[] neighbours = SteeringManager.GetNeigbours(manager_, radius_);

//                    Vector3 sumdir = new Vector3();

//                    if (neighbours.Length > 0) {
//                        for (int i = 0; i < neighbours.Count(); i++) {
//                            sumdir += (neighbours[i].currentvelocity_).normalized;
//                        }
//                        sumdir = sumdir / neighbours.Length;

//                        return ((sumdir.normalized - CurrentVelocity().normalized)) * weight_;
//                    }
//                    return Vector3.zero;
//                }

//            // Datas 

//                public float radius_;

//    }
//}
