using System;
using System.Collections.Generic;
using SharpDX;
using SharpDX.Direct3D11;
using System.Runtime.InteropServices;
using D3D11 = SharpDX.Direct3D11;

using Troll3D.Components.Lighting;

namespace Troll3D
{
    public class LightManager
    {
        public static LightManager Instance;

        public LightManager()
        {
            m_MaxLights = 10;
            Instance = this;
            m_Data = new LightsDesc();
            m_Data.Lights = new LightDesc[m_MaxLights];
            SetAcneBias( 0.005f );
            m_Data.ActivatePCF = false;
            // Pour les lumières, je réserve le buffer d'index 1
            m_ConstantBuffer = new CBuffer<LightsDesc>( 1, m_Data );
            InitializeBuffer();
        }

        public void Dispose() { }


        public Light AddLight( Light light )
        {
            m_Lights.Add( light );
            return light;
        }

        /// <summary> Retourne la lumière d'index i </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Light GetLight( int index )
        {
            return m_Lights[index];
        }

        public void RemoveLightAt( int index )
        {
            m_Lights.RemoveAt( index );
            InitializeBuffer();
        }

        /// <summary>
        /// Met à jour les lumières
        /// </summary>
        /// <param name="view"></param>
        public void Update( View view )
        {
            int lightcount = 0;

            // Si une lumière est inactive, elle est retirée de la liste envoyé aux shaders
            for ( int i = 0; i < m_Lights.Count; i++ )
            {
                if ( m_Lights[i].IsActive )
                {
                    m_Lights[i].Update();
                    m_Data.Lights[lightcount] = m_Lights[i].Description;
                    lightcount++;
                }
            }

            m_Data.LightCount = lightcount;

            Matrix matrixView = view.Transformation.localmatrix_;
            matrixView.Invert();

            m_Data.CameraPosition = matrixView.TranslationVector;

            m_ConstantBuffer.UpdateStruct( m_Data );
        }

        /// <summary>
        /// Truc pour les ombres
        /// </summary>
        public void SwitchPCF()
        {
            m_Data.ActivatePCF = !m_Data.ActivatePCF;
        }

        /// <summary>
        ///  Truc pour les ombres
        /// </summary>
        /// <returns></returns>
        public bool IsPCFActivated()
        {
            return m_Data.ActivatePCF;
        }

        /// <summary>
        /// Envoie les informations des différentes lumières présente dans la scène au gpu
        /// </summary>
        public void SendLights()
        {
            for ( int i = 0; i < m_Lights.Count; i++ )
            {
                if ( m_Lights[i].shadowmap_ != null )
                {
                    ApplicationDX11.Instance.DeviceContext.VertexShader.SetShaderResource( 20 + i, m_Lights[i].shadowmap_.shaderResourceView_ );
                    ApplicationDX11.Instance.DeviceContext.PixelShader.SetShaderResource( 20 + i, m_Lights[i].shadowmap_.shaderResourceView_ );

                }
            }
            m_ConstantBuffer.Send();
        }

        /// <summary>
        /// Retourne la taille en octet des données stockées par une lumière
        /// </summary>
        /// <returns></returns>
        public int Size()
        {
            return Marshal.SizeOf( typeof( LightsDesc ) );
        }

        /// <summary>
        ///  Truc pour tenté de gérer les artefacts avec les ombres et compagnies
        /// </summary>
        /// <param name="val"></param>
        public void SetAcneBias( float val )
        {
            m_Data.AcneBias = val;
        }

        /// <summary>
        ///  Truc pour tenté de gérer les artefacts avec les ombres et compagnies
        /// </summary>
        /// <returns></returns>
        public float GetAcneBias()
        {
            return m_Data.AcneBias;
        }

        /// <summary>
        /// Retourne le nombre de lumière que l'application peut gérer simultanément. Au delà de cette
        /// limite, les lumières ne sont plus prise en compte par le shader
        /// </summary>
        public int GetMaxLight()
        {
            return m_MaxLights;
        }

        /// <summary>
        ///  Retourne le nombre de lumières présentes dans la scène
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            return m_Lights.Count;
        }

        /// <summary>Cette méthode se charge de créer un buffer qui correspond aux dimensions des différentes
        /// constantes existante.
        /// </summary>
        private void InitializeBuffer()
        {
            if ( m_ConstantBuffer != null )
            {
                m_ConstantBuffer.Dispose();
            }

            m_ConstantBuffer = new CBuffer<LightsDesc>( 1, m_Data );
        }

        // Datas

        /// <summary>
        /// Indique le nombre maximal de lumière que l'application peut gérer. Au delà de ce nombre fixé
        /// arbitrairement, les lumières ne sont plus prises en compte
        /// </summary>
        private int m_MaxLights;

        private LightsDesc m_Data;
        private CBuffer<LightsDesc> m_ConstantBuffer;
        private List<Light> m_Lights = new List<Light>();
    }

}
