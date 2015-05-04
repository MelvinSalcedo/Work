//using SharpDX;
//using System.Collections.Generic;

namespace Troll3D {


    public enum Troll3DPrimitiveType {
        T3D_CIRCLE,
        T3D_HASHEDLINE,
        T3D_LINE,
        T3D_LINE_ANIMATED,
        T3D_LINE_RECT,
        T3D_POINT
    }

    public abstract class Primitive {

        // Public

            // Virtual Methods

                public abstract void Draw();

            // Datas

                public Troll3DPrimitiveType type_;
    }


}