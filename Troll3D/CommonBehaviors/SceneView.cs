using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Troll3D.Components
{

    /// <summary>
    /// Une FreeView permet de se déplacer librement dans la scène. La touche Z permet de se déplacer dans 
    /// la direction dans laquelle la caméra pointe
    /// </summary>
    public class SceneView : Behaviour
    {

        // Public

        // Lifecycle

        public SceneView() { }

        // Methods

        public void Init(float speed)
        {
            Speed = speed;
        }


        public override void OnMouseWheel(MouseEvent e){

            Entity.transform_.Translate( e.mouse_.wheeldelta * Entity.transform_.GetForwardVector() * 0.005f );
        }

        public override void OnMouseMove(MouseEvent e)
        {
            if (e.mouse_.leftbutton){
                Entity.transform_.Translate( e.mouse_.deltax * Entity.transform_.GetRightVector() * 0.05f );
                Entity.transform_.Translate( -e.mouse_.deltay * Entity.transform_.GetUpVector() * 0.05f );
            }
        }

        public override void Update()
        {
            base.Update();
        }



        // Datas

        public float Speed = 0.1f;

        // Private

        // Datas

        private float m_XOffset;
        private float m_YOffset;
    }
}
