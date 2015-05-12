using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharpDX.Windows;
using System.Drawing;
using SharpDX;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using Troll3D;
using Troll3D.Components;

namespace EnvironmentalMapping
{
    class Program
    {
        static void Main( string[] args )
        {
            ApplicationDX11 application = new ApplicationDX11( 1000, 1000 );

            Camera.Main.Entity.AddComponent<Trackview>();
            //Camera.Main.Entity.transform_.LookAt( new Vector3( 0.0f, 0.0f, 0.0f ), new Vector3( 0.0f, 5.0f, -5.0f ) );
            Camera.Main.Skybox = new Skybox( Camera.Main.Entity );

            
            Entity entity = new Entity();
            MeshRenderer mr = entity.AddComponent<MeshRenderer>();
            mr.material_ = new MaterialDX11();
            mr.material_.SetMainColor( 0.0f, 1.0f, 0.0f, 1.0f );
            mr.model_ = Cube.Mesh;
            
            entity.transform_.SetPosition( 0.0f, 0.0f, 0.0f );


            application.Run();
        }
    }
}
