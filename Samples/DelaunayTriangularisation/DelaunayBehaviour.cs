using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Troll3D.Components;
using Troll3D;
using SharpDX;

using DelaunayTriangularisation.WingedEdge;

namespace DelaunayTriangularisation
{
    public class DelaunayBehaviour : Behaviour
    {
        public MeshRenderer meshRenderer;

        public void CleanVoronoi()
        {
            // On commence par "effacer" les derniers points
            for ( int i = 0; i < VoronoiPoints.Count; i++ )
            {
                Scene.CurrentScene.RemoveRenderable( ( MeshRenderer )VoronoiPoints[i].GetComponent( ComponentType.MeshRenderer ) );
            }
            VoronoiPoints.Clear();
            ( ( LineRenderer )VoronoiLines.GetComponent( ComponentType.LineRenderer ) ).Display = false;
        }

        /// <summary>
        /// Construit une représentation graphique du Diagramme de Voronoi de la triangulation
        /// </summary>
        public void BuildVoronoi(WingedEdgeMesh mesh)
        {
             List<StandardVertex> lines = new List<StandardVertex>();

            // On va commencer par construire les sommets correspondant aux centre des faces de la triangulation
            // qui sont les noeuds du diagramme de voronoir
             CleanVoronoi();
            
            for ( int i = 0; i < mesh.Faces.Count; i++ )
            {
                // On récupère le centre de la face (barycentre)

                List<VertexWE> vertices = mesh.GetFaceVertices( mesh.Faces[i] );

                Vector3 center = vertices[0].Position + vertices[1].Position + vertices[2].Position;
                center = center / 3.0f;

                Troll3D.Entity entity = new Troll3D.Entity( Entity );
                entity.transform_.SetPosition(
                    center.X,
                    center.Y,
                    0.0f );

                entity.transform_.SetScale( 0.02f, 0.02f, 1.0f );

                MaterialDX11 material = new MaterialDX11();
                material.SetMainColor( 0.0f, 1.0f, 1.0f, 1.0f );
                MeshRenderer meshrenderer = entity.AddComponent<MeshRenderer>();
                meshrenderer.material_ = material;
                meshrenderer.model_ = Quad.GetMesh();

                // On récupère les voisins

                List<FaceWE> neighbours = mesh.GetFaceNeighbours( mesh.Faces[i] );

                for ( int j = 0; j < neighbours.Count; j++ )
                {
                    // On récupère le centre de la face (barycentre)

                    List<VertexWE> verticesNeighbour = mesh.GetFaceVertices( neighbours[j] );

                    Vector3 centerNeighbour = verticesNeighbour[0].Position + verticesNeighbour[1].Position + verticesNeighbour[2].Position;
                    centerNeighbour = centerNeighbour / 3.0f;

                    lines.Add( new StandardVertex( center ) );
                    lines.Add( new StandardVertex( centerNeighbour ) );
                }

                VoronoiPoints.Add( entity );
            }

            if ( lines.Count > 0 )
            {
                ( ( LineRenderer )VoronoiLines.GetComponent( ComponentType.LineRenderer ) ).Display = true;
                ( ( LineRenderer )VoronoiLines.GetComponent( ComponentType.LineRenderer ) ).Vertices = lines;
                ( ( LineRenderer )VoronoiLines.GetComponent( ComponentType.LineRenderer ) ).UpdateRenderer();
            }
            
        }

        public List<Entity> VoronoiPoints = new List<Entity>();
        public Entity VoronoiLines;

        public override void Initialize()
        {
            VoronoiLines = new Entity();
            LineRenderer lr =  VoronoiLines.AddComponent<LineRenderer>();
            lr.material_ = new MaterialDX11("vDefault.cso","pUnlit.cso","gDefaultLine.cso");
            lr.material_.SetMainColor( 0.0f, 0.0f, 1.0f, 1.0f );

            meshRenderer = Entity.AddComponent<MeshRenderer>();
            meshRenderer.material_ = new MaterialDX11();
        }

