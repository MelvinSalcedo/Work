using System;
using System.Collections.Generic;
using SharpDX;
using Troll3D.Components;
namespace Troll3D
{

    /// <summary>
    /// Un billBoard est tout simplement un Quad qui s'oriente en fonction de la position de la caméra
    /// de manière à lui faire face en permanence
    /// </summary>
    public class Billboard : Entity
    {


        public Billboard()
        {

            MaterialDX11 mat = new MaterialDX11( "vBillboarding.cso", "pUnlit.cso", "gBillboarding.cso" );
            mat.SetMainColor( 0.0f, 0.0f, 1.0f, 1.0F );

            PointMesh pm = new PointMesh();
            pm.AddVertex( new StandardVertex( new Vector3( 0.0f, 0.0f, 0.0f ) ) );
            pm.UpdateBuffers();

            //modelrenderer_ = new MeshRenderer( mat, pm );

            desc = new BillboardDesc()
            {
                CameraUpVector = new Vector3( 1.0f, 0.0f, 0.0f ),
                ScaleValue = new Vector3( 1.0f, 1.0f, 1.0f )
            };

            buffer_ = new CBuffer<BillboardDesc>( 6, desc );
        }

        BillboardDesc desc;
        public CBuffer<BillboardDesc> buffer_;

        public override void Update()
        {


            Vector4 up = Vector4.Transform( new Vector4( 0.0f, 1.0f, 0.0f, 0.0f ), Matrix.Invert( Camera.Main.m_transform.worldmatrix_ ) );
            Vector4 right = Vector4.Transform( new Vector4( 1.0f, 0.0f, 0.0f, 0.0f ), Matrix.Invert( Camera.Main.m_transform.worldmatrix_ ) );
            desc.CameraUpVector = ( Vector3 )up;
            desc.CameraRightVector = ( Vector3 )right;
            buffer_.UpdateStruct( desc );

        }



    }
}
