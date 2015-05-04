using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.Direct3D11;
using SharpDX.DXGI;


namespace Troll3D.Rendering
{
    public class DepthStencil
    {

        public DepthStencil( int width, int height, bool isShaderResource = false )
        {
            IsShaderResource = isShaderResource;
            width_ = width;
            height_ = height;

            // Initialize and set up the description of the depth buffer.
            InitializeDepthBufferDescription();

            // Create the texture for the depth buffer using the filled out description.
            InitializeDepthStencilBuffer();

            // Initialize and set up the description of the stencil state.
            InitializeDepthStencilStateDescription();

            // Create the depth stencil state.
            depthstencilstate = new DepthStencilState( ApplicationDX11.Instance.device_, depthstencilstatedesc );

            // Initialize and set up the depth stencil view.
            InitializeDepthStencilViewDescription();

            // Create the depth stencil view.
            depthstencilview = new DepthStencilView(
                ApplicationDX11.Instance.device_,
                depthstencilbuffer,
                depthstencilviewdesc );

            // On précise si on souhaite enregistrer le DepthStencil dans une texture pour une utilisation ultérieure
            if ( IsShaderResource )
            {
                ShaderResourceViewDesc = new ShaderResourceViewDescription
                {
                    Dimension = SharpDX.Direct3D.ShaderResourceViewDimension.Texture2D,
                    Format = SharpDX.DXGI.Format.R24_UNorm_X8_Typeless,
                    Texture2D = new ShaderResourceViewDescription.Texture2DResource()
                    {
                        MipLevels = 1,
                        MostDetailedMip = 0
                    }
                };

                shaderResourceView_ = new ShaderResourceView( ApplicationDX11.Instance.device_, depthstencilbuffer, ShaderResourceViewDesc );
            }
        }


        public void Clear()
        {
            ApplicationDX11.Instance.devicecontext_.ClearDepthStencilView(
                depthstencilview,
                DepthStencilClearFlags.Depth | DepthStencilClearFlags.Stencil,
                1.0f,
                0
            );
        }

        public ShaderResourceView shaderResourceView_;
        ShaderResourceViewDescription ShaderResourceViewDesc;

        public int width_;
        public int height_;

        public Texture2DDescription depthbufferdescription;
        public Texture2D depthstencilbuffer;

        public DepthStencilState depthstencilstate;
        public DepthStencilStateDescription depthstencilstatedesc;

        public DepthStencilViewDescription depthstencilviewdesc;
        public DepthStencilView depthstencilview;

        // Private

        // Methods

        public bool IsShaderResource;

        public void InitializeDepthBufferDescription()
        {

            BindFlags bf = BindFlags.DepthStencil;
            if ( IsShaderResource )
            {
                bf = BindFlags.DepthStencil | BindFlags.ShaderResource;
            }

            depthbufferdescription = new Texture2DDescription()
            {
                Width = width_,
                Height = height_,
                MipLevels = 1,
                ArraySize = 1,
                Format = Format.R24G8_Typeless,
                Usage = ResourceUsage.Default,
                BindFlags = bf,
                CpuAccessFlags = CpuAccessFlags.None,
                OptionFlags = ResourceOptionFlags.None,
                SampleDescription = new SampleDescription
                {
                    Count = 1,
                    Quality = 0
                }
            };
        }

        public void InitializeDepthStencilBuffer()
        {
            depthstencilbuffer = new Texture2D( ApplicationDX11.Instance.device_, depthbufferdescription );
        }

        public void InitializeDepthStencilStateDescription()
        {
            depthstencilstatedesc = new DepthStencilStateDescription()
            {
                IsDepthEnabled = true,
                DepthWriteMask = DepthWriteMask.All,
                DepthComparison = Comparison.Less,
                IsStencilEnabled = true,
                StencilReadMask = 0xFF,
                StencilWriteMask = 0xFF,
                // Stencil operation if pixel front-facing.
                FrontFace = new DepthStencilOperationDescription()
                {
                    FailOperation = StencilOperation.Keep,
                    DepthFailOperation = StencilOperation.Increment,
                    PassOperation = StencilOperation.Keep,
                    Comparison = Comparison.Always
                },
                // Stencil operation if pixel is back-facing.
                BackFace = new DepthStencilOperationDescription()
                {
                    FailOperation = StencilOperation.Keep,
                    DepthFailOperation = StencilOperation.Decrement,
                    PassOperation = StencilOperation.Keep,
                    Comparison = Comparison.Always
                }
            };
        }


        public void InitializeDepthStencilViewDescription()
        {
            depthstencilviewdesc = new DepthStencilViewDescription()
            {
                Format = Format.D24_UNorm_S8_UInt,
                Dimension = DepthStencilViewDimension.Texture2D,
                Texture2D = new DepthStencilViewDescription.Texture2DResource()
                {
                    MipSlice = 0
                }
            };
        }



    }
}
