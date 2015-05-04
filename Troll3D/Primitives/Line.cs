//using System;
//using System.Collections.Generic;
//using SharpDX;

//using Troll3D;

//namespace Troll3D{

//    // Une ligne affiche un trait entre 2 points A et B
//    public class Line : Primitive{

//        // Public 

//            // Lifecycle 

//                public Line( Vector3 a, Vector3 b, Color color ) {
//                    a_  = a;
//                    b_  = b;
//                    color_ = color;

//                    mesh_ = new Mesh();
//                    mesh_.AddVertex(new Vertex(new Vector4(a.X, a.Y, a.Z, 1.0f), new Vector4(), new Vector4(color.R, color.B, color.G,1.0f), new Vector2(), new Vector2()));
//                    mesh_.AddVertex(new Vertex(new Vector4(b.X, b.Y, b.Z, 1.0f), new Vector4(), new Vector4(color.R, color.B, color.G,1.0f), new Vector2(), new Vector2())));

//                }

//            // Methods

//            // Virtual Methods

//                public override void Draw(){

//                }


//            // Methods 

//                public float Magnitude() {
//                    return ( b_- a_).Length();
//                }

//                public Vector3 Direction() {
//                    Vector3 norm = (b_ - a_);
//                    norm.Normalize();
//                    return norm;
//                }

//            // Datas 

//                public Vector3[] vertices_;
//                public Mesh     mesh_;
//                public Vector3  a_;
//                public Vector3  b_;
//                public Color    color_;
//    }
//}