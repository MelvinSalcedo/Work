using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Troll3D.Components
{
    public enum ComponentType
    {
        Transform,
        MeshRenderer,
        Behavior,
        Light,
        Camera,
        TileMap,
        Collider,
        LineRenderer,
        FrontQuad
    }

    public abstract class TComponent
    {
        /// <summary> Met à jour, si besoin, les informations du composant</summary>
        public abstract void Update();

        /// <summary> Attache un composant à son Entité</summary>
        /// <param name="entity"></param>
        public abstract void Attach( Entity entity );

        public ComponentType Type { get; protected set; }
    }
}
