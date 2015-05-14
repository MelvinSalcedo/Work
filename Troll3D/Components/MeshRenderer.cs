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
            RasterizerStateDescription rasterDescription = ApplicationDX11.Instance.DeviceContext.Rasterizer.State.Description;
            rasterDescription.FillMode = mode;
            State = new RasterizerState( ApplicationDX11.Instance.Device, rasterDescription );
        }

        public MeshRenderer( MaterialDX11 material, Mesh model )
        {
            material_   = material;
            model_      = model;
            Type        = ComponentType.MeshRenderer;

            mode        = FillMode.Solid;

            RasterizerStateDescription rasterDescription = ApplicationDX11.Instance.DeviceContext.Rasterizer.State.Description;
            rasterDescription.FillMode = mode;
            State = new RasterizerState( ApplicationDX11.Instance.Device, rasterDescription );
        }

        public override void Attach( Entity entity )
        {
            Transform = entity.transform_;
            Scene.CurrentScene.Renderables.Add( this );
        }


        public void SetFillMode( FillMode fillmode )
        {
            mode = fillmode;
            RasterizerStateDescription rasterDescription = ApplicationDX11.Instance.DeviceContext.Rasterizer.State.Description;
            rasterDescription.FillMode = mode;
            State = new RasterizerState( ApplicationDX11.Instance.Device, rasterDescription );
        }

        public override void Update() { }

        public void Render()
        {
            ApplicationDX11.Instance.DeviceContext.Rasterizer.State = State;

            Transform.Update();
            // On envoie les informations de transformation
            Transform.SendConstantBuffer();
            // On envoie les informations portant sur la lumière
            LightManager.Instance.SendLights();

            // On envoie les informations de material / effet
            if ( material_ != null )
            {
                material_.Begin();
                if ( model_ != null )
                {
                    // On affiche le modèle
                    model_.Render();
                }

                material_.End();
            }
            
        }

        public Transform Transform{get;set;}

        FillMode mode;
        public RasterizerState State;
        public RasterizerState rasterstate_;
        public MaterialDX11 material_;
        public Mesh model_;


    }
}
