using System;
using System.Collections.Generic;
using SharpDX;


namespace Troll3D {


    // SAT signifie Separate Axis Theorem, et permet de déterminer si 2 formes rentrent en collisions.
    // Pour se faire, on analyse chacune des 'arrêtes" de la forme que l'on souhaite analyse, et on projete les points blabla
    public class SAT {

        // Public 

            // Static Methods 
                    
                public static bool Intersect3D(Shape shape1, Shape shape2){

                    // Ces valeurs concernent le MTV, (Minimum translation vector)
                    // Elles permettent de potentiellement pousser une forme au mieux pour éviter que
                    // les formes ne se rentrent dedans 

                    Vector3 smallestaxis;
                    float smallestmagnitude = 99999999999.0f;

                    // Shape contient déjà les axes à tester
                    
                    
                    Vector3[] axes = new Vector3[shape1.axes_.Length + shape2.axes_.Length + (shape1.axes_.Length * shape2.axes_.Length) ];
                    shape1.axes_.CopyTo(axes, 0);
                    shape2.axes_.CopyTo(axes, shape1.axes_.Length);



                    for (int i = 0; i<shape1.axes_.Length; i++) {
                        for(int j=0; j< shape2.axes_.Length; j++){
                            axes[shape1.axes_.Length + shape2.axes_.Length + i * shape1.axes_.Length + j] =
                                Vector3.Cross(shape1.axes_[i], shape2.axes_[j]);
                        }
                    }

                    Vector3[] shape1projections = new Vector3[axes.Length];
                    Vector3[] shape2projections = new Vector3[axes.Length];

                    for (int i = 0; i < axes.Length; i++) {

                        shape1projections[i] = ProjectShape(shape1, axes[i]);
                        shape2projections[i] = ProjectShape(shape2, axes[i]);

                        if (!Overlap(shape1projections[i].X, shape1projections[i].Y, shape2projections[i].X, shape2projections[i].Y)) {
                            return false;
                        } else {
                            float overlapvalue = GetOverlap(shape1projections[i].X, shape1projections[i].Y, shape2projections[i].X, shape2projections[i].Y);

                            if (overlapvalue < smallestmagnitude) {
                                smallestmagnitude = overlapvalue;
                                smallestaxis = axes[i];
                            }
                        }
                    }
                    return true;
                }

                // La méthode Intersect va tester les axes du premier argument avec le deuxieme
                public static bool Intersect(Shape shape1, Shape shape2) {

                    // Ces valeurs concernent le MTV, (Minimum translation vector)
                    // Elles permettent de potentiellement pousser une forme au mieux pour éviter que
                    // les formes ne se rentrent dedans 

                    Vector3 smallestaxis;
                    float smallestmagnitude = 99999999999.0f;

                    // La première étape consiste à récupérer l'intégralité des axes à tester
                    // Une normale d'un arrête de la forme correspond à un axe à traiter

                    Vector3[] axes = new Vector3[shape1.axes_.Length + shape2.axes_.Length];
                    shape1.axes_.CopyTo(axes, 0);
                    shape2.axes_.CopyTo(axes, shape1.axes_.Length);

                    Vector3[] shape1projections = new Vector3[axes.Length];
                    Vector3[] shape2projections = new Vector3[axes.Length];

                    for (int i = 0; i < axes.Length; i++) {

                        shape1projections[i] = ProjectShape(shape1, axes[i]);
                        shape2projections[i] = ProjectShape(shape2, axes[i]);

                        if (!Overlap(shape1projections[i].X, shape1projections[i].Y, shape2projections[i].X, shape2projections[i].Y)) {
                            return false;
                        } else {
                            float overlapvalue = GetOverlap(shape1projections[i].X, shape1projections[i].Y, shape2projections[i].X, shape2projections[i].Y);

                            if (overlapvalue < smallestmagnitude) {
                                smallestmagnitude = overlapvalue;
                                smallestaxis = axes[i];
                            }
                        }
                    }
                    return true;
                }

                // Projette les sommets de la forme sur l'axe passé en parametre, et retourne le segment englobant tout ces sommets
                public static Vector3 ProjectShape(Shape shape, Vector3 axis) {

                    float min = Vector3.Dot(shape.vertices_[0], axis);
                    float max = min;

                    for (int i = 0; i < shape.vertices_.Length; i++) {

                        float val = Vector3.Dot(shape.vertices_[i], axis);

                        if (val < min) {
                            min = val;
                        }

                        if (val > max) {
                            max = val;
                        }
                    }

                    return new Vector3(min, max, 0.0f);

                }
           /* public static Vector3[] ComputeAxis(Shape shape) {

                Vector3[] axes = new Vector3[shape.arretes_.Length];

                for (int i = 0; i < shape.arretes_.Length ; i++) {
                    axes[i] = Vector3.Cross(shape.arretes_[i].u_ - shape.arretes_[i].v_, new Vector3(0.0f, 0.0f, 1.0f));
                }
                return axes;
            }*/


