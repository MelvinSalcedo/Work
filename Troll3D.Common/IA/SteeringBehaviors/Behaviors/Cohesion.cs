//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace IA.SteeringBehaviors.Behaviors {
//    class Cohesion {

//        /* Public */

//        /* Lifecycle */

//        public Cohesion(SteeringManager manager, float radius, float weight) {
//            manager_ = manager;
//            radius_ = radius;
//            weight_ = weight;
//        }

//        /* Virtual Methods */

//        public override Vector3 Compute() {
//            SteeringManager[] neighbours = SteeringManager.GetNeigbours(manager_, radius_);

//            Vector3 pos = new Vector3();

//            if (neighbours.Length > 0) {

//                for (int i = 0; i < neighbours.Count(); i++) {
//                    pos += (neighbours[i].Pos() - Pos());
//                }
//                pos = pos / neighbours.Length;
//                return (pos - (CurrentVelocity())) * weight_;
//            }

//            return Vector3.zero;
//        }

//        /* Methods */

//        public float radius_;
//    }
//}
