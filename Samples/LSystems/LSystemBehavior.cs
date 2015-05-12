using System;
using System.Collections.Generic;
using Troll3D;
using SharpDX;
using Troll3D.Common.LSystems;
using Troll3D.Components;

namespace LSystems
{
    public class LSystemBehavior : Behaviour
    {
        public override void Initialize()
        {
            m_renderer = Entity.AddComponent<LineRenderer>();
            m_renderer.material_ = new MaterialDX11( "vDefault.cso", "pLSystem.cso", "gDefaultLine.cso" );
        }

        public override void OnKeyDown( KeyboardEvent e )
        {
            if ( e.keycode_ == KeyCode.Key_1 )
            {
                currentsystemindex = 1;
                BuildSierpinskiTriangle();
            }
            if ( e.keycode_ == KeyCode.Key_2 )
            {
                currentsystemindex = 2;
                BuildPythagorasTree();
            }
            if ( e.keycode_ == KeyCode.Key_3 )
            {
                currentsystemindex = 3;
                BuildDragonCurve();
            }
            if ( e.keycode_ == KeyCode.Key_4 )
            {
                currentsystemindex = 4;
                BuildFractalPlant();
            }
            if ( e.keycode_ == KeyCode.Key_5 )
            {
                currentsystemindex = 5;
                BuildOtherFractalPlant();
            }
            if ( e.keycode_ == KeyCode.Key_6 )
            {
                currentsystemindex = 6;
                BuildKoch();
            }
            if ( e.keycode_ == KeyCode.Key_7 )
            {
                currentsystemindex = 7;
                BuildThirdPlant();
            }

            if ( e.keycode_ == KeyCode.Key_I )
            {
                Iterate();
            }

            if ( e.keycode_ == KeyCode.Key_D )
            {
                if ( startAnimation == true )
                {
                    animationValue = 1.0f;
                    ( ( CBuffer<float> )( m_renderer.material_.constantBuffer ) ).UpdateStruct( animationValue );
                    startAnimation = false;
                }
            }
            if ( e.keycode_ == KeyCode.Key_S )
            {
                startAnimation = true;
            }
            if ( e.keycode_ == KeyCode.Key_R )
            {
                startAnimation = false;
                animationValue = 0.0f;
                ( ( CBuffer<float> )( mat.constantBuffer ) ).UpdateStruct( animationValue );
            }

            if ( e.keycode_ == KeyCode.Key_O )
            {
                m_segmentLength_ += 0.01f;
                BuildMesh();
            }
            if ( e.keycode_ == KeyCode.Key_L )
            {
                m_segmentLength_ -= 0.01f;
                BuildMesh();
            }

            if ( e.keycode_ == KeyCode.Key_P )
            {
                m_initialAngle += 0.01f;
                BuildMesh();
            }
            if ( e.keycode_ == KeyCode.Key_M )
            {
                m_initialAngle -= 0.01f;
                BuildMesh();
            }

            if ( e.keycode_ == KeyCode.Key_U )
            {
                m_angleValue += 0.01f;
                BuildMesh();
            }
            if ( e.keycode_ == KeyCode.Key_J )
            {
                m_angleValue -= 0.01f;
                BuildMesh();
            }
        }

        public override void Update()
        {
            if ( startAnimation )
            {
                animationValue += 0.0003f;
                ( ( CBuffer<float> )( m_renderer.material_.constantBuffer ) ).UpdateStruct( animationValue );
            }
        }

        public void Iterate()
        {
            if ( m_currentLSystem != null )
            {
                m_currentLSystem.ApplyRules();

                if ( currentsystemindex == 1 )
                {
                    m_angleValue = -m_angleValue;
                }
                BuildMesh();
            }
        }

        public List<Vector3> GenerateLineFromLSystem( LSystem lsystem, Vector3 startingpoint, float angle, float initialangle )
        {
            Vector3 startingpoint_ = new Vector3( 0.0f, 0.0f, 0.0f );


            List<Vector3> points = new List<Vector3>();
            Vector3 lastpoint = startingpoint;

            float initialAngle = initialangle;

            float currentangle = initialAngle;
            Stack<float> anglestack = new Stack<float>();
            Stack<Vector3> positionstack = new Stack<Vector3>();
            Vector3 currentPoint = Vector3.Zero;

            // Turtle representation
            for ( int i = 0; i < lsystem.current_.Length; i++ )
            {

                switch ( lsystem.current_[i] )
                {

                    // Signifie que l'on tourne positivement l'angle
                    case '+':
                        currentangle += angle;
                        break;

                    // Signifie qu'on tourne négativement l'angle
                    case '-':
                        currentangle -= angle;
                        break;

                    // On sauvegarde l'angle et la position actuel, et on tourne l'angle positivement
                    case '[':
                        anglestack.Push( currentangle );
                        positionstack.Push( currentPoint );
                        //currentangle+=angle;
                        break;

                    // On retourne au dernier état enregistré par '[' et on tourne négativement l'angle
                    case ']':
                        // On revient au précédent point enregistré par '['
                        currentangle = anglestack.Pop();
                        currentPoint = positionstack.Pop();
                        //currentangle    -=angle;
                        break;

                    // Dans le cas ou on  F ou G on dessine un segment
                    case 'F':
                    case 'G':

                        Vector3 newpoint = new Vector3(
                                    currentPoint.X + ( float )Math.Cos( currentangle ) * m_segmentLength_,
                                    currentPoint.Y + ( float )Math.Sin( currentangle ) * m_segmentLength_,
                                    0.0f
                                    );

                        points.Add( currentPoint );
                        points.Add( newpoint );
                        currentPoint = newpoint;
                        break;
                };
            }
            return points;
        }

