using System;
using System.Collections.Generic;
using SharpDX;
using SharpDX.Direct3D11;
using D3D11 = SharpDX.Direct3D11;
using System.Runtime.InteropServices;

namespace Troll3D {

    /// <summary>
    /// Le ConstantBuffer correspond à un block de données situé à l'intérieur des shaders model 4.0+
    /// identifié par le mot clé cbuffer. Il est composé de différentes valeurs qui doivent être converti pour pouvoir être envoyé 
    /// aux différents shaders
    /// </summary>
    public class CBuffer<T> : AbstractCBuffer where T : struct {

        // Public

            // Lifecycle

                public CBuffer(int index, T data){
                    m_Struct = data;
                    m_Index  = index;
                    m_DataStream = new DataStream(Size(), true, true);
                    InitializeBuffer();
                    UpdateStruct(data);
                }

                public void Dispose(){
                    Utilities.Dispose<D3D11.Buffer>(ref m_Buffer);
                    Utilities.Dispose<DataStream>(ref m_DataStream);
                }

            // Datas

                /// <summary>
                ///  Met à jour les données du constant Buffer
                /// </summary>
                /// <param name="data"></param>
                public void UpdateStruct(T data) {
                    Marshal.StructureToPtr(data, m_DataStream.DataPointer, false);
                    ApplicationDX11.Instance.DeviceContext.UpdateSubresource(
                        new DataBox(m_DataStream.DataPointer, 0, 0),
                        m_Buffer);
                }

                /// <summary>
                /// Lié le constant Buffer au shader actuellement utilisé pour l'affichage
                /// </summary>
                public override void Send() {
                    ApplicationDX11.Instance.DeviceContext.VertexShader.SetConstantBuffer( m_Index, m_Buffer);
                    ApplicationDX11.Instance.DeviceContext.PixelShader.SetConstantBuffer(  m_Index, m_Buffer);
                    ApplicationDX11.Instance.DeviceContext.GeometryShader.SetConstantBuffer(m_Index, m_Buffer);
                }

                /// <summary>
                ///  Retourne la taille en octet de la somme des variables constante affecté à ce buffer
                /// </summary>
                public int Size() {
                    int size = Marshal.SizeOf(typeof(T));
                    if (size < 16) {
                        size = 16;
                    }
                    return size;
                }

        // Private

            // Methods

                /// <summary>
                /// Cette méthode se charge de créer un buffer qui correspond aux dimensions des différentes
                /// constantes existante.
                /// </summary>
                private void InitializeBuffer() {
                    if (m_Buffer != null) {
                        Utilities.Dispose<D3D11.Buffer>(ref m_Buffer);
                    }

                    m_Buffer = new D3D11.Buffer(
                        ApplicationDX11.Instance.Device,
                        Size(),
                        ResourceUsage.Default,
                        BindFlags.ConstantBuffer,
                        CpuAccessFlags.None,
                        ResourceOptionFlags.None,
                        0);
                }

            // Datas

                private DataStream      m_DataStream;
                private DataBox         m_DataBox;
                private T               m_Struct;
                private int             m_Index;
                private D3D11.Buffer    m_Buffer;
    }
}
