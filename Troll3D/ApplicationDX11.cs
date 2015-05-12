using System.Linq;
using System.Text;
using SharpDX.Direct3D11;
using System;
using System.Collections.Generic;
using SharpDX.DXGI;
using SharpDX;
using System.Drawing;
using SharpDX.D3DCompiler;
using SharpDX.Direct3D;
using SharpDX.Windows;
using System.Windows.Forms;

using D3D11 = SharpDX.Direct3D11;

using Troll3D.Components;
using Troll3D.Rendering;

namespace Troll3D
{
    public class ApplicationDX11
    {
        public static ApplicationDX11 Instance;

        public bool Isglowing;
        public static bool DesactivatePixelShader = false;

        public Scene scene_;
        public StencilManager stencilmanager_;
        public StencilManager shadowmap;
        public ApplicationInformation m_ApplicationInformation;
        public ImageProcessing imageProcessing;
        public RenderTexture renderTex;

        /// <summary>
        /// Crée une application dans le handle défini 
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public ApplicationDX11( int width, int height, IntPtr handle )
        {
            new Screen( width, height );
            Instance = this;
            backgroundColor = new Color4( 0.0f, 0.2f, 0.7f, 1.0f );
            Initialize( handle );
        }

        /// <summary>
        /// Si aucun Handle n'est défini dans les paramètres, l'application crée automatiquement un renderForm
        /// </summary>
        /// <param name="width">Longueur de la fenêtre</param>
        /// <param name="height">Hauteur de la fenêtre</param>
        public ApplicationDX11( int width, int height )
        {
            new Screen( width, height );
            Instance = this;
            backgroundColor = new Color4( 0.0f, 0.2f, 0.7f, 1.0f );
            m_renderForm = new RenderForm();
            m_renderForm.ClientSize = new Size( width, height );
            Initialize( m_renderForm.Handle );
        }

        public void Dispose()
        {
            // General Direct3D 11 stuff
            m_SwapChainHelper.Dispose();
            Device.Dispose();
            DeviceContext.Dispose();
            rendertargetview_.Dispose();
        }


        public void DrawIndexed( int indexCount, int startIndexLocation, int baseVertexLocation )
        {
            DeviceContext.DrawIndexed( indexCount, startIndexLocation, baseVertexLocation );
            m_ApplicationInformation.AddDrawCall( 1 );
            m_ApplicationInformation.AddTriangles( indexCount / 3 );
        }

        public void Draw( int vertexCount, int startVertexLocation )
        {
            DeviceContext.Draw( vertexCount, startVertexLocation );
        }

        public void Run()
        {
            Run( m_renderForm );
        }

        public void Run( RenderForm renderform )
        {
            TimeHelper.Instance.Start();
            RenderLoop.Run( renderform, Update );
        }

        public void Resize( int width, int height )
        {
            m_SwapChainHelper.Resize( width, height );
            Screen.Instance.Resize( width, height );
            rendertargetview_ = m_SwapChainHelper.GetRenderTargetView();
            stencilmanager_ = new StencilManager( width, height, true );

            Camera.Main.m_StencilManager = new StencilManager( width, height, true );
            Camera.Main.SetViewport( 0, 0, width, height );

            renderTex.Dispose();
            renderTex = new RenderTexture( width, height );

            imageProcessing.Resize( width, height, renderTex.GetSRV() );
        }


        public void Update()
        {
            UpdateLogic();
            DrawScene();
        }

        private void UpdateLogic()
        {
            TimeHelper.Instance.Update();
            //m_ApplicationInformation.Update();

            CollisionManager.Instance.UpdateCollisions();
            CollisionManager.Instance.DispatchCollisions();
            InputManager.Instance.Update();
            scene_.Update();
        }

        public void SetViewport( Troll3D.Viewport viewport )
        {
            DeviceContext.Rasterizer.SetViewport( viewport.XOffset, viewport.YOffset, viewport.Width, viewport.Height );
        }

        public void SetViewport( float xoffset, float yoffset, float width, float height )
        {
            DeviceContext.Rasterizer.SetViewport( xoffset, yoffset, width, height );
        }

