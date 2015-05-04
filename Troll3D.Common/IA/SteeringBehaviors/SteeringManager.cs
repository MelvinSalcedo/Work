//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using SharpDX;
//using Troll3D;

//namespace IA {

//    public class SteeringManager {

//        // Public

//            // Static Methods

//                //public static SteeringManager[] GetNeigbours(SteeringManager pos, float radius) {

//                //    List<SteeringManager> neighbours = new List<SteeringManager>();

//                //    for (int i = 0; i < steerings_.Count; i++) {
//                //        if (steerings_[i].nocollision_ == false) {
//                //            if (steerings_[i] != pos) {
//                //                if ((steerings_[i].Pos() - pos.Pos()).Length() < radius) {
//                //                    neighbours.Add(steerings_[i]);
//                //                }
//                //            }
//                //        }
//                //    }

//                //    return neighbours.ToArray();
//                //}

        
//        //public static List<Shape> walls_ = new List<Shape>();

//        // Lifecycle

//            public SteeringManager(Vector3 startposition) {
//                m_orientation       = 0.0f;
//                steeringforces_     = new Vector3();
//                currentvelocity_    = new Vector3();
//                m_position          = startposition;
//                m_behaviors         = new List<SteeringBehavior>();
//            }

//        // Methods 

//            public void AddSteeringBehavior(SteeringBehavior sb) {
//                m_behaviors.Add(sb);
//                sb.manager_ = this;
//            }

//            public Vector3 GetPosition() {
//                return m_position;
//            }

//            public float GetOrientation() {
//                return m_orientation;
//            }

//            public void Update() {
//                ComputeSteering();
//                ComputePosition();
//            }

//        // Datas 

//            public float maxvelocity = 0.2f;
//            public float maxsteeringforce = 1.0f;

//            // public RectFormation formation_;
//            public bool         nocollision_ = false;
//            public bool         constantspeed = false;
//            public Vector3      desiredorientation_;
//            public float        desiredspeed_;

//            public Vector3 currentvelocity_;
//            public Vector3 currentdirection_;
//            public Vector3 steeringforces_;


//        // Private 

//            // Methods 

//                private void ComputeSteering() {
//                    for (int i = 0; i < m_behaviors.Count; i++) {
//                        steeringforces_ += m_behaviors[i].Compute();
//                    }
//                }

//                private void ComputePosition() {

//                    currentvelocity_ += steeringforces_;
//                    if (currentvelocity_ != Vector3.Zero) {
//                        currentdirection_ = Vector3.Normalize(currentvelocity_);
//                    }

//                    if (constantspeed) {
//                        currentvelocity_ = Vector3.Normalize(currentvelocity_) * maxvelocity;
//                    } else {
//                        if (currentvelocity_.Length() > maxvelocity) {
//                            currentvelocity_ = Vector3.Normalize(currentvelocity_) * maxvelocity;
//                        }
//                    }

//                    m_position      = m_position + currentvelocity_;
                    
//                    m_orientation   = (float)Math.Acos( Vector3.Dot(Vector3.Normalize(currentvelocity_), Vector3.Right));
//                    if (currentvelocity_.Y < 0) {
//                        m_orientation = -m_orientation;
//                    }
//                    steeringforces_ = new Vector3();
//                }


//            // Datas 

//                public List<Entity> m_obstacles = new List<Entity>();
//                public Vector3      m_position;
//                private float       m_orientation;
//                private List<SteeringBehavior> m_behaviors;
//    }
//}