        /// <summary>
        /// Cette méthode se charge de vérifier si le point P se trouve à l'intérieur du triangle ABC
        /// Pour se faire, on calcule le determinant d'une matrice et on vérifie si le résultat
        /// est supérieur à 0 ou non
        /// ABC doit être un triangle dans l'odre horaire (anti trigo)
        /// </summary>
        /// <returns></returns>
        public bool IsPointInTriangle( Vector3 P, Vector3 A, Vector3 B, Vector3 C )
        {
            float [] matvalues = new float[16]
            {
                A.X, A.Y, (float)(Math.Pow(A.X,2)+Math.Pow(A.Y,2)),1,
                B.X, B.Y, (float)(Math.Pow(B.X,2)+Math.Pow(B.Y,2)),1,
                C.X, C.Y, (float)(Math.Pow(C.X,2)+Math.Pow(C.Y,2)),1,
                P.X, P.Y, (float)(Math.Pow(P.X,2)+Math.Pow(P.Y,2)),1
            };
            return ((new Matrix(matvalues)).Determinant()>0);
        }

        /// <summary>
        /// Pour trouver le centre du cercle circonscrit au triangle,
        /// il faut trouver le point d'intersection des 3 médiatrices du triangle
        /// </summary>
        /// <returns></returns>
        public Vector3 GetCircleCenter( Vector3 A, Vector3 B, Vector3 C )
        {
            Vector3 AB = ( B - A );
            Vector3 BC = ( C - B );
            Vector3 CA = ( A - C );

            // Je récupère un produit vectoriel que je pourai ensuite utiliser pour
            // trouver les médiatrices
            Vector3 cross = Vector3.Cross(AB,BC);
            cross.Normalize();

            // Sommet d'ou part la médiatrice
            Vector3 M = A + (AB / 2.0f);
            Vector3 U = Vector3.Cross( AB, cross );
            U.Normalize();

            Vector3 N = B + (BC / 2.0f);
            Vector3 V = Vector3.Cross( BC, cross );
            V.Normalize();

            Vector3 result = new Vector3() ;
            MathStuffs.SegmentIntersect( M, M + U, N, N + V, ref result );

            // Et maintenant on cherche le point d'intersection entre ces 2 segments

            // Pour se faire, on va résoudre l'équation à 2 inconnues utilisant l'équation
            //// paramétriques des 2 droites

            //float s = M.Y + ( ( N.X * U.Y ) / U.X ) - ( ( M.X * U.Y )/ U.X  ) - N.Y;
            //float denominateur = U.Y  - (( V.X * U.Y ) / U.X);
            //s = s / denominateur;

            //float x = N.X + s * V.X;
            //float y = N.Y + s * V.Y;

            return new Vector3( result.X, result.Y, 0.0f );
        }

        /// <summary>
        /// Retourne le rayon d'un cercle circonscrit à un triangle de centre P
        /// </summary>
        /// <returns></returns>
        public float GetCircleRadius( Vector3 A, Vector3 B, Vector3 C, Vector3 P )
        {
            float maxradius = -1.0f;

            if ( ( P - A ).Length() > maxradius)
            {
                maxradius = ( P - A ).Length();
            }

            if ( ( P - B ).Length() > maxradius )
            {
                maxradius = ( P - B ).Length();
            }

            if ( ( P - C).Length() > maxradius )
            {
                maxradius = ( P - C ).Length();
            }
            return maxradius;

        }
        
        /// <summary>
        /// Retourne le rayon d'un cercle circonscrit à un triangle
        /// AB / 2*Sin(
        /// </summary>
        /// <returns></returns>
        public float GetCircleRadius( Vector3 A, Vector3 B, Vector3 C )
        {
            Vector3 AB = ( B - A );
            Vector3 BC = ( C - B );
            Vector3 CA = ( A - C );

            float bclength = BC.Length();
            BC.Normalize();
            AB.Normalize();

            float dotResult = Vector3.Dot( BC, -AB );

            float angleB    = ( float )Math.Acos( Vector3.Dot( BC, -AB ) );

            float value     = bclength / ( 2.0f * ( float )Math.Sin( angleB ) );

            return value;
        }

