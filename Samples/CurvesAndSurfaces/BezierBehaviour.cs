using System;
using System.Collections.Generic;
using SharpDX;

using Troll3D;
using Troll3D.Components;

namespace CurvesAndSurfaces
{
    public class BezierBehaviour : Behaviour
    {
        
        public override void Initialize()
        {
            m_lineRenderer = Entity.AddComponent<LineRenderer>();
            m_lineRenderer.material_ = new MaterialDX11( "vDefault.cso", "pUnlit.cso", "gDefaultLine.cso" );
            m_lineRenderer.material_.SetMainColor( 0.0f, 0.0f, 1.0f, 1.0f );
            InitializeControlsPoints();
            RebuildCurve();
        }

        public override void Update()
        {
            BuildBSpline();
        }

        public override void OnMouseDown( MouseEvent e )
        {
                Vector3 point = new Vector3( e.mouse_.x / ( float )Screen.Instance.Width * 2 - 1.0f,
                    1.0f - e.mouse_.y / ( float )Screen.Instance.Height * 2, 0.0f );
                point.Z = 0.0f;
                AddControlPoint( point );
        }

        public void AddControlPoint( Vector3 position )
        {
            Entity pointControler   = new Entity();
            pointControler.transform_.SetPosition( position );
            pointControler.transform_.SetScale( 0.1f, 0.1f, 1.0f );

            MeshRenderer mr =  pointControler.AddComponent<MeshRenderer>();
            mr.material_    = new MaterialDX11();
            mr.model_       = Quad.GetMesh();
            mr.material_.SetMainColor( 1.0f, 0.0f, 1.0f, 1.0f );

            Entity.Append( pointControler );
            //Entity.transform_.SetScale( 0.1f, 0.1f, 0.5f );
        }

        private void InitializeControlsPoints()
        {
            Vector3[] points = new Vector3[4]{
                        new Vector3(-1.0f, 0.0f, 0.0f),
                        new Vector3(-1.0f, 1.0f, 0.0f),
                        new Vector3(1.0f, 1.0f, 0.0f),
                        new Vector3(1.0f, 0.0f, 0.0f)
                    };

            for ( int i = 0; i < points.Length; i++ )
            {
                AddControlPoint( points[i] );
            }
        }

        

        private void BuildBSpline()
        {
            BSpline spline = new BSpline();


            for ( int i = 0; i < Entity.sons_.Count; i++ )
            {
                spline.AddControlPoint( Entity.sons_[i].transform_.WorldPosition() );
            }
            spline.Degree = 2;
            spline.Loop = false;
            spline.Clamp = true;
            
            spline.SetDiscretisation( 100 );
            spline.ConstructSpline();

            BuildCurve(spline.GetCurve());
            

        }
        private void RebuildCurve()
        {
            m_bezier = new BezierCurve();

            for ( int i = 0; i < Entity.sons_.Count; i++ )
            {
                m_bezier.AddControlPoint( Entity.sons_[i].transform_.WorldPosition() );
            }

            m_bezier.SetDiscretisation( 100 );
            m_bezier.ConstructSpline();
            BuildCurve(m_bezier.GetCurve());
        }

        /// <summary>
        /// Construit la courbe pour l'affichage
        /// </summary>
        private void BuildCurve( List<Vector3> points )
        {
            List<StandardVertex> vertices = new List<StandardVertex>();

            LineMesh linemesh = new LineMesh();

            for ( int i = 0; i < points.Count - 1; i++ )
            {
                vertices.Add( new StandardVertex( points[i] ) );
                vertices.Add( new StandardVertex( points[i + 1] ) );
            }

            m_lineRenderer.Vertices = vertices;
            m_lineRenderer.UpdateRenderer();
        }


        private BezierCurve m_bezier;
        private LineRenderer m_lineRenderer;
        private int m_selectedControlPoint;

    }
}
