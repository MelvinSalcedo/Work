using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.Direct3D11;
using Troll3D;
using Troll3D.Components;
using SharpDX;


namespace NoiseGeneration
{
    class Program
    {
        static void Main( string[] args )
        {
            ApplicationDX11 application = new ApplicationDX11( 1280, 720 );

            Camera.Main.SetProjection( new OrthoProjection( 1.6f, 0.9f, 0.0f, 100.0f ) );
            Camera.Main.m_transform.LookAt( new Vector3( 0.0f, 0.0f, 1.0f ), Vector3.Zero );

            Camera.Main.Entity.AddComponent<Trackview>();

            Entity entity = new Entity();
            entity.AddComponent < PerlinBehaviour>().Init(1280,720);

            application.Run();

        }
    }
}
