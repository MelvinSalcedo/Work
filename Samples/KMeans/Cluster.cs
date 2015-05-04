using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Troll3D;
using Troll3D.Components;

using SharpDX;

namespace KMeans
{
    /// <summary>
    /// Représente un Cluster. Enregistre une liste d'élément appartement au cluster (groupe),
    /// ainsi que la couleur du cluster
    /// </summary>
    public class Cluster 
    {
        public Cluster(int id, Vector4 color, Vector3 centroid)
        {
            Id = id;
            Color = color;
            Centroid = centroid;
        }

        public void AddEntity( Entity entity )
        {
            MeshRenderer meshrenderer = (MeshRenderer)entity.GetComponent( ComponentType.MeshRenderer );
            meshrenderer.material_.SetMainColor( Color );
        }

        /// <summary>
        /// Moyenne de l'heuristique du cluster
        /// </summary>
        public float Mean;

        public Vector3 Centroid;

        public List<Entity> Entities = new List<Entity>();
        public Vector4 Color;
        public int Id;
    }
}
