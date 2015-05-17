using System;
using System.Collections.Generic;
using SharpDX;
using SharpDX.Direct3D11;

using Troll3D.Components;

namespace Troll3D
{
    /// <summary>
    /// Représente un projecteur, se charge de "projeter" une image en suivant la projection (frustum) qui lui
    /// est associé
    /// </summary>
    public class Projector : TComponent
    {
        public Projector()
        {
            Type = ComponentType.Projector;
            ProjectorManager.Instance.AddProjector( this );
            IsActive = true;
        }

        public override void Update()
        {
            
        }

        public override void Attach( Entity entity )
        {
            Entity = entity;
        }

        public void SetImage( ShaderResourceView srv )
        {
            m_SRV = srv;
        }

        public void SetProjection( Projection projection )
        {
            Projection = projection;
            m_View = new View( Entity.transform_, projection );
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
            projectorDesc.Transformation = Entity.transform_.GetViewMatrix();
            projectorDesc.Inverse = Matrix.Invert( Entity.transform_.GetViewMatrix() );
        }

        public Projection Projection;

        public ProjectorDesc projectorDesc;
        public bool IsActive;
        public View m_View;
        public ShaderResourceView m_SRV;
        public Entity Entity;


    }
}
