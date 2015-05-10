using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;

using Troll3D;

namespace DelaunayTriangularisation.WingedEdge
{
    /// <summary>
    /// Baumgart 1975
    /// Pour opengl, les triangles doivent être défini en Counter Clock Wise (sens trigo donc)
    /// Structure Winged Edge utilisé pour représenter un maillage
    /// L'idée générale étant qu'on utilise principale les arrête pour enregistrer les
    /// informations de connexité. Une arrête enregistre les 2 faces qu'elle sépare, ainsi que 
    /// l'arrête précédente et suivante pour chacune des faces (si elles existent)
    /// </summary>
    public class WingedEdgeMesh
    {
        public WingedEdgeMesh() { }

        /// <summary>
        /// Retourne un maillage interprétable par mon moteur de rendu
        /// </summary>
        public Mesh MakeMesh()
        {
            Mesh mesh = new Mesh( VertexTypeD11.STANDARD_VERTEX );

            for ( int i = 0; i < Vertices.Count; i++ )
            {
                mesh.Vertices.Add( new StandardVertex( Vertices[i].Position, new Vector3(0.0f,0.0f,1.0f), new Vector2(0.0f,0.0f) ) );
            }

            for ( int i = 0; i < Faces.Count; i++ )
            {
                //if (! IsFaceBorder( Faces[i] ) )
                //{
                    List<VertexWE> v = GetFaceVertices( Faces[i] );
                    mesh.AddFace( v[0].Id, v[1].Id, v[2].Id );
                //}
            }
            if ( mesh.Faces.Count == 0 )
            {
                return null;
            }
            mesh.UpdateMesh();
            return mesh;
        }

        /// <summary>
        /// Crée et retourne un nouveau sommet
        /// </summary>
        public VertexWE AddVertex( float x, float y, float z )
        {
            VertexWE v = new VertexWE( x, y, z );
            v.Id = Vertices.Count;
            Vertices.Add( v );
            return v;
        }

        /// <summary>
        /// Retourne une liste de face voisines à la face passé en paramètre
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        public List<FaceWE> GetFaceNeighbours( FaceWE f )
        {
            List<FaceWE> faces = new List<FaceWE>();

            for ( int i = 0; i < f.Edges.Count; i++ )
            {
                if ( f.Edges[i].LeftFace != null && f.Edges[i].LeftFace != f )
                {
                    if (! faces.Contains( f.Edges[i].LeftFace ) )
                    {
                        faces.Add( f.Edges[i].LeftFace );
                    }
                }
                if ( f.Edges[i].RightFace != null && f.Edges[i].RightFace != f )
                {
                    if ( !faces.Contains( f.Edges[i].RightFace ) )
                    {
                        faces.Add( f.Edges[i].RightFace );
                    }
                }
            }

            return faces;
        }


        /// <summary>
        /// Ajoute une nouvelle face à la structure à partir des sommets spécifiés
        /// </summary>
        public FaceWE AddFace( VertexWE v1, VertexWE v2, VertexWE v3 )
        {
            // On commence par vérifier si la face n'a pas déjà été crée
            if ( FaceAlreadyExist( v1, v2, v3 ) ==null )
            {
                // On vérifie si les sommets sont dans le bon ordre 

                if ( !IsTriangleClockwise( v1, v2, v3 ) )
                {
                    // Si ce n'est pas le cas, j'inverse v2 et v3 pour avoir un triangle clockwise

                    VertexWE temp = v2;
                    v2 = v3;
                    v3 = temp;
                }

                // On crée un nouvelle face

                FaceWE f = new FaceWE();

                EdgeWE e1 = EdgeAlreadyExist( v1, v2 ) ;
                EdgeWE e2 = EdgeAlreadyExist( v2, v3 );
                EdgeWE e3 = EdgeAlreadyExist( v3, v1 );

                // On commence par crée les arrêtes si elles n'existent pas
                if ( e1 == null )
                {
                    e1 = new EdgeWE( v1, v2 );
                    Edges.Add( e1 );
                    e1.LeftFace = f;
                }
                else
                {
                    if ( e1.LeftFace == null )
                    {
                        e1.LeftFace = f;
                    }
                    else
                    {
                        e1.RightFace= f;
                    }
                }

                if ( e2 == null )
                {
                    e2 = new EdgeWE( v2, v3 );
                    Edges.Add( e2 );
                    e2.LeftFace = f;
                }
                else
                {
                    if ( e2.LeftFace == null )
                    {
                        e2.LeftFace = f;
                    }
                    else
                    {
                        e2.RightFace = f;
                    }
                }

                if ( e3 == null )
                {
                    e3 = new EdgeWE( v3, v1 );
                    e3.LeftFace = f; 
                    Edges.Add( e3 );
                }
                else
                {
                    if ( e3.LeftFace == null )
                    {
                        e3.LeftFace = f;
                    }
                    else
                    {
                        e3.RightFace = f;
                    }
                }

                // On se charge ensuite de connecter les arrêtes entre elles

                //if ( e1.RightFace == null )
                //{
                //    e1.NextLeft = e2;
                //    e1.PreviousLeft = e3; 
                //}
                //else
                //{
                //    e1.NextRight    = e2;
                //    e1.PreviousRight= e3;
                //}

                //if ( e2.RightFace == null )
                //{
                //    e2.NextLeft     = e3;
                //    e2.PreviousLeft = e1; 
                //}
                //else
                //{
                //    e2.NextRight        = e3;
                //    e2.PreviousRight    = e1;
                //}

                //if ( e3.RightFace == null )
                //{
                //    e3.NextLeft     = e1;
                //    e3.PreviousLeft = e2;
                //}
                //else
                //{
                //    e3.NextRight        = e1;
                //    e3.PreviousRight    = e2;
                //}
               
                f.Edges.Add( e1 );
                f.Edges.Add( e2 );
                f.Edges.Add( e3 );

                Faces.Add( f );
                return f;
            }
            return null;
        }

