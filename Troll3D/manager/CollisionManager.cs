using System;
using System.Collections.Generic;
using SharpDX;

namespace Troll3D {

    public class CollisionManager {

        public static bool AABBtoAABB(AABB boxa, AABB boxb)
        {
            if(
                boxa.X + boxa.Width     > boxb.X                &&
                boxa.X                  < boxb.X + boxb.Width   &&
                boxa.Y + boxa.Height    > boxb.Y                &&
                boxa.Y                  < boxb.Y + boxb.Height  &&
                boxa.Z + boxa.Depth     > boxb.Z                &&
                boxa.Z                  < boxb.Z + boxb.Depth )
            {
                    return true;
            }
            return false;
        }
        public static bool OBBtoOBB(OBB boxa, OBB boxb) 
        {
            return SAT.Intersect3D(boxa.GetShape(), boxb.GetShape());
        }
        public static bool BoundingSphereToBoundingSphere(BoundingSphere a, BoundingSphere b) 
        {
            if ((a.transform_.WorldPosition() - b.transform_.WorldPosition()).Length() <= (a.radius_ + b.radius_)) {
                return true;
            }
            return false;
        }
        public static bool OBBtoBoundingSphere(OBB obox, BoundingSphere sphere) 
        {
            return SAT.Intersect3DShapeShere(obox, sphere.transform_.WorldPosition(), sphere.radius_);
        }

        public static CollisionManager Instance;

        public MaterialDX11 material_;

        public CollisionManager() {

            // Le CollisionManager charge aussi le matérial utilisé par défault par les bouding forms
            material_ = new MaterialDX11("vDefault.cso", "pUnlit.cso", "gDefault.cso");
            material_.AddConstantBuffer<Vector4>(new Vector4(0.0f, 0.0f, 1.0f, 1.0f));
                    
            Instance        = this;
            events_         = new Stack<CollisionEvent>();
            boundingforms_  = new List<BoundingForm>();
            collisionsenters_ = new List<CollisionEvent>();
        }

        // Vérifie que les boites englobantes se recoupe, crée des objets 
        // Collision event 
        public void UpdateCollisions() {

            if (boundingforms_.Count > 1) {

                List<CollisionEvent> toremove = new List<CollisionEvent>();
                for (int i = 0; i < collisionsenters_.Count; i++) {
                    if (collisionsenters_[i].a_.active_ == false || collisionsenters_[i].b_.active_ == false) {
                        toremove.Add(collisionsenters_[i]);
                    }
                }

                for (int i = 0; i < toremove.Count; i++) {
                    collisionsenters_.Remove(toremove[i]);
                }

                    for (int i = 0; i < boundingforms_.Count; i++) {

                        for (int j = i + 1; j < boundingforms_.Count; j++) {

                            if (boundingforms_[i].active_ && boundingforms_[j].active_) {
                                // Teste entre layers histoire de vérifier qu'on doit bien tester cette collision
                                if (LayerManager.Instance.AreColliding(boundingforms_[i].layer_, boundingforms_[j].layer_)) {
                                    // Si il y a une collision, je vérifie si la collision a déjà eu lieu, de manière à determiner si je retourne
                                    // un collisionENter Event ou un COllision Event
                                    if (Collide(boundingforms_[i], boundingforms_[j])) {
                                        if (!DidTheyAlreadyCollide(boundingforms_[i], boundingforms_[j])) {
                                            events_.Push(new CollisionEvent(boundingforms_[i], boundingforms_[j], CollisionType.CollisionEnter));
                                            collisionsenters_.Add(events_.Peek());
                                        } else {
                                            events_.Push(new CollisionEvent(boundingforms_[i], boundingforms_[j], CollisionType.Colliding));
                                        }
                                    }
                                }
                            }
                        }
                    }
            }

            // Méthode qui va vérifier si les objets qui sont en collision actuellement le sont encore et lève un événement CollisionExit
            AreTheyStillColliding();
        }

                private bool DidTheyAlreadyCollide(BoundingForm a, BoundingForm b) {
                    for (int i = 0; i < collisionsenters_.Count; i++) {
                        if((collisionsenters_[i].a_ == a && collisionsenters_[i].b_ == b ) || 
                        (collisionsenters_[i].a_ == b && collisionsenters_[i].b_ == a )){
                            return true;
                        }
                    }
                    return false;
                }
                private void AreTheyStillColliding() {
                    for (int i = 0; i < collisionsenters_.Count; i++) {
                        if (Collide(collisionsenters_[i].a_, collisionsenters_[i].b_)) {

                        } else {
                            events_.Push(new CollisionEvent(collisionsenters_[i].a_, collisionsenters_[i].b_, CollisionType.CollisionExit));
                            collisionsenters_.RemoveAt(i);
                            i--;
                        }
                    }
                }

                public bool Collide(BoundingForm a, BoundingForm b) {
                    switch (b.type_) {

                        case BoundingType.OBB:
                            if (a.Collide((OBB)b)) {
                                return true;
                            }
                            break;

                        case BoundingType.Bounding_Sphere:
                            if (a.Collide((BoundingSphere)b)) {
                                return true;
                            }
                            break;
                    }
                    return false;
                }

                public CollisionEvent Pop(){
                    if(events_.Count>0){
                        return events_.Pop();
                    }
                    return null;
                }

            // Datas

                // On tient à jour une liste de collisionEnter pour vérifier que ces éléments
                // sont toujours en collision. S'ils ne le sont pas, on lève un événement de type COllisionExit
                public List<CollisionEvent> collisionsenters_;
                public Stack<CollisionEvent> events_;
                public List<BoundingForm> boundingforms_;
    }
}
