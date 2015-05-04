using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using SharpDX;
using Troll3D;
using Troll3D.Components;

namespace Collisions {

    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() 
        {
            ApplicationDX11 application = new ApplicationDX11( 1000, 1000 );

            Camera.Main.m_transform.LookAt( new Vector3( 0.0f, 0.0f, 1.0f ), new Vector3( 0.0f, 0.0f, -2.0f ) );
            Entity entity   = new Entity();
            MeshRenderer mr =  entity.AddComponent<MeshRenderer>();
            mr.material_    = new MaterialDX11();
            mr.model_       = Quad.GetMesh();
            application.Run();
        }
    }

}
