using System;
using System.Collections.Generic;
using SharpDX;

namespace Troll3D
{
    /// <summary>
    ///  Classe utilisé pour représenté un plan "mathématiquement". Petit rappel, un plan est déterminé soit par
    ///  un point P et 2 vecteurs directeurs u et v, ou par un point P et sa normale
    /// </summary>
    public class Plane{

        // Public

            // Lifecycle

                /// <summary>
                /// Construction d'un plan à partir d'un point dans l'espace et de 2 vecteurs directeurs.
                /// La normale du plan sera construite à partir d'un produit vectoriel des 2 vecteurs directeurs
                /// </summary>
                public Plane(Vector3 point, Vector3 tangent, Vector3 bitangent){
                    m_point         = point;
                    m_tangent       = Vector3.Normalize(tangent);
                    m_bitangent     = Vector3.Normalize(bitangent);
                    m_normal        = Vector3.Normalize(Vector3.Cross(m_tangent, m_bitangent));
                    ComputCoefficient();
                }

                /// <summary>
                /// Construction d'un plan à partir d'un point dans l'espace et de sa normale
                /// </summary>
                public Plane(Vector3 point, Vector3 normal){
                    m_point     = point;
                    m_normal    = normal;
                    ComputCoefficient();
                }

            // Methods

                /// <summary>
                /// Calcul le point d'intersection entre la droite et le plan. Pour se faire, 
                /// On prend l'équation ax+by+cz+d = 0 du plan, et on remplace x,y,z par les valeurs 
                /// différentes composantes de l'équation paramétrique de la droite 
                /// 
                /// </summary>
                public Vector3 IntersectionWithLine(LineGeometry line){

                    float denom = line.GetDirection().X * m_a + line.GetDirection().Y * m_b+ line.GetDirection().Z * m_c;

                    // non  Colinarité
                    if (denom != 0)
                    {
                        float t = -line.GetPoint().X * m_a - line.GetPoint().Y * m_b- line.GetPoint().Z * m_c - m_d;
                        t = t / denom;

                        return new Vector3(
                            line.GetPoint().X + t * line.GetDirection().X,
                            line.GetPoint().Y + t * line.GetDirection().Y,
                            line.GetPoint().Z + t * line.GetDirection().Z
                        );
                    }
                    else
                    {
                        // Dans le cas assez improbable ou la droite et le plan serait colinéaire, on renvoit le point 
                        // définit dans la droite
                        return line.GetPoint();
                    }
                }

            // Datas

        // Private

            // Methods

                private void ComputCoefficient(){
                    m_a = m_normal.X;
                    m_b = m_normal.Y;
                    m_c = m_normal.Z;
                    m_d = -(m_point.X * m_a + m_point.Y * m_b + m_point.Z * m_c);
                }

            // Datas


                // Coefficients de l'équation du plan ax+by+cz+d=0
                private float m_a;
                private float m_b;
                private float m_c;
                private float m_d;
                        

                private Vector3 m_point;
                private Vector3 m_normal;
                private Vector3 m_tangent;
                private Vector3 m_bitangent;
                
            // On recherche l'équation du plan du triangle, on a besoin de la normale pour se faire
            // Rappel, équation du plan : ax+by+cz+d=0



    }
}
