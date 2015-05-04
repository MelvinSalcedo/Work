using SharpDX.Direct3D11;
using System;
using System.Collections.Generic;
using SharpDX.DXGI;
using SharpDX;
using System.Drawing;
using SharpDX.D3DCompiler;
using SharpDX.Direct3D;
using SharpDX.Windows;
using System.Windows.Forms;

using D3D11 = SharpDX.Direct3D11;

namespace Troll3D.Rendering.DirectX11
{
    /// <summary> Classe utilisé pour afficher de la 3D via DirectX11 </summary>
    public class DirectX11Renderer : Renderer
    {
        public DirectX11Renderer()
        {
            Instance = this;
        }

        public D3D11.Device Device;
        public D3D11.DeviceContext DeviceContext;
        public SwapChainHelper SwapChain;
    }
}
