using System;
using System.Collections.Generic;
using SharpDX;
using System.Runtime.InteropServices;

namespace Troll3D.Components.Lighting
{
    public enum LightType
    {
        POINT_LIGHT = 0,
        DIRECTIONNAL_LIGHT = 1,
        SPOTLIGHT = 2
    }

    /// <summary>Objet de base pour définir les différents types de lumières pouvant être utilisé par l'application </summary>
    public abstract class Light : TComponent
    {
        public Light()
        {
            LightManager.Instance.AddLight( this );
            IsActive = true;
            Type = ComponentType.Light;

            // Je rajoute un petit mesh pour pouvoir visualiser la position de la lumière

            SetLightMaterial( 
                new Vector4( 1.0f, 1.0f, 1.0f, 1.0f ),
                new Vector4( 0.0f, 0.0f, 0.0f, 1.0f ) 
                );
        }

        public override void Attach( Entity entity )
        {
            m_transform = entity.transform_;
        }

        public void Dispose() { }

        public override void Update()
        {
            SetTransformation( m_transform.GetViewMatrix() );
            SetInverse( Matrix.Invert( m_transform.GetViewMatrix() ) );
        }

        public void SetLightMaterial( Vector4 diffusecolor, Vector4 ambiantcolor )
        {
            Description.AmbiantColor = ambiantcolor;
            Description.LightColor = diffusecolor;
        }

        public void AddShadowMap( int width, int height )
        {
            shadowmap_ = new StencilManager( width, height, true );
            Description.IsCastingShadows = true;
            Description.ShadowmapHeight = height;
            Description.ShadowmapWidth = width;
        }

        public void SetAngle( float angle )
        {
            Description.Angle = angle;
        }

        public void SetType( LightType type )
        {
            Description.Type = ( int )type;
        }

        public void SetRange( float range )
        {
            Description.Range = range;
        }

        /// <summary>
        /// L'intensité doit varié entre 0 et 1
        /// </summary>
        public void SetIntensity( float intensity )
        {
            Description.Intensity = intensity;
        }


        public void SetColor( Vector4 diffuseColor )
        {
            Description.LightColor = diffuseColor;
        }

        public void SetAmbiantColor( Vector4 ambiantColor )
        {
            Description.AmbiantColor = ambiantColor;
        }

        public void SetProjection( Projection projection )
        {
            Description.Projection = projection.Data;
        }

        public void SetTransformation( Matrix transformation )
        {
            Description.Transformation = transformation;
        }

        public void SetInverse( Matrix inverse )
        {
            Description.Inverse = inverse;
        }

        public void SetSpecularIntensity( float specularIntensity )
        {
            Description.SpecularIntensity = specularIntensity;
        }

        public void SetCastingShadow( bool value )
        {
            Description.IsCastingShadows = true;
        }

        public void SetShadowmapWidth( float width )
        {
            Description.ShadowmapWidth = width;
        }

        public void SetShadowmapHeight( float height )
        {
            Description.ShadowmapHeight = height;
        }

        public void SetCurrentView()
        {
            View.Current = m_View;
        }

        public Matrix GetProjection()
        {
            return Description.Projection;
        }

        public Matrix GetTransformation()
        {
            return Description.Transformation;
        }

        public float GetAngle()
        {
            return Description.Angle;
        }

        public bool IsCastingShadow()
        {
            return Description.IsCastingShadows;
        }

        public LightType GetLightType()
        {
            return ( LightType )Description.Type;
        }

        public float GetRange()
        {
            return Description.Range;
        }

        public float GetIntensity()
        {
            return Description.Intensity;
        }

        public Vector4 GetColor()
        {
            return Description.LightColor;
        }

        public Vector4 GetAmbiantColor()
        {
            return Description.AmbiantColor;
        }


        public float GetSpecularIntensity()
        {
            return Description.SpecularIntensity;
        }

        public Transform m_transform;
        public StencilManager shadowmap_;

        public bool IsActive;
        public LightDesc Description;
        public View m_View;
    }
}
