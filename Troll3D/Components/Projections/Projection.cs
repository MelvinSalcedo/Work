using System;
using System.Collections.Generic;
using SharpDX;

namespace Troll3D{

    public enum ProjectionType{
        OrthoProjection,
        FrustumProjection
    }

    public abstract class Projection{

        // Public

            // Lifecycle

              
            // Methods

                public Matrix Data{
                    get { return data_; }
                }

            // Datas

                public abstract ProjectionType   GetProjectionType();
                

        // Protected

            // Datas

                protected Matrix  data_;
    }
}
