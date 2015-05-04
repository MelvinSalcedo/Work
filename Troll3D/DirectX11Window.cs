using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.Direct3D11;
using SharpDX;
using SharpDX.Windows;
using System.Drawing;

namespace Troll3D
{
    public class DirectX11Window : Window
    {
        //private DirectX11Window(int width, int height){
        //    Width   = width;
        //    Height  = height;


        //    new Screen( width, height );

        //    RenderForm rf = new RenderForm();
        //    rf.ClientSize = new Size( width, height );
        //    Initialize( rf.Handle );

            
        //}

        //private void Initialize( IntPtr handle )
        //{
        //    m_SwapChainHelper = new SwapChainHelper( handle );
        //    device_ = m_SwapChainHelper.CreateDeviceWithSwapChain( DeviceCreationFlags.Debug );
        //    devicecontext_ = device_.ImmediateContext;

        //    scene_ = new Scene( new Color4( 0.0f, 0.2f, 0.7f, 1.0f ) );

        //    // On récupère le RenderTargetView généré par notre swapchain représentant
        //    // le backBuffer

        //    rendertargetview_ = m_SwapChainHelper.GetRenderTargetView();

        //    stencilmanager_ = new StencilManager( Screen.Instance.GetWidth(), Screen.Instance.GetHeight() );

        //    new LightManager();
        //    new ProjectorManager();
        //    new CollisionManager();
        //    new TimeHelper();

        //    // Initialisation de la transparence pour les RenderTexture
        //    RenderTargetBlendDescription renderdesc3 = new RenderTargetBlendDescription()
        //    {
        //        SourceAlphaBlend = BlendOption.One,
        //        DestinationAlphaBlend = BlendOption.Zero,
        //        AlphaBlendOperation = BlendOperation.Add,

        //        SourceBlend = BlendOption.SourceAlpha,
        //        DestinationBlend = BlendOption.Zero,
        //        BlendOperation = BlendOperation.Add,

        //        IsBlendEnabled = true,
        //        RenderTargetWriteMask = ColorWriteMaskFlags.All
        //    };

        //    BlendStateDescription blendStateDesc3 = new BlendStateDescription()
        //    {
        //        IndependentBlendEnable = false,        // DirectX peut utiliser 8 RenderTarget simultanément, chauqe renderTarget
        //        // peut être lié à un RenderTargetBlendDescription différent
        //        AlphaToCoverageEnable = false
        //    };

        //    blendStateDesc3.RenderTarget[0] = renderdesc3;

        //    LastRenderToTextureBlendState = new BlendState( device_, blendStateDesc3 );
        //    LastRenderToTextureBlendState = null;

        //    // Initialisation de la transparence pour les RenderTexture
        //    RenderTargetBlendDescription renderdesc = new RenderTargetBlendDescription()
        //    {

        //        SourceAlphaBlend = BlendOption.One,
        //        DestinationAlphaBlend = BlendOption.Zero,
        //        AlphaBlendOperation = BlendOperation.Add,

        //        SourceBlend = BlendOption.SourceAlpha,
        //        DestinationBlend = BlendOption.Zero,
        //        BlendOperation = BlendOperation.Add,

        //        IsBlendEnabled = true,
        //        RenderTargetWriteMask = ColorWriteMaskFlags.All
        //    };

        //    BlendStateDescription blendStateDesc2 = new BlendStateDescription()
        //    {
        //        IndependentBlendEnable = false,        // DirectX peut utiliser 8 RenderTarget simultanément, chauqe renderTarget
        //        // peut être lié à un RenderTargetBlendDescription différent
        //        AlphaToCoverageEnable = false
        //    };

        //    blendStateDesc2.RenderTarget[0] = renderdesc;


        //    RenderToTextureBlendState = new BlendState( device_, blendStateDesc2 );
        //    RenderToTextureBlendState = null;
        //    // Initialisation de la transparence
        //    RenderTargetBlendDescription desc = new RenderTargetBlendDescription()
        //    {
        //        AlphaBlendOperation = BlendOperation.Add,
        //        BlendOperation = BlendOperation.Add,


        //        SourceAlphaBlend = BlendOption.SourceAlpha,
        //        DestinationAlphaBlend = BlendOption.One,

        //        SourceBlend = BlendOption.SourceAlpha,
        //        DestinationBlend = BlendOption.InverseSourceAlpha,


        //        IsBlendEnabled = true,
        //        RenderTargetWriteMask = ColorWriteMaskFlags.All

        //    };

        //    BlendStateDescription blendStateDesc = new BlendStateDescription()
        //    {
        //        IndependentBlendEnable = false,        // DirectX peut utiliser 8 RenderTarget simultanément, chauqe renderTarget
        //        // peut être lié à un RenderTargetBlendDescription différent
        //        AlphaToCoverageEnable = false
        //    };

        //    blendStateDesc.RenderTarget[0] = desc;

        //    mainBlend = new BlendState( device_, blendStateDesc );

        //    devicecontext_.OutputMerger.BlendState = mainBlend;
        //    // Fin Transparence

        //    InitializeRasterState();

        //    new GlowingManager( Screen.Instance.GetWidth(), Screen.Instance.GetHeight() );
        //    m_ApplicationInformation = new ApplicationInformation();

        //    renderTex = new RenderTexture( Screen.Instance.GetWidth(), Screen.Instance.GetHeight() );
        //    imageProcessing = new ImageProcessing( Screen.Instance.GetWidth(), Screen.Instance.GetHeight(), renderTex.GetSRV() );

        //}


        //private Device          m_device;
        //private DeviceContext   m_deviceContext;


    }
}