        public bool IsPointInCircle( Vector3 point, Vector3 circle, float radius )
        {
            return ( ( circle - point ).Length() < (radius-0.01f) );
        }

        public List<Entity> CircleCenter = new List<Entity>();

        /// <summary>
        /// Inspecte une edge et détermine si un basculement est nécéssaire
        /// Si un basculement a eu lieu, réinvoque la fonction
        /// </summary>
        /// <param name="e"></param>
        private void InspectEdge(List<EdgeWE> edges, WingedEdgeMesh mesh)
        {
            foreach(EdgeWE e in edges)
            {
                // On vérifie d'abord que l'arrête relie 2 triangles
                if ( e.LeftFace != null && e.RightFace != null )
                {
                    List<VertexWE> v1 = mesh.GetFaceVertices(e.LeftFace);
                    List<VertexWE> v2 = mesh.GetFaceVertices(e.RightFace);

                    // Récupère le coté opposé à la face gauche
                    VertexWE leftOppositeVertex = null;

                    for(int i=0; i< v1.Count; i++)
                    {
                        if( (v1[i] != e.Vertex1) && (v1[i]!=e.Vertex2))
                        {
                            leftOppositeVertex = v1[i];
                        }
                    }

                    // Récupère le coté opposé à la face droite
                    VertexWE rightOppositeVertex = null;

                    for(int i=0; i< v2.Count; i++)
                    {
                        if( (v2[i] != e.Vertex1) && (v2[i]!=e.Vertex2))
                        {
                            rightOppositeVertex = v1[i];
                        }
                    }

                    // On récupère les informations sur les cercle Right et Left Face

                    Vector3 leftCircleCenter = GetCircleCenter( v1[0].Position, v1[1].Position, v1[2].Position );
                    float leftCircleRadius   = GetCircleRadius( v1[0].Position, v1[1].Position, v1[2].Position, leftCircleCenter );

                    Vector3 rightCircleCenter   = GetCircleCenter( v2[0].Position, v2[1].Position, v2[2].Position );
                    float rightCircleRadius     = GetCircleRadius( v2[0].Position, v2[1].Position, v2[2].Position, rightCircleCenter );

                    // On vérifie si un des coté opposé ne rentre pas dans le cercle de la face opposé

                    if ( IsPointInCircle( leftOppositeVertex.Position, rightCircleCenter, rightCircleRadius ) || IsPointInCircle( rightOppositeVertex.Position, leftCircleCenter, leftCircleRadius) )
                    {

                        // On bascule l'arrête
                        List<FaceWE> newfaces = mesh.FlipEdge( e );

                        // On inspecte les nouvelles arrêtes

                        List<EdgeWE> newEdges = new List<EdgeWE>();

                        for(int i=0; i< newfaces.Count; i++)
                        {
                            for(int j=0; j< newfaces[i].Edges.Count; j++)
                            {
                                if(newEdges.Contains(newfaces[i].Edges[j]))
                                {
                                    newEdges.Add(newfaces[i].Edges[j]);
                                }
                            } 
                        }
                        InspectEdge(newEdges,mesh);
                        return;
                    }
                }
            }
            return ;
        }

