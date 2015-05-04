using System;
using System.Collections.Generic;
using Troll3D;

namespace Troll3D
{

  //  public class BSpline : BaseCurve
   // {

        //// Public 

        //// Constructors 

        //public BSpline()
        //{
        //    m_ControlPoints = new List<Vector3>();
        //    m_Type = CurveType.BSpline;
        //    m_Echantillonage = 3;
        //}

        //public BSpline(List<Vector3> controlsPoints)
        //{
        //    m_ControlPoints = controlsPoints;
        //    m_Type = CurveType.BSpline;

        //    m_Echantillonage = 3;
        //    m_Degree = m_ControlPoints.Count - 1; ;
        //}

        //// Methods 

        //public override void ConstructSpline()
        //{
        //    List<Vector3> vertices = new List<Vector3>();

        //    int startSegment = 0;
        //    int endSegment = SegmentCount;

        //    if (Loop)
        //    {
        //        startSegment = -(Degree - 1);
        //        endSegment = SegmentCount + (Degree - 1);
        //    }

        //    if (m_ControlPoints.Count > 1)
        //    {
        //        for (int i = startSegment; i < endSegment; i++)
        //        {
        //            for (int j = 0; j < m_Echantillonage; j++)
        //            {
        //                vertices.Add(ComputeBSlineVertex(i, (float)j / m_Echantillonage));
        //            }
        //            vertices.Add(ComputeBSlineVertex(i, 1.0f));
        //        }
        //    }
        //    m_Points = vertices;
        //}


        //// Accessors 

        //public int SegmentCount
        //{
        //    get { return m_ControlPoints.Count - m_Degree; }
        //}

        //public bool Clamp
        //{
        //    get { return m_IsClamp; }
        //    set
        //    {
        //        if (m_IsClamp != value)
        //        {
        //            m_IsClamp = value;
        //        }
        //    }
        //}

        //public bool Loop
        //{
        //    get { return m_IsLoop; }
        //    set
        //    {
        //        if (m_IsLoop != value)
        //        {
        //            m_IsLoop = value;
        //        }
        //    }
        //}

        //public int Degree
        //{
        //    get { return m_Degree; }
        //    set
        //    {
        //        if (m_Degree != value && value > 0 && value < m_ControlPoints.Count)
        //        {
        //            m_Degree = value;
        //        }
        //    }
        //}

        //// Private 

        //// Methods 

        //private Vector3 ComputeBSlineVertex(int idSegment, float t)
        //{
        //    Vector3 vertex = new Vector3();


        //    for (int i = idSegment; i <= idSegment + Degree; i++)
        //    {
        //        double riesenfeld = MathCommon.PolynomeRiesenfeld(i - idSegment, Degree, t);

        //        int val = i;
        //        if (i < 0)
        //        {
        //            if (Clamp)
        //            {
        //                val = 0;
        //            }
        //            else
        //            {
        //                val = m_ControlPoints.Count + i;
        //            }

        //        }
        //        if (i >= m_ControlPoints.Count)
        //        {
        //            if (Clamp)
        //            {
        //                val = m_ControlPoints.Count - 1;
        //            }
        //            else
        //            {
        //                val = i - m_ControlPoints.Count;
        //            }
        //        }
        //        vertex.x += (float)riesenfeld * m_ControlPoints[val].x;
        //        vertex.y += (float)riesenfeld * m_ControlPoints[val].y;
        //        vertex.z += (float)riesenfeld * m_ControlPoints[val].z;
        //    }
        //    return vertex;
        //}

        //// Datas

        //private int m_Degree;
        //private bool m_IsLoop;
        //private bool m_IsClamp;
  //  }

}
