using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Troll3D{

    public class MouseEvent : InputEvent {

        // Public

            // Lifecycle

                public MouseEvent( MouseEventType type,  MouseInformation info) {
                    type_ = InputEventType.MouseEvent;
                    mouseeventtype_ = type;
                    mouse_ = info;
                }

            // Methods

                public MouseEventType mouseeventtype_;
                public MouseInformation mouse_;

    }
}
