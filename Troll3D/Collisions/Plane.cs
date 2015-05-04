using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Troll3D{

    // Classe représentant un plan "géométrique
    public class GeoPlane {

        // Public

            // Lifecycle

            // Methods
    }
}

///*!	Brief Represent a plane. A plane is represend by a point inside the plane, a normal  vector	*/

//Plane	=	function(planePoint, planeNormal)
//{
//    /********************************/
//    /*			Properties			*/
//    /********************************/
	
//        this.m_Normal;	/*!< Plane's normal			*/
//        this.m_Point;	/*!< One point on the plane	*/
		
//        //	A plane can also be represented as a Cartesian equation ax+by+cz+d = 0
//        //	Thoses values can be determined from one point on the plan and its normal
		
//        this.a;
//        this.b;
//        this.c;
//        this.d;
		
//    /********************************/
//    /*			Constructor			*/
//    /********************************/
	
		
//        this.m_Point	=	planePoint;
//        this.m_Normal	=	planeNormal;
//        this.InitCartesian();
//}


//Plane.prototype.InitCartesian	=	function()
//{
//    this.a=this.m_Normal.x;
//    this.b=this.m_Normal.y;
//    this.c=this.m_Normal.z;
//    this.d=	- Vec4.Dot(this.m_Normal, this.m_Point);
//}

///*!	brief Init the plan from 2 colinear vector	*/
//Plane.prototype.InitFromPoints	=	function()
//{

//}

//Plane.PlaneFromFace	=	function(face)
//{
//    //	First, we must convert the vertex position in world space
	
	
//    array = new Array();
//    array.push(face.m_Mesh.m_Entity.m_Transform.m_Position.MultVec4( this.m_Vertices[0].m_Attributes.m_Position));
//    array.push(matrix.MultVec4( this.m_Vertices[1].m_Attributes.m_Position));
//    array.push(matrix.MultVec4( this.m_Vertices[2].m_Attributes.m_Position));
//    return array;


//    return new Plane( face.m_Vertices[0].m_Attributes.m_Position, face.m_Normal);
	
//}
