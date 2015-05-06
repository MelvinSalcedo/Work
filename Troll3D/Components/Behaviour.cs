using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Troll3D.Components
{
    /// <summary>Cette classe est faite pour être surchargé et controler le comportement d'un objet 
    ///  de la scène. Doit être lié à Entity pour fonctionner
    /// </summary>
    public class Behaviour : TComponent
    {
        public static List<Behaviour> Behaviours = new List<Behaviour>();

        public Behaviour() 
        {
            Type = ComponentType.Behavior;

            InputManager.Instance.KeyDown   += OnKeyDown;
            InputManager.Instance.KeyUp     += OnKeyUp;
            InputManager.Instance.MouseMove += OnMouseMove;
            InputManager.Instance.MouseButtonDown += OnMouseDown;
            InputManager.Instance.MouseButtonUp += OnMouseUp;
            InputManager.Instance.MouseWheel += OnMouseWheel;
            Behaviours.Add( this );
        }

        public override void Attach( Entity entity )
        {
            Entity = entity;
            Initialize();
        }

        public override void Update()
        {
            
        }
        
        /// <summary>
        ///  La méthode Initialize est invoqué juste après avoir été ajouté à un objet de type Entity via la méthode
        ///  append. Surchargé de préférence cette méthode pour tout ce qui touche à l'initialisation
        /// </summary>
        public virtual void Initialize() {}

        public virtual void BeforeUpdate() { }

        //public  virtual void Update() {}

        public virtual void AfterUpdate() { }

        public virtual void OnKeyDown(KeyboardEvent e) {}

        public virtual void OnKeyUp(KeyboardEvent e) {}

        public virtual void OnMouseWheel(MouseEvent e) {}

        public virtual void OnMouseMove(MouseEvent e) {}

        public virtual void OnMouseDown(MouseEvent e) {}

        public virtual void OnMouseUp(MouseEvent e) {}

        public virtual void BeforeDraw() {}

        public virtual void AfterDraw() {}

        public virtual void OnCollisionEnter( CollisionEvent e ) { }

        public virtual void OnCollisionExit( CollisionEvent e ) { }

        public virtual void OnCollisionStay( CollisionEvent e ) { }

        public  Entity      Entity;
        public  Transform   transform;
        
    }
}
