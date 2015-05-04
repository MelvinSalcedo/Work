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
            width_ = width;
            height_ = height;
            datas_ = new float[width_ * height_ * 4];
        }

        public TImage( float[] datas, int width, int height )
        {
            datas_ = datas;
            width_ = width;
            height_ = height;
        }

        // Methods

        public Resource GetTexture2D()
        {
            SharpDX.DXGI.SampleDescription sampleDescription = new SharpDX.DXGI.SampleDescription()
            {
                Count = 1
            };

            DataStream stre = new DataStream( width_ * height_ * 4 * 4, true, true );

            for ( int i = 0; i < height_; i++ )
            {
                for ( int j = 0; j < width_; j++ )
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
                Width = width_,
                Height = height_,
                MipLevels = 1,
                Format = SharpDX.DXGI.Format.R32G32B32A32_Float,
                Usage = ResourceUsage.Dynamic,
                BindFlags = BindFlags.ShaderResource,
                CpuAccessFlags = CpuAccessFlags.Write,
                OptionFlags = 0,
                SampleDescription = sampleDescription
            };

            DataRectangle rec = new DataRectangle( stre.DataPointer, width_ * 4 * 4 );

            Texture2D texture2D = new Texture2D( ApplicationDX11.Instance.device_, description, rec );

            texture2D.FilterTexture( ApplicationDX11.Instance.devicecontext_, 0, FilterFlags.Mirror );

            Resource res = new Resource( texture2D.NativePointer );
            return res;

        }

        public TPixel GetPixel( int x, int y )
        {
            return new TPixel(
                datas_[y * width_ * 4 + x * 4],
                datas_[y * width_ * 4 + x * 4 + 1],
                datas_[y * width_ * 4 + x * 4 + 2],
                datas_[y * width_ * 4 + x * 4 + 3]
                );
        }

        public int Width { get { return width_; } }
        public int Height { get { return height_; } }
        public float[] Data { get { return datas_; } }


        public void SetPixel( int x, int y, float r, float g, float b, float a )
        {
            datas_[y * width_ * 4 + x * 4] = r;
            datas_[y * width_ * 4 + x * 4 + 1] = g;
            datas_[y * width_ * 4 + x * 4 + 2] = b;
            datas_[y * width_ * 4 + x * 4 + 3] = a;
        }

        private bool isfloat_;
        private int width_;
        private int height_;
        private float[] datas_;
    }
}
