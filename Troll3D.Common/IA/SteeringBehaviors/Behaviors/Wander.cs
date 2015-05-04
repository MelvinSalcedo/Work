//using System;
//using System.Collections.Generic;
//using SharpDX;


//namespace IA{

//    public class Wander : SteeringBehavior{

//        // Public 

//            // Lifecycle 

//                public Wander(float circleradius, float circledistance, float weight, float angle) {
//                    circleradius_ = circleradius;
//                    circledistance_ = circledistance;
//                    weight_ = weight;
//                    anglestuff = angle;
//                    angle_ = 0.0f;
//                    m_random = new Random();
//                }

//            // Virtual Methods

//                public override Vector3 Compute() {

//                    float anglechange = m_pi / 11;

//                    angle_ += (m_random.NextFloat(-anglestuff, anglestuff));

//                    // Calcul du centre du cercle
//                    Vector3 circlecenter = Vector3.Normalize( manager_.currentdirection_) * circledistance_;

//                    // Calcul du vecteur de déplacement

//                    Vector3 circlevector = new Vector3();

//                    circlevector.X = (float)Math.Cos(angle_);
//                    circlevector.Y = (float)Math.Sin(angle_);

//                    circlevector = circlevector * circleradius_;

//                    steeringforce   = circlecenter  + circlevector  ;

//                    return steeringforce * weight_;
//                }

        

//        // Datas 

//            public Vector3 steeringforce;
//            public Random m_random;
//            public float angle_;
//            public float circleradius_;
//            public float circledistance_;
            

//            private float m_pi = 3.141592f;
//            public float anglestuff;
//    }
//}
