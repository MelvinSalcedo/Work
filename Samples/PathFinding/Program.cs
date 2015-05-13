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
using Troll3D.Components;

namespace PathFinding
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationDX11 application = new ApplicationDX11( 1000, 1000 );

            Camera.Main.SetProjection( new OrthoProjection( 1.0f, 1.0f, 0.0f, 100.0f ) );
            Camera.Main.m_transform.LookAt( new Vector3( 0.0f, 0.0f, 1.0f ), Vector3.Zero);

            Entity entity = new Entity();
            Texture2D tilesetImage = ( Texture2D )Texture2D.FromFile( ApplicationDX11.Instance.Device, "D:\\Work\\Resources\\tilesetAStar.png" );
            TileSet tileset = new TileSet( tilesetImage, 32, 32 );

            TileMap tm = entity.AddComponent<TileMap>();
            tm.SetTileMap( 10, 10, tileset, 0.1f, 0.1f );

            MaterialDX11 material = new MaterialDX11();

            PathfindingBehavior mb = entity.AddComponent<PathfindingBehavior>();
            application.Run();
        }
    }
}