        private void DrawScene()
        {

            // UpdateRenderTexture();
            // renderTex.ClearRenderTargetView();

            //devicecontext_.ClearRenderTargetView(renderTex.GetRenderTargetView(), new Color4(0.5f, 0.5f, 0.5f, 1.0f));


            //devicecontext_.OutputMerger.SetRenderTargets( stencilmanager_.depthstencilview, rendertargetview_ );

            //devicecontext_.ClearRenderTargetView( rendertargetview_, new Color4( 0.0f, 0.0f, 1.0f, 1.0f ) );
            //stencilmanager_.Clear();

            for ( int i = 0; i < Camera.Cameras.Count; i++ )
            {
                if ( Camera.Cameras[i].IsActive )
                {
                    DrawFromCamera( Camera.Cameras[i], m_mainRenderTarget );
                }
            }


            //imageProcessing.UpdatePasses();

            //devicecontext_.OutputMerger.SetRenderTargets(stencilmanager_.depthstencilview, rendertargetview_);
            //devicecontext_.ClearRenderTargetView(rendertargetview_, new Color4(0.0f, 0.0f, 1.0f, 1.0f));
            //stencilmanager_.Clear();
            //SetViewport(imageProcessing.PostProcessingView.viewport);

            //imageProcessing.Draw();

            m_SwapChainHelper.Present( 0, PresentFlags.None );
        }

        public BlendState mainBlend;
        public BlendState RenderToTextureBlendState;
        public BlendState LastRenderToTextureBlendState;

        private void Initialize( IntPtr handle )
        {

            m_SwapChainHelper = new SwapChainHelper( handle );
            Device = m_SwapChainHelper.CreateDeviceWithSwapChain( DeviceCreationFlags.Debug );
            DeviceContext = Device.ImmediateContext;

            scene_ = new Scene( new Color4( 0.0f, 0.2f, 0.7f, 1.0f ) );

            // On récupère le RenderTargetView généré par notre swapchain représentant
            // le backBuffer

            m_mainRenderTarget = new RenderTarget(
                m_SwapChainHelper.GetRenderTargetView(),
                DeviceContext,
                Screen.Instance.Width,
                Screen.Instance.Height
            );

            new LightManager();
            new ProjectorManager();
            new CollisionManager();
            new TimeHelper();
            new LayerManager();

            // Initialisation de la transparence pour les RenderTexture
            RenderTargetBlendDescription renderdesc3 = new RenderTargetBlendDescription()
            {
                SourceAlphaBlend = BlendOption.One,
                DestinationAlphaBlend = BlendOption.Zero,
                AlphaBlendOperation = BlendOperation.Add,

                SourceBlend = BlendOption.SourceAlpha,
                DestinationBlend = BlendOption.Zero,
                BlendOperation = BlendOperation.Add,

                IsBlendEnabled = true,
                RenderTargetWriteMask = ColorWriteMaskFlags.All
            };

            BlendStateDescription blendStateDesc3 = new BlendStateDescription()
            {
                IndependentBlendEnable = false,        // DirectX peut utiliser 8 RenderTarget simultanément, chauqe renderTarget
                // peut être lié à un RenderTargetBlendDescription différent
                AlphaToCoverageEnable = false
            };

            blendStateDesc3.RenderTarget[0] = renderdesc3;

            LastRenderToTextureBlendState = new BlendState( Device, blendStateDesc3 );
            LastRenderToTextureBlendState = null;

            // Initialisation de la transparence pour les RenderTexture
            RenderTargetBlendDescription renderdesc = new RenderTargetBlendDescription()
            {

                SourceAlphaBlend = BlendOption.One,
                DestinationAlphaBlend = BlendOption.Zero,
                AlphaBlendOperation = BlendOperation.Add,

                SourceBlend = BlendOption.SourceAlpha,
                DestinationBlend = BlendOption.Zero,
                BlendOperation = BlendOperation.Add,

                IsBlendEnabled = true,
                RenderTargetWriteMask = ColorWriteMaskFlags.All
            };

            BlendStateDescription blendStateDesc2 = new BlendStateDescription()
            {
                IndependentBlendEnable = false,        // DirectX peut utiliser 8 RenderTarget simultanément, chauqe renderTarget
                // peut être lié à un RenderTargetBlendDescription différent
                AlphaToCoverageEnable = false
            };

            blendStateDesc2.RenderTarget[0] = renderdesc;


            RenderToTextureBlendState = new BlendState( Device, blendStateDesc2 );
            RenderToTextureBlendState = null;
            // Initialisation de la transparence
            RenderTargetBlendDescription desc = new RenderTargetBlendDescription()
            {
                AlphaBlendOperation = BlendOperation.Add,
                BlendOperation = BlendOperation.Add,


                SourceAlphaBlend = BlendOption.SourceAlpha,
                DestinationAlphaBlend = BlendOption.One,

                SourceBlend = BlendOption.SourceAlpha,
                DestinationBlend = BlendOption.InverseSourceAlpha,


                IsBlendEnabled = true,
                RenderTargetWriteMask = ColorWriteMaskFlags.All

            };

            BlendStateDescription blendStateDesc = new BlendStateDescription()
            {
                IndependentBlendEnable = false,        // DirectX peut utiliser 8 RenderTarget simultanément, chauqe renderTarget
                // peut être lié à un RenderTargetBlendDescription différent
                AlphaToCoverageEnable = false
            };

            blendStateDesc.RenderTarget[0] = desc;

            mainBlend = new BlendState( Device, blendStateDesc );

            DeviceContext.OutputMerger.BlendState = mainBlend;
            // Fin Transparence

            InitializeRasterState();

            new GlowingManager( Screen.Instance.Width, Screen.Instance.Height );
            m_ApplicationInformation = new ApplicationInformation();

            renderTex = new RenderTexture( Screen.Instance.Width, Screen.Instance.Height );
            imageProcessing = new ImageProcessing( Screen.Instance.Width, Screen.Instance.Height, renderTex.GetSRV() );

            new InputManager( m_renderForm );
        }

