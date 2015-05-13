using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using SharpDX;
using Troll3D;
using Troll3D.Components;
using Troll3D.Components.Lighting;
using Troll3D.Components.Collisions;

namespace Collisions 
{
    static class Program 
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() 
        {
            ApplicationDX11 application = new ApplicationDX11( 1000, 1000 );

            Entity light = new Entity();
            light.transform_.Translate( 10.0f, 10.0f, 0.0f );
            light.AddComponent<PointLight>();

            //Camera.Main.m_transform.LookAt( new Vector3( 0.0f, 0.0f, 1.0f ), new Vector3( 0.0f, 0.0f, -2.0f ) );
            Camera.Main.Entity.AddComponent<Trackview>();

            Entity entity   = new Entity();
            entity.AddComponent<OBB>();
            MeshRenderer mr =  entity.AddComponent<MeshRenderer>();
            mr.material_    = new MaterialDX11( "vDefault.cso", "pDiffuse.cso","gDefault.cso" );
            mr.model_       = Cube.Mesh;
            mr.material_.SetMainColor( 0.0F, 1.0f, 0.0f, 1.0f );
            entity.AddComponent<FirstCube>();


            Entity entity2 = new Entity();
            // Petit fix de mon soucis actuel, lors de la première frame, la transformation de
            // l'entité n'est pas mise à jour et la boite de collision se croit donc en (0,0,0)
            entity2.transform_.Translate( 3.0f, 0.0f, 0.0F );
            entity2.transform_.Update();
            entity2.AddComponent<OBB>();
            entity2.AddComponent<PickableCube>();
            MeshRenderer mr2 = entity2.AddComponent<MeshRenderer>();
            mr2.material_ = new MaterialDX11( "vDefault.cso", "pDiffuse.cso", "gDefault.cso" );
            mr2.model_ = Cube.Mesh;
            mr2.material_.SetMainColor( 0.0f, 0.0F, 1.0f, 1.0f );
            


            application.Run();
        }
    }

}
