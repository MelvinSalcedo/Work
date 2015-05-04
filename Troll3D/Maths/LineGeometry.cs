using System;
using System.Collections.Generic;
using SharpDX;

namespace Troll3D{

    /// <summary>
    /// LineGeometry représente une droite "mathématiquement" Pour rappel, l'équation cartésienne
    /// d'une droite est ax+by+c=0 
    /// 
    /// Quant à l'équation paramétrique, elle s'exprime de la manière suivante : 
    /// X = at + Xa
    /// Y = bt + Ya
    /// Z = ct + Za
    /// </summary>
    public class LineGeometry{

        // Public

            // Lifecycle

                /// <summary>
                /// On définit les paramètres de la courbe à partir d'un point dans l'espace et de son vecteur directeur
                /// </summary>
                public LineGeometry(Vector3 point, Vector3 directeur){
                    m_point         = point;
                    m_direction     = Vector3.Normalize(directeur);
                }

                /// <summary>
                /// On définit directement les coefficients de l'équation cartésienne
                /// </summary>
                public LineGeometry(float a, float b, float c){
                    m_a = a;
                    m_b = b;
                    m_c = c;
                    m_isCartesienne = true;
                }

            // Methods

                /// <summary>
                /// Retourne le coefficient a de la droite ax+by+c=0
                /// </summary>
                public float GetA(){
                    return m_a;
                }

                /// <summary>
                /// Retourne le coefficent b de la droite ax+by+c=0
                /// </summary>
                public float GetB(){
                    return m_b;
                }

                /// <summary>
                /// Retourne le coefficient c de la droite ax+by+c=0
                /// </summary>
                public float GetC(){
                    return m_c;
                }

                /// <summary>
                /// Si la droite à été défini de manière paramétrique, retourne le point P
                /// </summary>
                /// <returns></returns>
                public Vector3 GetPoint(){
                    if (m_isCartesienne)
                    {
                        return Vector3.Zero;
                    }
                    return m_point;
                }

                public Vector3 GetDirection(){
                    if (m_isCartesienne){
                        return Vector3.Zero;
                    }
                    return m_direction;
                }

        // Private
            
            // Datas

                /// <summary>
                /// Permet de savoir si la droite à été déterminer de manière cartésienne ou non
                /// </summary>
                private bool m_isCartesienne;
                
                /// <summary>
                /// Coefficients de l'équation cartésienne
                /// </summary>
                private float m_a;
                private float m_b;
                private float m_c;

                private Vector3 m_point;
                private Vector3 m_direction;
    }
}
