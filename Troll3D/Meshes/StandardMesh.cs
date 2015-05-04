using System;
using System.Collections.Generic;
using SharpDX;
using SharpDX.Direct3D11;

namespace Troll3D{

    /// <summary>
    /// La classe StandardMesh regroupe l'ensemble des informations nécessaire à l'édition d'un maillage de structure Face-Vertex
    /// StandardMesh s'utilise conjointement avec la classe StandardVertex pour les informations de sommet
    /// </summary>
    public class StandardMesh{

        // Public

            // Lifecycle

                public StandardMesh() {
                    m_Vertices      = new List<StandardVertex>();
                    m_Faces         = new List<Face>();
                    m_FacesNormals  = new List<Vector3>();
                    m_FacesTangent  = new List<Vector3>();
                    m_FacesBinormal = new List<Vector3>();
                }

            // Methods

                /// <summary>
                /// Retourne une structure interprétable par DirectX
                /// </summary>
                /// <returns></returns>
                public Mesh ReturnMesh(){

                    Mesh meshDirect = new Mesh();

                    for (int i = 0; i < m_Vertices.Count; i++){
                        meshDirect.AddVertex(m_Vertices[i]);
                    }

                    for (int i = 0; i < m_Faces.Count; i++){
                        meshDirect.AddFace(
                            m_Faces[i].Indexes[0],
                            m_Faces[i].Indexes[1],
                            m_Faces[i].Indexes[2]
                        );
                    }

                    meshDirect.UpdateMesh();
                    return meshDirect;
                }

                public int GetVertexCount(){
                    return m_Vertices.Count;

                }
                public void AddVertex(StandardVertex vertex){
                    m_Vertices.Add(vertex);
                }

                public void AddFace(int index1, int index2, int index3){
                    m_Faces.Add(new Face(index1, index2, index3));
                    m_Vertices[index1].AddFace(m_Faces.Count - 1);
                    m_Vertices[index2].AddFace(m_Faces.Count - 1);
                    m_Vertices[index3].AddFace(m_Faces.Count - 1);
                }

                /// <summary>
                /// On commence par calculer l'intégralité des normales pour chaque face du maillage. A noter que si 
                /// l'on charge le maillage depuis un fichier, il n'est pas nécéssaire de lancer cette étape
                /// 
                /// Une fois que les normales ont été calculé pour chaque face, on parcours les sommets du maillage, et on fait 
                /// la moyenne des normales des faces connecté au sommet en cours d'évaluation
                /// </summary>
                public void ComputeNormals(){
                    ComputeFaceNormals();
                    AverageNormals();
                }

                /// <summary>
                /// Calcul des vecteurs tangent et Binormaux (voir explication cours BumpMapping)
                /// Important car permet de créer une matrice de transformation permettante de passer de l'espace "tangent" ou de texture
                /// vers l'espace objet, et donc de faciliter le bump Mapping
                /// Les tangentes, comme les normales, sont d'abord créer par face, puis moyenner
                /// </summary>
                public void ComputeTangents(){

                    for (int i = 0; i < m_Faces.Count; i++){

                        Vector3 P0 = m_Vertices[m_Faces[i].Indexes[0]].Position;
                        Vector3 P1 = m_Vertices[m_Faces[i].Indexes[1]].Position;
                        Vector3 P2 = m_Vertices[m_Faces[i].Indexes[2]].Position;

                        Vector2 st0 = m_Vertices[m_Faces[i].Indexes[0]].Uv;
                        Vector2 st1 = m_Vertices[m_Faces[i].Indexes[1]].Uv;
                        Vector2 st2 = m_Vertices[m_Faces[i].Indexes[2]].Uv;

                        Vector3 P01 = P1 - P0;
                        Vector3 P02 = P2 - P0;

                        Vector2 st01  = st1 - st0;
                        Vector2 st02  = st2 - st0;

                        // On calcule la tangente et la binomiale à partir de la résolution d'équation linéaire
                        // à 6 inconnus

                        float denominator = 1.0f / ((st01.X * st02.Y) - (st02.X * st01.Y));

                        m_FacesTangent.Add( new Vector3(){
                            X = ((st02.Y * P01.X) + (-st01.Y * P02.X)) * denominator,
                            Y = ((st02.Y * P01.Y) + (-st01.Y * P02.Y)) * denominator,
                            Z = ((st02.Y * P01.Z) + (-st01.Y * P02.Z)) * denominator
                        });

                        m_FacesBinormal.Add(new Vector3()
                        {
                            X = ((st01.X * P02.X) + (-st02.X * P01.X)) * denominator,
                            Y = ((st01.X * P02.Y) + (-st02.X * P01.Y)) * denominator,
                            Z = ((st01.X * P02.Z) + (-st02.X * P01.Z)) * denominator
                        });
                    }
                    AverageTangentBinormals();
                }

                public int GetFaceCount(){
                    return m_Faces.Count;
                }


               

