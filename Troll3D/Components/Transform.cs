using System;
using System.Collections.Generic;
using SharpDX;
using SharpDX.Direct3D11;
using Troll3D.Components;

namespace Troll3D
{
    /// <summary>
    /// Gère la "position", les dimensions (scale) ainsi que la rotation d'un objet 3D ( et ses "parents")
    /// Toutes ces données sont ensuite enregistré dans une structure enregistrant les informations des matrices MVC à envoyé
    /// lors de l'affichage d'un élément
    /// </summary>
    public class Transform : TComponent
    {
        /// <summary>
        /// Possibilité de faire une transformation sans Entity pour les cas ou je souhaite
        /// stocker une transformation
        /// </summary>
        public Transform()
        {
            Type = ComponentType.Transform;
            InitializeDirectionsVectors();
            Initialize();
        }

        /// <summary>
        /// Attache le composant à son Entité
        /// </summary>
        /// <param name="entity"></param>
        public override void Attach( Entity entity )
        {
            if ( entity.Parent != null )
            {
                m_parent = ( Transform )entity.Parent.GetComponent( ComponentType.Transform );
            }
        }

        /// <summary>
        /// Initialise les valeurs de la transformation
        /// </summary>
        public void Initialize()
        {
            InitializeConstantBuffer();

            eulerangle_     = new Vector3();
            position_       = new Vector3();
            scaling_        = new Vector3( 1.0f, 1.0f, 1.0f );
            Update();
        }

        /// <summary>
        /// Envoie les informations de la transformation au GPU
        /// </summary>
        public void SendConstantBuffer()
        {
            constantbuffer_.Send();
        }

        /// <summary>
        /// La transformation est définit par une matrice spécifié par l'utilisateur
        /// </summary>
        /// <param name="mat"></param>
        public void SetLocalMatrix( Matrix mat )
        {
            localmatrix_ = mat;
            custommatrix_ = true;
        }

        /// <summary>
        /// Incrémente les dimensions de l'objet
        /// </summary>
        public void Scale( float x, float y, float z )
        {
            Scale( new Vector3( x, y, z ) );
        }

        /// <summary>
        /// Incrémente les dimensions de l'objet
        /// </summary>
        /// <param name="v"></param>
        public void Scale( Vector3 v )
        {
            scaling_ += v;
        }

        /// <summary>
        /// Définit les dimensions de l'objet
        /// </summary>
        public void SetScale( float x, float y, float z )
        {
            SetScale( new Vector3( x, y, z ) );
        }

        /// <summary>
        /// Définit les dimensions de l'objet
        /// </summary>
        public void SetScale( Vector3 v )
        {
            scaling_ = v;
        }

        /// <summary>
        /// Déplace l'objet
        /// </summary>
        public void Translate( float x, float y, float z )
        {
            Translate( new Vector3( x, y, z ) );
        }

        /// <summary>
        /// Déplace l'objet
        /// </summary>
        public void Translate( Vector3 v )
        {
            position_ += v;
        }

        /// <summary>
        /// La matrice de transformation va pointer en direction de l'objet à "regarder", depuis
        /// sa position actuelle
        /// </summary>
        /// <param name="v"></param>
        public void LookAt( Vector3 lookAtObject )
        {
            LookAt( lookAtObject, position_ );
        }

        /// <summary>
        /// La matrifce de transformation va pointer en direction de l'objet à regarder, et sa 
        /// position sera modifé
        /// </summary>
        /// <param name="lookAtObject"></param>
        /// <param name="from">Nouvelle position de la matrice de transformation</param>
        public void LookAt( Vector3 lookAtObject, Vector3 from )
        {
            localmatrix_ = Matrix.LookAtLH( from, lookAtObject, Vector3.Up );
            islookingat_ = true;
        }

        /// <summary>
        /// Positionne l'objet à la position exacte
        /// </summary>
        public void SetPosition( float x, float y, float z )
        {
            SetPosition( new Vector3( x, y, z ) );
        }

        /// <summary>
        /// Positionne l'objet à la position exacte
        /// </summary>
        public void SetPosition( Vector3 translation )
        {
            position_ = translation;
        }

