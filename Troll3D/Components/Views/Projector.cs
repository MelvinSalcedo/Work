using System;
using System.Collections.Generic;
using SharpDX;
using SharpDX.Direct3D11;

namespace Troll3D
{

    public class Projector : Entity
    {

        public Projector( ShaderResourceView srv, Projection projection, Entity parent = null )
            : base( parent )
        {
            m_View = new View( transform_, projection );
            m_SRV = srv;
            ProjectorManager.Instance.AddProjector( this );
            IsActive = true;
        }

        public void SetFrustum( float fieldOfView, float ratio, float near, float far )
        {
            m_View.SetFrustsumProjection( fieldOfView, ratio, near, far );
        }

        public FrustumProjection GetFrustum()
        {
            return m_View.GetFrustumProjection();
        }

        public void UpdateMatrix()
        {
            projectorDesc.Projection = m_View.projection_.Data;
            projectorDesc.Transformation = transform_.GetViewMatrix();
            projectorDesc.Inverse = Matrix.Invert( transform_.GetViewMatrix() );
        }

        public ProjectorDesc projectorDesc;
        public bool IsActive;
        public View m_View;
        public ShaderResourceView m_SRV;


    }
}
