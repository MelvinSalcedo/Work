using System;
using System.Collections.Generic;
using SharpDX;
using SharpDX.Direct3D11;
using SharpDX.DXGI;

using Troll3D.Rendering;

namespace Troll3D
{
    /// <summary>
    /// L'objectif d'une renderTexture est de récupérer l'image généré par une "caméra" dans une texture qui
    /// pourra ensuite être appliqué à un objet ( et potentiellement faire l'objet de traitement supplémentaires (post processing)
    /// Pour créer une renderTexture, on commence par créer une Texture2D pour laquelle on précise l'usage 
    /// en " RENDER_TARGET" et SHADER_RESOURCE
    /// Ensuite, on crée un nouveau RenderTarget et ShaderResourceView utilisable par des materials
    /// </summary>
    public class RenderTexture : RenderTarget
    {
        /// <summary>
        /// Creation d'une RenderTexture aux dimensions spécifiés
        /// </summary>
        public RenderTexture( int width, int height ) : base(width,height)
        {
            Initialize();

            //ApplicationDX11.Instance.RenderTextures.Add( this );
        }

        public void Dispose()
        {
            Utilities.Dispose<ShaderResourceView>( ref m_ShaderResourceView );
            Utilities.Dispose<Texture2D>( ref m_Texture );
            Utilities.Dispose<RenderTargetView>( ref RenderTargetView );
        }

        private void Initialize()
        {
            InitializeTargetTexture();
            InitializeRenderTargetView();
            InitializeShaderResourceView();
        }

        /// <summary>
        /// Initialisation de la texture qui sera utilisé pour récupérer les opérations d'affichage.
        /// On remarque que Bindflag est set à RenderTarget en plus de ShaderResource pour pouvoir utilisé
        /// cette texture dans un ShaderResourceView et donc l'appliquer sur un autre objet
        /// </summary>
        private void InitializeTargetTexture()
        {
            m_Texture = new Texture2D
            ( 
                ApplicationDX11.Instance.Device,
                new Texture2DDescription
                {
                    ArraySize = 1,
                    Width = Width,
                    Height = Height,
                    MipLevels = 1,
                    Format = Format.R32G32B32A32_Float,
                    Usage = ResourceUsage.Default,
                    BindFlags = BindFlags.RenderTarget | BindFlags.ShaderResource,
                    CpuAccessFlags = CpuAccessFlags.None,
                    OptionFlags = 0,
                    SampleDescription = new SampleDescription()
                    {
                        Count = 1
                    }
                } 
            );
        }

        /// <summary>
        /// Initialise le RenderTargetView de la RenderTexture. C'est cet objet qui sera
        /// utilisé pour préciser ou l'on souhaite dessiner
        /// </summary>
        private void InitializeRenderTargetView()
        {
            RenderTargetView = new RenderTargetView
            (
                ApplicationDX11.Instance.Device,
                m_Texture,
                new RenderTargetViewDescription
                {
                    Format = m_Texture.Description.Format,
                    Dimension = RenderTargetViewDimension.Texture2D,
                    Texture2D = new RenderTargetViewDescription.Texture2DResource()
                    {
                        MipSlice = 0
                    }
                } 
            );
        }

        /// <summary>
        /// Initialisation du ShaderResourceView. Cet objet peut être utilisé pour récupérer 
        /// et plaquer la texture sur un objet de la scène
        /// </summary>
        private void InitializeShaderResourceView()
        {
            SRV = new ShaderResourceView
            ( 
                ApplicationDX11.Instance.Device,
                m_Texture,
                new ShaderResourceViewDescription
                {
                    Dimension = SharpDX.Direct3D.ShaderResourceViewDimension.Texture2D,
                    Format = m_Texture.Description.Format,
                    Texture2D = new ShaderResourceViewDescription.Texture2DResource()
                    {
                        MipLevels = 1,
                        MostDetailedMip = 0
                    }
                } 
            );
        }

        public ShaderResourceView SRV { get; private set; }
        private Texture2D m_Texture;
        private ShaderResourceView m_ShaderResourceView;
    }
}
