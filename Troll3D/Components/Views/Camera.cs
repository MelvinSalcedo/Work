using System;
using System.Collections.Generic;
using SharpDX;
using SharpDX.Direct3D11;

using Troll3D.Rendering;

namespace Troll3D.Components
{

    /// <summary> 
    /// Une caméra est uniquement utilisé pour rendre la scène depuis son point de vue 
    /// </summary>
    public class Camera : TComponent
    {
        /// <summary>
        /// Retourne la caméra définit comme étant la caméra principale. Les applications ayant rarement besoin de plus d'une caméra,
        /// ce raccourcis peut s'avérer des plus pratique
        /// </summary>
        public static Camera Main;

        /// <summary> 
        /// Enregistre les différentes caméras initialisé dans la scène 
        /// </summary>
        public static List<Camera> Cameras = new List<Camera>();

        /// <summary>
        /// Construit un nouvel objet de type camera
        /// </summary>
        public Camera()
        {
            Type = ComponentType.Camera;
        }

        public override void Attach( Entity entity )
        {
            Entity = entity;
            m_transform = entity.transform_;
            m_transform.IsViewMatrix = true;
        }

        public void Initialize( Projection projection )
        {
            if ( Main == null )
            {
                Main = this;
                SetRenderTarget( ApplicationDX11.Instance.MainRenderTarget );
            }

            m_View = new View( m_transform, projection );
            m_StencilManager = new StencilManager( Screen.Instance.Width, Screen.Instance.Height );
            IsActive = true;
            HasRenderTexture = false;

            Cameras.Add( this );
        }

        public void DisplayFrustum()
        {
            Entity entity = new Entity( Entity );
            MeshRenderer mr =  entity.AddComponent<MeshRenderer>();
            mr.material_ = new MaterialDX11();
            mr.SetFillMode( FillMode.Wireframe );
            mr.material_.SetMainColor( 1.0f, 1.0F, 1.0F, 1.0F );
            mr.model_ = ProjectionMesh.GetModel( GetProjection() );
        }

        public override void Update()
        {
            m_View.Transformation = m_transform;
        }

        public void SetCurrentView()
        {
            View.Current = m_View;
        }

        public void SetViewport( Troll3D.Viewport viewport )
        {
            m_View.viewport = viewport;
            if ( m_View.projection_.GetProjectionType() == ProjectionType.FrustumProjection )
            {
                m_View.SetFrustsumProjection(
                    m_View.GetFrustumProjection().GetFieldOfView(),
                    ( float )viewport.Width / ( float )viewport.Height,
                    m_View.GetFrustumProjection().GetNearPlane(),
                    m_View.GetFrustumProjection().GetFarPlane()
                    );
            }
        }

        public void SetViewport( int offsetx, int offsety, int width, int height )
        {
            SetViewport( new Troll3D.Viewport( offsetx, offsety, width, height ) );
        }

        public Troll3D.Viewport GetViewport() { return m_View.viewport; }
        public void SetProjection( Projection projection ) { m_View.projection_ = projection; }

        /// <summary>
        /// Lie un renderTarget avec sa caméra
        /// </summary>
        /// <param name="rt"></param>
        public void SetRenderTarget( RenderTarget rt )
        {
            rt.Camera = this;
        }

        public ShaderResourceView GetRenderTextureSRV()
        {
            return RenderTexture.SRV;
        }

        public ProjectionType GetProjectionType()
        {
            return m_View.projection_.GetProjectionType();
        }

        public OrthoProjection GetOrthoProjection()
        {
            return m_View.GetOrthoProjection();
        }

        public FrustumProjection GetFrustumProjection()
        {
            return m_View.GetFrustumProjection();
        }

        public Projection GetProjection()
        {
            return m_View.GetProjection();
        }

        /// <summary> Détermine si la caméra doit être affiché ou non (possibilité de splitter l'écran en utilisant différents viewport)</summary>
        public bool IsActive;

        /// <summary> Détermine si la caméra affiche son contenu à l'intérieur d'une texture </summary>
        public bool HasRenderTexture;

        /// <summary>
        /// Lien vers une éventuelle RenderTexture ou "rendre" la caméra
        /// </summary>
        public RenderTexture RenderTexture{get;private set;}

        /// <summary>
        /// Gère le DepthStencilBuffer de la caméra
        /// </summary>
        public StencilManager m_StencilManager;

        public Transform m_transform;
        public Entity Entity;
        public Skybox Skybox;

        private View m_View;

    }
}
