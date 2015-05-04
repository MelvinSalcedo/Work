using System;
using System.Collections.Generic;
using SharpDX;

namespace Troll3D{

    /// <summary> Une ligne est tout simplement composé de 2 points dans l'espace </summary>
    public class Line{

        public Line(Vector3 a, Vector3 b)
        {
            m_a = a;
            m_b = b;
        }

        public Vector3 GetA(){
            return m_a;
        }
        public Vector3 GetB(){
            return m_b;
        }

        Vector3 m_a;
        Vector3 m_b;

    }
}
