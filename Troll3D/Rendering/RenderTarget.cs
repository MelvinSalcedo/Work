using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.Direct3D11;
using SharpDX;

namespace Troll3D.Rendering
{
    /// <summary>
    /// Un RenderTarget est tout simplement "l'endroit" ou le résultat des opérations d'affichage sera effectué.
    /// Généralement, ne peut prendre que 2 valeurs : 1 texture ou un écran/fenêtre
    /// </summary>
    public class RenderTarget
    {
       /// <summary>Crée un nouveau RenderTarget </summary>
        public RenderTarget(RenderTargetView view, DeviceContext context, int width, int height)
        {
            Context = context;
            RenderTargetView    = view;
            Width               = width;
            Height              = height;
            DepthStencil = new DepthStencil( width, height );

            //Renderer.Instance.RenderTargets.Add( this );
        }

        

        public  void Clear()
        {
            Context.ClearRenderTargetView( RenderTargetView, ClearColor );
            DepthStencil.Clear();
        }

        public  void Bind()
        {
            Context.OutputMerger.SetTargets( ( DepthStencil ).depthstencilview, RenderTargetView );
        }

        public  void UnBind()
        {

        }

        DeviceContext Context;
        public RenderTargetView RenderTargetView;

        public bool IsActive { get; set; }

        public void AddViewport( Viewport viewport )
        {
            Viewports.Add( viewport );
        }
        public void AddViewport( int offsetx, int offsety, int width, int height )
        {
            Viewports.Add( new Viewport( offsetx, offsety, width, height ) );
        }

        public int Width  { get; protected set; }
        public int Height { get; protected set; }

        public Color4 ClearColor        = new Color4(0.7f,0.7f,0.7f,1.0F);
        public List<Viewport> Viewports = new List<Viewport>();
        public DepthStencil DepthStencil;
    }
}
