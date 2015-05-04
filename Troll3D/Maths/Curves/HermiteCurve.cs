using System;
using System.Collections.Generic;


namespace Troll3D
{

    //public enum hermiteState
    //{
    //    Vertex,
    //    Tan
    //};

    //public class Hermite
    //{

    //    // Public

    //    // Lifecycle

    //    public Hermite()
    //    {
    //        step = Vertex;
    //        discretisation = 10;
    //        color = vec3(1.0, 1.0, 0.0);
    //        poid = 2;
    //        segments_ = 0;
    //    }

    //    // Methods

    //    /**
    //     * @brief Ajoute un sommet ou une tangente à la courbe de Hermite
    //     */
    //    public void AddVertex(Vector3 vec)
    //    {
    //        if (step == Vertex)
    //        { // Ajout d'un sommet à la courbe
    //            AddPoint(vec);
    //        }
    //        else
    //        {        //  Ajoute la dérivé d'un sommet
    //            AddTangente(vec);
    //        }
    //    }


    //    /**
    //     * @brief ComputeVertices : Discretise la courbe en un ensemble de sommet
    //     */
    //    void ComputeVertices()
    //    {

    //        float t = 0.0;    //  Valeur de t qui varie entre 0 et 1

    //        if (segments_ > 0)    //  Si on a au moins un segment de courbe de Hermite
    //        {
    //            vertices_.clear(); //    J'efface mon tableau de sommets

    //            for (int i = 0; i < segments_; i++)
    //            {    //  Pour chaque segment de la courbe
    //                qDebug() << i;
    //                for (int j = 0; j < discretisation; j++)
    //                {   //  Je crée autant de sommet que le niveau de discretisation spécifié

    //                    float s = 1.0 / (float)discretisation;
    //                    t = j * s;

    //                    vec3 vec;

    //                    vec.x(((2 * pow(t, 3) - 3 * pow(t, 2) + 1) * vertexControl[i].x())
    //                            + ((pow(t, 3) - 2 * pow(t, 2) + t) * (tan[i].x() * poid))
    //                            + ((-2 * pow(t, 3) + 3 * pow(t, 2)) * vertexControl[i + 1].x())
    //                            + ((pow(t, 3) - pow(t, 2)) * (tan[i + 1].x() * poid)));

    //                    vec.y(((2 * pow(t, 3) - 3 * pow(t, 2) + 1) * vertexControl[i].y())
    //                            + ((pow(t, 3) - 2 * pow(t, 2) + t) * (tan[i].y() * poid))
    //                            + ((-2 * pow(t, 3) + 3 * pow(t, 2)) * vertexControl[i + 1].y())
    //                            + ((pow(t, 3) - pow(t, 2)) * (tan[i + 1].y() * poid)));

    //                    vec.z(-1);

    //                    qDebug() << vec.x() << " " << vec.y();
    //                    //qDebug()<<t;
    //                    vertices_.push_back(vec); //  On ajoute le vecteur dans la liste des sommets
    //                }
    //            }
    //        }

    //    }

    //    // Datas

    //    List<Vector3> vertexControl;      //  Coordonnées des points de controles de la courbe
    //    List<Vector3> tan;                //  Vecteur représentant la force et l'orientation de la tangente pour le point correspondant

    //    List<Vector3> vertices_;

    //    float poid;

    //    hermiteState step;               //  Cette variable permet de savoir si la prochaine insertion via Add Vertex
    //    // représentera un sommet ou une tangente

    //    int segments_;          //  Définit le nombre de segments que comporte la courbe
    //    int discretisation;     //  Définit le niveau de discretisation d'un segment de la courbe de Hermite

    //    Vector3 color;              //  Couleur de la courbe

    //    // Private

    //    // Methods

    //    private void AddTangente(vec3 v)
    //    {
    //        tan.push_back(vertexControl.back() - v);   //  Calcul du vecteur entre le dernier sommet

    //        step = Vertex;    //  La prochaine étape consistera à saisir un nouveau sommet


    //        if (tan.size() >= 2)   //  Dans le cas ou on a plus de 2 tangente d'enregistré, alors on considère qu'un nouveau
    //        {                   //  segment de la courbe de Hermite à été crée
    //            segments_++;
    //        }
    //    }

    //    private void AddPoint(vec3 v)
    //    {
    //        vertexControl.push_back(v);

    //        step = Tan;   //  Je spécifie que la prochaine étape consistera à ajouter la tangente
    //        //  au sommet qui vient d'être saisi
    //    }

   // }
}
