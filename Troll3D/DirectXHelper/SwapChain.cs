using System;
using SharpDX;
using SharpDX.DXGI;
using SharpDX.Windows;
using System.Windows.Forms;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using D3D11 = SharpDX.Direct3D11;

namespace Troll3D
{
    public class SwapChainHelper{

        // Public

            // Lifecycle

                public SwapChainHelper(System.IntPtr handle){
                    Initialize(handle);
                }

                public SwapChainHelper(RenderForm renderForm){
                    Initialize(renderForm.Handle);
                }

                private void Initialize(System.IntPtr handle){

                    // Description de l'affichage
                    m_modedesc = new ModeDescription(){
                        Width       = Screen.Instance.Width,
                        Height      = Screen.Instance.Height,
                        RefreshRate = new Rational(60, 1),
                        Format      = Format.R8G8B8A8_UNorm
                    };

                    // Description de la SwapChain
                    m_swapchaindesc= new SwapChainDescription()
                    {
                        ModeDescription = m_modedesc,
                        SampleDescription = new SampleDescription(1, 0),
                        Usage = Usage.RenderTargetOutput,
                        BufferCount = 1,
                        OutputHandle = handle,
                        IsWindowed = true,
                        SwapEffect = SwapEffect.Discard
                    };
                }

                public void Dispose(){
                   // Utilities.Dispose<SwapChain>(ref swapChain);
                }

            // Methods

                 public RenderTargetView GetRenderTargetView() {
                    return m_RTV;
                }

                /// <summary>
                /// Crée le Device ainsi que la swapChain
                /// </summary>
                /// <param name="flags"></param>
                /// <param name="device"></param>
                public D3D11.Device CreateDeviceWithSwapChain(DeviceCreationFlags flags){

                    FeatureLevel[] levels = new FeatureLevel[] { FeatureLevel.Level_11_0 };

                    D3D11.Device.CreateWithSwapChain(DriverType.Hardware, flags, levels, m_swapchaindesc,
                    out     m_device,
                    out     m_swapchain);

                    m_backbuffer_   = m_swapchain.GetBackBuffer<Texture2D>(0);
                    m_RTV = new RenderTargetView(m_device, m_backbuffer_);

                    return m_device;
                }

                public void Resize(int width, int height){
                    Utilities.Dispose<Texture2D>(ref m_backbuffer_);
                    Utilities.Dispose<RenderTargetView>(ref m_RTV);
                    
                    m_swapchain.ResizeBuffers(0, width, height, Format.R8G8B8A8_UNorm, SwapChainFlags.None);
                    m_backbuffer_   = m_swapchain.GetBackBuffer<Texture2D>(0);
                    m_RTV           = new RenderTargetView(m_device, m_backbuffer_);
                }

                public void Present(int syncInterval, PresentFlags flags){
                    m_swapchain.Present(syncInterval, flags);
                }

            // Datas

                public Texture2D                m_backbuffer_;
                public RenderTargetView         m_RTV;
                public ModeDescription          m_modedesc;
                public SwapChainDescription     m_swapchaindesc;
                public SwapChain                m_swapchain;
                public D3D11.Device             m_device;

    }
}
