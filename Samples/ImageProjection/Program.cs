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
using Troll3D.Components.Collisions;

namespace ImageProjection
{

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationDX11 application = new ApplicationDX11( 1280, 720 );
            Camera.Main.Entity.AddComponent<Trackview>();

            MaterialDX11 projectorMaterial = new MaterialDX11( "vDefault.cso", "pProjector.cso", "gDefault.cso" );

            projectorMaterial.samplers.Add
            (
                new SamplerState
                (
                    ApplicationDX11.Instance.Device,
                    new SamplerStateDescription()
                    {
                        AddressU = TextureAddressMode.Border,
                        AddressV = TextureAddressMode.Border,
                        AddressW = TextureAddressMode.Border,
                        BorderColor = new Color4( 1.0f, 0.0f, 0.0f, 1.0f ),
                        ComparisonFunction = Comparison.LessEqual,
                        Filter = Filter.MinMagMipPoint,
                        MaximumAnisotropy = 0,
                        MaximumLod = sizeof( float ),
                        MinimumLod = 0,
                        MipLodBias = 0
                    }
                )
            );

            //MaterialDX11 testRender = new MaterialDX11("vDefault.cso", "pShowAlphaValue.cso", "gDefault.cso");

            Entity pointLight = new Entity();
            DirectionalLight pl = pointLight.AddComponent<DirectionalLight>();
            pl.SetSpecularIntensity( 0.01f );
            pl.SetIntensity( 1.0f );
            pointLight.transform_.SetPosition( 0.0f, 20.0f, 0.0f );
            pointLight.transform_.RotateEuler( 0.0f, 3.141592f/1.5f, 0.0f );

            Entity sphere = new Entity();
            MeshRenderer spheremr = sphere.AddComponent<MeshRenderer>();
            spheremr.material_ = projectorMaterial;
            spheremr.model_ = Sphere.Mesh( 1.0f, 30, 30 );

            Entity cube4 = new Entity();
            MeshRenderer cube4mr = cube4.AddComponent<MeshRenderer>();
            cube4mr.material_ = projectorMaterial;
            cube4mr.material_.SetMainColor( 0.5f, 0.5f, 0.5f, 1.0F );
            cube4mr.model_ = Cube.Mesh;
            cube4.transform_.Translate( 1.0f, 5.5f, 0.0f );
            cube4.transform_.SetScale( 4.0f, 4.0f, 4.0f );

            sphere.transform_.Translate( 10.0f, 7.0f, 7.0f );
            sphere.transform_.RotateEuler( 0.0f, 3.141592f / 2.0f, 0.0f );
            sphere.transform_.SetScale( 3.0f, 3.0f, 3.0f );

            Entity floor = new Entity();
            MeshRenderer floormr = floor.AddComponent<MeshRenderer>();
            floormr.material_ = projectorMaterial;
            floormr.model_ = Quad.GetMesh();

            floor.transform_.RotateEuler( 0.0f, 3.141592f / 2.0f, 0.0f );
            floor.transform_.SetScale( 40.0f, 40.0f, 40.0f );
            floor.transform_.Translate( 0.0f, 0.0f, 0.0f );

            Entity wallN = new Entity();
            MeshRenderer wallNmr = wallN.AddComponent<MeshRenderer>();
            wallNmr.material_ = projectorMaterial;
            wallNmr.model_ = Quad.GetMesh();
            wallN.transform_.RotateEuler( 0.0f, 0.0f, 0.0f );
            wallN.transform_.SetScale( 40.0f, 40.0f, 40.0f );
            wallN.transform_.Translate( 0.0f, 20.0f, 20.0f );

            Entity wallS = new Entity();
            MeshRenderer wallSmr = wallS.AddComponent<MeshRenderer>();
            wallSmr.material_ = projectorMaterial;
            wallSmr.model_ = Quad.GetMesh();

            wallS.transform_.RotateEuler( 3.141592f, 0.0f, 0.0f );
            wallS.transform_.SetScale( 40.0f, 40.0f, 40.0f );
            wallS.transform_.Translate( 0.0f, 20.0f, -20.0f );

            Entity wallE = new Entity();
            MeshRenderer wallEmr = wallE.AddComponent<MeshRenderer>();
            wallEmr.material_ = projectorMaterial;
            wallEmr.model_ = Quad.GetMesh();


            wallE.transform_.RotateEuler( -3.141592f / 2.0f, 0.0f, 0.0f );
            wallE.transform_.SetScale( 40.0f, 40.0f, 40.0f );
            wallE.transform_.Translate( -20.0f, 20.0f, 0.0f );

            ////application.scene_.Append(new BitmapImage(application.shadowmap.shaderResourceView_, testRender));

            Entity proj = new Entity();
            Projector projector =  proj.AddComponent<Projector>();

            projector.SetProjection
            (
                new FrustumProjection
                (
                    3.141592f / 3.0f,
                    1.0f,
                    0.1f, 1000.0f
                )
            );

            projector.SetImage( ResourceManager.GetImageFromFile( "blastoise.png" ) );

            MeshRenderer mrProjector = proj.AddComponent<MeshRenderer>();
            mrProjector.model_ = ProjectionMesh.GetModel( projector.Projection );
            mrProjector.material_ = new MaterialDX11();
            mrProjector.material_.SetMainColor( 0.0f, 0.0f, 1.0f, 1.0f );
            mrProjector.SetFillMode( FillMode.Wireframe );
            

            //    )
            


            //    (new Projector(
            //    cam.GetRenderTextureSRV(),
            //    );
            //proj.transform_.Translate(0.0f, 10.0f, -10.0f);
            //proj.AddComponent<ProjectorBehavior>();
            application.Run();
        }
    }
}
