using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.Direct3D11;
using SharpDX;

using Troll3D.Components;

namespace Troll3D.Rendering
{
    /// <summary>
    /// Un RenderTarget est tout simplement "l'endroit" ou le résultat des opérations d'affichage sera effectué.
    /// Généralement, ne peut prendre que 2 valeurs : 1 texture ou un écran/fenêtre
    /// </summary>
    public class RenderTarget
    {
        public RenderTarget( int width, int height )
        {
            Width = width;
            Height = height;
            InitializeDepthStencil();
        }

        /// <summary>
        /// Crée un nouveau RenderTarget
        /// </summary>
        public RenderTarget( RenderTargetView view, int width, int height )
        {
            RenderTargetView = view;
            Width = width;
            Height = height;
            InitializeDepthStencil();

            //// Initialisation de la transparence pour les RenderTexture
            //RenderTargetBlendDescription renderdesc3 = new RenderTargetBlendDescription()
            //{
            //    SourceAlphaBlend = BlendOption.One,
            //    DestinationAlphaBlend = BlendOption.Zero,
            //    AlphaBlendOperation = BlendOperation.Add,

            //    SourceBlend = BlendOption.SourceAlpha,
            //    DestinationBlend = BlendOption.Zero,
            //    BlendOperation = BlendOperation.Add,

            //    IsBlendEnabled = true,
            //    RenderTargetWriteMask = ColorWriteMaskFlags.All
            //};

            //BlendStateDescription blendStateDesc3 = new BlendStateDescription()
            //{
            //    IndependentBlendEnable = false,        // DirectX peut utiliser 8 RenderTarget simultanément, chauqe renderTarget
            //    // peut être lié à un RenderTargetBlendDescription différent
            //    AlphaToCoverageEnable = false
            //};

            //blendStateDesc3.RenderTarget[0] = renderdesc3;
            ////LastRenderToTextureBlendState = new BlendState(ApplicationDX11.Instance.Device, blendStateDesc3 );


            //Renderer.Instance.RenderTargets.Add( this );
        }

        public void InitializeDepthStencil()
        {
            DepthStencil = new DepthStencil( Width, Height );
        }

        public void Clear()
        {
            Context.ClearRenderTargetView( RenderTargetView, ClearColor );
            DepthStencil.Clear();
        }

        public void Bind()
        {
            Context.OutputMerger.SetTargets(  DepthStencil.depthstencilview, RenderTargetView );
            
        }

        public void UnBind()
        {

        }

        DeviceContext Context { get { return ApplicationDX11.Instance.DeviceContext; } }

        public RenderTargetView RenderTargetView;
        public bool IsActive;

        public void AddViewport( Viewport viewport )
        {
            Viewports.Add( viewport );
        }

        public void AddViewport( int offsetx, int offsety, int width, int height )
        {
            Viewports.Add( new Viewport( offsetx, offsety, width, height ) );
        }

        public int Width { get; protected set; }
        public int Height { get; protected set; }

        public Color4 ClearColor = new Color4( 0.7f, 0.7f, 0.7f, 1.0F );
        public Camera Camera;

        public List<Viewport> Viewports = new List<Viewport>();

        public DepthStencil DepthStencil;
    }
}
