using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Troll3D.Components;
using Troll3D;
using SharpDX;

namespace KMeans
{
    /// <summary>
    /// L'algorithme des KMeans permet de partionner n élements d'un espace dans k clusters.
    /// L'algorithme se divise en deux étapes, la première, qui consiste à comparer chaque 
    /// sommet/point au centroid/barycentre des clusters, sélectionner le cluster le plus proche et ajouter
    /// le sommet/point au cluster.
    /// La deuxième étape consiste à mettre à jour le centroid en calculant la moyenne des éléments constituant
    /// le clusters
    /// On répète ces 2 étapes jusqu'à atteindre l'équilibre
    /// </summary>
    public class KmeanBehavior : Behaviour
    {
        public override void Initialize()
        {
            m_elements = 1000;
            m_clusters = 15;
            Populate( m_elements, 2, 2 );
        }

        public override void OnKeyDown( Troll3D.KeyboardEvent e )
        {
            if ( e.keycode_ == Troll3D.KeyCode.Key_S )
            {
                Start();
            }
        }

        public void Start()
        {
            Random rand = new Random();
            // On commence par sélectionner un "point" au hasard pour chaque cluster
            for ( int i = 0; i < m_clusters; i++ )
            {
                Clusters.Add(new Cluster( 
                    i,
                    new Vector4( 
                        rand.NextFloat(0.0f,0.8f),
                        rand.NextFloat(0.0f,0.8f),
                        rand.NextFloat(0.0f,0.8F),
                        1.0f),
                    new Vector3(rand.NextFloat(-1.0f,1.0f), rand.NextFloat(-1.0f,1.0f),0.0f)));

                Centroids.Add( new Entity(Entity) );
                MeshRenderer meshrenderer = Centroids[i].AddComponent<MeshRenderer>();
                meshrenderer.material_ = new MaterialDX11();
                meshrenderer.material_.SetMainColor( Clusters[i].Color );
                meshrenderer.model_ = Quad.GetMesh();
                Centroids[i].transform_.SetScale( 0.05f, 0.05f, 1.0f );

            }

            m_start = true;
        }

        public override void Update()
        {
            for ( int i = 0; i < Clusters.Count; i++ )
            {
                Centroids[i].transform_.SetPosition( Clusters[i].Centroid.X, Clusters[i].Centroid.Y, 0.0f );
            }

            bool equilibrium = true;

            if ( m_start )
            {
                // On cherche à assigner un Cluster à chacun des éléments
                for ( int i = 0; i < Elements.Count; i++ )
                {
                    float minval = 100000.0f;
                    int minindex = 0;
                    // On recherche le Cluster le plus proche de l'élément en cours d'analyse
                    for(int j=0; j< Clusters.Count; j++)
                    {
                        float distance = ( Elements[i].transform_.position_ - Clusters[j].Centroid ).Length();
                        if ( distance < minval )
                        {
                            minval = distance;
                            minindex = j;
                            
                        }    
                    }
                    if ( ClusterId[i] == -1 ||  ClusterId[i] != minindex)
                    {
                        if ( ClusterId[i] != -1 )
                        {
                            Clusters[ClusterId[i]].Entities.Remove( Elements[i] );
                        }

                        equilibrium = false;
                        ClusterId[i] = minindex;
                        Clusters[minindex].Entities.Add( Elements[i] );
                        
                        ( ( MeshRenderer )Elements[i].GetComponent( ComponentType.MeshRenderer ) ).material_.SetMainColor( Clusters[minindex
                            ].Color );
                    }

                    
                }

                // La deuxième étape consiste à recaluler les centroids des clusters

                for ( int i = 0; i < Clusters.Count; i++ )
                {
                    Vector3 sum = Vector3.Zero;

                    for ( int j = 0; j < Clusters[i].Entities.Count; j++ )
                    {
                        sum += Clusters[i].Entities[j].transform_.position_;
                    }

                    sum = sum / Clusters[i].Entities.Count;
                    Clusters[i].Centroid = sum;
                }

                // Si aucun changement n'a été enregistré, l'équilibre a été trouvé et le partionnement
                // de l'espace est donc terminé
                if ( equilibrium )
                {
                    m_start = false;
                }
            }
        }

        /// <summary>
        /// Crée les éléments à classer
        /// </summary>
        public void Populate(int n, int width, int height)
        {
            Random random = new Random();
            for ( int i = 0; i < n; i++ )
            {
                Troll3D.Entity entity = new Troll3D.Entity( Entity );

                entity.transform_.SetPosition( 
                    (float)random.NextDouble() * width - width/2.0f,
                    (float)random.NextDouble() * height - height/2.0f, 
                    0.0f );

                entity.transform_.SetScale( 0.02f, 0.02f, 1.0f );

                MaterialDX11 material = new MaterialDX11();
                material.SetMainColor( 0.0f, 0.0f, 0.0f, 1.0f );
                MeshRenderer meshrenderer = entity.AddComponent<MeshRenderer>();
                meshrenderer.material_ = material;
                meshrenderer.model_ = Quad.GetMesh();

                Elements.Add( entity );
                ClusterId.Add( -1 );
            }
            
        }

        public List< Cluster> Clusters = new List< Cluster>();

        /// <summary>
        /// Enregistre l'identifiant de cluster d'un élément
        /// </summary>
        public List<int> ClusterId = new List<int>();

        public List<Entity> Elements = new List<Entity>();

        /// <summary>
        /// Représente visuellement les centroids des Clusters
        /// </summary>
        public List<Entity> Centroids = new List<Entity>();

        /// <summary>
        /// Nombre d'élément à "segmenter"
        /// </summary>
        public int m_elements;

        /// <summary>
        /// Nombre de régions 
        /// </summary>
        public int m_clusters;
        public bool m_start=false;



       


    }
}
