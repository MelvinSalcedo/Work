using System;
using SharpDX;

namespace Troll3D{

    /// <summary>
    /// Classe représentant une projection en "perspective". Définit par une sorte de pyramide tronqué par les plans near et far
    /// le fieldOfView permet de déterminer l'angle de "vue", et le ratio le rapport entre la hauteur et la largeur
    /// ( 4/3 ou 16/9 par exemple)
    /// </summary>
    public class FrustumProjection : Projection{

        // Public

            // Lifecycle

                public FrustumProjection(float fieldOfView, float ratio, float near, float far){
                    SetFrustumProjection(fieldOfView, ratio, near, far);
                }

            // Methods

                public override ProjectionType GetProjectionType(){
 	                return ProjectionType.FrustumProjection;
                }

                public void SetFrustumProjection(float fieldOfView, float ratio, float near, float far){
                    fieldOfView_    = fieldOfView;
                    ratio_          = ratio;
                    near_           = near;
                    far_            = far;
                    Resize();
                }

            // Gets

                public float GetRatio(){
                    return ratio_;
                }

                public float GetFieldOfView(){
                    return fieldOfView_;
                }

                public float GetNearPlane(){
                    return near_;
                }

                public void SetNearPlane(float near){
                    near_ = near;
                    Resize();
                }

                public float GetFarPlane(){
                    return far_;
                }

                public void SetFarPlane(float far){
                    far_ = far;
                    Resize();
                }

            // Sets

                public void SetRatio(float ratio){
                    ratio_ = ratio;
                    Resize();
                }

                public void SetFieldOfView(float fieldOfView){
                    fieldOfView_ = fieldOfView;
                    Resize();
                }

        // Private

            // Methods

                private void Resize(){
                    data_ = Matrix.PerspectiveFovLH(fieldOfView_, ratio_, near_, far_);
                }

            // Datas

                private float fieldOfView_;
                private float ratio_;
                private float near_;
                private float far_;
            
    }
}
