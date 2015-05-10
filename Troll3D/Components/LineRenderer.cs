using System;
using System.Collections.Generic;

using SharpDX;
using SharpDX.Direct3D11;
using SharpDX.Direct3D;
using SharpDX.DXGI;
using D3D11 = SharpDX.Direct3D11;

namespace Troll3D.Components
{
    /// <summary>
    /// Affiche une série de ligne
    /// </summary>
    public class LineRenderer : TComponent, IRenderable
    {
        public LineRenderer()
        {
            Type = ComponentType.LineRenderer;

            mode = FillMode.Solid;
            RasterizerStateDescription rasterDescription = ApplicationDX11.Instance.devicecontext_.Rasterizer.State.Description;
            rasterDescription.FillMode = mode;
            State = new RasterizerState( ApplicationDX11.Instance.device_, rasterDescription );
        }

        public override void Update(){}

        public override void Attach( Entity entity )
        {
            m_transform = entity.transform_;
            Scene.CurrentScene.Renderables.Add( this );   
        }

        public void Render()
        {
            if ( Display )
            {
                ApplicationDX11.Instance.devicecontext_.InputAssembler.PrimitiveTopology = PrimitiveTopology.LineList;

                ApplicationDX11.Instance.devicecontext_.Rasterizer.State = State;

                m_transform.SendConstantBuffer();
                LightManager.Instance.SendLights();

                material_.Begin();

                ApplicationDX11.Instance.devicecontext_.InputAssembler.SetVertexBuffers(
                    0,
                    new VertexBufferBinding( m_Vertexbuffer, VertexSize(),
                    0 ) );

                //ApplicationDX11.Instance.devicecontext_.InputAssembler.SetIndexBuffer(
                //    m_IndexBuffer,
                //    SharpDX.DXGI.Format.R32_UInt,
                //    0 );
                ApplicationDX11.Instance.devicecontext_.InputAssembler.PrimitiveTopology = PrimitiveTopology.LineList;

                ApplicationDX11.Instance.devicecontext_.Draw( Vertices.Count, 0 );

                //ApplicationDX11.Instance.DrawIndexed( LineIndexes.Count,0, 0);

                material_.End();
            }
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

        public void UpdateRenderer()
        {
            CreateVertices();
            //CreateLinesIndex();
        }

        protected void CreateVertices()
        {
            if ( m_Vertexbuffer != null )
            {
                SharpDX.Utilities.Dispose<SharpDX.Direct3D11.Buffer>( ref m_Vertexbuffer );
            }

            m_Vertexbuffer = SharpDX.Direct3D11.Buffer.Create( ApplicationDX11.Instance.device_,
                BindFlags.VertexBuffer,
                GetVerticesArray()
                );
        }

        protected void CreateLinesIndex()
        {
            if ( m_IndexBuffer != null )
            {
                SharpDX.Utilities.Dispose<SharpDX.Direct3D11.Buffer>( ref m_IndexBuffer );
            }

            m_IndexBuffer = SharpDX.Direct3D11.Buffer.Create( ApplicationDX11.Instance.device_,
                BindFlags.IndexBuffer,
                LineIndexes.ToArray() );
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

        public List<StandardVertex>     Vertices = new List<StandardVertex>();
        public List<int>                LineIndexes = new List<int>();

        private VertexTypeD11 m_VertexType;
        protected D3D11.Buffer m_Vertexbuffer;
        protected D3D11.Buffer m_IndexBuffer;

        Transform m_transform;
        FillMode mode;
        RasterizerState State;
        public RasterizerState rasterstate_;
        public MaterialDX11 material_;
        public Mesh model_;
        public bool Display=true;
    }
}
