using System;
using SharpDX;

namespace Troll3D{

    /// <summary>
    ///  ProjectionOrthogonale : Le but d'une projection orthogonale est de transformé l'espace "directx", comprise dans un cube 
    ///  de coordonnées [-1;1], en "world space"
    /// </summary>
    public class OrthoProjection : Projection{

        // Public

            // LifeCycle

                /// <summary>
                ///  Raccourcis pour créer une Projection orthogonale centrée
                /// </summary>
                public OrthoProjection(float width, float height, float near, float far){
                    SetOrthoCenter(width, height, near, far);
                }

                public OrthoProjection(float left, float right, float top, float bottom, float near, float far){
                    SetOrtho(left, right, top, bottom, near, far);
                }

            // Methods

                public override ProjectionType GetProjectionType(){
                    return ProjectionType.OrthoProjection;
                }

                /// <summary>
                /// Crée une matrice de projection orthogonale centré en 0 
                /// </summary>
                public void SetOrthoCenter(float width, float height, float near, float far){
                    left_   = -width / 2.0f;
                    right_  = width / 2.0f;
                    top_    = height / 2.0f;
                    bottom_ = -height / 2.0f;
                    near_   = near;
                    far_    = far;
                    Resize();
                }

                /// <summary>
                /// Crée une matrice de projection orthogonale pouvant être décalé par rapport au centre 0
                /// </summary>
                public void SetOrtho(float left, float right, float top, float bottom, float near, float far){
                    left_   = left;
                    right_  = right;
                    top_    = top;
                    bottom_ = bottom;
                    near_   = near;
                    far_    = far;
                    Resize();
                }

            // Gets

                /// <summary>
                /// Retourne la différence entre right et left
                /// </summary>
                /// <returns></returns>
                public float GetWidth(){
                    return (right_ - left_);
                }

                /// <summary>
                /// Retourne la différence entre top et bottom
                /// </summary>
                public float GetHeight(){
                    return (top_ - bottom_);
                }

                public float GetLeft(){
                    return left_;
                }

                public float GetRight(){
                    return right_;
                }

                public float GetTop(){
                    return top_;
                }

                public float GetBottom(){
                    return bottom_;
                }

                public float GetNear(){
                    return near_;
                }

                public float GetFar(){
                    return far_;
                }

            // Sets


                /// <summary>
                /// Centre la matrice Ortho horizontalement par rapport au point (0,0) à partir de la variable width
                /// </summary>
                /// <param name="width"></param>
                public void SetWidth(float width){
                    right_= width/ 2.0f;
                    left_= -width/ 2.0f;
                    Resize();
                }

                /// <summary>
                /// Centre la matrice ortho verticalement par rapport au point 0 à partir de la variable height 
                /// </summary>
                public void SetHeight(float height){
                    top_ = height/ 2.0f;
                    bottom_ = -height/ 2.0f;
                    Resize();
                }

                public void SetLeft(float left){
                    left_ = left;
                    Resize();
                }

                public void SetRight(float right){
                    right_ = right;
                    Resize();
                }

                public void SetTop(float top){
                    top_ = top;
                    Resize();
                }

                public void SetBottom(float bottom){
                    bottom_ = bottom;
                    Resize();
                }

                public void SetNear(float near){
                    near_ = near;
                    Resize();
                }

                public void SetFar(float far){
                    far_ = far;
                    Resize();
                }

        // Private

            // Methods

                private void Resize(){
                    data_ = Matrix.OrthoOffCenterLH(left_, right_, bottom_, top_, near_, far_);
                }

            // Datas

                private float left_;
                private float right_;

                private float top_;
                private float bottom_;

                private float near_;
                private float far_;
            

    }
}
