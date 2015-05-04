using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;

namespace Troll3D{

    public class Sphere {

        // Public

        // Static Methods


        public static Mesh GetMesh(float radius, int xdiscretisation, int ydiscretisation) {

            StandardMesh mesh = new StandardMesh();

            List<StandardVertex> vertices = new List<StandardVertex>();
            List<int> indices_ = new List<int>();

            mesh.AddVertex(new StandardVertex(
                new Vector3(0.0f, -radius, 0.0f),
                new Vector3(0.0f, -1.0f, 0.0f),
                new Vector2(0.0f, 0.0f)
            ));

            for (int i = 1; i < ydiscretisation; ++i) {

                for (int j = 0; j < xdiscretisation; ++j) {

                    float val = (float)Math.PI * (float)i / (float)ydiscretisation;
                    val = -(float)Math.Cos(val);

                    Vector3 position = new Vector3(
                        (float)Math.Sin(Math.PI * i / (float)ydiscretisation) * (float)Math.Cos(2.0f * Math.PI * j / xdiscretisation)*radius,
                        val*radius,
                        (float)Math.Sin(Math.PI * i / (float)ydiscretisation) * (float)Math.Sin(2.0f * Math.PI * j / xdiscretisation)*radius);

                    Vector3 normal = position;
                    normal.Normalize();
                    mesh.AddVertex(
                        new StandardVertex(
                            position,
                            normal,
                            new Vector2((float)j / (float)xdiscretisation, (float)i / (float)ydiscretisation)
                    ));
                }
            }

            mesh.AddVertex(new StandardVertex(
                new Vector3(0.0f, radius, 0.0f),
                new Vector3(0.0f, 1.0f, 0.0f),
                new Vector2(0.0f, 1.0f)
            ));

            for (int i = 0; i < xdiscretisation-1; i++) {
                indices_.Add(0);
                indices_.Add(i + 1);
                indices_.Add(i + 2);
            }

            indices_.Add(0);
            indices_.Add(xdiscretisation);
            indices_.Add(1);
            
            for (int i = 0; i < ydiscretisation-2; i++) {
                for (int j = 0; j < xdiscretisation-1; j++) {

                    mesh.AddFace(
                        1 + xdiscretisation * i + j,        // 1
                        1 + xdiscretisation * (i + 1) + j,  // 5
                        1 + xdiscretisation * i + j + 1);   // 2

                    mesh.AddFace(
                        (1 + xdiscretisation * i + j + 1), // 2
                        (1 + xdiscretisation * (i + 1) + j), // 5
                        (1 + xdiscretisation * (i + 1) + j + 1)); // 6
                }

                mesh.AddFace(
                    (1 + xdiscretisation * (i + 1) - 1), // 4
                    (1 + xdiscretisation * (i + 1) ), // 5
                    (1 + xdiscretisation * i)); // 1

                mesh.AddFace(
                    (1 + xdiscretisation * (i + 1) - 1), // 4
                    (1 + xdiscretisation * (i + 1) + xdiscretisation - 1), // 5
                    (1 + xdiscretisation * (i + 1))); // 5
            }
            
            for (int i = 1; i < xdiscretisation; i++) {
                mesh.AddFace(
                (mesh.GetVertexCount() - i - 2),
                (mesh.GetVertexCount() - 1),
                (mesh.GetVertexCount() - i - 1));
            }

            mesh.AddFace(
            (mesh.GetVertexCount() - 2),
            (mesh.GetVertexCount() - 1),
            (mesh.GetVertexCount() - xdiscretisation - 1));

            mesh.ComputeNormals();
            mesh.ComputeTangents();
            
            

            return mesh.ReturnMesh();
        }
    }

        // Methods


}
