using System;
using System.Collections.Generic;
using SharpDX;
using SharpDX.Direct3D11;
using SharpDX.DXGI;

namespace Troll3D{

    /// <summary>
    /// L'objectif d'une renderTexture est de récupérer l'image généré par une "caméra" dans une texture qui
    /// pourra ensuite être appliqué à un objet ( et potentiellement faire l'objet de traitement supplémentaires (post processing)
    /// Pour créer une renderTexture, on commence par créer une Texture2D pour laquelle on précise l'usage 
    /// en " RENDER_TARGET" et SHADER_RESOURCE
    /// Ensuite, on crée un nouveau RenderTarget et ShaderResourceView utilisable par des materials
    /// </summary>
    public class RenderTexture{

        /// <summary> Creation d'une RenderTexture directement à partir de la taille du viewport de la vue passé en paramètre </summary>
        public RenderTexture(View view){
            Initialize(view.viewport.Width, view.viewport.Height);
        }

        /// <summary> Creation d'une RenderTexture aux dimensions spécifiés </summary>
        public RenderTexture(int width, int height){
            Initialize(width, height);
        }

        public void Dispose(){
            Utilities.Dispose<ShaderResourceView>(ref m_ShaderResourceView);
            Utilities.Dispose<Texture2D>(ref m_Texture);
            Utilities.Dispose<RenderTargetView>(ref m_RenderTargetView);
        }


        /// <summary> Il faut invoquer cette méthode avant de commencer à dessiner des objets pour  les dessiner dans la texture </summary>
        public void BeginRender(){

            ApplicationDX11.Instance.devicecontext_.OutputMerger.SetTargets(
                ApplicationDX11.Instance.stencilmanager_.depthstencilview,
                m_RenderTargetView);

            ClearRenderTargetView();

            ApplicationDX11.Instance.stencilmanager_.Clear();
            ApplicationDX11.Instance.SetViewport(0, 0, m_Width, m_Height);
        }

        public void ClearRenderTargetView(){
            //ApplicationDX11.ClearRenderTargetView(m_RenderTargetView);
        }

        public ShaderResourceView GetSRV(){
            return m_ShaderResourceView;
        }

        public RenderTargetView GetRenderTargetView(){
            return m_RenderTargetView;
        }

        private void Initialize(int width, int height){
            m_Width     = width;
            m_Height    = height;

            InitializeTargetTexture();
            InitializeRenderTargetView();
            InitializeShaderResourceView();
        }

        /// <summary>
        /// Initialisation de la texture qui sera utilisé pour récupérer les opérations d'affichage.
        /// On remarque que Bindflag est set à RenderTarget en plus de ShaderResource pour pouvoir utilisé
        /// cette texture dans un ShaderResourceView et donc l'appliquer sur un autre objet
        /// </summary>
        private void InitializeTargetTexture(){

            m_Texture = new Texture2D(ApplicationDX11.Device(), new Texture2DDescription{
                ArraySize            = 1,
                Width                = m_Width,
                Height               = m_Height,
                MipLevels            = 1,
                Format               = Format.R32G32B32A32_Float,
                Usage                = ResourceUsage.Default,
                BindFlags            = BindFlags.RenderTarget | BindFlags.ShaderResource,
                CpuAccessFlags       = CpuAccessFlags.None,
                OptionFlags          = 0,
                SampleDescription    = new SampleDescription(){
                    Count = 1
                }
            });
        }

        private void InitializeRenderTargetView(){

            m_RenderTargetView = new RenderTargetView(ApplicationDX11.Instance.device_, m_Texture,  new RenderTargetViewDescription{
                Format = Format.R32G32B32A32_Float,
                Dimension = RenderTargetViewDimension.Texture2D,
                Texture2D = new RenderTargetViewDescription.Texture2DResource()
                {
                    MipSlice = 0
                }
            });
        }

        private void InitializeShaderResourceView(){

            m_ShaderResourceView = new ShaderResourceView(ApplicationDX11.Instance.device_, m_Texture, new ShaderResourceViewDescription{
                Dimension   = SharpDX.Direct3D.ShaderResourceViewDimension.Texture2D,
                Format      = Format.R32G32B32A32_Float,
                Texture2D   = new ShaderResourceViewDescription.Texture2DResource(){
                    MipLevels = 1,
                    MostDetailedMip = 0
                }
            });
        }

        private Texture2D                       m_Texture;
        private ShaderResourceView              m_ShaderResourceView;
        private RenderTargetView                m_RenderTargetView;

        private int m_Width;
        private int m_Height;


    }
}
