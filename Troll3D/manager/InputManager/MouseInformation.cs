using System;
using System.Collections.Generic;
using SharpDX;

namespace Troll3D{

    /// <summary>
    ///  Objet géré par InputManager. Contient les informations concernant la souris. L'état des boutons,
    ///  du wheel, sa position actuelle et au dernier rafraichissement
    /// </summary>
    public class MouseInformation {

        public void Update(int ax, int ay, int adeltax, int adeltay, int awheeldelta, bool aleftbut, bool arightbutton, bool amiddlebutton) {
            x_              = ax;
            y_              = ay;
            deltax_         = adeltax;
            deltay_         = adeltay;
            wheelval_       = awheeldelta;
            leftbutton_     = aleftbut;
            rightbutton_    = arightbutton;
            middlebutton_   = amiddlebutton;
        }

        public void UpdateLeftButton(bool value) {
            leftbutton_ = value;
        }

        public void UpdateRightButton(bool value) {
            rightbutton_ = value;
        }

        public void UpdateMiddleButton(bool value) {
            middlebutton_ = value;
        }

        public void UpdateWheelValue(int value) {
            wheelval_ = value;
        }

        public Vector2 xy { get { return new Vector2(x_, y_); } }
        public int x { get { return x_; } }
        public int y { get { return y_; } }

        public int deltax { get { return deltax_; } }
        public int deltay { get { return deltay_; } }

        public int wheeldelta { get { return wheelval_; } }

        public bool leftbutton      { get { return leftbutton_; } }
        public bool rightbutton     { get { return rightbutton_; } }
        public bool middlebutton    { get { return middlebutton_; } }

        private int x_;
        private int y_;

        private int deltax_;
        private int deltay_;

        private int wheelval_;

        private bool leftbutton_;
        private bool middlebutton_;
        private bool rightbutton_;


    }
}
