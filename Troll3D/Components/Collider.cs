using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Troll3D.Components
{
    /// <summary>
    /// Un Collider se charge d'envoyé des informations (de collision) à l'entité auquel il est attaché
    /// </summary>
    public class Collider : TComponent
    {
        public Collider()
        {
            Type = ComponentType.Collider;
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }

        public override void Attach( Entity entity )
        {
            throw new NotImplementedException();
        }


    }
}
