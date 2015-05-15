using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;
using SharpDX.Direct3D11;

namespace Troll3D.Components
{
    /// <summary>
    /// Représente une skybox
    /// Une skybox utilise une cubemap pour simuler l'environnement lointain, ainsi qu'une sphère dont les 
    /// faces pointent vers l'intérieur qui "entoure" la caméra. 
    /// </summary>
    public class Skybox 
    {
        public Skybox( Entity ent)
        {
            m_cameraEntity = ent;

            m_SkyboxEntity = new Entity();
            m_SkyboxEntity.transform_.SetScale( 10.0f, 10.0f, 10.0F );

            
            m_renderer = new MeshRenderer();
            m_renderer.material_ = new MaterialDX11("vSkybox.cso","pSkybox.cso");

            m_renderer.material_.AddTexture( "D:\\Work\\Resources\\snowcube1024.dds" );

            m_renderer.material_.SetMainColor( 0.0f, 1.0f, 1.0F, 0.5F );
            m_renderer.model_       = Sphere.ReverseMesh(1.0f,50,50);
            m_renderer.Transform = m_SkyboxEntity.transform_;

            Scene.CurrentScene.RemoveRenderable( m_renderer );
        }

        public void Render()
        {
            m_SkyboxEntity.transform_.SetPosition( m_cameraEntity.transform_.GetPosition() );
            m_SkyboxEntity.transform_.Update();

            //Console.WriteLine( 
            //    m_renderer.Transform.GetPosition().X + " " +
            //    m_renderer.Transform.GetPosition().Y + " " +
            //    m_renderer.Transform.GetPosition().Z
            //);

            m_renderer.Render();
        }

        public void CreateCubeMap()
        {
            //Texture2DDescription descrition = new Texture2DDescription(){
            //    Width = 512,
            //    Height = 512,
            //    MipLevels = 1,
            //    ArraySize = 6,
            //    Format = SharpDX.DXGI.Format.R8G8B8A8_UNorm,
            //    CpuAccessFlags = 0,
            //    SampleDescription = new SharpDX.DXGI.SampleDescription(){
            //        Count= 1,
            //        Quality = 0
            //    },
            //    Usage = ResourceUsage.Default,
            //    BindFlags = BindFlags.ShaderResource,
            //    OptionFlags = ResourceOptionFlags.TextureCube
            //};

            //ShaderResourceViewDescription srvd = new ShaderResourceViewDescription(){
            //    Format = descrition.Format,
            //    Dimension = SharpDX.Direct3D.ShaderResourceViewDimension.TextureCube,
            //    TextureCube = new ShaderResourceViewDescription.TextureCubeResource{
            //        MipLevels=descrition.MipLevels,
            //        MostDetailedMip=0
            //    }
                
            //};

            //for(int i=0; i< 6; i++)
            //{

            //}
            
            //D3D11_SUBRESOURCE_DATA pData[6];
            //std::vector<vector4b> d[6]; // 6 images of type vector4b = 4 * unsigned char

            //for (int cubeMapFaceIndex = 0; cubeMapFaceIndex < 6; cubeMapFaceIndex++)
            //{   
            //    d[cubeMapFaceIndex].resize(description.width * description.height);

            //    // fill with red color  
            //    std::fill(
            //        d[cubeMapFaceIndex].begin(), 
            //        d[cubeMapFaceIndex].end(), 
            //        vector4b(255,0,0,255));

            //    pData[cubeMapFaceIndex].pSysMem = &d[cubeMapFaceIndex][0];// description.data;
            //    pData[cubeMapFaceIndex].SysMemPitch = description.width * 4;
            //    pData[cubeMapFaceIndex].SysMemSlicePitch = 0;
            //}
            
            //string []names = new string[6]
            //{
            //    "sea_up.JPG",
            //    "sea_dn.JPG",
            //    "sea_ft.JPG",
            //    "sea_lf.JPG",
            //    "sea_rt.JPG",
            //    "sea_bk.JPG"
            //};

            //int stride = 0;
            //ApplicationDX11.Instance.DeviceContext.UpdateSubresource
            //for(int i=0;i < names.Length; ++i)
            //{
            //    Resource t = Texture2D.FromFile(ApplicationDX11.Instance.Device, names[i]);
                
            //    var buffer = new DataStream(bitmaps[0].Size.Height * stride,true,true);
            //    bitmaps[i].CopyPixels(stride, buffer);
            //    DataBox box = new DataBox(buffer.DataPointer, stride, 1);

            //    Game.GraphicsDevice.ImmediateContext.UpdateSubresource(box,
            //        texArray,
            //        Resource.CalculateSubResourceIndex(
            //        0, i, CountMips
            //        (bitmaps[0].Size.Width,
            //        bitmaps[0].Size.Height)));
            //    buffer.Dispose();
            //}

            //Texture2D tex = new Texture2D(
            //    ApplicationDX11.Instance.Device,
            //    descrition);

            //ShaderResourceView rv = new ShaderResourceView(
            //    ApplicationDX11.Instance.Device,
            //    tex,
            //    srvd
            //    );

            //HRESULT hr = renderer->getDevice()->CreateTexture2D(&texDesc, 
            //    description.data[0] ? &pData[0] : nullptr, &m_pCubeTexture);
            //assert(hr == S_OK);


        }

        public Texture2D tex1;
        public Texture2D tex2;
        public Texture2D tex3;

        public Texture2D tex4;
        public Texture2D tex5;
        public Texture2D tex6;

        public MeshRenderer m_renderer;
        public Entity       m_cameraEntity;
        public Entity       m_SkyboxEntity;
    }
}
