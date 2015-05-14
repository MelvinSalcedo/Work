using System;
using System.Collections.Generic;
using System.Linq;
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
using Troll3D.Components.Lighting;

namespace RenderTextureTest
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationDX11 application = new ApplicationDX11(1280, 720);
            Camera.Main.Entity.AddComponent<Trackview>();

            RenderTexture renderTexture = new RenderTexture( 1280, 720 );

            Entity newCamera = new Entity();
            Camera cam = newCamera.AddComponent<Camera>();
            cam.Initialize( new FrustumProjection( 3.141592f / 3.0f, Troll3D.Screen.Instance.GetRatio(), 0.1f, 1000.0f ) );
            cam.SetRenderTarget( renderTexture );
            cam.Entity.AddComponent<Trackview>();

            // Pour éviter des soucis avec le code existant, pour l'instant, je rajoute
            // manuellement ma renderTexture à la liste des RenderTextures à mettre à jour 

            ApplicationDX11.Instance.RenderTextures.Add( renderTexture );

            ////newCamera.AddComponent<CameraBehaviour>();
            
            ////cam.IsActive = false;
            ////cam.AddRenderTexture(100, 100);

            ////MeshRenderer mr = newCamera.AddComponent<MeshRenderer>();

            //mr = new MeshRenderer( );
            //mr.material_ = new MaterialDX11();
            //mr.model_ = ProjectionMesh.GetModel( cam.GetProjection() ) ;
            //mr.material_.SetMainColor( 1.0F, 1.0f, 1.0f, 1.0f );
            //mr.SetFillMode( SharpDX.Direct3D11.FillMode.Wireframe );

            MaterialDX11 renderMaterial = new MaterialDX11( "vDefault.cso", "pUnlit.cso" );
            renderMaterial.SetMainColor( 0.1f, 0.3f, 0.1f, 1.0f );
            renderMaterial.AddShaderResourceView( renderTexture.SRV );

            MaterialDX11 projectorMaterial = new MaterialDX11( "vDefault.cso", "pDiffuse.cso", "gDefault.cso" );

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
            PointLight pl = pointLight.AddComponent<PointLight>();
            pl.SetSpecularIntensity( 0.01f );
            pl.SetIntensity( 1.0f );
            pointLight.transform_.SetPosition(0.0f, 20.0f, 0.0f);

            Entity cube = new Entity();
            cube.AddComponent<RotateCube>();
            MeshRenderer cubemr = cube.AddComponent<MeshRenderer>();
            cubemr.material_ = projectorMaterial;
            cubemr.material_.SetMainColor( 0.5f, 0.5f, 0.5f, 1.0F );
            cubemr.model_       = Cube.Mesh ;
            cube.transform_.Translate(1.0f, 0.5f, 0.0f);
            cube.transform_.SetScale(4.0f, 4.0f, 4.0f);


            Entity sphere = new Entity();
            MeshRenderer spheremr = sphere.AddComponent<MeshRenderer>();
            spheremr.material_ = projectorMaterial;
            spheremr.model_ = Sphere.Mesh( 1.0f, 30, 30 );

            Entity cube4 = new Entity();
            cube4.AddComponent<RotateCube>();
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

            Entity frontQuad = new Entity();
            FrontQuad fq =  frontQuad.AddComponent<FrontQuad>();
            fq.SetQuad( 0, 0, 300, 300 );
            fq.Material.SetMainColor( 1.0f, 0.0f, 0.0f, 1.0f );
            fq.Material.AddShaderResourceView( renderTexture.SRV );
            


            ////application.scene_.Append(new BitmapImage(application.shadowmap.shaderResourceView_, testRender));

            //Entity proj = new Entity();
                
                
            //    (new Projector(
            //    cam.GetRenderTextureSRV(),
            //    new FrustumProjection(3.141592f / 3.0f,

            //        cam.GetViewport().Width / (float)cam.GetViewport().Height, 1, 80.0f))

            //    );
            //proj.transform_.Translate(0.0f, 10.0f, -10.0f);
            //proj.AddComponent<ProjectorBehavior>();
            application.Run();
        }
    }
}