        /// <summary>
        /// Implémentation de l'algorithme de triangulation de Delaunay incremental 
        /// </summary>
        public void DelaunayIncremental()
        {
            List<Entity> p = new List<Entity>();
            List<Entity> currentPoints = new List<Entity>();

            p.AddRange( Points );

            WingedEdgeMesh mesh = new WingedEdgeMesh();

            // On commence par créer un big triangle capable de contenir tout les autres

            mesh.AddVertex(-2.0f,-1.0f, 0.0f);
            mesh.AddVertex(-2.0f, 5.0f, 0.0f);
            mesh.AddVertex(2.0f, -1.0f, 0.0f);

            mesh.AddFace( mesh.Vertices[0], mesh.Vertices[1], mesh.Vertices[2] );

            // Maintenant, on va trianguler point par point
            // On récupère un point de l'ensemble de point, on cherche le triangle qui le contient,
            // puis, on subdivise en 3 triangles. On vérifie ensuite que chacun des triangles crée est
            // Delaunay, si ce n'est pas le cas, on flip l'arrête "opposé" au sommet rajouté
            while ( p.Count > 0 )
            {
                // On commence par extraire le point

                Entity point = p[0];
                p.RemoveAt(0);

                FaceWE f=null;
                int findex = 0;
                // On cherche le triangle qui contient le point
                for(int i=0; i< mesh.Faces.Count; i++)
                {
                    FaceWE currentFace = mesh.Faces[i];
                    List<VertexWE> vertices = mesh.GetFaceVertices(currentFace);

                    bool isIn = Troll3D.TRaycast.PointInTriangle( vertices[0].Position, vertices[1].Position, vertices[2].Position, point.transform_.position_ );

                    if ( isIn )
                    {
                        f=currentFace;
                        findex = i;
                    }
                }

                // En théorie, on a récupéré le triangle qui contient le point, on va désormais diviser
                // ce triangle en 3 en utilisant le point actuel

                List<FaceWE> faces = mesh.AddVertex( f, point.transform_.position_.X, point.transform_.position_.Y, point.transform_.position_.Z );
                
                // Maintenant, on doit vérifier que les triangles obtenus sont delaunay, c'est à dire
                // que le cercle circonscrit au triangle ne contienne que des points du triangle
                
                // On inspecte les faces crées
                for ( int i = 0; i < faces.Count; i++ )
                {   
                    InspectEdge(faces[i].Edges,mesh);
                }
            }

            // Je retire les parties du maillage qui sont utilisé pour construire la triangulation

            List<FaceWE> faceToRemove = new List<FaceWE>();

            for ( int i = mesh.Faces.Count - 1; i >= 0; i-- )
            {
                if ( mesh.IsFaceBorder( mesh.Faces[i] ) )
                {
                    faceToRemove.Add( mesh.Faces[i] );
                }
            }

            for ( int i = 0; i < faceToRemove.Count; i++ )
            {
                mesh.RemoveFace( faceToRemove[i]);
            }

                for ( int i = 0; i < CircleCenter.Count; i++ )
                {
                    Scene.CurrentScene.RemoveRenderable( ( MeshRenderer )CircleCenter[i].GetComponent( ComponentType.MeshRenderer ) );
                }

                if ( DisplayCenters )
                {
                    for ( int i = 0; i < Circles.Count; i++ )
                    {
                        Scene.CurrentScene.RemoveRenderable( ( LineRenderer )Circles[i].GetComponent( ComponentType.LineRenderer ) );
                    }

                    CircleCenter.Clear();

                    for ( int i = 0; i < mesh.Faces.Count; i++ )
                    {
                        List<VertexWE> vertices = mesh.GetFaceVertices( mesh.Faces[i] );

                        Vector3 circleCenter = GetCircleCenter( vertices[0].Position, vertices[1].Position, vertices[2].Position );
                        float radius = GetCircleRadius( vertices[0].Position, vertices[1].Position, vertices[2].Position, circleCenter );

                        AddCircleCenter( circleCenter.X, circleCenter.Y, radius );
                    }
                }
                else
                {
                    for ( int i = 0; i < Circles.Count; i++ )
                    {
                        Scene.CurrentScene.RemoveRenderable( ( LineRenderer )Circles[i].GetComponent( ComponentType.LineRenderer ) );
                    }
                    CircleCenter.Clear();
                }

            if ( mesh.MakeMesh() != null )
            {
                meshRenderer.model_ = mesh.MakeMesh();
                meshRenderer.SetFillMode( SharpDX.Direct3D11.FillMode.Wireframe );
                meshRenderer.material_.SetMainColor( 1.0f, 0.0f, 0.0f, 1.0f );
            }
            else
            {
            }

            if ( DisplayVoronoi )
            {
                BuildVoronoi( mesh );
            }
            else
            {
                CleanVoronoi();
            }
        }


        public Entity pointDragged = null;
        public bool IsDragging = false;

        public override void OnMouseMove( MouseEvent e )
        {
            if ( IsDragging )
            {
                pointDragged.transform_.position_ = new Vector3( e.mouse_.x / ( float )Screen.Instance.Width * 2 - 1.0f,
                    1.0f - e.mouse_.y / ( float )Screen.Instance.Height * 2, 0.0f );
                DelaunayIncremental();
            }
        }

