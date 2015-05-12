using System;
using System.Collections.Generic;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using SharpDX;

using Troll3D.Rendering;
using Troll3D.Components;

namespace Troll3D
{
    /// <summary>
    /// La class ImageProcessing va se charger de créer une vue dont la projection est de nature orthogonale
    /// et un quad aux proportions demandés et fera x passes pour "traiter" la texture d'entrée
    /// </summary>
    public class ImageProcessing
    {
        /// <summary>
        /// Crée une image, "vide" (comprendre tout pixel à 0 0 0 0), pour effectuer ses traitements
        /// </summary>
        public ImageProcessing( int width, int height )
        {

            TImage image = new TImage( width, height );

            for ( int i = 0; i < image.Height; i++ )
            {
                for ( int j = 0; j < image.Width; j++ )
                {
                    image.SetPixel( j, i, 0.0f, 1.0f, 0.0f, 1.0f );
                }
            }

            m_initialImage = new ShaderResourceView( ApplicationDX11.Instance.Device, image.GetTexture2D() );
            Initialize( width, height );
        }

        /// <summary>
        /// Traite une image existante
        /// </summary>
        public ImageProcessing( int width, int height, ShaderResourceView srv )
        {

            // Initialisation des passes et des RenderTextures (il faudra peut être améliorer le schmlblik pour
            //ne lui faire accepter que des Pixel Shader en paramètre de "passe"

            m_initialImage = srv;
            Initialize( width, height );
        }

        private void Initialize( int width, int height )
        {
            passes_ = new List<MaterialDX11>();

            // On Initialise le transform, puisqu'on se trouve en dehors de la scène
            transform_ = new Transform();
            transform_.SetScale( width, height, 1.0f );

            // Initialisation du material servant à dessiner le quad final
            m_DrawingMaterial = new MaterialDX11( "vDefault.cso", "pUnlit.cso", "gDefault.cso" );


            m_currentoutput = 0;
            m_output = new RenderTexture( width, height );
            m_outputSwitch = new RenderTexture( width, height );

            // Initialisation du Quad que l'on utilise pour dessiner
            model_ = new MeshRenderer( m_DrawingMaterial, Quad.GetMesh());
            // Initialisation de la vue utilisé pour correspondre aux dimensions

            PostProcessingView = new View( new Transform(), new OrthoProjection( width, height, 0.1f, 100.0f ) );

            Quaternion xquat = Quaternion.RotationAxis( new Vector3( 1.0f, 0.0f, 0.0f ), 0 * 0.01f );
            Quaternion yquat = Quaternion.RotationAxis( new Vector3( 0.0f, 1.0f, 0.0f ), 0 * 0.01f );

            Quaternion rotQuat = Quaternion.Multiply( xquat, yquat );
            Matrix mview = Matrix.AffineTransformation( 1.0f, rotQuat, new Vector3( 00.0f, 0.0f, 10.0f ) );

            PostProcessingView.Transformation.SetLocalMatrix( mview );
            PostProcessingView.Transformation.Update();
        }

        public void Resize( int width, int height, ShaderResourceView srv )
        {


            m_initialImage = srv;
            transform_ = new Transform();
            transform_.SetScale( width, height, 1.0f );

            m_output = new RenderTexture( width, height );
            m_outputSwitch = new RenderTexture( width, height );

            PostProcessingView = new View( new Transform(), new OrthoProjection( width, height, 0.1f, 100.0f ) );

            PostProcessingView.viewport = new Troll3D.Viewport( 0, 0, width, height );
            Quaternion xquat = Quaternion.RotationAxis( new Vector3( 1.0f, 0.0f, 0.0f ), 0 * 0.01f );
            Quaternion yquat = Quaternion.RotationAxis( new Vector3( 0.0f, 1.0f, 0.0f ), 0 * 0.01f );

            Quaternion rotQuat = Quaternion.Multiply( xquat, yquat );
            Matrix mview = Matrix.AffineTransformation( 1.0f, rotQuat, new Vector3( 00.0f, 0.0f, 10.0f ) );

            PostProcessingView.Transformation.SetLocalMatrix( mview );
            PostProcessingView.Transformation.Update();
        }

        // Methods

        /// <summary>
        /// Comme on travail uniquement sur les pixels de l'image, on ne rajoute qu'un PixelShader
        /// </summary>
        /// <param name="material"></param>
        public void AddPasse( MaterialDX11 material )
        {
            passes_.Add( material );
        }

