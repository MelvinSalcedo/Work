using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Troll3D{

    
    public enum KeyCode {
        Key_1,
        Key_2,
        Key_3,
        Key_4,
        Key_5,
        Key_6,
        Key_7,
        Key_8,
        Key_9,
        Key_0,
        Key_A,
        Key_B,
        Key_C,
        Key_D,
        Key_E,
        Key_F,
        Key_G,
        Key_H,
        Key_I,
        Key_J,
        Key_K,
        Key_L,
        Key_M,
        Key_N,
        Key_O,
        Key_P,
        Key_Q,
        Key_R,
        Key_S,
        Key_T,
        Key_U,
        Key_V,
        Key_W,
        Key_X,
        Key_Y,
        Key_Z,
        Key_Undefined
    }

    public enum InputEventType {
        MouseEvent,
        KeyboardEvent
    }

    public enum MouseEventType {
        MouseMove,
        MouseDrag,
        MouseWheel,
        MouseDown,
        MouseUp
    };

    public enum KeyboardEventType {
        KeyDown, // KeyDown se déclenche tant que la touche n'a pas été relaché
        KeyUp,
        KeyPressed      // KeyPressed ne se déclence qu'une fois au moment de la pression
    }

    public class InputEvent {

        // Public

            // Lifecycle

                public InputEvent() {}

            // Datas

                public InputEventType type_;
    }
}
