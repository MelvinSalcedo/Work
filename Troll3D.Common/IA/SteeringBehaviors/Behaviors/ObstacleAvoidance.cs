//using System;
//using System.Collections.Generic;
//using SharpDX;
//using Troll3D;

//namespace IA{

//    public class ObstacleAvoidance : SteeringBehavior{

//        // Public 

//            // Lifecycle 

//                public ObstacleAvoidance( float ratio, float weight) {
//                    weight_ = weight;
//                    ratio_ = ratio;

//                   //// midray_ = new Troll3D.TrollRay(GetPosistion(), new Vector3(1.0f,0.0f,0.0f);

//                   // midray_ = new Shape(vertices, Vector3.Zero, Vector3.Zero, Vector3.One);

//                   // Vector3[] vertices2 = new Vector3[2];
//                   // vertices2[0] = new Vector3(0.0f, 0.0f, 0.0f);
//                   // vertices2[1] = new Vector3(0.3f, 0.2f, 0.0f);

//                   // leftray_ = new Shape(vertices2, Vector3.Zero, Vector3.Zero, Vector3.One);

//                   // Vector3[] vertices3 = new Vector3[2];
//                   // vertices3[0] = new Vector3(0.0f, 0.0f, 0.0f);
//                   // vertices3[1] = new Vector3(0.3f, -0.2f, 0.0f);

//                   // rightray_ = new Shape(vertices3, Vector3.Zero, Vector3.Zero, Vector3.One);
//                }

//        // Virtual Methods 

//        public override Vector3 Compute() {

//            float currentspeed = CurrentVelocity().Length();

//            InitializeShape(midray_, currentspeed);
//            InitializeShape(leftray_, currentspeed);
//            InitializeShape(rightray_, currentspeed);

//            float distance = 9999999999999999;
//            Vector3[] data = new Vector3[2];
//            bool found = false;

//            for (int i = 0; i < SteeringManager.walls_.Count(); i++) {
//                if (SAT.Intersect(midray_, SteeringManager.walls_[i])) {

//                    found = true;
//                    Vector3[] currendata = SAT.GetNormalAndClosestPoint(
//                    SteeringManager.walls_[i],
//                    midray_.worldvertices_[0],
//                    midray_.worldvertices_[1]);

//                    float localdistance = (currendata[0] - Pos()).magnitude;

//                    if (localdistance < distance) {
//                        data = currendata;
//                        distance = localdistance;
//                    }
//                }
//            }

//            if (found) {
//                Console.WriteLine("found");
//                Console.WriteLine(data[1]);
//                Vector3 vec = data[0] - GetPosistion();
//                float magnitude = vec.Length();
//                //manager_.currentvelocity_ = manager_.currentvelocity_ * ( magnitude / (magnitude*magnitude );

//                Vector3 steeringforces_ = data[1] * ((1.0f / magnitude) * 1.5f);

//                return steeringforces_ * weight_;
//            } else {
//                return Vector3.Zero;
//            }
//        }

//        // Methods 

//            public void InitializeShape(Shape shape, float currentspeed) {
//                shape.Rotate(manager_.obj_.EulerAngle());
//                shape.Translate(manager_.obj_.Position());
//                shape.Scale(new Vector3(ratio_ * currentspeed, ratio_ * currentspeed, ratio_ * currentspeed));
//                shape.UpdateVertices();
//            }

//        // Datas 

//        public float    ratio_;

//        public TrollRay midray_;
//        public TrollRay leftray_;
//        public TrollRay rightray_;

//    }
//}