        public void UpdatePasses()
        {
            m_input = m_initialImage;

            for ( int i = 0; i < passes_.Count; i++ )
            {
                UpdatePasse( i );
                if ( i + 1 < passes_.Count )
                {

                    m_input = GetCurrentOutput().GetSRV();

                    // On Switch les Render to Texture de manière à ne pas avoir à en stocker 3 milliards
                    SwitchCurrentOutput();
                }
            }
        }

        public ShaderResourceView GetFinalSRV()
        {
            if ( passes_.Count > 0 )
            {
                return GetCurrentOutput().GetSRV();
            }
            else
            {
                return m_initialImage;
            }
        }

        /// <summary>
        /// Dessine un Quad 
        /// </summary>
        public void Draw()
        {
            ApplicationDX11.Instance.DeviceContext.OutputMerger.BlendState = ApplicationDX11.Instance.LastRenderToTextureBlendState;

            View.Current = PostProcessingView;

            if ( m_DrawingMaterial.textures_.Count == 0 )
            {
                m_DrawingMaterial.AddShaderResourceView( GetFinalSRV() );
            }
            else
            {
                m_DrawingMaterial.textures_[0] = GetFinalSRV();
            }

            model_.material_ = m_DrawingMaterial;
            transform_.Update();
            //model_.Draw(  );

        }

        // Datas

        // Bien qu'on ne travaille que sur le pixel Shader, le material contient aussi des fonctions bien pratique
        // pour définir un constant Buffer
        public List<MaterialDX11> passes_;

        public List<RenderTexture> rendertextures_;
        public Transform transform_;
        public View PostProcessingView;
        public bool IsActive;
        public MeshRenderer model_;


        // Private

        // Methods

        private void SwitchCurrentOutput()
        {
            m_currentoutput++;
            m_currentoutput = m_currentoutput % 2;
        }

        private RenderTexture GetCurrentOutput()
        {
            if ( m_currentoutput == 0 )
            {
                return m_output;
            }
            else
            {
                return m_outputSwitch;
            }
        }

        private void UpdatePasse( int index )
        {


            // On commence par récupérer le matérial que l'on souhaite appliquer à l'image
            MaterialDX11 mat = passes_[index];

            //// On met à jour la vue

            View.Current = PostProcessingView;

            ApplicationDX11.Instance.DeviceContext.OutputMerger.BlendState = ApplicationDX11.Instance.RenderToTextureBlendState;

            //// On définit le renderTargetView pour bien dessiner dans la bonne RenderTexture
            GetCurrentOutput().BeginRender();

            ApplicationDX11.Instance.DeviceContext.ClearRenderTargetView(
               GetCurrentOutput().GetRenderTargetView(),
               new Color4( 0.0f, 1.0f, 0.0f, 1.0f ) );

            if ( mat.textures_.Count == 0 )
            {
                mat.AddShaderResourceView( m_input );
            }
            else
            {
                mat.textures_[0] = m_input;
            }


            SamplerState state = new SamplerState( ApplicationDX11.Instance.Device, new SamplerStateDescription()
            {
                AddressU = TextureAddressMode.Clamp,
                AddressV = TextureAddressMode.Clamp,
                AddressW = TextureAddressMode.Clamp,
                BorderColor = new Color4( 0.0f, 1.0f, 0.0f, 1.0f ),
                ComparisonFunction = Comparison.LessEqual,
                Filter = Filter.MinLinearMagMipPoint,
                MaximumAnisotropy = 0,
                MaximumLod = 0,
                MinimumLod = 0,
                MipLodBias = 0
            } );
            if ( mat.samplers.Count == 0 )
            {
                mat.samplers.Add( state );
            }
            else
            {
                mat.samplers[0] = state;
            }

            // On dessine
            model_.material_ = mat;

            transform_.Update();
            //model_.Draw(  );
        }

        // Data

        private ShaderResourceView m_initialImage;
        private ShaderResourceView m_input;

        private int m_currentoutput;

        // On utilise 2 renderTexture qu'on alterne pour pouvoir dessiner
        private RenderTexture m_output;
        private RenderTexture m_outputSwitch;

        // Material utilisé pour dessiner le quad final
        private MaterialDX11 m_DrawingMaterial;
    }
}
