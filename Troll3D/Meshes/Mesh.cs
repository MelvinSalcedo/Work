using System;
using System.Collections.Generic;
using SharpDX.DXGI;
using SharpDX;
using D3D11 = SharpDX.Direct3D11;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;

namespace Troll3D
{

    public enum MeshType
    {
        TRIANGLE_MESH,
        LINE_MESH
    }

    /// <summary>Le modèle stocke les informations du mesh sous la forme Face-Vertex. C'est à dire qu'on tient à jour plusieurs liste.
    /// Une première liste contenant tout les sommets du maillage. Une deuxième liste contenant une face. Une face étant
    /// défini comme une liste d'indice pointant vers les sommets du maillage. Et une troisième liste qui contient les indices des 
    /// faces lié à un sommet
    /// </summary>
    public class Mesh
    {

        public Mesh( VertexTypeD11 type = VertexTypeD11.STANDARD_VERTEX )
        {
            m_VertexType = type;
            m_type = MeshType.TRIANGLE_MESH;
        }

        public MeshType GetType()
        {
            return m_type;
        }

        /// <summary> Retourne la taille en octet des sommets </summary>
        public int VerticesSize()
        {
            return AbstractVertex.Size( m_VertexType ) * Vertices.Count;
        }

        /// <summary> Retourne la taille en octet d'un des sommets</summary>
        public int VertexSize()
        {
            return AbstractVertex.Size( m_VertexType );
        }

        public VertexTypeD11 GetVertexType()
        {
            return m_VertexType;
        }

        public void AddVertex( AbstractVertex vertex )
        {
            Vertices.Add( vertex );
        }

        public void AddFace( int index1, int index2, int index3 )
        {
            Faces.Add( new Face( index1, index2, index3 ) );
            int faceIndex = Faces.Count - 1;
            try
            {
                Vertices[index1].AddFace( faceIndex );
                Vertices[index2].AddFace( faceIndex );
                Vertices[index3].AddFace( faceIndex );
            }
            catch
            {
                Console.WriteLine( "Erreur lors de l'ajout d'une face " );
            }
            
        }

        public void UpdateMesh()
        {
            CreateVertices();
            CreateTriangles();
        }

        public virtual void Render()
        {
            ApplicationDX11.Instance.DeviceContext.InputAssembler.PrimitiveTopology = PrimitiveTopology.TriangleList;

            ApplicationDX11.Instance.DeviceContext.InputAssembler.SetVertexBuffers(
                0,
                new VertexBufferBinding( m_Vertexbuffer, VertexSize(),
                0 ) );

            ApplicationDX11.Instance.DeviceContext.InputAssembler.SetIndexBuffer(
                m_Indexbuffer,
                SharpDX.DXGI.Format.R32_UInt,
                0 );

            ApplicationDX11.Instance.DrawIndexed( Faces.Count * 3, 0, 0 );
        }

        protected void CreateVertices()
        {
            if ( m_VertexType != null )
            {
                SharpDX.Utilities.Dispose<SharpDX.Direct3D11.Buffer>( ref m_Vertexbuffer );
            }

            m_Vertexbuffer = SharpDX.Direct3D11.Buffer.Create( ApplicationDX11.Instance.Device,
                BindFlags.VertexBuffer,
                GetVerticesArray()
                );
        }

        private void CreateTriangles()
        {
            if ( m_VertexType != null )
            {
                SharpDX.Utilities.Dispose<SharpDX.Direct3D11.Buffer>( ref m_Indexbuffer );
            }

            m_Indexbuffer = SharpDX.Direct3D11.Buffer.Create(
                ApplicationDX11.Instance.Device,
                BindFlags.IndexBuffer,
                GetFaceIndexesArray() );
        }

        /// <summary> Retourne un tableau d'entier contenant les indices des faces utilisable par directX</summary>
        private int[] GetFaceIndexesArray()
        {
            int[] indexes = new int[Faces.Count * 3];

            int count = 0;
            for ( int i = 0; i < Faces.Count; i++ )
            {
                for ( int j = 0; j < Faces[i].Indexes.Length; j++ )
                {
                    indexes[count] = Faces[i].Indexes[j];
                    count++;
                }
            }
            return indexes;
        }

        /// <summary>Retourne un tableau d'octet contenant les informations de sommets interpretable par DirectX,
        /// en adéquation avec le type de sommet préalablement établi lors de la création du mesh
        /// </summary>
        protected byte[] GetVerticesArray()
        {
            byte[] bytes = new byte[VerticesSize()];
            for ( int i = 0; i < Vertices.Count; i++ )
            {
                Vertices[i].Datas().CopyTo( bytes, i * VertexSize() );
            }
            return bytes;
        }

        public List<AbstractVertex> Vertices = new List<AbstractVertex>();
        public List<Face> Faces = new List<Face>();

        protected MeshType m_type;

        private VertexTypeD11 m_VertexType;
        protected D3D11.Buffer m_Vertexbuffer;
        protected D3D11.Buffer m_Indexbuffer;
    }
}