            public static bool Overlap(Vector3 u, Vector3 v) {
                return true;
            }


            public static bool Overlap(float valmin1, float valmax1, float valmin2, float valmax2) {

                if (valmin2 >= valmin1 && valmin2 <= valmax1) {
                    return true;
                }

                if (valmax2 >= valmin1 && valmax2 <= valmax1) {
                    return true;
                }

                if (valmin1 >= valmin2 && valmin1 <= valmax2) {
                    return true;
                }

                if (valmax1 >= valmin2 && valmax1 <= valmax2) {
                    return true;
                }

                return false;
            }

            public static float GetOverlap(float valmin1, float valmax1, float valmin2, float valmax2) {

                if (valmin2 >= valmin1 && valmin2 <= valmax1) {
                    float max = valmax2;
                    if (valmax1 > valmax2) {
                        max = valmax1;
                    }
                    return max - valmin2;
                }

                if (valmax2 >= valmin1 && valmax2 <= valmax1) {
                    float min = valmin1;
                    if (valmin2 > valmin1) {
                        min = valmin2;
                    }
                    return valmax2 - min;
                }

                return 0.0f;
            }




        //// Si le vecteur A-B rentre en collision avec un axe de shape, retourne le point de collision et sa normale
        //public static Vector3[] GetNormalAndClosestPoint(Shape shape, Vector3 a, Vector3 b) {

        //    List<Vector3> points = new List<Vector3>();
        //    List<int> pointsindex = new List<int>();
        //    List<Vector3> normals = new List<Vector3>();

        //    Vector3 vec;
        //    Vector3 oternormal;
        //    Vector3 veclol;
        //    Vector3 veclol1;

        //    for (int i = 0; i < shape.worldvertices_.Length - 1; i++) {

        //        vec = new Vector3();

        //        veclol = new Vector3(shape.worldvertices_[i + 1].x, shape.worldvertices_[i + 1].y, shape.worldvertices_[i + 1].z);
        //        veclol1 = new Vector3(shape.worldvertices_[i].x, shape.worldvertices_[i].y, shape.worldvertices_[i].z);

        //        normals.Add((Vector3.Cross(veclol - veclol1, Vector3.forward)).normalized);

        //        if (SegmentIntersect(shape.worldvertices_[i], shape.worldvertices_[i + 1], a, b, ref vec)) {
        //            points.Add(vec);
        //            pointsindex.Add(i);
        //        }
        //    }

        //    vec = new Vector3();

        //    veclol = new Vector3(shape.worldvertices_[shape.worldvertices_.Length - 1].x, shape.worldvertices_[shape.worldvertices_.Length - 1].y, shape.worldvertices_[shape.worldvertices_.Length - 1].z);
        //    veclol1 = new Vector3(shape.worldvertices_[0].x, shape.worldvertices_[0].y, shape.worldvertices_[0].z);

        //    normals.Add((Vector3.Cross(veclol1 - veclol, Vector3.forward)).normalized);

        //    if (SegmentIntersect(shape.worldvertices_[shape.worldvertices_.Length - 1], shape.worldvertices_[0], a, b, ref vec)) {

        //        points.Add(vec);
        //        pointsindex.Add(shape.worldvertices_.Length - 1);
        //    }

        //    float min = 9999999999999999999.0f;

        //    int indexmin = 0;

        //    for (int i = 0; i < points.Count; i++) {
        //        if ((points[i] - a).magnitude < min) {
        //            indexmin = i;
        //            min = (points[i] - a).magnitude;
        //        }
        //    }

        //    int index = pointsindex[indexmin];

        //    oternormal = normals[index] * -1.0f;

        //    //if ((points[indexmin] + oternormal).magnitude < (points[indexmin] + normals[index]).magnitude) {
        //    //    Debug.Log("fuck YOU");
        //    //    normals[index] = oternormal;
        //    //}

        //    Vector3[] returnvec = new Vector3[2];
        //    returnvec[0] = points[indexmin];
        //    returnvec[1] = normals[index];
        //    return returnvec;
        //}

        //public static Vector3 GetClosetPoint(Shape shape, Vector3 a, Vector3 b) {
        //    List<Vector3> points = new List<Vector3>();
        //    Vector3 vec;

        //    for (int i = 0; i < shape.worldvertices_.Length - 1; i++) {
        //        vec = new Vector3();
        //        if (SegmentIntersect(shape.worldvertices_[i], shape.worldvertices_[i + 1], a, b, ref vec)) {
        //            points.Add(vec);
        //        }
        //    }

