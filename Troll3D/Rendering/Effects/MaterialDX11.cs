using System;
using System.Collections.Generic;
using SharpDX.Direct3D11;
using SharpDX.D3DCompiler;
using SharpDX;

namespace Troll3D
{

    /// <summary>Un Matérial sert à gérer la communication entre l'application et le processeur graphique
    ///  en enregistrant les shaders et les différentes variables associés ( Variables constantes)
    /// </summary>
    public class MaterialDX11
    {

        private static string RealPath( string path )
        {
#if DEBUG
            path = DebugPath + path;
#else
                path = ReleasePath   + path ;
#endif

            return path;
        }

        public static PixelShader LoadPixelShader( string filename )
        {
            return new PixelShader( ApplicationDX11.Instance.device_, ShaderBytecode.FromFile( RealPath( filename ) ) );
        }

        /// <summary>
        /// Le debug Path est utilisé pour récupérer les shaders directement dans le répertoire de ressource
        /// de manière à éviter les manipulations de fichier ressource sous C# et de faciliter 
        /// la mise en place d'une éventuelle application
        /// </summary>
        public static string DebugPath = "D:\\Work\\Resources\\Shaders\\";
        public static string ReleasePath = "D:\\Work\\Resources\\Shaders\\";

        public MaterialDX11( string vs = "vDefault.cso", string ps = "pUnlit.cso", string gs = "gDefault.cso", VertexTypeD11 type = VertexTypeD11.STANDARD_VERTEX )
        {
            LoadShaders( vs, ps, gs );
            SetInputLayout( type );

            m_TextureConstantBufferDesc = new TextureConstantBufferDesc()
            {
                TilingHeight = 1.0f,
                TilingWidth = 1.0f,
                XOffset = 0.0f,
                YOffset = 0.0f,
                HasTexture = false,
                MainColor = new Vector4( 0.7f, 0.7f, 0.7f, 1.0f )
            };

            m_MaterialDesc = new MaterialDesc( 0.1f, 0.5f, 0.5f );

            m_TextureConstantBuffer = new CBuffer<TextureConstantBufferDesc>( 5, m_TextureConstantBufferDesc );
            m_MaterialCBufferDesc = new CBuffer<MaterialDesc>( 4, m_MaterialDesc );
        }

        /// <summary>Lie les données du material (shaders, constant buffer et ShaderResourceView) au gpu </summary>
        public void Begin()
        {
            ApplicationDX11.Instance.devicecontext_.InputAssembler.InputLayout = inputlayout_;

            if ( !ApplicationDX11.DesactivatePixelShader )
            {
                ApplicationDX11.Instance.devicecontext_.PixelShader.Set( pixelshader_ );
            }
            else
            {
                ApplicationDX11.Instance.devicecontext_.PixelShader.Set( null );
            }
            ApplicationDX11.Instance.devicecontext_.VertexShader.Set( vertexshader_ );

            ApplicationDX11.Instance.devicecontext_.GeometryShader.Set( geometryshader_ );
            SendConstantBuffers();
            BindShaderResources();
            m_TextureConstantBuffer.Send();
            m_MaterialCBufferDesc.Send();
        }

        /// <summary>On "Unbind", c'est à dire qu'on remet à 0 ce qui peut l'être pour éviter que certaines donnée du material en cours
        /// ne déborde sur le prochain drawCall
        /// </summary>
        public void End()
        {
            UnBindShaderResources();
        }

        /// <summary>
        /// Ajoute un constant buffer de type T ( ou T est une structure dont l'organisation des données est controlé
        /// pour être conformes aux spécifications hlsl (comprendre, par bloc de 16 octets)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        public void AddConstantBuffer<T>( T data ) where T : struct
        {
            constantBuffer = new CBuffer<T>( 2, data );
        }

        public void UpdateStruct<T>( T data ) where T : struct
        {
            ( ( CBuffer<T> )( constantBuffer ) ).UpdateStruct( data );
        }

        public void AddTexture( Resource resource )
        {
            m_TextureConstantBufferDesc.HasTexture = true;
            // A priori le shaderResourceView correspond vraiment uniquement aux "textures"
            textures_.Add( new ShaderResourceView( ApplicationDX11.Instance.device_, resource ) );

            m_TextureConstantBuffer.UpdateStruct( m_TextureConstantBufferDesc );
        }

        public void SetTexture( int index, Resource resource )
        {
            textures_[index] = new ShaderResourceView( ApplicationDX11.Instance.device_, resource );
        }

        public void AddShaderResourceView( ShaderResourceView shaderResourceView )
        {
            m_TextureConstantBufferDesc.HasTexture = true;
            textures_.Add( shaderResourceView );
            m_TextureConstantBuffer.UpdateStruct( m_TextureConstantBufferDesc );
        }

        public void SetShaderResourceView( int index, ShaderResourceView shaderResourceView )
        {
            textures_[index] = shaderResourceView;
        }

        public void SetPixelShader( string pixelname )
        {
            SetPixelShader( new PixelShader( ApplicationDX11.Instance.device_, ShaderBytecode.FromFile( RealPath( pixelname ) ) ) );
        }

        public void SetPixelShader( PixelShader pixelShader )
        {
            pixelshader_ = pixelShader;
        }

        public void SetTextureWidth( float width )
        {
            m_TextureConstantBufferDesc.TilingWidth = width;
            m_TextureConstantBuffer.UpdateStruct( m_TextureConstantBufferDesc );
        }

        public void SetTextureHeight( float height )
        {
            m_TextureConstantBufferDesc.TilingHeight = height;
            m_TextureConstantBuffer.UpdateStruct( m_TextureConstantBufferDesc );
        }

