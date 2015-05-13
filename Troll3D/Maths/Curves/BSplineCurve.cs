using System;
using System.Collections.Generic;
using Troll3D;
using SharpDX;

using Troll3D.Common;

namespace Troll3D
{
    /// <summary>
    /// Représente une courbe BSPline
    /// </summary>
    public class BSpline : BaseCurve
    {
        public BSpline()
        {
            m_controlpoints = new List<Vector3>();
            m_type = CurveType.CURVE_BSPLINE;
            m_Echantillonage = 3;
        }

        public BSpline( List<Vector3> controlsPoints )
        {
            m_controlpoints = controlsPoints;
            m_type = CurveType.CURVE_BSPLINE;

            m_Echantillonage = 3;
            m_degree = m_controlpoints.Count - 1; ;
        }


        public override void ConstructSpline()
        {
            List<Vector3> vertices = new List<Vector3>();

            int startSegment = 0;
            int endSegment = SegmentCount;

            if ( Loop )
            {
                startSegment = -( Degree - 1 );
                endSegment = SegmentCount + ( Degree - 1 );
            }

            if ( m_controlpoints.Count > 1 )
            {
                for ( int i = startSegment; i < endSegment; i++ )
                {
                    for ( int j = 0; j < m_Echantillonage; j++ )
                    {
                        vertices.Add( ComputeBSlineVertex( i, ( float )j / m_Echantillonage ) );
                    }
                    vertices.Add( ComputeBSlineVertex( i, 1.0f ) );
                }
            }
            m_points = vertices;
        }

        /// <summary>
        /// Retourne le nombre de segment qui composent la courbe
        /// </summary>
        public int SegmentCount{get { return m_controlpoints.Count - m_degree; }}

        /// <summary>
        /// Détermine si la courbe doit être "Clamp" ou non (c'est à dire qu'elle touche le premier et le 
        /// dernier point de controle
        /// </summary>
        public bool Clamp { get; set; }

        /// <summary>
        /// Détermine si la courbe doit ou non Loop, c'est à dire qu'elle se 
        /// se rejoint
        /// </summary>
        public bool Loop { get; set; }

        public int Degree
        {
            get{return m_degree;}
            set
            {
                if ( value > 0 && value < m_controlpoints.Count )
                {
                    m_degree = value;
                }
            }
        }

        

        private Vector3 ComputeBSlineVertex( int idSegment, float t )
        {
            Vector3 vertex = new Vector3();


            for ( int i = idSegment; i <= idSegment + Degree; i++ )
            {
                double riesenfeld = MathStuffs.PolynomeRiesenfeld( i - idSegment, Degree, t );

                int val = i;
                if ( i <= 0 )
                {
                    if ( Clamp )
                    {
                        val = 0;
                    }
                    else
                    {
                        val = m_controlpoints.Count + i;
                    }

                }
                if ( i >= m_controlpoints.Count )
                {
                    if ( Clamp )
                    {
                        val = m_controlpoints.Count - 1;
                    }
                    else
                    {
                        val = i - m_controlpoints.Count;
                    }
                }
                vertex.X += ( float )riesenfeld * m_controlpoints[val].X;
                vertex.Y += ( float )riesenfeld * m_controlpoints[val].Y;
                vertex.Z += ( float )riesenfeld * m_controlpoints[val].Z;
            }
            return vertex;
        }

        private int m_degree;
        private int m_Echantillonage;
    }

}
