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
using Troll3D.Components.Lighting;
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

            Entity light = new Entity();
            PointLight pl =  light.AddComponent<PointLight>();
            light.transform_.Translate( 3.0f, 4.0f, 0.0f );
            pl.SetSpecularIntensity( 40.0f );
            pl.SetIntensity( 1.0F );
            
            Entity entity = new Entity();
            MeshRenderer mr = entity.AddComponent<MeshRenderer>();
            mr.material_ = new MaterialDX11("vDefault.cso","pEmap.cso");
            mr.material_.AddTexture( "D:\\Work\\Resources\\grasscube1024.dds" );
            mr.model_ = Sphere.Mesh(1.0f,20,20);
            
            entity.transform_.SetPosition( 0.0f, 0.0f, 0.0f );


            application.Run();
        }
    }
}