        //    vec = new Vector3();
        //    if (SegmentIntersect(shape.worldvertices_[shape.worldvertices_.Length - 1], shape.worldvertices_[0], a, b, ref vec)) {
        //        points.Add(vec);
        //    }

        //    float min = 9999999999999999999.0f;

        //    int indexmin = 0;

        //    for (int i = 0; i < points.Count; i++) {
        //        if ((points[i] - a).magnitude < min) {
        //            indexmin = i;
        //            min = (points[i] - a).magnitude;
        //        }
        //    }

        //    return points[indexmin];

        //}



        //public static bool Intersect(Shape shape1, Shape shape2) {

        //    if (shape1.type_ == ShapeType.POLYGON && shape2.type_ == ShapeType.POLYGON) {
        //        return IntersectPolygons(shape1, shape2);
        //    } else if (shape1.type_ == ShapeType.CIRCLE && shape2.type_ == ShapeType.CIRCLE) {
        //        return IntersectCircles((Circle)shape1, (Circle)shape2);
        //    } else if (shape1.type_ == ShapeType.CIRCLE) {
        //        return IntersectPolygonCircle(shape2, (Circle)(shape1));
        //    } else if (shape2.type_ == ShapeType.CIRCLE) {
        //        return IntersectPolygonCircle(shape1, (Circle)(shape2));
        //    }
        //    return false;
        //}


            public static bool Intersect3DShapeShere(OBB box, Vector3 position, float radius) {

                // On récupère le sommet le plus proche du cercle

                float min = 99999999999999.0f;
                Shape shape1 = box.GetShape();

                //for (int i = 0; i < shape1.vertices_.Length; i++) {
                //    Vector3 vec = shape1.vertices_[i] - position;

                //    if (vec.Length() < min) {
                //        minindex = i;
                //        min = vec.Length();
                //    }
                //}

                Vector3 closestpoint = GetClosestPointInOBB(box, position);
                Vector3 circleaxis = position - closestpoint;

                // Affiche le closest point
               // DirectxApplication.Instance.primitiverenderer_.AddPrimitive(new T3DPoint(closestpoint, 0.1f, Color.Purple));
               // Console.WriteLine("circleaxis " + circleaxis + " sphere position " + position + " closest v " + shape1.vertices_[minindex]);

                // J'ai aucune idée du pourquoi mais il faut faire le cross product entre les 
                // axes de séparations des 2 formes
                //Vector3[] crossaxes = new Vector3[shape1.axes_.Length * 1];

                //for (int i = 0; i < shape1.axes_.Length; i++) {
                //    crossaxes[i] = Vector3.Cross(shape1.axes_[i], circleaxis);
                //}

                circleaxis.Normalize();
                Vector3[] axes = new Vector3[shape1.axes_.Length +1 ];
                shape1.axes_.CopyTo(axes, 0);
                //crossaxes.CopyTo(axes, shape1.axes_.Length);
                axes[axes.Length-1] = circleaxis;


                // On récupére les 2 sommets qui travers le cercle 
                Vector3[] circlevertices;
                Shape shapecircle = new Shape(); ;

                for (int i = 0; i < axes.Length; i++) {

                    circlevertices = new Vector3[2];
                    circlevertices[0] = position + axes[i] * radius;
                    circlevertices[1] = position - axes [i]* radius;

                     shapecircle = new Shape(circlevertices, null);

                     Vector3 proj1 = ProjectShape(shape1, axes[i]);
                     Vector3 proj2 = ProjectShape(shapecircle, axes[i]);

                    if (!Overlap(proj1.X, proj1.Y, proj2.X, proj2.Y)) {
                     
                        return false;
                    } else {
                   
                        float overlapvalue = GetOverlap(proj1.X, proj1.Y, proj2.X, proj2.Y);

                        //if (overlapvalue < smallestmagnitude) {
                        //    smallestmagnitude = overlapvalue;
                        //    smallestaxis = shapeaxis[i];
                        //}
                    }
                }

               

                return true;
            }

            // L'idée, " en gros", est de récupérer le vecteur entre le centre de la boite et le point désiré
            // Puis, de projeter le vecteur sur chacun des axes de la box, puis "clamp" par rapport aux 
            // valeurs des axes
            public static Vector3 GetClosestPointInOBB(OBB box, Vector3 point) {

                Vector3 dir = point - box.transform_.WorldPosition();

                Vector3 returnvec = box.transform_.WorldPosition(); ;
                // Pour chaque axe de la boite
                
                // Up, right, forward
                float[] coefs = new float[3];
                coefs[0] = box.transform_.scaling_.Y/2.0f;
                coefs[1] = box.transform_.scaling_.X / 2.0f;
                coefs[2] = box.transform_.scaling_.Z / 2.0f;

                for(int i=0; i< box.GetShape().axes_.Length; i++){

                    float dist = Vector3.Dot(dir,box.GetShape().axes_[i]);

                    if(dist> coefs[i]){
                        dist= coefs[i];
                    } else if (dist < -coefs[i]) {
                        dist = -coefs[i];
                    }

                    returnvec += dist * box.GetShape().axes_[i];
                }
                return returnvec;
            }
        //public static bool IntersectPolygonCircle(Shape shape1, Circle circle) {

