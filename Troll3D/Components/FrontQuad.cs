using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;

namespace Troll3D.Components
{
    /// <summary>
    /// Le but d'un composant de type FrontQuad est d'afficher un quad potentiellement texturable
    /// dans les coordonnées [0;1] (sans les transformations)
    /// </summary>
    public class FrontQuad : TComponent, IRenderable
    {
        public FrontQuad()
        {
            Type = ComponentType.FrontQuad;
            Initialize();
        }

        public override void Attach( Entity entity )
        {
            Scene.CurrentScene.Renderables.Add( this );
        }

        public void SetQuad( int x, int y, int width, int height )
        {
            float ratio = ( float )Screen.Instance.Width / ( float )Screen.Instance.Height;

            float fwidth    = (float)width / (float)Screen.Instance.Width;
            fwidth = fwidth * 2.0f;
            float fheight   = ( float )height / ( float )Screen.Instance.Height;

            float fx = ( ( float )x / ( float )Screen.Instance.Width ) * 2.0f - 1.0f + fwidth/2.0f;
            float fy = ( ( float )y / ( float )Screen.Instance.Height ) * 2.0f - 1.0f + fheight * ratio / 2.0f; ;

            m_transform.SetScale( fwidth, fheight * ratio, 1.0f );
            m_transform.SetPosition( fx, fy, 0.0f );
        }

        public override void Update()
        {
            
        }

        public void Render()
        {
            ApplicationDX11.Instance.DeviceContext.Rasterizer.State = m_meshRenderer.State;
            m_transform.Update();
            m_transform.SetWVP( new WorldViewProj(){
                CameraPosition = Vector3.Zero,
                Projection       = Matrix.Identity,
                View             = Matrix.Identity,
                World            = m_transform.worldmatrix_,
                WorldInverse     = Matrix.Identity
            });

            // On envoie les informations de transformation
            m_transform.SendConstantBuffer();
            // On envoie les informations portant sur la lumière
            LightManager.Instance.SendLights();

            // On envoie les informations de material / effet
            if ( m_meshRenderer.material_ != null )
            {
                m_meshRenderer.material_.Begin();
                if ( m_meshRenderer.model_!= null )
                {
                    // On affiche le modèle
                    m_meshRenderer.model_.Render();
                }

                m_meshRenderer.material_.End();
            }
        }

        private void Initialize()
        {
            m_transform         = new Transform();
            m_meshRenderer      = new MeshRenderer();
            Material = new MaterialDX11( "vDefault.cso", "pUnlit.cso" );
            m_meshRenderer.material_ = Material ;
            m_meshRenderer.model_ = Quad.GetMesh();
        }

        public MaterialDX11     Material;

        public  MeshRenderer     m_meshRenderer;
        public Transform        m_transform;
    }
}
