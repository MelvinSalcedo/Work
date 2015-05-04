using System;
using SharpDX;
using System.Collections.Generic;

namespace Troll3D{

    // Représente un noeud compris à l'intérieur d'un Quadtree. Peut contenir 4 enfants, ou pas 
    public class QuadNode<T>{

        // Public

            // Lifecycle

                public QuadNode(){
                    Initialize();
                }

                public QuadNode(T data){
                    Initialize();
                    data_ = data;
                }

                public void Initialize(){
                    sons_ = new List<QuadNode<T>>();
                    hassons_ = false;
                }
        
            // Methods

                // "ferme" le quad, comprendre va détruire ses enfants
                public void CloseQuad(){
                    hassons_ = false;
                    sons_.Clear();
                }

                // "ouvre" le quad, comprendre va créer ses 4 noeuds fils avec des valeurs vides
                public void OpenQuad(){
                    if (!hassons_){
                        hassons_ = true;
                        for (int i = 0; i < 4; i++){
                            sons_.Add(new QuadNode<T>());
                        }
                    }
                }

                public void OpenQuad(T a, T b, T c, T d){
                    if (!hassons_){
                        hassons_ = true;
                        sons_.Add(new QuadNode<T>(a));
                        sons_.Add(new QuadNode<T>(b));
                        sons_.Add(new QuadNode<T>(c));
                        sons_.Add(new QuadNode<T>(d));
                    }
                }

                public QuadNode<T> GetSon(int i){
                    return sons_[i];
                }

                public bool HasSons{
                    get { return hassons_; }
                }

            // Datas

                public T data_;
        
        // Private

            // Datas

                public  List<QuadNode<T>>   sons_;
                private bool                hassons_;

    }
}