        /// <summary>
        /// Positionne l'objet dans l'espace "réel"
        /// </summary>
        public void SetWorldPosition( Vector3 position )
        {
            if ( Parent != null )
            {
                position_ = position - Parent.WorldPosition();
            }
            else
            {
                position_ = position;
            }
        }

        /// <summary>
        /// Effectue une rotation d'euler x/y/z sur l'objet
        /// </summary>
        public void RotateEuler( float x, float y, float z )
        {
            RotateEuler( new Vector3( x, y, z ) );
        }

        /// <summary>
        /// Effectue une rotation d'euler x/y/z sur l'objet
        /// </summary>
        public void RotateEuler( Vector3 v )
        {
            eulerangle_ += v;
        }

        /// <summary>
        /// Fais tourner l'objet jusqu'à atteindre les valeurs données en paramètre
        /// </summary>
        public void SetRotationEuler( float x, float y, float z )
        {
            SetRotationEuler( new Vector3( x, y, z ) );
        }

        /// <summary>
        /// Fais tourner l'objet jusqu'à atteindre les valeurs données en paramètre
        /// </summary>
        public void SetRotationEuler( Vector3 v )
        {
            eulerangle_ = v;
        }

        /// <summary>
        /// Tourne l'objet en fonction de l'axe et de l'angle passé en paramètre
        /// </summary>
        public void RotateAroundAxis( float x, float y, float z, float teta )
        {
            rotationmatrix_ = Matrix.RotationAxis( new Vector3( x, y, z ), teta );
        }

        /// <summary>
        /// Retourne le vecteur Up transformé
        /// </summary>
        public Vector3 GetUpVector()
        {
            return ( Vector3 )Vector4.Transform( new Vector4( 0.0f, 1.0f, 0.0f, 0.0f ), worldmatrix_ );
        }

        /// <summary>
        /// Retourne le vecteur Forward transformé
        /// </summary>
        public Vector3 GetForwardVector()
        {
            return ( Vector3 )Vector4.Transform( new Vector4( 0.0f, 0.0f, 1.0f, 0.0f ), worldmatrix_ );
        }

        /// <summary>
        /// Retourne le vecteur Right transformé
        /// </summary>
        /// <returns></returns>
        public Vector3 GetRightVector()
        {
            return ( Vector3 )Vector4.Transform( new Vector4( 1.0f, 0.0f, 0.0f, 0.0f ), worldmatrix_ );
        }

        /// <summary>
        /// Retourne la position de la matrice
        /// </summary>
        public Vector3 GetPosition()
        {
            if ( custommatrix_ || islookingat_ )
            {
                Matrix mat = Matrix.Invert( localmatrix_ );
                return mat.TranslationVector;
            }
            else
            {
                return position_;
            }
        }

        public Vector3 WorldPosition()
        {
            Vector3 position = position_;
            if ( m_parent != null )
            {
                if ( m_parent != null )
                {
                    position += m_parent.WorldPosition();
                }
            }
            return position;
        }

        public override void Update()
        {
            UpdateLocalMatrix();
            UpdateWorldMatrix();
            UpdateDirectionsVector();
            UpdateConstantBuffer();
        }

        public void SetWVP( WorldViewProj wvp )
        {
            constantbuffer_.UpdateStruct( wvp );
        }

        private void UpdateWorldMatrix()
        {
            if ( m_parent != null )
            {
                if ( m_parent != null && noInheritance_ == false )
                {
                    worldmatrix_ = localmatrix_ * m_parent.worldmatrix_;
                }
                else
                {
                    worldmatrix_ = localmatrix_;
                }
            }
            else
            {
                worldmatrix_ = localmatrix_;
            }
        }

        public Matrix GetViewMatrix()
        {
            if ( custommatrix_ || islookingat_ )
            {
                return Matrix.Invert( localmatrix_ );
            }

            Matrix view = rotationmatrix_ * positionmatrix_;
            view.Invert();
            return view;
        }

        public Transform Parent { get; set; }