        public void SetTextureXOffset( float xoffset )
        {
            m_TextureConstantBufferDesc.XOffset = xoffset;
            m_TextureConstantBuffer.UpdateStruct( m_TextureConstantBufferDesc );
        }

        public void SetTextureYOffset( float yoffset )
        {
            m_TextureConstantBufferDesc.YOffset = yoffset;
            m_TextureConstantBuffer.UpdateStruct( m_TextureConstantBufferDesc );
        }

        public void SetMainColor( Vector4 color )
        {
            m_TextureConstantBufferDesc.MainColor = color;
            m_TextureConstantBuffer.UpdateStruct( m_TextureConstantBufferDesc );
        }

        public void SetMainColor( float r, float g, float b, float a )
        {
            m_TextureConstantBufferDesc.MainColor = new Vector4( r, g, b, a );
            m_TextureConstantBuffer.UpdateStruct( m_TextureConstantBufferDesc );
        }

        private void SendConstantBuffers()
        {
            if ( constantBuffer != null )
            {
                constantBuffer.Send();
            }
        }

        /// <summary> Charge et combine un vertex et fragment Shader précompilé dont le chemin est passé en paramètre </summary>
        private void LoadShaders( string vs = "vDefault.cso", string fs = "pDiffuse.cso", string gs = "gDefault.cso" )
        {
            LoadVertexShaderFromFile( vs );
            LoadPixelShaderFromFile( fs );
            LoadGeometryShaderFromFile( gs );
        }

        /// <summary> Récupère le Vertex Shader précompilé </summary>
        private void LoadVertexShaderFromFile( string file )
        {
            InitializeVertexShader( ShaderBytecode.FromFile( RealPath( file ) ) );
        }

        /// <summary> Récupère et Initialise le Geometry Shader précompilé </summary>
        private void LoadGeometryShaderFromFile( string file )
        {
            InitializeGeometryShader( ShaderBytecode.FromFile( RealPath( file ) ) );
        }

        /// <summary> Récupère le PixelShader précompilé </summary>
        private void LoadPixelShaderFromFile( string file )
        {
            InitializePixelShader( ShaderBytecode.FromFile( RealPath( file ) ) );
        }

        private void SetInputLayout( VertexTypeD11 type )
        {
            inputlayout_ = new InputLayout(
                ApplicationDX11.Instance.device_,
                vertexsignature_,
                AbstractVertex.Infos( type )
                );
        }

        /// <summary> Lie les resources ( textures) au shader </summary>
        private void BindShaderResources()
        {
            for ( int i = 0; i < textures_.Count; i++ )
            {
                ApplicationDX11.Instance.devicecontext_.VertexShader.SetShaderResource( i, textures_[i] );
                ApplicationDX11.Instance.devicecontext_.PixelShader.SetShaderResource( i, textures_[i] );
            }
            for ( int i = 0; i < samplers.Count; i++ )
            {
                ApplicationDX11.Instance.devicecontext_.PixelShader.SetSampler( i, samplers[i] );
            }
        }

        /// <summary>On remet les ShaderResourceView que l'on vient d'utiliser à null
        ///  de manière à éviter qu'une texture "déborde" sur un autre material
        /// </summary>
        private void UnBindShaderResources()
        {
            for ( int i = 0; i < textures_.Count; i++ )
            {
                ApplicationDX11.Instance.devicecontext_.VertexShader.SetShaderResource( i, null );
                ApplicationDX11.Instance.devicecontext_.PixelShader.SetShaderResource( i, null );
            }
        }

        private void InitializeVertexShader( ShaderBytecode sbc )
        {
            vertexshader_ = new VertexShader( ApplicationDX11.Instance.device_, sbc );
            GetSignatureAndReflection( sbc, ref vertexsignature_, ref vertexreflection_ );
        }

        private void InitializePixelShader( ShaderBytecode sbc )
        {
            pixelshader_ = new PixelShader( ApplicationDX11.Instance.device_, sbc );
            GetSignatureAndReflection( sbc, ref pixelsignature_, ref pixelreflection_ );
        }

        private void InitializeGeometryShader( ShaderBytecode sbc )
        {
            geometryshader_ = new GeometryShader( ApplicationDX11.Instance.device_, sbc );
            GetSignatureAndReflection( sbc, ref geometrysignature_, ref geometryreflection_ );
        }

        private void GetSignatureAndReflection( ShaderBytecode sbc, ref ShaderSignature shaderSignature, ref ShaderReflection reflection )
        {
            shaderSignature = ShaderSignature.GetInputSignature( sbc );
            reflection = new ShaderReflection( sbc );
        }

        public AbstractCBuffer constantBuffer;

        public ShaderSignature vertexsignature_;
        public ShaderSignature pixelsignature_;
        public ShaderSignature geometrysignature_;
        public ShaderSignature hullsignature_;
        public ShaderSignature computesignature_;
        public ShaderSignature domainsignature_;

        public ShaderReflection vertexreflection_;
        public ShaderReflection pixelreflection_;
        public ShaderReflection geometryreflection_;

        public VertexShader vertexshader_;
        public PixelShader pixelshader_;
        public GeometryShader geometryshader_;
        public HullShader hullShader_;
        public DomainShader domainshader_;
        public ComputeShader computeshader_;

        public InputLayout inputlayout_;


        public List<ShaderResourceView> textures_ = new List<ShaderResourceView>();
        public List<SamplerState> samplers = new List<SamplerState>();

        private CBuffer<TextureConstantBufferDesc> m_TextureConstantBuffer;
        private CBuffer<MaterialDesc> m_MaterialCBufferDesc;
        private TextureConstantBufferDesc m_TextureConstantBufferDesc;
        private MaterialDesc m_MaterialDesc;
    }
}
