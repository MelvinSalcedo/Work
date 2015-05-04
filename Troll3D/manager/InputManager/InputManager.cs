using System;
using System.Collections.Generic;
using SharpDX;
using SharpDX.Windows;
using System.Windows.Forms;


namespace Troll3D{

    /// <summary>
    /// La classe InputManager sert à récupérer les événéments clavier/souris de la fenêtre, à les mettre dans une stack
    /// qui, à chaque rafraichissement de l'image, est inspecté et dispatché aux différentes entités de la scène
    /// </summary>
    public class InputManager {

        public delegate void MouseEventDelegate(MouseEvent e);
        public delegate void KeyEventDelegate(KeyboardEvent e);

        public event MouseEventDelegate MouseButtonDown;
        public event MouseEventDelegate MouseButtonUp;
        public event MouseEventDelegate MouseWheel;
        public event MouseEventDelegate MouseMove;

        public event KeyEventDelegate KeyUp;
        public event KeyEventDelegate KeyDown;

        public static InputManager Instance;

        public InputManager() 
        {
            Instance            = this;
            events_             = new Stack<InputEvent>();
            mouseinformation_   = new MouseInformation();
        }

        public InputManager(RenderControl rendercontrol)
        {
            events_ = new Stack<InputEvent>();
            Instance = this;
            mouseinformation_ = new MouseInformation();
            rendercontrol.MouseMove += OnMouseMove;
            rendercontrol.MouseDown += OnMouseDown;
            rendercontrol.MouseUp += OnMouseUp;
            rendercontrol.MouseWheel += OnMouseWheel;
            rendercontrol.KeyDown += OnKeyDown;
            rendercontrol.KeyPress += OnKeyPress;
            rendercontrol.KeyUp += OnKeyUp;
        }

        public InputManager(RenderForm renderform) {
            events_ = new Stack<InputEvent>();
            Instance = this;
            mouseinformation_ = new MouseInformation();
            renderform.MouseMove    += OnMouseMove;
            renderform.MouseDown    += OnMouseDown;
            renderform.MouseUp      += OnMouseUp;
            renderform.MouseWheel   += OnMouseWheel;
            renderform.KeyDown      += OnKeyDown;
            renderform.KeyPress     += OnKeyPress;
            renderform.KeyUp        += OnKeyUp;
        }

        public bool IsKeyPressed(KeyCode code)
        {
            return true;
        }

        public void Update() 
        {

            InputEvent e;

            if (events_.Count > 0) 
            {
                while (events_.Count>0) 
                {
                    e = events_.Pop();
                    switch (e.type_) 
                    {

                        case InputEventType.KeyboardEvent:

                            KeyboardEvent ke = (KeyboardEvent)e;

                            switch (ke.keyboardtype_) 
                            {
                                case KeyboardEventType.KeyDown:
                                    if ( KeyDown != null )
                                    {
                                        KeyDown.Invoke( ke );
                                    }
                                break;

                                case KeyboardEventType.KeyUp :
                                    if ( KeyUp != null )
                                    {
                                        KeyUp.Invoke( ke );
                                    }
                                break;

                            }

                        break;

                        case InputEventType.MouseEvent:

                            MouseEvent me = (MouseEvent)e;

                            switch (me.mouseeventtype_) 
                            {

                                case MouseEventType.MouseMove:

                                    if( MouseMove!=null)
                                    {
                                        MouseMove.Invoke( me );
                                    }
                                    
                                break;

                                case MouseEventType.MouseDown:
                                    if ( MouseButtonDown != null )
                                    {
                                        MouseButtonDown.Invoke( me );
                                    }
                                    
                                break;

                                case MouseEventType.MouseUp:
                                    if ( MouseButtonUp != null )
                                    {
                                        MouseButtonUp.Invoke( me );
                                    }
                                break;

                                case MouseEventType.MouseWheel:
                                    if ( MouseWheel != null )
                                    {
                                        MouseWheel.Invoke( me );
                                    }
                                break;

                            }
                        break;
                    }
                }
            }
        }

        public void OnKeyDown(Object sender, KeyEventArgs e) 
        {
            events_.Push( new KeyboardEvent(KeyboardEventType.KeyDown, MapKey(e.KeyCode)));
        }

        public void OnKeyUp(Object sender, KeyEventArgs e) 
        {
            events_.Push( new KeyboardEvent(KeyboardEventType.KeyUp, MapKey(e.KeyCode)));
        }

