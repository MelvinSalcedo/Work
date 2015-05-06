using System;
using System.Collections.Generic;
using SharpDX;

namespace Troll3D.Components
{

    /// <summary>
    ///  La classe Trackview permet de controler la caméra de manière à la faire tourner autours d'un 
    ///  point
    /// </summary>
    public class Trackview : Behaviour{

        public Trackview()
        {
            Xoffset = 00.0f;
            Yoffset = 0.0f;
            Radius = 10.0f;
            Init( Radius, 0.01f, 0.03f );
        }

        public Trackview( float radius = 10.0f, float mouseSpeed = 0.01f, float wheelSpeed = 0.03f ): base()
        {
            Xoffset     =   00.0f;
            Yoffset     =   0.0f;
            Radius      =   10.0f;
            Init( radius, mouseSpeed, wheelSpeed );
        }

        public void Init(float radius, float mouseSpeed, float wheelSpeed) {
            Radius      = radius;
            MouseSpeed  = mouseSpeed;
            WheelSpeed  = wheelSpeed;
        }

        public override void Attach( Entity entity )
        {
            base.Attach( entity );
            camera = ( Camera )entity.GetComponent( ComponentType.Camera );
        }

        public override void OnMouseMove(MouseEvent e) {
            if (e.mouse_.leftbutton) {
                Xoffset-= e.mouse_.deltay;
                Yoffset+= e.mouse_.deltax;
            }
        }

        public override void OnMouseWheel(MouseEvent e) {
            if (camera.GetProjectionType() == ProjectionType.OrthoProjection){

                camera.GetOrthoProjection().SetOrthoCenter(
                    camera.GetOrthoProjection().GetWidth() + e.mouse_.wheeldelta * WheelSpeed,
                    camera.GetOrthoProjection().GetHeight() + e.mouse_.wheeldelta * WheelSpeed,
                    camera.GetOrthoProjection().GetNear(),
                    camera.GetOrthoProjection().GetFar());

                Radius += e.mouse_.wheeldelta * WheelSpeed;
            }
            else{
                Radius += e.mouse_.wheeldelta * WheelSpeed;
            }
        }

        public override void Update() {
            Quaternion xquat = Quaternion.RotationAxis(new Vector3(1.0f, 0.0f, 0.0f), Xoffset* MouseSpeed);
            Quaternion yquat = Quaternion.RotationAxis(new Vector3(0.0f, 1.0f, 0.0f), Yoffset* MouseSpeed);

            Quaternion rotQuat = Quaternion.Multiply(xquat, yquat);
            Matrix view = Matrix.AffineTransformation(1.0f, rotQuat, new Vector3(0.0f, 0.0f, Radius));

            Entity.transform_.SetLocalMatrix( view );
        }

        // Data

            public Camera  camera;
            public float WheelSpeed;
            public float Radius;
            public float MouseSpeed;
            public float Xoffset;
            public float Yoffset;
        
    }
}
