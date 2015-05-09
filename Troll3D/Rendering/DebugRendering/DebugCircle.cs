using System;
using System.Collections.Generic;

using SharpDX;
using D3D11=SharpDX.Direct3D11;
using SharpDX.DXGI;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;

namespace Troll3D.Rendering.DebugRendering
{
    /// <summary>
    /// Représente un cercle 
    /// </summary>
    public class DebugCircle : DebugPrimitive
    {
        /// <summary>
        /// Construit un nouvel objet de type DebugCircle
        /// </summary>
        public DebugCircle( Vector3 p, float r, Vector3 c )
        {
            Position    = p;
            Radius      = r;
            Color       = c;

            int discretisation = 10;

            for ( int i = 0; i < discretisation; i++ )
            {
                float angleVal = 2.0f * 3.141592f * ( float )i / ( float )discretisation;

                Vertices.Add( 
                    new StandardVertex(
                        new Vector3(
                                        p.X + ( float )Math.Cos( angleVal ),
                                        0.0f,
                                        p.Y + ( float )Math.Sin( angleVal )

                        ) ) );
            }

            CreateVertices();
            CreateLines();
        }

        public void Render( MaterialDX11 mat )
        {
           // ApplicationDX11.Instance.devicecontext_.Rasterizer.State = State;

            //m_transform.SendConstantBuffer();
            LightManager.Instance.SendLights();

            mat.Begin();

            mat.End();

            ApplicationDX11.Instance.devicecontext_.InputAssembler.PrimitiveTopology = PrimitiveTopology.LineList;

            ApplicationDX11.Instance.devicecontext_.InputAssembler.SetVertexBuffers(
                0,
                new VertexBufferBinding( m_Vertexbuffer, VertexSize(),
                0 ) );

            ApplicationDX11.Instance.devicecontext_.InputAssembler.SetIndexBuffer(
                m_Indexbuffer,
                SharpDX.DXGI.Format.R32_UInt,
                0 );

            ApplicationDX11.Instance.DrawIndexed( Lines.Count * 2, 0, 0 );
            
        }

        protected void CreateVertices()
        {
            if ( m_VertexType != null )
            {
                SharpDX.Utilities.Dispose<SharpDX.Direct3D11.Buffer>( ref m_Vertexbuffer );
            }

            m_Vertexbuffer = SharpDX.Direct3D11.Buffer.Create( ApplicationDX11.Instance.device_,
                BindFlags.VertexBuffer,
                GetVerticesArray()
                );
        }

        private void CreateLines()
        {
            if ( m_VertexType != null )
            {
                SharpDX.Utilities.Dispose<SharpDX.Direct3D11.Buffer>( ref m_Indexbuffer );
            }

            m_Indexbuffer = SharpDX.Direct3D11.Buffer.Create(
                ApplicationDX11.Instance.device_,
                BindFlags.IndexBuffer,
                Lines.ToArray());
        }



        /// <summary>
        /// Retourne un tableau d'octet contenant les informations de sommets interpretable par DirectX,
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

        /// <summary> 
        /// Retourne la taille en octet des sommets 
        /// </summary>
        public int VerticesSize()
        {
            return AbstractVertex.Size( m_VertexType ) * Vertices.Count;
        }

        /// <summary> 
        /// Retourne la taille en octet d'un des sommets
        /// </summary>
        public int VertexSize()
        {
            return AbstractVertex.Size( m_VertexType );
        }

        private VertexTypeD11   m_VertexType;
        protected D3D11.Buffer  m_Vertexbuffer;
        protected D3D11.Buffer  m_Indexbuffer;

        public List<StandardVertex>     Vertices = new List<StandardVertex>();
        public List<int>                Lines    = new List<int>();
        public Vector3  Position    { get; private set; }
        public float    Radius      { get; private set; }
        public Vector3 Color        { get; private set; }
    }
}
