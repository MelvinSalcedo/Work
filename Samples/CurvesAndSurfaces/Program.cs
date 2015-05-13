using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Troll3D;
using Troll3D.Components;
using SharpDX.Direct3D11;
using SharpDX;

namespace CurvesAndSurfaces
{
    class Program
    {
        static void Main( string[] args )
        {
            ApplicationDX11 application = new ApplicationDX11( 1000, 1000 );

            Camera.Main.m_transform.LookAt( new Vector3( 0.0f, 0.0f, 1.0f ), new Vector3( 0.0f, 0.0f, -2.0f ) );
            Camera.Main.SetProjection( new OrthoProjection( 3, 3, 0, 100 ) );
            //Camera.Main.Entity.AddComponent<Trackview>();
            Entity entity = new Entity();
            entity.AddComponent<BezierBehaviour>();
            application.Run();
        }

    }
}
