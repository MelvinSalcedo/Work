using System;
using System.Collections.Generic;
using SharpDX;
using SharpDX.Direct3D11;
using SharpDX.DXGI;

using Troll3D.Components;
using Troll3D.Rendering;

namespace Troll3D
{
    /// <summary>
    /// Représente une Dynamic Cubemap. Cette dernière est composé d'une texture décomposé en 6 composantes,
    /// et de 6 caméras qui vont se charger de capturer les images du point d'ancrage de la cubemap
    /// </summary>
    public class DynamicCubemap
    {
        public DynamicCubemap(int cubemapsize)
        {
            CubeMapSize = cubemapsize;

            // On commence par initialiser la description de la texture qui sera mise
            // à jour par cette classe
            TextureDescription = new Texture2DDescription()
            {
                Width = CubeMapSize,
                Height = CubeMapSize,
                MipLevels = 0,
                ArraySize = 6,                  // Comme on cherche à créer une cubemap, ArraySize est à 6
                Format = Format.R32G32B32A32_Float,
                Usage = ResourceUsage.Default,
                BindFlags = BindFlags.ShaderResource | BindFlags.RenderTarget,
                CpuAccessFlags = CpuAccessFlags.None,
                OptionFlags = ResourceOptionFlags.GenerateMipMaps | ResourceOptionFlags.TextureCube,
                SampleDescription = new SampleDescription
                {
                    Count = 1,
                    Quality = 0
                }
            };

            //Création de la texture
            Texture = new Texture2D
            (
                ApplicationDX11.Instance.Device,
                TextureDescription
            );

            // Création d'un renderTargetDescription qui sera utilisé
            // par tout les autres RenderTargetView
            RenderTargetViewDescription rtvDesc = new RenderTargetViewDescription() 
            {
                Format      = TextureDescription.Format,
                Dimension   = RenderTargetViewDimension.Texture2DArray,
                Texture2DArray = new RenderTargetViewDescription.Texture2DArrayResource()
                {
                    MipSlice = 0,
                    ArraySize   = 1,
                }
            };

            // Initialisation des 6 renderTargetView
            for (int i = 0; i < 6; i++) 
            {
                // On utilise FirstArraySlice pour préciser sur quel "Slice", ou Texture, 
                // on se trouve
                rtvDesc.Texture2DArray.FirstArraySlice = i;
                RenderTargetViews.Add(new RenderTargetView( ApplicationDX11.Instance.Device, Texture, rtvDesc ));
            }

            // Initialisation du ShaderResourceView
            ShaderResourceViewDescription SRVDescription = new ShaderResourceViewDescription() 
            {
                Format = TextureDescription.Format,
                Dimension = SharpDX.Direct3D.ShaderResourceViewDimension.TextureCube,
                Texture2D = new ShaderResourceViewDescription.Texture2DResource()
                {
                    MipLevels = 1,
                    MostDetailedMip = 0
                }
            };

            SRV = new ShaderResourceView(ApplicationDX11.Instance.Device, Texture, SRVDescription);

            // Création des renderTarget

            foreach ( RenderTargetView view in RenderTargetViews )
            {
                RenderTargets.Add( new RenderTarget( view, CubeMapSize, CubeMapSize ) );
            }

            // Création de la transformation

            Transform = new Transform();

            // Création des caméras

            for ( int i = 0; i < 6; i++ )
            {
                Entity entity = new Entity();
                Camera cam = entity.AddComponent<Camera>();
                
                cam.Initialize( new FrustumProjection( 3.141592f / 2.0f, 1.0f, 0.1f, 1000.0f ) );
                cam.SetViewport( 0, 0, CubeMapSize, cubemapsize );
                Cameras.Add( cam );
            }

            // Right Camera

            Cameras[0].m_transform.LookAt( new Vector3(1.0f,0.0f,0.0F), Transform.position_ );

            // Left Camera

            Cameras[1].m_transform.LookAt( new Vector3( -1.0f, 0.0f, 0.0F ), Transform.position_ );

            // Up Camera

            Cameras[2].m_transform.LookAt( Vector3.Up + new Vector3(0.0f,0.0f,0.00001f), Transform.position_ );
            
            // Down Camera

            Cameras[3].m_transform.LookAt( Vector3.Down + new Vector3( 0.0f, 0.0f, 0.00001f ), Transform.position_ );

            // Forward Camera

            Cameras[4].m_transform.LookAt(  Vector3.ForwardLH, Transform.position_ );

            // Backward Camera

            Cameras[5].m_transform.LookAt( Vector3.BackwardLH, Transform.position_ );

            ApplicationDX11.Instance.Cubemaps.Add( this );

        }

        /// <summary>
        /// On ajoute la skybox à toutes les caméras
        /// </summary>
        /// <param name="sk"></param>
        public void AddSkybox( Skybox sk )
        {
            foreach ( Camera cam in Cameras )
            {
                cam.Skybox = new Skybox( cam .Entity);
            }
        }

        public void Update()
        {
            for ( int i = 0; i < Cameras.Count; i++ )
            {
                Cameras[i].Update();
                ApplicationDX11.Instance.DrawFromCamera( Cameras[i] , RenderTargets[i]);
            }
        }
        
        /// <summary>
        /// Transformation qui permet de savoir ou se trouve la Dynamic Cube Map
        /// </summary>
        public Transform Transform { get; private set; }

        /// <summary>
        /// Retourne le Shader Resource View utilisable par un Shader
        /// </summary>
        public ShaderResourceView SRV { get; private set; }

        /// <summary>
        /// Liste des 6 caméras utilisé pour capturer les 6 faces de la cubemap
        /// </summary>
        public List<Camera> Cameras = new List<Camera>();

        /// <summary>
        /// Liste contenant les 6 renderTargetView ou les caméras afficheront leur données
        /// </summary>
        public List<RenderTargetView> RenderTargetViews = new List<RenderTargetView>();

        public int CubeMapSize;


        private List<RenderTarget>      RenderTargets = new List<RenderTarget>();
        private Texture2DDescription    TextureDescription;
        private Texture2D               Texture;



    }
}
