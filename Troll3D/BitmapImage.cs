using System;
using System.Collections.Generic;
using SharpDX.Direct3D11;
using SharpDX;

using Troll3D.Components;

namespace Troll3D
{

    /// <summary>
    /// La classe BitmapImage sera tout simplement un quad sur lequel on affichera une image
    /// la particularité étant que l'image sera affiché devant tout le reste et par rapport aux coordonnées 
    /// de la fenêtre/viewport
    /// </summary>
    public class BitmapImage : Entity
    {

        /// <summary>
        /// Petit raccourcis pour récupérer l'endroit ou sont stocké les images et faciliter un peu tout
        /// </summary>
        private string DebugPath        = "D:\\Work\\Resources\\";
        private string ReleasePath      = ".\\";

            

                public BitmapImage(string path, int offsetx = 0, int offsety= 0){
                    string totalpath;

                    #if DEBUG
                        totalpath= DebugPath + path;
                    #else
                        totalpath = ReleasePath+ path;
                    #endif

                    Initialize((Texture2D)Texture2D.FromFile(ApplicationDX11.Instance.device_, totalpath), offsetx,offsety);
                }

                public BitmapImage(Texture2D image, int offsetx=0, int offsety=0 ){
                    Initialize(image, offsetx, offsety);
                }

                public BitmapImage(RenderTexture renderTexture, int offsetx = 0, int offsety = 0){
                   // Initialize(renderTexture.TargetTexture, offsetx, offsety);
                }

                public BitmapImage(ShaderResourceView src, MaterialDX11 mat = null, int offsetx = 0, int offsety = 0){

                     MaterialDX11 material;
                    if (mat == null){
                        material = new MaterialDX11("vDefault.cso", "pUnlit.cso", "gDefault.cso");
                    }else{
                        material = mat;
                    }
                    if (material.textures_.Count == 0){
                        material.AddShaderResourceView(src);
                    }
                    
                    Width = 200;
                    Height = 200;
                    //modelrenderer_ = new MeshRenderer(material, Quad.GetMesh());
                    Resize();
                }

                public void Initialize(Texture2D image, int offsetx, int offsety){                    
                    MaterialDX11 material = new MaterialDX11("vDefault.cso","pUnlit.cso", "gDefault.cso");
                    material.AddTexture(image);
                 
                    Texture             = image;
                    Width               = image.Description.Width;
                    Height              = image.Description.Height;
                    //modelrenderer_ = new MeshRenderer( material, Quad.GetMesh() );
                    Resize();
                }

            // Methods

                //public override void Draw(){

                //    //ApplicationDX11.Instance.devicecontext_.Rasterizer.State = modelrenderer_.rasterstate_;
                    
                //    WorldViewProj wvp = new WorldViewProj(){
                //        World       = transform_.localmatrix_,
                //        View        = Matrix.Identity,
                //        Projection  = Matrix.Identity
                //    };

                //    transform_.SetWVP(wvp);

                //    //modelrenderer_.Draw();

                //    //for (int i = 0; i < sons_.Count; i++){
                //    //    sons_[i].Draw();
                //    //}
                //}

                public void Resize(){
                    transform_.SetPosition(
                        -((  (float)Screen.Instance.Width/2.0f     - (float)Width/2.0f))  / (float)Screen.Instance.Width*2.0f ,
                        -((  (float)Screen.Instance.Height/2.0f    - (float)Height/2.0f)) / (float)Screen.Instance.Height*2.0f,
                        0.31f
                        );
                   // transform_.RotationEuler(3.141592f / 5.0f, 3.141592f / 5.0f, 3.141592f / 5.0f);
                    transform_.SetScale((float)Width / (float)Screen.Instance.Width*2.0f, (float)Height / (float)Screen.Instance.Height* 2.0f, 1.0f);
                    transform_.Update();
                }

            // Datas

                public Texture2D Texture;
                public int XOffset;
                public int YOffset;
                public int Width;

                public int Height{
                    get { return height_; }
                    set { height_ = value; }
                }

        // Private

            // Methods

            // Datas

                private int width_;
                private int height_;

                private int xoffset_;
                private int yoffset_;
    }
}
