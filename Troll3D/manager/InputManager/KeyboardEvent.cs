using System;
using System.Collections.Generic;


namespace Troll3D{

    public class KeyboardEvent : InputEvent{

        // Public

            // Lifecycle

                public KeyboardEvent(KeyboardEventType type, KeyCode keycode) {
                    type_           = InputEventType.KeyboardEvent;
                    keyboardtype_   = type;
                    keycode_        = keycode;
                }

            // Datas

                public KeyboardEventType    keyboardtype_;
                public KeyCode              keycode_;
    }
}