        private void UpdateLocalMatrix()
        {
            // Dans le cas ou la matrice n'est pas controlé par "setLocalMatrix" ou par lookat
            if ( !custommatrix_ && !islookingat_ )
            {
                scalematrix_        = Matrix.Scaling( scaling_ );
                positionmatrix_     = Matrix.Translation( position_ );
                rotationmatrix_     = Matrix.RotationQuaternion( Quaternion.RotationYawPitchRoll( eulerangle_.X, eulerangle_.Y, eulerangle_.Z ) );
                localmatrix_        = scalematrix_ * rotationmatrix_ * positionmatrix_;
            }

            if ( IsViewMatrix )
            {
                if ( custommatrix_ || islookingat_ )
                {
                    //localmatrix_.Invert();
                }
            }
        }

        private void UpdateDirectionsVector()
        {
            forward_ = ( Vector3 )Vector4.Transform( new Vector4( forward_, 0.0f ), rotationmatrix_ );
            up_ = ( Vector3 )Vector4.Transform( new Vector4( up_, 0.0f ), rotationmatrix_ );
            right_ = ( Vector3 )Vector4.Transform( new Vector4( right_, 0.0f ), rotationmatrix_ );
        }

        private void InitializeDirectionsVectors()
        {
            forward_ = new Vector3( 0.0f, 0.0f, 1.0f );
            up_ = new Vector3( 0.0f, 1.0f, 0.0f );
            right_ = new Vector3( 1.0f, 0.0f, 0.0f );
        }

        private void InitializeConstantBuffer()
        {
            worldviewproj_ = new WorldViewProj()
            {
                World = worldmatrix_,
                WorldInverse = new Matrix(),
                View = new Matrix(),
                Projection = new Matrix(),
                CameraPosition = new Vector3()
            };

            // Le constant buffer des transformations devra toujours se trouver à l'index 0 pour éviter les blagues
            constantbuffer_ = new CBuffer<WorldViewProj>( 0, worldviewproj_ );
        }

        /// <summary>
        /// Met à jour le constant Buffer contenant les informations sur
        /// la matrice de modélisation / vue / projection
        /// </summary>
        private void UpdateConstantBuffer()
        {
            UpdateWorlViewProjStruct();
            constantbuffer_.UpdateStruct( worldviewproj_ );
        }

        private void UpdateWorlViewProjStruct()
        {
            worldviewproj_.World = worldmatrix_;

            if ( View.Current == null )
            {
                worldviewproj_.View = new Matrix();
                worldviewproj_.Projection = new Matrix();
                worldviewproj_.CameraPosition = new Vector3();
            }
            else
            {
                if ( View.Current.Transformation.islookingat_ || View.Current.Transformation.custommatrix_ )
                {
                    Matrix matrixView =  View.Current.Transformation.localmatrix_;
                    worldviewproj_.View = matrixView;
                    
                    worldviewproj_.CameraPosition = matrixView.TranslationVector;

                    
                }
                else
                {
                    worldviewproj_.View = View.Current.Transformation.GetViewMatrix();
                    worldviewproj_.CameraPosition = View.Current.Transformation.position_;
                }

                worldviewproj_.Projection = View.Current.projection_.Data;
            }

            Matrix invert = worldmatrix_;
            invert.Invert();
            worldviewproj_.WorldInverse = invert;
        }

        // Dans le cas ou noInheritance est à true, on utilise pas la matrice du parent
        public bool noInheritance_;
        public bool IsViewMatrix;

        public Vector3 position_;
        public Vector3 eulerangle_;
        public Vector3 scaling_;

        Matrix rotationmatrix_;
        Matrix positionmatrix_;

        public Matrix worldmatrix_;
        public Matrix localmatrix_;
        Matrix scalematrix_;

        Vector3 forward_;
        Vector3 right_;
        Vector3 up_;

        private Transform m_parent;

        // Booléen qui permet de définir si on utilise une matrix particulière
        private bool custommatrix_;

        // Booléen dans le cas ou on utilise "look at"
        private bool islookingat_;

        /// <summary>Contient les données concernant les matrices de modélisation, de vue et de projection
        /// qui seront envoyé au shader
        /// </summary>
        private CBuffer<WorldViewProj> constantbuffer_;

        private WorldViewProj worldviewproj_;

    }

}
