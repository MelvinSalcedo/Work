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
using Troll3D.Common;
using Troll3D.Components;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace LSystems{

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            ApplicationDX11 application = new ApplicationDX11(1280, 720);
            Camera.Main.Entity.transform_.LookAt(new Vector3(0.0f,0.0f,0.0f),new Vector3(0.0f,0.0f,-1.0f));

            Entity enti = new Entity();

            enti.AddComponent<LSystemBehavior>();

            application.Run();
        }
    }
}
