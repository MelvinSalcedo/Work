//using System;
//using System.Collections.Generic;
//using UnityEngine;

//public class GLLineRect: GLLine {

//    // Public

//        // Lifecycle

//            public GLLineRect( Vector3 a, Vector3 b, Color color, float radius ) : base( a, b, color ) {
//                radius_ = radius;
//                type_ = GLPrimitiveEnum.GL_LINE_RECT;
//            }

//        // Virtual Methods

//            public override void Draw() {

//                Vector3 direction = Direction();
//                Vector3 orthdirection = Vector3.Cross( Vector3.forward, direction );
//                orthdirection.Normalize();

//                Vector3 a,b,c,d;

//                a = a_ - orthdirection * radius_;
//                b = a_ + orthdirection * radius_;
//                c = b_ + orthdirection * radius_;
//                d = b_ - orthdirection * radius_;

//                GL.Begin( GL.QUADS );

//                    GL.Color( color_);


//                    GL.Vertex3( a.x,a.y,a.z);
//                    GL.Vertex3( b.x, b.y, b.z );
//                    GL.Vertex3( c.x, c.y, c.z );
//                    GL.Vertex3( d.x, d.y, d.z );

//                GL.End();
//            }

//        // Datas

//            public float radius_;
//}
