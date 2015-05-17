using System;
using System.Collections.Generic;
using SharpDX;


namespace Troll3D.Components.Lighting
{
    public class DirectionalLight : Light
    {
        public override void Attach( Entity entity )
        {
            base.Attach( entity );
            Entity = entity;
        }

        public DirectionalLight()
        {
            SetIntensity( 0.1f );
            SetSpecularIntensity( 6.0f );
            SetRange( 1.0f );
            SetAmbiantColor( new Vector4( 0.2f, 0.2f, 0.2f, 1.0f ) );
            SetColor( new Vector4( 1.0f, 1.0f, 1.0f, 1.0f ) );
            SetAngle( 0.5f );
            SetType( LightType.DIRECTIONNAL_LIGHT );
        }

        public void AddProjection( Projection projection )
        {
        //    Entity son = new Entity( Entity );
        //    MeshRenderer mr = son.AddComponent<MeshRenderer>();
        //    mr.material_ = new MaterialDX11();
        //    mr.model_ = ProjectionMesh.GetModel( projection );
        //    mr.SetFillMode( SharpDX.Direct3D11.FillMode.Wireframe );
            SetProjection( projection );
            m_View = new View( Entity.transform_, projection );
        }

        public Entity Entity;
    }
}
