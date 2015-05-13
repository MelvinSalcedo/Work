using System;
using System.Collections.Generic;
using SharpDX;

namespace Troll3D
{
    public enum hermiteState
    {
        Vertex,
        Tan
    };

    public class Hermite 
    {
        /// <summary>
        /// Construit une nouvelle courbe Hermitienne
        /// </summary>
        public Hermite(int discretisation = 10)
        {
            step = hermiteState.Vertex;
            m_discretisation = discretisation;
            poid = 2;
            segments_ = 0;
        }

        /// <summary>
        /// Ajoute un sommet ou une tangente à la courbe de Hermite
        /// </summary>
        public void AddVertex( Vector3 vec )
        {
            if ( step == hermiteState.Vertex )
            { // Ajout d'un sommet à la courbe
                AddPoint( vec );
            }
            else
            {        //  Ajoute la dérivé d'un sommet
                AddTangente( vec );
            }
        }

        /// <summary>
        ///  Discretise la courbe en un ensemble de sommet
        /// </summary>
        void ComputeVertices()
        {
            //  Valeur de t qui varie entre 0 et 1
            float t = 0.0f;

            //  Si on a au moins un segment de courbe de Hermite
            if ( segments_ > 0 )
            {
                // J'efface mon tableau de sommets
                vertices_.Clear();

                //  Pour chaque segment de la courbe
                for ( int i = 0; i < segments_; i++ )
                {
                    //  Je crée autant de sommet que le niveau de discretisation spécifié
                    for ( int j = 0; j < m_discretisation; j++ )
                    {

                        float s = 1.0f / ( float )m_discretisation;
                        t = j * s;

                        float tpow3 = ( float )Math.Pow( t, 3 );
                        float tpow2 = ( float )Math.Pow( t, 2 );
                        Vector3 vec;

                        // Ca doit très certainement être l'équation des courbes hermitiennes
                        vec.X = ( ( 2 * tpow3 - 3 * tpow2 + 1 ) * vertexControl[i].X )
                                + ( ( tpow3 - 2 * tpow2 + t ) * ( tan[i].X * poid ) )
                                + ( ( -2 * tpow3 + 3 * tpow2 ) * vertexControl[i + 1].X )
                                + ( ( tpow3 - tpow2 ) * ( tan[i + 1].X * poid ) );

                        vec.Y = ( ( ( 2 * tpow3 - 3 * tpow2 + 1 ) * vertexControl[i].Y )
                                + ( ( tpow3 - 2 * tpow2 + t ) * ( tan[i].Y * poid ) )
                                + ( ( -2 * tpow3 + 3 * tpow2 ) * vertexControl[i + 1].Y )
                                + ( ( tpow3 - tpow2 ) * ( tan[i + 1].Y * poid ) ) );

                        vec.Z = 0.0f;

                        vertices_.Add( vec ); //  On ajoute le vecteur dans la liste des sommets
                    }
                }
            }

        }

        List<Vector3> vertexControl;      //  Coordonnées des points de controles de la courbe
        List<Vector3> tan;                //  Vecteur représentant la force et l'orientation de la tangente pour le point correspondant

        List<Vector3> vertices_;

        float poid;

        private hermiteState step;               //  Cette variable permet de savoir si la prochaine insertion via Add Vertex
        // représentera un sommet ou une tangente

        int segments_;          //  Définit le nombre de segments que comporte la courbe
        int m_discretisation;     //  Définit le niveau de discretisation d'un segment de la courbe de Hermite


        private void AddTangente( Vector3 v )
        {
            tan.Add( vertexControl[vertexControl.Count - 1] - v );   //  Calcul du vecteur entre le dernier sommet

            //  La prochaine étape consistera à saisir un nouveau sommet
            step = hermiteState.Vertex;

            //  Dans le cas ou on a plus de 2 tangente d'enregistré, alors on considère qu'un nouveau
            //  segment de la courbe de Hermite à été crée
            if ( tan.Count >= 2 )
            {
                segments_++;
            }
        }

        private void AddPoint( Vector3 v )
        {
            vertexControl.Add( v );
            //  Je précise que la prochaine étape consistera à ajouter la tangente
            //  au sommet qui vient d'être saisi
            step = hermiteState.Tan;   
        }
    }
}