        // Private

            // Methods

                private void AverageTangents(){

                    for (int i = 0; i < m_Vertices.Count; i++){

                        Vector3 averagedTangent = new Vector3(0.0f, 0.0f, 0.0f);

                        int normalCount = 0;
                        for (int j = 0; j < m_Vertices[i].Faces.Count; j++){
                            averagedTangent += m_FacesTangent[m_Vertices[i].Faces[j]];
                            normalCount++;
                        }

                        if (normalCount > 0){
                            averagedTangent = averagedTangent / (float)normalCount;
                        }
                        m_Vertices[i].Tangent =new Vector4(Vector3.Normalize(averagedTangent),0.0f);
                    }
                }

                private void AverageBinormals(){

                    for (int i = 0; i < m_Vertices.Count; i++){

                        Vector3 averageBinormal = new Vector3(0.0f, 0.0f, 0.0f);
                        int normalCount = 0;
                        for (int j = 0; j < m_Vertices[i].Faces.Count; j++)
                        {
                            averageBinormal += m_FacesBinormal[m_Vertices[i].Faces[j]];
                            normalCount++;
                        }
                        if (normalCount > 0)
                        {
                            averageBinormal = averageBinormal / (float)normalCount;
                        }
                        m_Vertices[i].Binormal = Vector3.Normalize(averageBinormal);
                    }
                }

                private void AverageTangentBinormals(){

                    AverageTangents();
                    AverageBinormals();

                    for (int i = 0; i < m_Vertices.Count; i++){

                        Vector3 N = m_Vertices[i].Normal;
                        Vector3 T = (Vector3)m_Vertices[i].Tangent;
                        Vector3 B = m_Vertices[i].Binormal;

                        // !! Atention, à ce state, les vecteurs tangent et binormaux ne sont pas encore
                        // ortho, on utilise une asture ou T' = T - (N.T)*N

                        T       = T - (Vector3.Cross(N, T) * N);
                        B       = B - ((Vector3.Cross(N, B)) * N) - ((Vector3.Cross( T , B)) * T);

                        // On n'enregistre pas la nouvelle binormale puisqu'on peut la calculer directement à l'aide d'un
                        // simple produit vectoriel dans le Vertex Shader en utilisant le déterminant de la matrice de 
                        // transformation 

                        Matrix3x3 tangentSpace = new Matrix3x3(
                            T.X, T.Y, T.Z,
                            B.X, B.Y, B.Z,
                            N.X, N.Y, N.Z);

                        float determinant = tangentSpace.Determinant();

                        m_Vertices[i].Tangent = new Vector4(T, determinant);
                        m_Vertices[i].Normal  = N;
                    }
                }

                
                /// <summary>
                /// On commence par calculer l'intégralité des normales pour chaque face du maillage
                /// </summary>
                private void ComputeFaceNormals(){
                    for (int i = 0; i < m_Faces.Count; i++){

                        Vector3 P0 = m_Vertices[m_Faces[i].Indexes[0]].Position;
                        Vector3 P1 = m_Vertices[m_Faces[i].Indexes[1]].Position;
                        Vector3 P2 = m_Vertices[m_Faces[i].Indexes[2]].Position;

                        // On récupère 2 vecteurs auquel on applique le produit vectoriel
                        // pour trouver le vecteur ortho

                        Vector3 P01 = P1 - P0;
                        Vector3 P02 = P2 - P0;

                        Vector3 normal = Vector3.Cross(P01, P02);
                        // On ne normalise pas la normale à ce stade car plus la face est grande, plus la normale le sera et plus
                        // elle aura d'influence au moment du moyennage 
                        m_FacesNormals.Add( normal);
                    }
                }

                /// <summary>
                /// Une fois que les normales ont été calculé pour chaque face, on parcours les sommets du maillage, et on fait 
                /// la moyenne des normales des faces connecté au sommet en cours d'évaluation
                /// </summary>
                private void AverageNormals(){

                    for (int i = 0; i < m_Vertices.Count; i++){

                        Vector3 averagedNormal = new Vector3(0.0f, 0.0f, 0.0f);
                        int normalCount = 0;
                        for (int j = 0; j < m_Vertices[i].Faces.Count; j++){
                            averagedNormal += m_FacesNormals[m_Vertices[i].Faces[j]];
                            normalCount++;
                        }
                        if (normalCount > 0){
                            averagedNormal = averagedNormal / (float)normalCount;
                        }
                        m_Vertices[i].Normal = Vector3.Normalize(averagedNormal);
                    }
                }

            // Datas

                private List<Vector3>           m_FacesNormals;     // Normales des faces 

                private List<Vector3>           m_FacesTangent;     // Tangentes des faces 
                private List<Vector3>           m_FacesBinormal;    // Binormales des faces

                private List<StandardVertex>    m_Vertices;
                private List<Face>              m_Faces;
    }
}
