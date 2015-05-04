using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Troll3D.Rendering
{
    /// <summary> Classe de base qui gère les rendus. La classe est abstraite pour permettre l'ajout
    /// de différents renderer (DirectX11, OpenGL etc)</summary>
    public class Renderer
    {
        public static Renderer Instance;

        //public abstract RenderTarget NewRenderTarget();

        //public void Render()
        //{
        //    foreach ( RenderTarget renderTarget in RenderTargets )
        //    {
        //        renderTarget.Render();
        //    }
        //}

        public List<RenderTarget> RenderTargets = new List<RenderTarget>();
    }
}
