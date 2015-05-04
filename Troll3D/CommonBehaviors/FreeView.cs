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
    public class FreeView : Behaviour{

        // Public

            // Lifecycle

                public FreeView() { }

            // Methods

                public void Init(float speed){
                    Speed = speed;
                }

                public override void OnKeyDown(KeyboardEvent e){

                    if (e.keycode_ == KeyCode.Key_Z){
                        Entity.transform_.Translate( Entity.transform_.GetForwardVector() * Speed );
                    }
                    if (e.keycode_ == KeyCode.Key_S)
                    {
                        Entity.transform_.Translate( -Entity.transform_.GetForwardVector() * Speed );
                    }

                    if (e.keycode_ == KeyCode.Key_D)
                    {
                        Entity.transform_.Translate( Entity.transform_.GetRightVector() * Speed );
                    }
                    if (e.keycode_ == KeyCode.Key_Q)
                    {
                        Entity.transform_.Translate( -Entity.transform_.GetRightVector() * Speed );
                    }

                }

                

                public override void OnMouseMove(MouseEvent e){
                    m_XOffset -= e.mouse_.deltay * 0.01f;
                    m_YOffset += e.mouse_.deltax * 0.01f;

                    Entity.transform_.SetRotationEuler( m_YOffset, -m_XOffset, 0.0f );
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
