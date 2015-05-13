using System;
using System.Collections.Generic;
using SharpDX;

namespace Troll3D
{
    /// <summary>
    /// Représente une courbe de Bézier
    /// To DO : Ajouter une explication précise du fonctionnement des courbes de béziers
    /// </summary>
    public class BezierCurve : BaseCurve
    {
        public BezierCurve()
        {
            m_type = CurveType.CURVE_BEZIER;
            m_controlpoints = new List<Vector3>();
        }

        /// <summary>
        /// Construit une courbe de Bezier à partir des points de controles passé en paramètre
        /// </summary>
        public BezierCurve( List<Vector3> vectors )
        {
            m_type = CurveType.CURVE_BEZIER;
            m_controlpoints = vectors;
        }

        /// <summary>
        /// Construit une courbe de bézier cubique ( d'ordre 3)
        /// </summary>
        public BezierCurve( Vector3 a, Vector3 b, Vector3 c, Vector3 d )
        {
            m_controlpoints = new List<Vector3>();
            m_controlpoints.Add( a );
            m_controlpoints.Add( b );
            m_controlpoints.Add( c );
            m_controlpoints.Add( d );
            m_type = CurveType.CURVE_BEZIER;
        }

        public override void ConstructSpline()
        {
            List<Vector3> points = new List<Vector3>();

            for ( int j = 0; j < m_discretisation; j++ )
            {
                float delta = ( float )j / m_discretisation;
                points.Add( ReturnPointFromSpline( delta ) );
            }

            m_points = points;
        }

        /// <summary>
        /// Retourne le point discrétisé
        /// </summary>
        private Vector3 ReturnPointFromSpline( float delta )
        {
            float x = 0.0f;
            float y = 0.0f;

            for ( int i = 0; i < m_controlpoints.Count; i++ )
            {
                float polynomResult = ( float )MathStuffs.PolynomeBernstein( m_controlpoints.Count - 1, i, delta );
                x += polynomResult * m_controlpoints[i].X;
                y += polynomResult * m_controlpoints[i].Y;
            }

            return new Vector3( x, y, 0.0f );
        }
    }
}
