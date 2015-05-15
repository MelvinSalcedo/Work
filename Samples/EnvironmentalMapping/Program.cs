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

namespace EnvironmentalMapping
{
    class Program
    {
        static void Main( string[] args )
        {
            ApplicationDX11 application = new ApplicationDX11( 1980, 1020 );

            DynamicCubemap cubemap = new DynamicCubemap( 1000 );

            Camera.Main.Entity.AddComponent<Trackview>();
            //Camera.Main.Entity.transform_.LookAt( new Vector3( 0.0f, 0.0f, 0.0f ), new Vector3( 0.0f, 5.0f, -5.0f ) );
            Camera.Main.Skybox = new Skybox( Camera.Main.Entity );

            cubemap.AddSkybox( Camera.Main.Skybox );
            Entity light = new Entity();
            PointLight pl =  light.AddComponent<PointLight>();
            light.transform_.Translate( 3.0f, 4.0f, 0.0f );
            pl.SetSpecularIntensity( 40.0f );
            pl.SetIntensity( 1.0F );
            
            Entity entity = new Entity();
            MeshRenderer mr = entity.AddComponent<MeshRenderer>();
            mr.material_ = new MaterialDX11("vDefault.cso","pEmap.cso");
            mr.material_.AddShaderResourceView( cubemap.SRV );
            mr.model_ = Sphere.Mesh(1.0f,50,50);

            SamplerState state = new SamplerState( ApplicationDX11.Instance.Device, new SamplerStateDescription()
            {
                AddressU = TextureAddressMode.Wrap,
                AddressV = TextureAddressMode.Wrap,
                AddressW = TextureAddressMode.Wrap,
                BorderColor = new Color4( 0.0f, 1.0f, 0.0f, 1.0f ),
                ComparisonFunction = Comparison.LessEqual,
                Filter = Filter.MinLinearMagMipPoint,
                MaximumAnisotropy = 0,
                MaximumLod = 0,
                MinimumLod = 0,
                MipLodBias = 0
            } );

            Entity floor = new Entity();
            MeshRenderer mrfloor = floor.AddComponent<MeshRenderer>();
            mrfloor.material_ = new MaterialDX11( "vDefault.cso", "pDiffuse.cso" );
            mrfloor.material_.AddTexture( "D:\\Work\\Resources\\ConcreteFloor.jpg" );
            mrfloor.model_ = Quad.GetMesh();

            mrfloor.material_.samplers.Add( state );
            floor.transform_.SetPosition( 0.0f, -5.0f, 0.0f );
            floor.transform_.RotateEuler( 0.0f, 3.1415f / 2.0f, 0.0f );
            floor.transform_.SetScale( 15.0f, 15.0f, 15.0f );
            mrfloor.material_.SetTextureWidth( 2.0f );
            mrfloor.material_.SetTextureHeight( 2.0f );

            entity.transform_.SetPosition( 0.0f, 0.0f, 0.0f );


            Entity caisse  = new Entity();

            caisse.AddComponent<OBB>();
            MeshRenderer mrcaisse = caisse.AddComponent<MeshRenderer>();
            mrcaisse.material_ = new MaterialDX11( "vDefault.cso", "pDiffuse.cso" );
            mrcaisse.model_ = Cube.Mesh;

            mrcaisse.material_.samplers.Add( state );
            mrcaisse.material_.SetMainColor( 1.0f, 0.0f, 1.0f, 1.0F );
            caisse.transform_.SetPosition( -2.0f, 0.0f, -2.0f );
            caisse.transform_.SetScale( 1.0f, 1.0f, 1.0f );

            caisse.AddComponent<Crate>();

            Entity caisse2 = new Entity();

            caisse2.AddComponent<OBB>();
            MeshRenderer mrcaisse2 = caisse2.AddComponent<MeshRenderer>();
            mrcaisse2.material_ = new MaterialDX11( "vDefault.cso", "pDiffuse.cso" );
            mrcaisse2.material_.SetMainColor( 1.0f, 0.0f, 0.0f, 1.0F );
            mrcaisse2.model_ = Cube.Mesh;

            mrcaisse2.material_.samplers.Add( state );
            caisse2.transform_.SetPosition( 2.0f, 0.0f, -2.0f );
            caisse2.transform_.SetScale( 1.0f, 1.0f, 1.0f );
            caisse2.AddComponent<Crate>();

            Entity caisse3 = new Entity();

            caisse3.AddComponent<OBB>();
            MeshRenderer mrcaisse3 = caisse3.AddComponent<MeshRenderer>();
            mrcaisse3.material_ = new MaterialDX11( "vDefault.cso", "pDiffuse.cso" );
            mrcaisse3.material_.SetMainColor( 0.0f, 0.0f, 1.0f, 1.0F );
            mrcaisse3.model_ = Cube.Mesh;

            mrcaisse3.material_.samplers.Add( state );
            caisse3.transform_.SetPosition( -2.0f, 0.0f, 2.0f );
            caisse3.transform_.SetScale( 1.0f, 1.0f, 1.0f );
            caisse3.AddComponent<Crate>();


            Entity caisse4 = new Entity();

            caisse4.AddComponent<OBB>();
            MeshRenderer mrcaisse4 = caisse4.AddComponent<MeshRenderer>();
            mrcaisse4.material_ = new MaterialDX11( "vDefault.cso", "pDiffuse.cso" );
            mrcaisse4.material_.SetMainColor( 0.0f, 1.0f, 0.0f, 1.0F );
            mrcaisse4.model_ = Cube.Mesh;

            mrcaisse4.material_.samplers.Add( state );
            caisse4.transform_.SetPosition( 2.0f, 0.0f, 2.0f );
            caisse4.transform_.SetScale( 1.0f, 1.0f, 1.0f );
            caisse4.AddComponent<Crate>();


            application.Run();
        }
    }
}
