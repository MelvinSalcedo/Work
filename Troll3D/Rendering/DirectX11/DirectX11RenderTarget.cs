using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.Direct3D11;

namespace Troll3D.Rendering.DirectX11
{
    //public class DirectX11RenderTarget : RenderTarget
    //{
    //    /// <summary>
    //    /// Crée un viewport par défaut aux dimension du renderTarget
    //    /// </summary>
    //    /// <param name="view"></param>
    //    /// <param name="width"></param>
    //    /// <param name="height"></param>
    //    public DirectX11RenderTarget(RenderTargetView view, int width, int height)
    //    {
    //        RenderTargetView    = view;
    //        Width               = width;
    //        Height              = height;

    //        Renderer.Instance.RenderTargets.Add( this );
    //    }

    //    public DirectX11RenderTarget( RenderTargetView view, int width, int height, Viewport viewport)
    //    {

    //    }

    //    DeviceContext Context;

    //    //public override void Clear()
    //    //{
    //    //    Context.ClearRenderTargetView( RenderTargetView, ClearColor);
    //    //}

    //    //public override void Bind()
    //    //{
    //    //    Context.OutputMerger.SetTargets( ( ( StencilViewDirectX11 )DepthStencil ).depthstencilview, RenderTargetView );
    //    //}

    //    //public override void UnBind()
    //    //{
            
    //    //}

    //    //public override void Render()
    //    //{
    //    //    Clear();
    //    //    Bind();
    //    //    foreach ( Viewport viewport in Viewports )
    //    //    {
    //    //        viewport
    //    //    }
    //    //    UnBind();
    //    //}

    //    public RenderTargetView RenderTargetView;
    //}
}