        private void DrawFromCamera( Camera camera, RenderTarget renderTarget )
        {
            GlowingPass(camera);
            ShadowMappingPass( camera );

            camera.SetCurrentView();
            SetViewport( camera.GetViewport() );

            renderTarget.Clear();
            renderTarget.Bind();

            // On vérifie si la camera possède une skybox

            if ( camera.Skybox != null )
            {
                // Dans ce cas, on affiche la skybox

                camera.Skybox.Render();
            }


            LightManager.Instance.Update( View.Current );

            if ( !turnOffSceneRendering )
            {
                //ProjectorManager.Instance.Bind();
                scene_.Render();
                //ProjectorManager.Instance.UnBind();
            }
            // if (Isglowing){
            // GlowingManager.Instance.DrawQuadOnTop();
            // }
        }

        /// <summary>
        ///  Met à jour les RenderTexture
        /// </summary>
        private void UpdateRenderTexture()
        {
            for ( int i = 0; i < Camera.Cameras.Count; i++ )
            {
                if ( Camera.Cameras[i].HasRenderTexture )
                {
                    //DrawFromCamera( Camera.Cameras[i], Camera.Cameras[i].m_RenderTexture.GetRenderTargetView() );
                }
            }
        }

        /// <summary>
        /// Initialise le raster state
        /// </summary>
        private void InitializeRasterState()
        {
            RasterizerState rasterstate = new RasterizerState( Device, new RasterizerStateDescription()
            {
                FillMode = FillMode.Solid,
                CullMode = CullMode.Back,
                IsFrontCounterClockwise = false,
                DepthBias = 0,
                DepthBiasClamp = 0,
                SlopeScaledDepthBias = 0,
                IsDepthClipEnabled = true,
                IsScissorEnabled = false,
                IsMultisampleEnabled = false,
                IsAntialiasedLineEnabled = false

            } );
            DeviceContext.Rasterizer.State = rasterstate;
        }


        private void GlowingPass( Camera camera )
        {
            // GlowingPass

            //if (Isglowing){
            //    camera.SetCurrentView();
            //    GlowingManager.Instance.GlowingPass(scene_);

            //}

            // EndGlowinPass
        }


        private void ShadowMappingPass( Camera camera )
        {
            // Shadow Pass

            ///DesactivatePixelShader = true;
            //for (int i = 0; i < LightManager.Instance.Count(); i++){

            //    Light light = LightManager.Instance.GetLight(i);
            //    if (light.IsActive)
            //    {
            //        if (light.IsCastingShadow()){

            //            light.SetCurrentView();
            //            //camera.SetCurrentView();
            //            SetViewport(new Viewport(0, 0, light.shadowmap_.width_, light.shadowmap_.height_));

            //            devicecontext_.OutputMerger.SetTargets(light.shadowmap_.depthstencilview, (RenderTargetView)(null) );
            //            light.shadowmap_.Clear();

            //            scene_.Draw();
            //        }
            //    }
            //}

            // End Shadow Pass

            DesactivatePixelShader = false;
        }


        private SwapChainHelper m_SwapChainHelper;

        // Le Device est divisé en 2 pour des raisons de multithreading. On peut désormais
        // continuer de charger des données (modèles, textures) dans device, pendant
        // que deviceContext se charge de l'affichage

        /// <summary>
        /// DeviceContext est utilisé pour dessiner les objets
        /// </summary>
        public DeviceContext DeviceContext { get; private set; }
        public D3D11.Device Device { get; private set; }

        public bool turnOffSceneRendering = false;
        public D3D11.RenderTargetView rendertargetview_;
        public Color4 backgroundColor;
        public RenderTarget m_mainRenderTarget;
        private RenderForm m_renderForm;

    }
}