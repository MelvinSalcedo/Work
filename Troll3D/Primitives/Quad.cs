using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;

namespace Troll3D{

    public class Quad{

        public static Mesh GetMesh() {

            Mesh mesh = new Mesh();

            mesh.AddVertex(new StandardVertex(
                    new Vector3(-0.5f, -0.5f, 0.0f),
                    new Vector3(0.0f, 0.0f, -1.0f),
                    new Vector2(0.0f, 1.0f)
                ));

            mesh.AddVertex(new StandardVertex(
                new Vector3(0.5f, -0.5f, 0.0f),
                new Vector3(0.0f, 0.0f, -1.0f),
                new Vector2(1.0f, 1.0f)));

            mesh.AddVertex(new StandardVertex(
                new Vector3(-0.5f, 0.5f, 0.0f),
                new Vector3(0.0f, 0.0f, -1.0f),
                new Vector2(0.0f, 0.0f)));

            mesh.AddVertex(new StandardVertex(
                new Vector3(0.5f, 0.5f, 0.0f),
                new Vector3(0.0f, 0.0f, -1.0f),
                new Vector2(1.0f, 0.0f)));

            mesh.AddFace(0, 2, 3);
            mesh.AddFace(0, 3, 1);
            mesh.UpdateMesh();

            return mesh;
        }


    }
}