        public LSystem currentLystem;

        public void BuildPythagorasTree()
        {
            m_currentLSystem = new LSystem( "FG", "", "F" );
            m_currentLSystem.rules_.Add( new LSystemRule( 'F', "G[+F]-F" ) );
            m_currentLSystem.rules_.Add( new LSystemRule( 'G', "GG" ) );
            m_initialAngle = 3.141592f / 2.0f;
            m_angleValue = 3.141592f / 3.0f;
            BuildMesh();
        }
        public void BuildSierpinskiTriangle()
        {
            m_currentLSystem = new LSystem( "FG", "[]", "F" );
            m_currentLSystem.rules_.Add( new LSystemRule( 'F', "G-F-G" ) );
            m_currentLSystem.rules_.Add( new LSystemRule( 'G', "F+G+F" ) );
            m_initialAngle = 0.0f;
            m_angleValue = 3.141592f / 3.0f;
            BuildMesh();
        }
        public void BuildKoch()
        {
            m_currentLSystem = new LSystem( "F", "+-", "F+F+F+F+F+F" );
            m_currentLSystem.rules_.Add( new LSystemRule( 'F', "F+F-F-F+F" ) );
            m_initialAngle = 0.0f;
            m_angleValue = 3.141592f / 3.0f;
            BuildMesh();
        }
        public void BuildDragonCurve()
        {
            m_currentLSystem = new LSystem( "XY", "F+-", "FX" );
            m_currentLSystem.rules_.Add( new LSystemRule( 'X', "X+YF+" ) );
            m_currentLSystem.rules_.Add( new LSystemRule( 'Y', "-FX-Y" ) );
            m_initialAngle = 0.0f;
            m_angleValue = 3.141592f / 2.0f;
            BuildMesh();
        }
        public void BuildFractalPlant()
        {
            m_currentLSystem = new LSystem( "XF", "+-[]", "X" );
            m_currentLSystem.AddRule( 'X', "F-[[X]+X]+F[+FX]-X" );
            m_currentLSystem.AddRule( 'F', "FF" );
            m_initialAngle = 3.141592f / 3.0f;
            m_angleValue = 3.141592f / 10.0f;
            BuildMesh();
        }
        public void BuildOtherFractalPlant()
        {
            m_currentLSystem = new LSystem( "F", "+-[]", "F" );
            m_currentLSystem.AddRule( 'F', "F[+F]F[-F]F" );
            m_initialAngle = 3.141592f / 2.0f;
            m_angleValue = 3.141592f / 10.0f;
            BuildMesh();
        }
        public void BuildThirdPlant()
        {
            m_currentLSystem = new LSystem( "FX", "+-[]", "X" );
            m_currentLSystem.AddRule( 'X', "F[+X][-X]FX" );
            m_currentLSystem.AddRule( 'F', "FF" );
            m_initialAngle = 3.141592f / 2.0f;
            m_angleValue = 3.141592f / 10.0f;
            BuildMesh();
        }

        public float m_initialAngle;
        public float m_angleValue;
        public LSystem m_currentLSystem;

        public void BuildMesh()
        {

            List<Vector3> points = GenerateLineFromLSystem( m_currentLSystem, new Vector3( 0.0f, 0.0f, 0.0f ), m_angleValue, m_initialAngle );

            if ( points.Count > 0 )
            {
                m_renderer.material_ = new MaterialDX11( "vDefault.cso", "pLsystem.cso", "gDefaultLine.cso" );
                m_renderer.material_.SetMainColor( 0.0f, 1.0f, 0.0f, 1.0f );
                m_renderer.material_.AddConstantBuffer<float>( 1.0f );


                List<StandardVertex> linemesh = new List<StandardVertex>();


                for ( int i = 0; i < points.Count; i++ )
                {

                    StandardVertex va = new StandardVertex( points[i] );
                    va.Uv = new Vector2( ( float )i / ( float )points.Count, 0.0f );

                    linemesh.Add( va );
                }


                m_renderer.Vertices = linemesh;
                m_renderer.UpdateRenderer();
            }

        }

        public float m_segmentLength_ = 0.1f;
        float animationValue = 0.0f;
        public bool startAnimation;
        public int currentsystemindex = -1;


        MaterialDX11 mat;
        LineRenderer m_renderer;


    }
}
