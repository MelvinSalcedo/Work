//using System;
//using System.Collections.Generic;
//using UnityEngine;

//public class GLHashedLine : GLLine {

//    // Public 

//        // Lifecycle 

//            public GLHashedLine( Vector3 a, Vector3 b, Color color, float gapmagnitude ) : base( a, b, color ) {
//                gapmagnitude_ = gapmagnitude;
//                type_ = GLPrimitiveEnum.GL_HASHEDLINE;
//            }

//        // Virtual Methods

//            public override void Draw( ) {

//                int pair = 0;
//                Vector3 dir = b_ - a_;

//                for ( float i = 0.0f; i < Magnitude(); i += gapmagnitude_ ) {

//                    if ( pair == 0 ) {

//                        Vector3 start = a_+ dir * ( i / Magnitude() );
//                        Vector3 end = a_+ dir * ( ( i + gapmagnitude_ ) / Magnitude() );

//                        GL.Begin( GL.LINES );

//                            GL.Color( color_ );

//                            GL.Vertex3( start.x, start.y, 0.0f );
//                            GL.Vertex3( end.x, end.y, end.z );

//                        GL.End();
//                    }
//                    pair++;
//                    pair = pair % 2;
//                }
//            }        
    
//        // Datas 

//            public float gapmagnitude_;
//}
