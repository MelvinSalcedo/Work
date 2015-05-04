using SharpDX;
using System.Collections;
using System.Collections.Generic;

namespace Troll3D{

    /*! @brief Un quadtree est un arbre composé de noeud. Chaque noeud est ensuite décomposé en 4 sous noeuds
     *
     *                         |------|
     *                         | quad |
     *                         |------|
     *                      /    /  \    \
     *                  /       /    \       \
     *              /          /      \          \
     *           /            /        \             \
     *  |------|       |------|        |------|        |------|
     *  | quad |       | quad |        | quad |        | quad |
     *  |------|       |------|        |------|        |------|
     */
    public class Quadtree<T>{

        // Public

            // Lifecycle

                public Quadtree(){
                }

                public Quadtree(T rootdata){
                    root_ = new QuadNode<T>(rootdata);
                }

                public Quadtree(QuadNode<T> rootnode){
                    root_ = rootnode;
                }

            // Methods

                public QuadNode<T> Root{
                    get{ return root_;}
                }

            // Datas

                public QuadNode<T> root_;

    }
}