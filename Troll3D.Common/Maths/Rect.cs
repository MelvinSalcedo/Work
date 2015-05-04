using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoTool {
    class Rect {
    }
}
//#ifndef RECT_H
//#define RECT_H

///**
// * @brief The Rect class représente un rectangle dont les coordonnées x-y représente le coin inférieur gauche
// */
//class Rect{

//    public:

//        // Lifecycle

//            Rect(int x_ = 0, int y_= 0, int width_ = 0, int height_ = 0);

//        // Methods

//            bool isIn(int x, int y)const;
//            bool Intersect(Rect rect)const ;

//        // Datas

//            int x;
//            int y;

//            int width;
//            int height;
//};

//#endif // RECT_H
//#include "rect.h"

//// Public

//    // Lifecycle

//        Rect::Rect(int x_, int y_, int width_, int height_) :
//            x(x_),
//            y(y_),
//            width(width_),
//            height(height_){
//        }

//    // Methods

//        bool Rect::isIn(int x_, int y_) const{
//            if( x_ <= x + width && x_ >= x_ ){
//                if( y_ <= y + height && y_>= y){
//                    return true;
//                }
//            }
//            return false;
//        }

//        bool Rect::Intersect(Rect rect) const{
//            if( x + width < rect.x ||
//                rect.x + rect.width < x ||
//                y + height < rect.y ||
//                rect.y + rect.height < y){
//                return false;
//            }
//            return true;
//        }


