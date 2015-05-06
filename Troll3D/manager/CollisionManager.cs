using System;
using System.Collections.Generic;
using SharpDX;

using Troll3D.Components.Collisions;
using Troll3D.Components;

namespace Troll3D 
{
    public class CollisionManager 
    {
        public static CollisionManager Instance;

        /// <summary>
        /// Calcule si les boites a et b rentrent en collision et retourne un booléen en fonction
        /// </summary>
        public static bool AABBtoAABB(AABB a, AABB b)
        {
            return(
                    a.X + a.Width   > b.X               &&
                    a.X             < b.X + b.Width     &&
                    a.Y + a.Height  > b.Y               &&
                    a.Y             < b.Y + b.Height    &&
                    a.Z + a.Depth   > b.Z               &&
                    a.Z             < b.Z + b.Depth );
        }

        public static bool OBBtoOBB(OBB a, OBB b) 
        {
            return SAT.Intersect3D(a.GetShape(), b.GetShape());
        }

        public static bool BoundingSphereToBoundingSphere(SphereCollider a, SphereCollider b) 
        {
            //if ((a.transform_.WorldPosition() - b.transform_.WorldPosition()).Length() <= (a.Radius + b.Radius)) 
            //{
            //    return true;
            //}
            return false;
        }

        public static bool OBBtoBoundingSphere(OBB b, SphereCollider s) 
        {
          //  return SAT.Intersect3DShapeShere(b, s.transform.WorldPosition(), s.Radius);
            return false;
        }


        public CollisionManager() 
        {
            // Le CollisionManager charge aussi le matérial utilisé par défault par les bouding forms
            material_ = new MaterialDX11("vDefault.cso", "pUnlit.cso", "gDefault.cso");
            material_.AddConstantBuffer<Vector4>(new Vector4(0.0f, 0.0f, 1.0f, 1.0f));
                    
            Instance            = this;
            events_             = new Stack<CollisionEvent>();
            m_colliders         = new List<Collider>();
            collisionsenters_   = new List<CollisionEvent>();
        }

        // Vérifie que les boites englobantes se recoupe, crée des objets 
        // Collision event 
        public void UpdateCollisions() 
        {
            if (m_colliders.Count > 1) 
            {
                for (int i = collisionsenters_.Count-1 ; i >=0 ; i--) 
                {
                    if ( collisionsenters_[i].a_.IsActive == false || collisionsenters_[i].b_.IsActive == false ) 
                    {
                        collisionsenters_.Remove( collisionsenters_[i] );
                    }
                }

                for (int i = 0; i < m_colliders.Count; i++) 
                {
                    for (int j = i + 1; j < m_colliders.Count; j++) 
                    {
                        if ( m_colliders[i].IsActive && m_colliders[j].IsActive ) 
                        {
                            // Teste entre layers histoire de vérifier qu'on doit bien tester cette collision
                            if (LayerManager.Instance.AreColliding(m_colliders[i].layer_, m_colliders[j].layer_)) 
                            {
                                // Si il y a une collision, je vérifie si la collision a déjà eu lieu, de manière à determiner si je retourne
                                // un collisionENter Event ou un COllision Event
                                if (Collide(m_colliders[i], m_colliders[j])) 
                                {
                                    if (!DidTheyAlreadyCollide(m_colliders[i], m_colliders[j])) 
                                    {
                                        events_.Push(new CollisionEvent(m_colliders[i], m_colliders[j], CollisionType.CollisionEnter));
                                        collisionsenters_.Add(events_.Peek());
                                    } 
                                    else 
                                    {
                                        events_.Push(new CollisionEvent(m_colliders[i], m_colliders[j], CollisionType.Colliding));
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

        /// <summary>
        /// Si deux éléments sont déjà rentré en collision, on appelle OnCollisionStay au lieu de OnCollisionEnter
        /// </summary>
        private bool DidTheyAlreadyCollide(Collider a, Collider b) 
        {
            for (int i = 0; i < collisionsenters_.Count; i++) 
            {
                if((collisionsenters_[i].a_ == a && collisionsenters_[i].b_ == b ) || 
                (collisionsenters_[i].a_ == b && collisionsenters_[i].b_ == a ))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Si deux objets était en collision, on rajoute une étape pour vérifier si ils sont toujours
        /// en collision. Dans le cas contraire, on appelle OnCollisionExit
        /// </summary>
        private void AreTheyStillColliding() 
        {
            for (int i = 0; i < collisionsenters_.Count; i++) 
            {
                if (!Collide(collisionsenters_[i].a_, collisionsenters_[i].b_)) 
                {
                    events_.Push( new CollisionEvent( collisionsenters_[i].a_, collisionsenters_[i].b_, CollisionType.CollisionExit ) );
                    collisionsenters_.RemoveAt( i );
                    i--;
                }
            }
        }

        /// <summary>
        ///  Vérifie si les colliders a et b rentrent en collision
        /// </summary>
        public bool Collide( Collider a, Collider b ) 
        {
            switch (b.ColliderType) 
            {
                case BoundingType.OBB:
                    if (a.Collide((OBB)b)) 
                    {
                        return true;
                    }
                break;

                case BoundingType.Bounding_Sphere:
                    if (a.Collide((SphereCollider)b)) 
                    {
                        return true;
                    }
                break;
            }
            return false;
        }

        /// <summary>
        /// Récupère le premier événement de Collision de la pile
        /// </summary>
        public CollisionEvent Pop()
        {
            if(events_.Count>0)
            {
                return events_.Pop();
            }
            return null;
        }

        /// <summary>
        /// Distribue les événements de collisions aux méthodes OnCollisionEnter, OnCollisionExit et 
        /// OnCollisionStay du composant Behaviour
        /// </summary>
        public void DispatchCollisions()
        {
            CollisionEvent e;

            while ( (e = Pop())!=null )
            {
                for ( int i = 0; i < Troll3D.Components.Behaviour.Behaviours.Count; i++ )
                {
                    Troll3D.Components.Behaviour b = Troll3D.Components.Behaviour.Behaviours[i];

                    switch ( e.type_ )
                    {
                        case CollisionType.CollisionEnter:
                            b.OnCollisionEnter( e );
                        break;

                        case CollisionType.CollisionExit:
                            b.OnCollisionExit( e );
                        break;

                        case CollisionType.Colliding:
                        b.OnCollisionStay( e );
                        break;
                    }
                }
            }
            
        }

        public MaterialDX11 material_;

        // On tient à jour une liste de collisionEnter pour vérifier que ces éléments
        // sont toujours en collision. S'ils ne le sont pas, on lève un événement de type COllisionExit
        public List<CollisionEvent> collisionsenters_;
        public Stack<CollisionEvent> events_;
        public List<Collider>   m_colliders;
    }
}
