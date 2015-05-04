using System;
using System.Collections.Generic;
using SharpDX;

namespace Troll3D{

    /// <summary>
    /// Cette classe sert à retourner le résultat d'un Raycast. Contient, s'il y en a, l'entity touché par le rayon,
    /// ainsi que le point d'intersection
    /// </summary>
    public class RaycastResult
    {
        public RaycastResult()
        {
            m_Entity = null;
        }

        public RaycastResult(Entity entity, Vector3 point)
        {
            m_Entity = entity;
            m_IntersectionPoint = point;
        }

        /// <summary>
        /// Retourne la première entité touché par le rayon envoyé par la méthode TRaycast.Fire().
        /// Null si aucune entité n'a été en contact avec le rayon
        /// </summary>
        public Entity GetEntity()
        {
            return m_Entity;
        }

        /// <summary>
        /// Retourne le point d'intersection le plus proche entre le rayon et les différents éléments de la scène
        /// </summary>
        /// <returns></returns>
        public Vector3 GetIntersectionPoint()
        {
            return m_IntersectionPoint;

        }

        private Entity   m_Entity;
        private Vector3  m_IntersectionPoint;

    }
}
