using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Troll3D {

    // Les layers permettent d'éviter de tester absolument tout les objets entre eux en leur
    // assignants un "filtre". Par exemple, le layer 1 sera de type "projectile" et le layer 2 "joueur"
    // on ne veut pas forcément que les projectiles testent leur collision entre eux, surtout si plusieurs centaines
    // sont actifs (on rappelle que les test entre collisions évoluent de manière quadratique, n^2)
    public class LayerManager {

        // Public

            // Static Datas

                public static LayerManager Instance;

            // Lifecycle

                public  LayerManager() {
                    Instance = this;
                    layers_ = new List<Layer>();
                    AddLayer(); // JE rajoute un layer de manière à ce que par défault, tout rentre en collision
                    // à moi ensuite de rajouter des layers et de gérer le layers des objets pour optimiser les perfs
                    // et éviter des collisions inutiles
                }

            // Methods

                public void AddLayer() {

                    layers_.Add(new Layer(layers_.Count));
                    UpdateLayers();
                }

                private void UpdateLayers() {
                    for (int i = 0; i < layers_.Count; i++) {
                        layers_[i].collisions_.Add(false);
                    }
                }

                // Vérifie si une collisions à lieu entre les 2 layers entré en paramètre

                public bool AreColliding(int i, int j) {
                    return layers_[i].IsColliding(j);
                }

                // Définit une collision effective entre 2 layers
                public void SetCollisionBetweenLayers(int i, int j) {
                    layers_[i].Collide(j);
                    layers_[j].Collide(i);
                }

            // Datas
                public List<Layer> layers_;

    }

    
    public class Layer{

        // Public
            
            // Lifecycle

                public Layer(int size) {
                    collisions_ = new List<bool>();
                    collisions_.Add(true);

                    for (int i = 0; i < size - 1; i++) {
                        collisions_.Add(false);
                    }
                }

            // Methods

                public void Collide(int index) {
                    collisions_[index] = true;
                }

                public bool IsColliding(int index) {
                    return collisions_[index];
                }


            // Datas

                public List<bool> collisions_;

                



    }
}
