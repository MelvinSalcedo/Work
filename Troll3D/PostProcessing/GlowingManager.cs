using System;
using System.Collections.Generic;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using SharpDX;

using Troll3D.Components;

namespace Troll3D{

    public class GlowingManager {

        // Public

            // Static Data

                public static GlowingManager Instance;

            // Lifecycle

                View PostProcessingView;
                MeshRenderer modelrenderer;
                Transform transform_;
                MaterialDX11 m_DrawingMaterial;
                BlendState glowingBlending;

                public GlowingManager(int width, int height){

                    // On Initialise le transform, puisqu'on se trouve en dehors de la scène
                    transform_ = new Transform();
                    transform_.SetScale(Screen.Instance.Width, Screen.Instance.Height, 1.0f);
                    transform_.Translate(0.0f, 0.0f, 0.5f);

                    // Initialisation du material servant à dessiner le quad final
                     m_DrawingMaterial = new MaterialDX11("vDefault.cso", "pUnlit.cso", "gDefault.cso");

                    // Initialisation du Quad que l'on utilise pour dessiner
                    modelrenderer = new MeshRenderer(m_DrawingMaterial, Quad.GetMesh());

                    PostProcessingView = new View(new Transform(), new OrthoProjection(Screen.Instance.Width, Screen.Instance.Height, 0.1f, 100.0f));

                    Quaternion xquat = Quaternion.RotationAxis(new Vector3(1.0f, 0.0f, 0.0f), 0 * 0.01f);
                    Quaternion yquat = Quaternion.RotationAxis(new Vector3(0.0f, 1.0f, 0.0f), 0 * 0.01f);

                    Quaternion rotQuat = Quaternion.Multiply(xquat, yquat);
                    Matrix mview = Matrix.AffineTransformation(1.0f, rotQuat, new Vector3(00.0f, 0.0f, 10.0f));

                    Instance = this;

                    MaterialDX11 HBlurMaterial = new MaterialDX11("vDefault.cso", "pHBlur.cso", "gDefault.cso");
                    HBlurMaterial.AddConstantBuffer<HBlurDesc>(new HBlurDesc(width/2,50));

                    MaterialDX11 VBlurMaterial = new MaterialDX11("vDefault.cso", "pVBlur.cso", "gDefault.cso");
                    VBlurMaterial.AddConstantBuffer<VBlurDesc>(new VBlurDesc(height/2,50));

                    m_Material  = new MaterialDX11("vDefault.cso", "pGlow.cso", "gDefault.cso");
                    m_Material.AddConstantBuffer<GlowDesc>(new GlowDesc(false, false, new Vector4(0.0f, 0.0f, 0.0f, 1.0f)));

                    m_RenderTexture = new RenderTexture(width, height);
                    m_ImageProcessing = new ImageProcessing(width, height, m_RenderTexture.SRV);

                    m_ImageProcessing.AddPasse(HBlurMaterial);
                    m_ImageProcessing.AddPasse(HBlurMaterial);

                    m_ImageProcessing.AddPasse(VBlurMaterial);
                    m_ImageProcessing.AddPasse(VBlurMaterial);

                    // Additive Blending
                    RenderTargetBlendDescription renderdesc = new RenderTargetBlendDescription(){

                        BlendOperation      = BlendOperation.Add,
                        AlphaBlendOperation = BlendOperation.Add,

                        SourceAlphaBlend        = BlendOption.Zero,
                        DestinationAlphaBlend   = BlendOption.Zero,

                        SourceBlend         = BlendOption.One,
                        DestinationBlend    = BlendOption.One,


                        IsBlendEnabled = true,
                        RenderTargetWriteMask = ColorWriteMaskFlags.All
                    };

                    BlendStateDescription blendStateDesc2 = new BlendStateDescription()
                    {
                        IndependentBlendEnable = false,        // DirectX peut utiliser 8 RenderTarget simultanément, chauqe renderTarget
                        // peut être lié à un RenderTargetBlendDescription différent
                        AlphaToCoverageEnable = false
                    };

                    blendStateDesc2.RenderTarget[0] = renderdesc;

                    glowingBlending = new BlendState(ApplicationDX11.Instance.Device, blendStateDesc2);
                }

            // Methods

