using System;
using System.Collections.Generic;
using SharpDX;
using SharpDX.DXGI;
using SharpDX.Direct3D11;
using D3D11 = SharpDX.Direct3D11;
using SharpDX.Direct3D;

namespace Troll3D{

    /// <summary>
    /// Un pointMesh est un "mesh" qui utilise la primitive de type "Point". Sera principalement utilisé
    /// lorsque l'on souhaite utilisé le geometry shader (pour les billboard par exemple)
    /// </summary>
    public class PointMesh : Mesh{

        // Public

            // Lifecycle

                public PointMesh(VertexTypeD11 vertextype = VertexTypeD11.STANDARD_VERTEX) : base(vertextype){}

            // Methods

                public void AddPoint(StandardVertex sv){
                    Vertices.Add(sv);
                }

                public void UpdateBuffers()
                {
                    if (m_Vertexbuffer != null)
                    {
                        SharpDX.Utilities.Dispose<SharpDX.Direct3D11.Buffer>(ref m_Vertexbuffer);
                    }

                    m_Vertexbuffer = SharpDX.Direct3D11.Buffer.Create(ApplicationDX11.Instance.device_,
                        BindFlags.VertexBuffer,
                        GetVerticesArray()
                        );

                }

                public override void Render(){
                    ApplicationDX11.Instance.devicecontext_.InputAssembler.PrimitiveTopology = PrimitiveTopology.PointList;
                    ApplicationDX11.Instance.devicecontext_.InputAssembler.SetVertexBuffers(
                        0,
                        new VertexBufferBinding(m_Vertexbuffer, VertexSize(),
                        0));

                    ApplicationDX11.Instance.devicecontext_.Draw(Vertices.Count, 0);
                }
    }
}
