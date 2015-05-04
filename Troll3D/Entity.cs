using System;
using System.Collections.Generic;
using SharpDX.Direct3D11;
using SharpDX;

using Troll3D.Components;

namespace Troll3D
{
    // Les objets de la classe Entity seront répertorié par le programme Direct, et pourront porter les 
    // composants de base d'une application 3D (Mesh, transformation etc)
    // La classe Entity va également stocker son parent et ses descendant. La classe transform utilisera
    // l'accesseur de Entity pour récupérer le transform de son parent et déterminer sa position
    public class Entity
    {

        public Entity( Entity parent = null )
        {
            m_components = new List<TComponent>();
            layer_          = 0;
            transform_ =(Transform) AddComponent( new Transform() );
            sons_           = new List<Entity>();
            Scene.CurrentScene.Sons.Add( this );

            parent_         = parent;
            show_       = true;
            GlowEffet   = new GlowEffect();
            IsPickingActivated = true;
            Name = "New Entity";
        }

        /// <summary> 
        /// Ajoute un composant à l'entité. Si l'entité dispose déjà du composant, retourne null </summary>
        /// <param name="componentToAdd"></param>
        /// <returns></returns>
        public TComponent AddComponent(TComponent componentToAdd)
        {
            foreach ( TComponent component in m_components )
            {
                if ( component.Type == componentToAdd.Type )
                {
                    return null;
                }
            }

            m_components.Add( componentToAdd );
            componentToAdd.Attach( this );
            return componentToAdd;
        }

        public T AddComponent<T>() where T : TComponent, new()
        {
            T component = new T();

            foreach(TComponent comp in m_components)
            {
                if ( comp.Type == component.Type )
                {
                    return null;
                }
            }
            m_components.Add( component );
            component.Attach( this );
            return component;
        }

        public TComponent GetComponent(ComponentType type) 
        {
            foreach ( TComponent component in m_components )
            {
                if ( component.Type == type )
                {
                    return component;
                }
            }
            return null;
        }

        public void Delete()
        {
            if ( parent_ != null )
            {
                parent_.RemoveSon( this );
            }
        }

        public void SetLayerRecursivly( int layerval )
        {
            layer_ = layerval;
            for ( int i = 0; i < sons_.Count; i++ )
            {
                sons_[i].SetLayerRecursivly( layerval );
            }
        }

        public Entity Append( Entity son )
        {
            son.parent_ = this;
            son.transform_.parent = this.transform_;
            sons_.Add( son );
            return son;
        }

        public Entity Parent
        {
            get { return parent_; }
        }

        public int SonsCount
        {
            get { return sons_.Count; }
        }

        public Entity Son( int i )
        {
            return sons_[i];
        }

        public void RemoveSon( int i )
        {
            sons_.RemoveAt( i );
        }

        public void RemoveSon( Entity son )
        {
            sons_.Remove( son );
        }

        public virtual void Update()
        {
            UpdateComponents();

            for ( int i = 0; i < sons_.Count; i++ )
            {
                sons_[i].Update();
            }
        }

        public void AddGlowEffect( Vector4 color )
        {
            GlowEffet = new GlowEffect( color );
            IsGlowing = true;
        }

        public void AddGlowEffect( Texture2D texture )
        {
            GlowEffet = new GlowEffect( texture );
            IsGlowing = true;
        }

        public Guid m_id;               // Identifiant unique de l'entité

        /// <summary> Nom de l'entité </summary>
        public string Name;

        public bool IsPickingActivated;
        public GlowEffect GlowEffet;
        public bool IsGlowing = false;

        public bool show_;
        public int layer_;

        public List<Entity> sons_;
        private Entity parent_;

        public bool showbb;
        public Transform transform_;

        public virtual void BeforeDraw()
        {
            for ( int i = 0; i < sons_.Count; i++ )
            {
                sons_[i].BeforeDraw();
            }
        }

        public virtual void AfterDraw()
        {
            for ( int i = 0; i < sons_.Count; i++ )
            {
                sons_[i].AfterDraw();
            }
        }

        public virtual void BeforeUpdate()
        {
            for ( int i = 0; i < sons_.Count; i++ )
            {
                sons_[i].BeforeUpdate();
            }
        }

        public virtual void AfterUpdate()
        {
            for ( int i = 0; i < sons_.Count; i++ )
            {
                sons_[i].AfterUpdate();
            }
        }

        private List<TComponent> m_components;


        private void UpdateComponents()
        {
            foreach ( TComponent component in m_components )
            {
                component.Update();
            }
        }

    }
}
