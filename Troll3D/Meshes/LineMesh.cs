using System;
using System.Collections.Generic;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using SharpDX;
using D3D11 = SharpDX.Direct3D11;
using SharpDX.Direct3D;

namespace Troll3D
{

    public class LineMesh : Mesh
    {

        public LineMesh( VertexTypeD11 type = VertexTypeD11.STANDARD_VERTEX )
            : base( type )
        {
            indexes = new List<int>();
            m_type = MeshType.LINE_MESH;
        }

        public void AddLine( Line line )
        {
            Vertices.Add( new StandardVertex( line.GetA() ) );
            Vertices.Add( new StandardVertex( line.GetB() ) );
            indexes.Add( Vertices.Count - 2 );
            indexes.Add( Vertices.Count - 1 );
        }
        public void AddVertex( StandardVertex v )
        {
            Vertices.Add( v );
            indexes.Add( Vertices.Count - 1 );
        }
        public void UpdateBuffers()
        {
            if ( m_Vertexbuffer != null )
            {
                SharpDX.Utilities.Dispose<SharpDX.Direct3D11.Buffer>( ref m_Vertexbuffer );
            }

            m_Vertexbuffer = SharpDX.Direct3D11.Buffer.Create( ApplicationDX11.Instance.device_,
                BindFlags.VertexBuffer,
                GetVerticesArray()
                );

            if ( m_Indexbuffer != null )
            {
                SharpDX.Utilities.Dispose<SharpDX.Direct3D11.Buffer>( ref m_Indexbuffer );
            }

            m_Indexbuffer = SharpDX.Direct3D11.Buffer.Create( ApplicationDX11.Instance.device_,
                BindFlags.IndexBuffer,
                indexes.ToArray() );
        }
        public override void Render()
        {
            ApplicationDX11.Instance.devicecontext_.InputAssembler.PrimitiveTopology = PrimitiveTopology.LineList;
            ApplicationDX11.Instance.devicecontext_.InputAssembler.SetVertexBuffers(
                0,
                new VertexBufferBinding( m_Vertexbuffer, VertexSize(),
                0 ) );

            ApplicationDX11.Instance.devicecontext_.InputAssembler.SetIndexBuffer(
                m_Indexbuffer,
                SharpDX.DXGI.Format.R32_UInt,
                0 );


            ApplicationDX11.Instance.devicecontext_.Draw( Vertices.Count, 0 );
        }

        public List<int> indexes;
    }
}
