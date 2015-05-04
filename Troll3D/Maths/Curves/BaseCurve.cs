using System;
using System.Collections.Generic;
using SharpDX;

namespace Troll3D{

    public enum CurveType{
        CURVE_HERMITE,
        CURVE_BEZIER,
        CURVE_BSPLINE
    }

    /// <summary>
    ///     Une courbe paramêtré est, comme son nom l'indique, controlé par des points de controles
    /// D'ou l'utilisation d'une classe abstraite pour stocker ces derniers et créer la fonction
    /// qui permet de récupérer un point situé sur la courbe sur une échelle [0.0f; 1.0f]
    /// </summary>
    public abstract class BaseCurve{

        // Public 

            //  Virtual Methods 

                public abstract void ConstructSpline();

            //  Methods

                /// <summary>
                /// Insère un point de controle à la position indiqué
                /// Si rien n'est précisé, insère le point de controle à la fin de la liste
                /// </summary>
                public void InsertControlPoint(Vector3 item, int index =-1 ){
                    if (index == -1){
                        m_controlpoints.Add(item);
                    }
                    m_controlpoints.Insert(index, item);
                }

                public void AddControlPoint(Vector3 item){
                    m_controlpoints.Add(item);
                }

                public void DeleteControlPoint(int index){
                    m_controlpoints.RemoveAt(index);
                }

                /// <summary>
                /// Modifie la valeur du point de controle à l'indice donnée
                /// </summary>
                /// <param name="value"></param>
                /// <param name="index"></param>
                public void UpdateControlPoint(Vector3 value, int index){
                    m_controlpoints[index] = value;
                }

                public Vector3 ControlPoint(int index){

                    if (index >= 0 && index < m_controlpoints.Count){
                        return new Vector3(m_controlpoints[index].X,
                       m_controlpoints[index].Y,
                       m_controlpoints[index].Z);
                    }
                    return Vector3.Zero;
                }

                public void SetDiscretisation(int value){
                    m_discretisation = value;
                }

            // Accesseurs

                public List<Vector3> GetContolPoints(){
                    return m_controlpoints;
                }

                public int GetControlPointCount(){
                    return m_controlpoints.Count;
                }

                public List<Vector3> GetCurve(){
                    return m_points;
                }

                public Point[] GetControlPolygon(){

                    Point[] controlPolygonPoints = new Point[m_controlpoints.Count + 1];

                        for (int i = 0; i < m_controlpoints.Count; i++)
                        {
                            controlPolygonPoints[i] = new Point((int)m_controlpoints[i].X, (int)m_controlpoints[i].Y);
                        }
                        controlPolygonPoints[m_controlpoints.Count] = new Point((int)m_controlpoints[0].X, (int)m_controlpoints[0].Y);
                        return controlPolygonPoints;
                    
                }

                public int GetDiscretisation(){
                    return m_discretisation;
                }

           
        // Protected

            // Datas

                protected CurveType         m_type;
                protected List<Vector3>     m_controlpoints;
                protected List<Vector3>     m_points;
                protected int               m_discretisation;
    }
}