        //    // Ces valeurs concernent le MTV, (Minimum translation vector)
        //    // Elles permettent de potentiellement pousser une forme au mieux pour éviter que
        //    // les formes ne se rentrent dedans 

        //    Vector3 smallestaxis;
        //    float smallestmagnitude = 99999999999.0f;

        //    Vector3[] shape11 = new Vector3[shape1.worldvertices_.Length];

        //    for (int i = 0; i < shape1.worldvertices_.Length; i++) {
        //        shape11[i] = new Vector3(shape1.worldvertices_[i].x, shape1.worldvertices_[i].y, shape1.worldvertices_[i].z);
        //    }


        //    // On récupère les axes du polygones

        //    Vector3[] shapeaxis = ComputeAxis(shape11);
        //    Vector3 circleaxis;

        //    // On récupère le sommet le plus proche du cercle

        //    float min = 99999999999999.0f;
        //    int minindex = 0;
        //    for (int i = 0; i < shape1.worldvertices_.Length; i++) {
        //        Vector3 vec = shape11[i] - circle.translation_;

        //        if (vec.sqrMagnitude < min) {
        //            minindex = i;
        //            min = vec.sqrMagnitude;
        //        }
        //    }

        //    circleaxis = circle.translation_ - shape11[minindex];

        //    // On récupére les 2 sommets qui travers le cercle 

        //    Vector3[] circlevertices;

        //    Vector3 shape1projections = new Vector3();
        //    Vector3 shape2projections = new Vector3();

        //    for (int i = 0; i < shapeaxis.Length; i++) {

        //        shape1projections = ProjectShape(shape11, shapeaxis[i]);

        //        circlevertices = new Vector3[2];
        //        circlevertices[0] = circle.translation_ + shapeaxis[i].normalized * circle.radius_;
        //        circlevertices[1] = circle.translation_ - shapeaxis[i].normalized * circle.radius_;

        //        shape2projections = ProjectShape(circlevertices, shapeaxis[i]);

        //        if (!Overlap(shape1projections.x, shape1projections.y, shape2projections.x, shape2projections.y)) {
        //            return false;
        //        } else {
        //            float overlapvalue = GetOverlap(shape1projections.x, shape1projections.y, shape2projections.x, shape2projections.y);

        //            if (overlapvalue < smallestmagnitude) {
        //                smallestmagnitude = overlapvalue;
        //                smallestaxis = shapeaxis[i];
        //            }
        //        }
        //    }

        //    circlevertices = new Vector3[2];
        //    circlevertices[0] = circle.translation_ + circleaxis.normalized * circle.radius_;
        //    circlevertices[1] = circle.translation_ - circleaxis.normalized * circle.radius_;

        //    shape1projections = ProjectShape(shape11, circleaxis);
        //    shape2projections = ProjectShape(circlevertices, circleaxis);

        //    if (!Overlap(shape1projections.x, shape1projections.y, shape2projections.x, shape2projections.y)) {
        //        return false;
        //    } else {
        //        float overlapvalue = GetOverlap(shape1projections.x, shape1projections.y, shape2projections.x, shape2projections.y);

        //        if (overlapvalue < smallestmagnitude) {
        //            smallestmagnitude = overlapvalue;
        //            //  smallestaxis = shapeaxis[ shapeaxis.Length -1];
        //        }
        //    }



        //    return true;
        //}

        //public static bool IntersectCircles(Circle circle1, Circle circle2) {
        //    Vector3 ab = circle2.translation_ - circle1.translation_;
        //    float radiuscount = circle1.radius_ + circle2.radius_;
        //    return ab.magnitude < radiuscount;
        //}


        //public static bool IntersectPolygons(Shape shape1, Shape shape2) {

        //    Vector3[] shape11 = new Vector3[shape1.worldvertices_.Length];
        //    Vector3[] shape22 = new Vector3[shape2.worldvertices_.Length];

        //    for (int i = 0; i < shape1.worldvertices_.Length; i++) {
        //        shape11[i] = new Vector3(shape1.worldvertices_[i].x, shape1.worldvertices_[i].y, shape1.worldvertices_[i].z);
        //    }

        //    for (int i = 0; i < shape2.worldvertices_.Length; i++) {
        //        shape22[i] = new Vector3(shape2.worldvertices_[i].x, shape2.worldvertices_[i].y, shape2.worldvertices_[i].z);
        //    }

        //    return Intersect(shape11, shape22);
        //}


    }
}