        public void OnKeyPress(Object sender, KeyPressEventArgs e) 
        {
            //events_.Push( new KeyboardEvent(KeyboardEventType.KeyPressed, MapKey(e.)));
        }

        public KeyCode MapKey(Keys key) 
        {
            switch (key) 
            {
                case Keys.A:
                return KeyCode.Key_A;

                case Keys.B : 
                return KeyCode.Key_B;

                case Keys.C :
                return KeyCode.Key_C;

                case Keys.D : 
                return KeyCode.Key_D;

                case Keys.E :
                return KeyCode.Key_E;

                case Keys.F :
                return KeyCode.Key_F;

                case Keys.G:
                return KeyCode.Key_G;

                case Keys.H:
                return KeyCode.Key_H;

                case Keys.I:
                return KeyCode.Key_I;

                case Keys.J:
                return KeyCode.Key_J;

                case Keys.K:
                return KeyCode.Key_K;

                case Keys.L:
                return KeyCode.Key_L;

                case Keys.M:
                return KeyCode.Key_M;

                case Keys.N:
                return KeyCode.Key_N;

                case Keys.O:
                return KeyCode.Key_O;

                case Keys.P:
                return KeyCode.Key_P;

                case Keys.Q:
                return KeyCode.Key_Q;

                case Keys.R:
                return KeyCode.Key_R;

                case Keys.S:
                return KeyCode.Key_S;

                case Keys.T:
                return KeyCode.Key_T;

                case Keys.U:
                return KeyCode.Key_U;

                case Keys.V:
                return KeyCode.Key_V;

                case Keys.W:
                return KeyCode.Key_W;

                case Keys.X:
                return KeyCode.Key_X;

                case Keys.Y:
                return KeyCode.Key_Y;

                case Keys.Z :
                return KeyCode.Key_Z;

                case Keys.D0 :
                return KeyCode.Key_0;

                case Keys.D1:
                return KeyCode.Key_1;

                case Keys.D2:
                return KeyCode.Key_2;

                case Keys.D3:
                return KeyCode.Key_3;

                case Keys.D4:
                return KeyCode.Key_4;

                case Keys.D5:
                return KeyCode.Key_5;

                case Keys.D6:
                return KeyCode.Key_6;

                case Keys.D7:
                return KeyCode.Key_7;

                case Keys.D8:
                return KeyCode.Key_8;

                case Keys.D9:
                return KeyCode.Key_9;

                default :
                return KeyCode.Key_Undefined;
            }
        }

        public void OnMouseMove(Object sender, MouseEventArgs  e) 
        {
            mouseinformation_.Update(
                e.X,
                e.Y,
                e.X  - mouseinformation_.x  ,
                e.Y - mouseinformation_.y,
                e.Delta,
                mouseinformation_.leftbutton,
                mouseinformation_.rightbutton,
                mouseinformation_.middlebutton);

            events_.Push(new MouseEvent(MouseEventType.MouseMove, mouseinformation_));
        }

        public void OnMouseDown(Object sender, MouseEventArgs e) {
            if(e.Button== MouseButtons.Left){
                mouseinformation_.UpdateLeftButton(true);
            }
            if(e.Button== MouseButtons.Right){
                mouseinformation_.UpdateRightButton(true);
            }
            if(e.Button== MouseButtons.Middle){
                mouseinformation_.UpdateMiddleButton(true);
            }
            events_.Push(new MouseEvent(MouseEventType.MouseDown, mouseinformation_));
        }

        public void OnMouseUp(Object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                mouseinformation_.UpdateLeftButton(false);
            }
            if (e.Button == MouseButtons.Right) {
                mouseinformation_.UpdateRightButton(false);
            }
            if (e.Button == MouseButtons.Middle) {
                mouseinformation_.UpdateMiddleButton(false);
            }
            events_.Push(new MouseEvent(MouseEventType.MouseUp, mouseinformation_));
        }

        public void OnMouseWheel(Object sender, MouseEventArgs e) {

            mouseInformation.UpdateWheelValue(e.Delta);
            events_.Push(new MouseEvent(MouseEventType.MouseWheel, mouseinformation_));
        }

        public Stack<InputEvent> events_;

        public MouseInformation mouseInformation { get { return mouseinformation_; } }

        private MouseInformation mouseinformation_;

            
    }
}
