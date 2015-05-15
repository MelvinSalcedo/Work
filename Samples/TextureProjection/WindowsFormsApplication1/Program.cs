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

namespace ProjectiveTexture
{

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);

            ApplicationDX11 application = new ApplicationDX11(512, 512);
            application.Isglowing = false;


            Troll3D.View.Main.AddBehavior<Trackview>().Init(10.0f, 0.01f, 0.01f);
            application.scene_.Append(new BitmapImage("Lenna.png")).AddBehavior(new ImageBehaviour());
            application.Run();
        }
    }
}
