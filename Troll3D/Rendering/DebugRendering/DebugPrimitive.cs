using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Troll3D.Rendering.DebugRendering
{
    /// <summary>
    /// Classe de base pour toutes les primitives utiliés par le DebugRenderer
    /// </summary>
    public interface DebugPrimitive
    {
        /// <summary>
        /// Méthode à surcharger pour permettre l'affichage de la primitive par le DebugRenderer
        /// </summary>
        void Render( MaterialDX11 material );
    }
}
