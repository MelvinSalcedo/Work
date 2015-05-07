using System;
using System.Collections.Generic;
using SharpDX;
using SharpDX.Direct3D11;
using SharpDX.Direct3D;
using SharpDX.DXGI;

namespace Troll3D.Components
{
    /// <summary> 
    /// La classe ModelRenderer se charge de combiner un Material
    /// et un maillage en vue de l'affichage 
    /// </summary>
    public class MeshRenderer : TComponent, IRenderable
    {
        public MeshRenderer() 
        {
            Type = ComponentType.MeshRenderer;

            mode = FillMode.Solid;
            RasterizerStateDescription rasterDescription = ApplicationDX11.Instance.devicecontext_.Rasterizer.State.Description;
            rasterDescription.FillMode = mode;
            State = new RasterizerState( ApplicationDX11.Instance.device_, rasterDescription );
        }

        public MeshRenderer( MaterialDX11 material, Mesh model )
        {
            material_   = material;
            model_      = model;
            Type        = ComponentType.MeshRenderer;

            mode        = FillMode.Solid;

            RasterizerStateDescription rasterDescription = ApplicationDX11.Instance.devicecontext_.Rasterizer.State.Description;
            rasterDescription.FillMode = mode;
            State = new RasterizerState( ApplicationDX11.Instance.device_, rasterDescription );
        }

        public override void Attach( Entity entity )
        {
            m_transform = entity.transform_;
            Scene.CurrentScene.Renderables.Add( this );
        }

        public void SetFillMode( FillMode fillmode )
        {
            mode = fillmode;
            RasterizerStateDescription rasterDescription = ApplicationDX11.Instance.devicecontext_.Rasterizer.State.Description;
            rasterDescription.FillMode = mode;
            State = new RasterizerState( ApplicationDX11.Instance.device_, rasterDescription );
        }

        public override void Update() { }

        public void Render()
        {
            ApplicationDX11.Instance.devicecontext_.Rasterizer.State = State;

            m_transform.SendConstantBuffer();
            LightManager.Instance.SendLights();

            material_.Begin();
            if ( model_ != null )
            {
                model_.Render();
            }
            
            material_.End();
        }

        Transform m_transform;
        FillMode mode;
        RasterizerState State;
        public RasterizerState rasterstate_;
        public MaterialDX11 material_;
        public Mesh model_;


    }
}
