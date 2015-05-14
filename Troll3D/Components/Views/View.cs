using System;
using System.Collections.Generic;
using SharpDX;
using SharpDX.Direct3D11;

namespace Troll3D
{

    /// <summary>
    /// Une Vue contient tout simplement une Matrice de Projection et une matrice de Transformation qui devra être initialisé
    /// lors de sa création. A partir de là, il devient possible de gérer les renderTexture et autres passes de ShadowMap serainement 
    /// </summary>
    public class View
    {
        /// <summary> 
        /// Les objets de la classe Entity ont besoin de connaitre la matrice de transformation et la
        /// projection de la Vue en cours d'utilisation. 
        /// Ils pourront piocher dans cette variable statique pour accomplir cet tâche
        /// </summary>
        public static View Current;

        /// <summary> 
        /// Si aucun argument n'est spécifié lors de la création de la vue,
        /// le viewport de cette dernière prendra automatiquement les coordonnées de l'écran
        /// </summary>
        public View( Transform transform, Projection projection )
        {
            viewport = new Troll3D.Viewport( 0, 0, Screen.Instance.Width, Screen.Instance.Height);
            Transformation = transform;
            projection_ = projection;
        }

        public void SetProjection( Projection projection )
        {
            projection_ = projection;
        }

        public void SetOrthoProjection( float width, float height, float near, float far )
        {
            projection_ = new OrthoProjection( width, height, near, far );
        }

        public void SetFrustsumProjection( float fov, float aspect, float near, float far )
        {
            projection_ = new FrustumProjection( fov, aspect, near, far );
        }

        /// <summary>
        /// Raccourcis pour retourner la projection directement sous la forme d'une orthoProjection
        /// Attention cependant, si la projection est de type FRustum/perspective, la fonction plantera le programme
        /// </summary>
        /// <returns></returns>
        public OrthoProjection GetOrthoProjection()
        {
            return ( OrthoProjection )projection_;
        }

        /// <summary>
        /// Raccourcis pour retourner la projection directement sous la forme d'une FrustumProjection
        /// Attention, si la projection est de type ortho, la fonction fera planter le programme
        /// </summary>
        /// <returns></returns>
        public FrustumProjection GetFrustumProjection()
        {
            return ( FrustumProjection )projection_;
        }

        public ProjectionType GetProjectionType()
        {
            return projection_.GetProjectionType();
        }

        public Projection GetProjection()
        {
            return projection_;
        }

        /// <summary>
        /// Toujours important lorsque l'on souhaite rendre la vue 
        /// </summary>
        public Troll3D.Viewport viewport;

        /// <summary>
        /// La projection 
        /// </summary>
        public Projection projection_;

        /// <summary>
        /// Ultra important pour rendre ce que la vue "voit" 
        /// </summary>
        public Transform Transformation;
    }
}
