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
using Troll3D.Common.Mazes;

using Troll3D.Components;

namespace Mazes
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationDX11 application = new ApplicationDX11( 1280, 720);
            Camera.Main.SetProjection( new OrthoProjection( 1.0f, 1.0f, 0.0f, 100.0f ) );

            Camera.Main.m_transform.Update(); ;

            Entity entity = new Entity();
            MazeBehavior mb = (MazeBehavior) entity.AddComponent( new MazeBehavior() );
            mb.Initialize();

            application.Run();
        }
    }
}