                public ShaderResourceView GetSRV(){
                    return m_ImageProcessing.GetFinalSRV();
                }

                public void GlowingPass(Scene scene){

                    // On commence par rendre l'intégralité de la scène dans une texture
                    ApplicationDX11.Instance.DeviceContext.OutputMerger.BlendState = ApplicationDX11.Instance.RenderToTextureBlendState;

                    m_RenderTexture.Bind();
                    ApplicationDX11.Instance.DeviceContext.ClearRenderTargetView(
                        m_RenderTexture.RenderTargetView,
                        new Color4(0.0f, 0.0f, 0.0f, 0.0f));
               
                    //for (int i = 0; i < scene.sons_.Count; i++){
                    //    Update(scene.sons_[i]);
                    //}

                    m_ImageProcessing.UpdatePasses();

                    if (m_DrawingMaterial.textures_.Count == 0){
                        m_DrawingMaterial.AddShaderResourceView(m_ImageProcessing.GetFinalSRV());
                    }else{
                        m_DrawingMaterial.textures_[0] = m_ImageProcessing.GetFinalSRV();
                    }
                }

                public void Update(Entity entity){

                    //if (entity.modelrenderer_ != null){
                     
                    //    if (entity.IsGlowing)
                    //    {
                    //        // On retire provisoirement le material de l'entité pour appliquer celui du glow effect
                    //        MaterialDX11 realMaterial = entity.modelrenderer_.material_;

                    //        Vector3 scaleval = entity.transform_.scaling_;
                    //        entity.modelrenderer_.material_ = m_Material;

                    //        m_Material.UpdateStruct<GlowDesc>(entity.GlowEffet.Description);
                    //        m_Material.SetTextureWidth(entity.GlowEffet.widthTiling);
                    //        m_Material.SetTextureHeight(entity.GlowEffet.heightTiling);

                    //        SamplerState state = new SamplerState(ApplicationDX11.Instance.device_, new SamplerStateDescription()
                    //        {
                    //            AddressU = TextureAddressMode.Wrap,
                    //            AddressV = TextureAddressMode.Wrap,
                    //            AddressW = TextureAddressMode.Wrap,
                    //            BorderColor = new Color4(0.0f, 1.0f, 0.0f, 1.0f),
                    //            ComparisonFunction = Comparison.LessEqual,
                    //            Filter = Filter.MinLinearMagMipPoint,
                    //            MaximumAnisotropy = 0,
                    //            MaximumLod = 0,
                    //            MinimumLod = 0,
                    //            MipLodBias = 0
                    //        });
                    //        if (m_Material.samplers.Count == 0)
                    //        {
                    //            m_Material.samplers.Add(state);
                    //        }
                    //        else
                    //        {
                    //            m_Material.samplers[0] = state;
                    //        }



                    //        if (entity.GlowEffet.Description.HasTexture) { 
                    //            if (m_Material.textures_.Count == 0)
                    //            {
                    //                m_Material.AddTexture(entity.GlowEffet.GlowTexture);
                    //            }
                    //            else
                    //            {
                    //                m_Material.SetTexture(0, entity.GlowEffet.GlowTexture);
                    //            }
                    //        }
                            
                    //        ///entity.modelrenderer_.Draw();
                    //        entity.modelrenderer_.material_ = realMaterial;
                    //        entity.transform_.SetScale(scaleval);
                    //    }

                       
                    //}
                        
                    //for (int i = 0; i < entity.sons_.Count; i++){
                    //    Update(entity.sons_[i]);
                    //}
                }

                /// <summary>
                /// On utilise un quad qu'on affiche par dessus le reste de l'image en transparence
                /// </summary>
                /// <param name="source"></param>
                public void DrawQuadOnTop(){

                    ApplicationDX11.Instance.DeviceContext.OutputMerger.BlendState = glowingBlending;
                    View.Current = PostProcessingView;
                    transform_.Update();
                    //modelrenderer.Draw();
                }

        // Private

            // Datas

                /// <summary>
                /// On Utilise une seule RenderTexture qu'on clear entre chaque caméra
                /// </summary>
                private RenderTexture       m_RenderTexture;
                private MaterialDX11        m_Material;
                private ImageProcessing     m_ImageProcessing;

                private int                 m_Width;
                private int                 m_Height;
    }
}
