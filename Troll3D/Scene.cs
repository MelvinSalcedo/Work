using System;
using System.Collections.Generic;
using SharpDX;
using SharpDX.Direct3D11;
using SharpDX.DXGI;

using Troll3D.Components;

namespace Troll3D
{
    public class Scene
    {
        /// <summary>Enregistre la scène en cours  </summary>
        public static Scene CurrentScene;

        /// <summary> Enregistre toutes les scènes existantes  </summary>
        public static List<Scene> Scenes;

        public Scene() : base()
        {
            Initialize(new Color4( 0.0f, 0.1f, 0.6f, 1.0f));
        }

        public Scene(Color4 backgroundcolor): base() 
        {
            Initialize(backgroundcolor);
        }

        public void Initialize(Color4 color) 
        {
            Renderables = new List<IRenderable>();
            Sons = new List<Entity>();
            AllEntities = new List<Entity>();
            backgroundcolor_ = color;
            entities = new Dictionary<string, Entity>();
            CurrentScene = this;
            // Une scène se doit de posséder au moins une caméra
            Entity entity = new Entity();
            entity.AddComponent(new Camera( ));
            ( ( Camera )entity.GetComponent( ComponentType.Camera ) ).Initialize( new FrustumProjection( 3.141592f / 3.0f, Screen.Instance.GetRatio(), 0.1f, 1000.0f ) );
        }

        public Entity Append( Entity entity )
        {
            Sons.Add( entity );
            return entity;
        }

        //public Entity NewEmptyEntity(string name="NewEntity") 
        //{
        //    Entity ent = new Entity();
        //    if (entities.ContainsKey(name)) 
        //    {
        //        name = FindName(name, 0);
        //        entities.Add(name, ent);
        //    }
        //    return Append(ent);
        //}

        public virtual void UpdateTransform() {}

        public void RemoveRenderable( IRenderable renderable )
        {
            Renderables.Remove( renderable );
        }

        public void Render( )
        {
            //Camera.SetCameraView();
            foreach ( IRenderable renderable in Renderables )
            {
                renderable.Render();
            }
        }

        public void Update()
        {
            foreach ( Entity entity in Sons )
            {
                entity.Update();
            }
        }

        public Color4 backgroundcolor_;
        public List<IRenderable> Renderables;

        /// <summary>Sons enregistre juste les entités qui n'ont pas de parent </summary>
        public List<Entity> Sons;

        /// <summary> All Entities quant à lui enregistre toutes les entités de la scène, utile s'il faut parcourir 
        /// rapidement toutes les entités</summary>
        public List<Entity> AllEntities;
                
        /// <summary> Enregistre les entités présente dans la scène </summary>
        public Dictionary<string, Entity> entities;
                
        /// <summary>  Cherche un nom d'entité non utilisé </summary>
        private string FindName(string name, int index) 
        {
            if (entities.ContainsKey(name + "(" + index + ")"))
            {
                return FindName(name, index + 1);
            }
            return (name + "(" + index + ")");
        }
    }

}
