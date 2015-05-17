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

namespace ShadowMapping
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

            //Troll3D.View.Main.AddStencilTexture(100, 100);

            Entity lightEntity = new Entity();

            DirectionalLight dl = lightEntity.AddComponent<DirectionalLight>();

            lightEntity.transform_.RotateEuler( 0.0f, 3.141592f / 2.0f, 0.0f );
            lightEntity.transform_.SetPosition( 0.0f, 50.0f, 50.0f );

            dl.AddProjection(new OrthoProjection(100.0f, 100.0f, 0.1f, 150.0f));
            dl.AddShadowMap(512, 512);

            lightEntity.AddComponent<LightBehaviour>();

            MaterialDX11 floorMaterial = new MaterialDX11("vDefault.cso", "pShadowMapping.cso", "gDefault.cso");
            floorMaterial.AddConstantBuffer<Vector4>(new Vector4(1.0f, 0.0f, 0.0f, 1.0f));

            floorMaterial.samplers.Add
            (
                new SamplerState
                (
                    ApplicationDX11.Instance.Device,
                    new SamplerStateDescription()
                    {
                        AddressU = TextureAddressMode.Border,
                        AddressV = TextureAddressMode.Border,
                        AddressW = TextureAddressMode.Border,
                        BorderColor = new Color4(0.0f, 0.0f, 1.0f, 1.0f),
                        ComparisonFunction = Comparison.LessEqual,
                        Filter = Filter.MinMagMipPoint,
                        MaximumAnisotropy = 0,
                        MaximumLod = sizeof(float),
                        MinimumLod = 0,
                        MipLodBias = 0
                    }
                )
            );

            MaterialDX11 testRender = new MaterialDX11("vDefault.cso", "pShowAlphaValue.cso", "gDefault.cso");
            testRender.AddShaderResourceView(dl.shadowmap_.shaderResourceView_);

            Entity cube = new Entity();
            cube.AddComponent<RotateCube>();
            MeshRenderer cubeRenderer = cube.AddComponent<MeshRenderer>();
            cubeRenderer.material_  = new MaterialDX11();
            cubeRenderer.model_     = Cube.Mesh; 
            
            cube.transform_.Translate(-10.0f, 5.5f, 5.0f);
            cube.transform_.SetScale(4.0f, 4.0f, 4.0f);

            Entity sphere = new Entity();
            MeshRenderer sphereMr = sphere.AddComponent<MeshRenderer>();
            sphereMr.material_  = floorMaterial;
            sphereMr.model_     = Sphere.Mesh(1.0f,30,30);

            sphere.transform_.Translate(10.0f, 7.0f, 7.0f);
            sphere.transform_.RotateEuler(0.0f, 3.141592f / 2.0f, 0.0f);
            sphere.transform_.SetScale(3.0f, 3.0f, 3.0f);


            Entity sphere2 = new Entity();
            MeshRenderer sphere2Mr = sphere2.AddComponent<MeshRenderer>();
            sphere2Mr.material_ = floorMaterial;
            sphere2Mr.model_ = Sphere.Mesh( 1.0f, 30, 30 );
            sphere2.transform_.Translate(0.00f, 10.0f, 25.0f);
            sphere2.transform_.RotateEuler(0.0f, 3.141592f / 2.0f, 0.0f);
            sphere2.transform_.SetScale(10.0f, 10.0f, 10.0f);


            Entity cube2 = new Entity();
            MeshRenderer cubeMeshRenderer2= cube2.AddComponent<MeshRenderer>();
            cubeMeshRenderer2.material_ = floorMaterial;
            cubeMeshRenderer2.model_ = Cube.Mesh;
            cube2.transform_.Translate(20.0f, 7.0f, 2.0f);
            cube2.transform_.RotateEuler(0.0f, 3.141592f / 2.0f, 0.0f);
            cube2.transform_.SetScale(2.0f, 2.0f, 10.0f);

            Entity floor = new Entity();
            MeshRenderer floorMeshRenderer = new MeshRenderer();
            floorMeshRenderer.material_ = floorMaterial;
            floorMeshRenderer.model_ = Cube.Mesh;
            floor.transform_.SetScale(80.0f, 1.0f, 80.0f);
            floor.transform_.Translate(0.0f, 0.0f, 0.0f);


            Entity Wall = new Entity();
            MeshRenderer WallMeshRenderer = Wall.AddComponent < MeshRenderer>();
            WallMeshRenderer.material_ = floorMaterial;
            WallMeshRenderer.model_ = Cube.Mesh;
            
            Wall.transform_.SetScale(80.0f, 30.0f, 2.0f);
            Wall.transform_.Translate(0.0f, 15.0f, 40.0f);


            //application.scene_.Append(new BitmapImage(application.shadowmap.shaderResourceView_, testRender));

            application.Run();
        }
    }
}
