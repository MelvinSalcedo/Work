//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace IA.SteeringBehaviors.Behaviors {
//    class Separation {
//        /* Public */

//        /* Lifecycle */

//        public Separation(SteeringManager manager, float radius, float k, float weight) {
//            manager_ = manager;
//            radius_ = radius;
//            weight_ = weight;
//            k_ = k;
//        }

//        /* Virtual Methods */

//        public override Vector3 Compute() {

//            SteeringManager[] neighbours = SteeringManager.GetNeigbours(manager_, radius_);

//            Vector3 returnvec = Vector3.zero;

//            for (int i = 0; i < neighbours.Count(); i++) {

//                float distance = (neighbours[i].Pos() - Pos()).magnitude;
//                Vector3 dir = (neighbours[i].Pos() - Pos()).normalized;

//                float force = (k_ / (distance * distance));
//                neighbours[i].steeringforces_ += ((dir * force) * weight_);


//            }
//            if (neighbours.Length > 1) {
//                returnvec = Vector3.one;
//            }

//            return returnvec;
//        }

//        /* Datas */

//        public float k_;
//        public float radius_;
//    }
//}
