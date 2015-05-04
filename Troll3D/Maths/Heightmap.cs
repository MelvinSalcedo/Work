using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Troll3D;
using SharpDX;

namespace Troll3D
{
    public class Heightmap : Entity{

        // public

            // Lifecycle

        Mesh mesh_;

                public Heightmap(int width, int height, float offset = 1.0f) : base(null){

                    width_ = width;
                    height_ = height;
                    offset_ = offset;

                    mesh_ = new Mesh();

                    trianglecount_ = 0;
                    vertices_ = new List<AbstractVertex>();

                    indexes_ = new List<int>();


                    BuildVertices();
                    BuildFaces();
                    InitializeGeometry();
                }

                public Heightmap(byte[] image, int width, int height, float offset = 1.0f){

                    mesh_ = new Mesh();

                    width_      = width;
                    height_     = height;
                    offset_     = offset;

                    trianglecount_ = 0;

                    vertices_ = new List<AbstractVertex>();

                    indexes_ = new List<int>();


                    BuildVertices();
                    BuildFaces();

                    for (int i = 0; i < Height; i++){
                        for (int j = 0; j < Width; j++){
                            ((StandardVertex) vertices_[(i * Width) + j]).Position.Y = (float)((int)image[((i * Width * 4) + j * 4)]) * 0.1f;
                        }
                    }

                    InitializeGeometry();
                }

            // Methods

                public void InitializeGeometry(){
                    //mesh_.SetVertices(vertices_);
                    //mesh_.SetTriangles(indexes_, trianglecount_);
                }

                public float Offet{
                    get { return offset_; }
                }

                public int Width{
                    get { return width_; }
                }

                public int Height{
                    get { return height_; }
                }

        // Private

            // Methods

                private void BuildVertices(){
                    for (int i = 0; i < Height; i++){
                        for (int j = 0; j < Width; j++){

                            vertices_.Add(new StandardVertex(
                                new Vector3(j * offset_ - (Width * Offet) / 2.0f, 0.0f, i * offset_ - (Height * Offet) / 2.0f),
                                new Vector3(0.0f, 1.0f, 0.0f),
                                new Vector2((float)j / (float)Width, (float)i / (float)Width)));
                        }

                    }
                }

                private void BuildFaces(){
                    for (int i = 0; i < Height - 1; i++){
                        for (int j = 0; j < Width - 1; j++){

                            int index = (i * (width_)) + j;

                            indexes_.Add(index);
                            indexes_.Add(index+width_);
                            indexes_.Add(index+width_+1);

                            indexes_.Add(index);
                            indexes_.Add(index+width_+1);
                            indexes_.Add(index+1);
                            trianglecount_ += 2;

                        }
                    }
                }

            // Datas

                private     List<AbstractVertex>    vertices_;
                private     List<int>               indexes_;
                private     int                     trianglecount_;

                private     int     width_;
                private     int     height_;
                private     float   offset_;
                
    }
}