        /// <summary>
        /// Supprime le sommet, ainsi que les arrêtes et faces auquel il était connecté
        /// </summary>
        /// <param name="v"></param>
        public void RemoveVertex( VertexWE v )
        {

        }

        /// <summary>
        /// Supprime l'arrête, et les faces auquelle elle était rattaché
        /// </summary>
        /// <param name="e"></param>
        public void RemoveEdge( EdgeWE e )
        {

        }

        /// <summary>
        /// Vérifie si une des arrêtes de la face est un "bord", c'est à dire que l'arrête
        /// n'a qu'une seule face d'enregistrer
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        public bool IsFaceBorder( FaceWE f )
        {
            foreach ( EdgeWE e in f.Edges )
            {
                if ( e.LeftFace == null || e.RightFace == null )
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Cette méthode ce charge de rajouter un sommet à l'intérieur du triangle f,
        /// et divise donc la face en 3 nouvelles faces
        /// </summary>
        public List<FaceWE> AddVertex( FaceWE f, float x, float y, float z )
        {
            List<FaceWE> faces = new List<FaceWE>();
            List<VertexWE> vertices = GetFaceVertices(f);
            RemoveFace( f );
            VertexWE v= AddVertex( x, y, z );
            faces.Add( AddFace( vertices[0], v, vertices[1] ) );
            faces.Add( AddFace( vertices[1], v, vertices[2] ) );
            faces.Add( AddFace( vertices[2], v, vertices[0] ) );
            return faces;
        }

        /// <summary>
        /// Cette opération consiste à "flip" une edge, utilisé dans la triangulation de deulaunay
        /// L'opération de flip consiste à détruire 2 triangles reliés par une arrête, et construire
        /// 2 nouveau triangle en utilisant les 2 sommets non utilisé comme arrête de séparation
        ///   /|\          /\
        ///  / | \   -->  /__\
        ///  \ | /        \  /
        ///   \|/          \/
        /// </summary>
        public List<FaceWE> FlipEdge( EdgeWE e )
        {
            List<FaceWE> newfaces = new List<FaceWE>();

            FaceWE f1 = e.LeftFace;
            FaceWE f2 = e.RightFace;

            // On enregistre les 2 sommets de l'arrêtes
            VertexWE v1 = e.Vertex1;
            VertexWE v2 = e.Vertex2;

            // On cherche les deux sommets inconnus qui serviront pour créer l'arrête commune
            //entre les 2 faces

            VertexWE v3 = null;
            VertexWE v4 = null;

            List<VertexWE> f1vertices = GetFaceVertices(f1);
            List<VertexWE> f2vertices = GetFaceVertices(f2);

            for(int i=0; i< f1vertices.Count; i++)
            {
                if(f1vertices[i]!= v1 && f1vertices[i]!=v2)
                {
                    v3 = f1vertices[i];
                }
            }

            for(int i=0; i< f2vertices.Count; i++)
            {
                if(f2vertices[i]!= v1 && f2vertices[i]!=v2)
                {
                    v4 = f2vertices[i];
                }
            }

            RemoveFace( f1 );
            RemoveFace( f2 );

            newfaces.Add(AddFace(v3,v4,v1));
            newfaces.Add(AddFace(v3,v4,v2));

            return newfaces;
        }

        /// <summary>
        /// Supprime la face
        /// </summary>
        /// <param name="f"></param>
        public void RemoveFace( FaceWE f )
        {
            foreach ( EdgeWE e in f.Edges )
            {
                if ( e.RightFace == f )
                {
                    e.RightFace = null;
                }

                if ( e.LeftFace == f )
                {
                    e.LeftFace = null;
                }

                if ( e.LeftFace == null && e.RightFace == null )
                {
                    Edges.Remove( e );
                    e.Vertex1.Edges.Remove( e );
                    e.Vertex2.Edges.Remove( e );
                }
            }

            Faces.Remove( f );
        }

        /// <summary>
        /// Retourne une liste de sommets correspondant aux sommets de la face
        /// les sommets sont retournés dans le sens trigo
        /// </summary>
        public List<VertexWE> GetFaceVertices( FaceWE f )
        {
            List<VertexWE> vertices = new List<VertexWE>() ;
            EdgeWE firstEdge = f.Edges[0];

            for ( int i = 0; i < f.Edges.Count; i++ )
            {
                VertexWE v1 = f.Edges[i].Vertex1;
                VertexWE v2 = f.Edges[i].Vertex2;

                if ( f.Edges[i].LeftFace == f )
                {
                    v1 = f.Edges[i].Vertex2;
                    v2 = f.Edges[i].Vertex1;
                }

                if ( !vertices.Contains( v1 ) )
                {
                    vertices.Add( v1 );
                }
                if ( !vertices.Contains( v2 ) )
                {
                    vertices.Add( v2 );
                }
            }

            return vertices;
            
        }

        /// <summary>
        /// Vérifie si une arrête reliant v1 et v2 n'a pas déjà été inséré
        /// Si tel est le cas, l'arrête est retourné, autrement, on retourne null
        /// </summary>
        private EdgeWE EdgeAlreadyExist( VertexWE v1, VertexWE v2 )
        {
            foreach ( EdgeWE edge in Edges )
            {
                if((edge.Vertex1 == v1 && edge.Vertex2 == v2 )||(edge.Vertex1==v2 && edge.Vertex2==v1))
                {
                    return edge;
                }
            }
            return null;
        }

        /// <summary>
        /// Vérifie si une face n'a pas déjà été ajoutée
        /// </summary>
        private FaceWE FaceAlreadyExist( VertexWE v1, VertexWE v2, VertexWE v3 )
        {
            foreach ( FaceWE face in Faces )
            {
                bool v1founded=false;
                bool v2founded=false;
                bool v3founded=false;

                foreach ( EdgeWE edge in face.Edges )
                {
                    if ( edge.Vertex1 == v1 || edge.Vertex2==v1)
                    {
                        v1founded = true;
                    }
                    if ( edge.Vertex1 == v2 || edge.Vertex2==v2)
                    {
                        v2founded = true;
                    }
                    if(edge.Vertex1 == v3 || edge.Vertex2==v3 ){
                        v3founded=true;
                    }
                }
                if ( v1founded && v2founded && v3founded )
                {
                    return face;
                }
            }
            return null;
        }

        /// <summary>
        /// Vérifie que le triangle est bien dans l'ordre anti trigo
        /// Pour se faire, on calcule le vecteur perpendiculaire au plan défini par 2 vecteurs du triangle
        /// Puis, on calcule son angle. Si l'angle est positif, alors le triangle est anti-trigo,
        /// autrement, il est trigo
        /// Bon, actuellement, je fais ça de manière un peu moche en 2D. 
        /// ToDo : Voir généralisation en 3D? (rapport avec le vecteur entre le triangle et la caméra
        /// </summary>
        private bool IsTriangleClockwise( VertexWE a, VertexWE b, VertexWE c )
        {
            Vector3 AB = b.Position - a.Position;
            Vector3 AC = c.Position - a.Position;

            Vector3 crossvalue = Vector3.Cross( AB, AC );
            if ( crossvalue.Z > 0.0f )
            {
                return true;
            }
            return false;
        }

        public List<EdgeWE>     Edges   = new List<EdgeWE>();
        public List<VertexWE>   Vertices= new List<VertexWE>();
        public List<FaceWE>     Faces   = new List<FaceWE>();
    }
}
