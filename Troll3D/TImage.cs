using System;
using System.Collections.Generic;
using SharpDX;
using SharpDX.Direct3D11;

namespace Troll3D
{
    public class TPixel
    {
        public TPixel( float red, float green, float blue, float alpha )
        {
            r = red;
            g = green;
            b = blue;
            a = alpha;
        }

        public float r;
        public float g;
        public float b;
        public float a;
    }

    /// <summary>
    ///  Une TImage est tout simplement une image RGBA composé d'un octet pour chaque composante de l'image
    /// </summary>
    public class TImage
    {
        public TImage( int width, int height )
        {
            Width = width;
            Height = height;
            datas_ = new float[Width * Height * 4];
        }

        public TImage( float[] datas, int width, int height )
        {
            datas_  = datas;
            Width   = width;
            Height  = height;
        }

        /// <summary>
        /// Retourne une ressource utilisable par DirectX
        /// </summary>
        public Resource GetTexture2D()
        {
            SharpDX.DXGI.SampleDescription sampleDescription = new SharpDX.DXGI.SampleDescription()
            {
                Count = 1
            };

            DataStream stre = new DataStream( Width* Height* 4 * 4, true, true );

            for ( int i = 0; i < Height; i++ )
            {
                for ( int j = 0; j < Width; j++ )
                {
                    stre.Write( GetPixel( j, i ).r );
                    stre.Write( GetPixel( j, i ).g );
                    stre.Write( GetPixel( j, i ).b );
                    stre.Write( GetPixel( j, i ).a );
                }
            }

            Texture2DDescription description = new Texture2DDescription()
            {
                ArraySize = 1,
                Width = Width,
                Height = Height,
                MipLevels = 1,
                Format = SharpDX.DXGI.Format.R32G32B32A32_Float,
                Usage = ResourceUsage.Dynamic,
                BindFlags = BindFlags.ShaderResource,
                CpuAccessFlags = CpuAccessFlags.Write,
                OptionFlags = 0,
                SampleDescription = sampleDescription
            };

            DataRectangle rec = new DataRectangle( stre.DataPointer, Width * 4 * 4 );

            Texture2D texture2D = new Texture2D( ApplicationDX11.Instance.Device, description, rec );

            texture2D.FilterTexture( ApplicationDX11.Instance.DeviceContext, 0, FilterFlags.Mirror );

            Resource res = new Resource( texture2D.NativePointer );
            return res;

        }

        public TPixel GetPixel( int x, int y )
        {
            return new TPixel(
                datas_[y * Width * 4 + x * 4],
                datas_[y * Width * 4 + x * 4 + 1],
                datas_[y * Width * 4 + x * 4 + 2],
                datas_[y * Width * 4 + x * 4 + 3]
                );
        }

        public void SetData( float[] datas )
        {
            if ( datas.Length != Width * Height * 4 )
            {
                throw new IndexOutOfRangeException( "Erreur, le tableau n'est pas aux même dimension que l'image" );
            }

            for ( int i = 0; i < datas.Length; i++ )
            {
                datas_[i] = datas[i];
            }
        }

        public int Width { get; private set; }
        public int Height { get; private set; }
        public float[] Data { get { return datas_; } }


        public void SetPixel( int x, int y, float r, float g, float b, float a )
        {
            datas_[y * Width * 4 + x * 4] = r;
            datas_[y * Width * 4 + x * 4 + 1] = g;
            datas_[y * Width * 4 + x * 4 + 2] = b;
            datas_[y * Width * 4 + x * 4 + 3] = a;
        }

        private float[] datas_;
    }
}