        public override void OnMouseUp( MouseEvent e )
        {
           
            if ( IsDragging )
            {
                IsDragging = false;
            }
        }

        public override void OnMouseDown( MouseEvent e )
        {
            if ( e.mouse_.leftbutton )
            {
                bool founded = false;
                for ( int i = 0; i < Points.Count && founded==false; i++ )
                {
                    if((Points[i].transform_.position_ - new Vector3(e.mouse_.x / ( float )Screen.Instance.Width * 2 - 1.0f,
                    1.0f - e.mouse_.y / ( float )Screen.Instance.Height * 2, 0.0f )).Length() < 0.01f )
                    {
                        IsDragging = true;
                        pointDragged = Points[i];
                        founded = true;

                    }
                }
            }

            if ( e.mouse_.rightbutton )
            {
                AddPoint( e.mouse_.x / ( float )Screen.Instance.Width * 2 - 1.0f,
                    1.0f - e.mouse_.y / ( float )Screen.Instance.Height * 2 );

                DelaunayIncremental();
            }
        }

        public override void OnKeyDown( KeyboardEvent e )
        {
            if ( e.keycode_ == KeyCode.Key_1 )
            {
                DisplayCenters = !DisplayCenters;
                DelaunayIncremental();
            }
            if ( e.keycode_ == KeyCode.Key_2 )
            {
                DisplayVoronoi = !DisplayVoronoi;
                DelaunayIncremental();
            }
        }
        public void AddCircleCenter( float x, float y, float radius )
        {
            Troll3D.Entity entityCircle = new Troll3D.Entity( Entity );
            entityCircle.transform_.RotateEuler( 0.0f, 3.1415f / 2.0f, 0.0f );
            entityCircle.transform_.Translate( x, y, 0.0f );
            entityCircle.transform_.SetScale( radius, 1.0f, radius );
            LineRenderer lineRenderer = entityCircle.AddComponent<LineRenderer>();


            lineRenderer.material_ = new MaterialDX11( "vDefault.cso", "pUnlit.cso", "gDefaultLine.cso" );
            lineRenderer.material_.SetMainColor( 0.0f, 1.0f, 0.0f, 1.0f );

            lineRenderer.Vertices = Circle.GetLines( 30 );
            lineRenderer.UpdateRenderer();

            Circles.Add( entityCircle );

            Troll3D.Entity entity = new Troll3D.Entity( Entity );

            entity.transform_.SetPosition(
                x,
                y, 
                0.0f );

            entity.transform_.SetScale( 0.02f, 0.02f, 1.0f );

            MaterialDX11 material = new MaterialDX11();
            material.SetMainColor( 0.0f, 1.0f, 0.0f, 1.0f );
            MeshRenderer meshrenderer = entity.AddComponent<MeshRenderer>();
            meshrenderer.material_ = material;
            meshrenderer.model_ = Quad.GetMesh();

            CircleCenter.Add( entity );
        }

        public void AddPoint( float x, float y)
        {
            Troll3D.Entity entity = new Troll3D.Entity( Entity );

            entity.transform_.SetPosition(
                x,
                y,
                0.0f );

            entity.transform_.SetScale( 0.02f, 0.02f, 1.0f );

            MaterialDX11 material = new MaterialDX11();
            material.SetMainColor( 1.0f, 0.0f, 1.0f, 1.0f );
            MeshRenderer meshrenderer = entity.AddComponent<MeshRenderer>();
            meshrenderer.material_ = material;
            meshrenderer.model_ = Quad.GetMesh();

            Points.Add( entity );
        }

        /// <summary>
        /// Contient les points 
        /// </summary>
        public List<Entity> Points = new List<Entity>();
        public List<Entity> Circles = new List<Entity>();

        /// <summary>
        /// Nombre de points à triangulariser
        /// </summary>
        public int PointCount{get;private set;}
        public bool m_start = false;

        public bool DisplayVoronoi=false;
        public bool DisplayCenters = false;
    }
}
