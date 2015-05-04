//#ifndef WAVE_H
//#define WAVE_H

//#include <geotool/vec3.h>
//#include "Business/wingededgemesh.h"
//#include <vector>
//#include  "Resources/Entity.h"
//#include "Resources/Texture.h"
//#include <QTime>

//#include "wavesin.h"

//using namespace std;

//class Wave : public Entity{

//    public:

//        // Lifecycle

//            Wave(int width, int height, float offset);

//        // Methods

//            virtual void Display();
//            void BuildMesh();

//        // Datas

//            float * GetAmplitudes();
//            float * GetWavelengths();
//            float * GetDirections();
//            float * GetSpeeds();

//            std::vector<WaveSin> waves_;
//            WingedEdgeMesh mesh_;
//            Texture * normalmap_;
//            Texture * reflectionTexture_;

//            QTime   timer;

//            int     width_;
//            int     height_;
//            float   offset_;
//};

//#endif // WAVE_H

//    #include "wave.h"
//#include "Managers/resourcemanager.h"
//#include <QDebug>
//#include "Resources/Lights/light.h"
//#include "View.h"

//// Public

//    // Lifecycle

//        Wave::Wave(int width, int height, float offset) :
//            width_(width),
//            height_(height),
//            offset_(offset){
//            material_ =  ResourceManager::GetMaterial("WaveMaterial.mat");
//            BuildMesh();

//            timer.start();

//        }

//    // Methods

//        void Wave::Display(){

//            material_->GetUniformFloat("time")->data_ = timer.elapsed();

//            transform_.Update();

//            if(renderer_!=0){
//                material_->Use();

//                Light::SendLights(*material_->programs_);

//                GLuint amplitudes   = material_->programs_->GetUniformLocation("amplitude");
//                GLuint wavelengths  = material_->programs_->GetUniformLocation("wavelength");
//                GLuint directions   = material_->programs_->GetUniformLocation("direction");
//                GLuint speeds       = material_->programs_->GetUniformLocation("speed");
//                GLuint wavecount    = material_->programs_->GetUniformLocation("wavecount");

//                GLuint ReflectionTexture = material_->programs_->GetUniformLocation("ReflectionTexture");

//                glUniform1i(wavecount, waves_.size());

//                glActiveTexture(GL_TEXTURE1);
//                glBindTexture(GL_TEXTURE_2D, reflectionTexture_->id_);
//                glUniform1i(ReflectionTexture, 1);


//                glUniform1fv(amplitudes,  waves_.size(), GetAmplitudes());
//                glUniform1fv(wavelengths, waves_.size(), GetWavelengths());
//                glUniform2fv(directions, waves_.size(), GetDirections());
//                glUniform1fv(speeds, waves_.size(), GetSpeeds());

//                glUniformMatrix4fv(material_->modelid_, 1, true, transform_.worldmatrix_.Array());
//                glUniformMatrix4fv(material_->viewid_ , 1, true, View::main_->transform_.worldmatrix_.Array());
//                glUniformMatrix4fv(material_->projid_ , 1, true, View::main_->projection_.Array());

//                renderer_->Render(material_->Program());
//            }
//             for(int i=0; i<childs_.size();i++){
//                 childs_.at(i)->Display();
//             }
//        }

//        float * Wave::GetAmplitudes(){

//            float * val = new float[waves_.size()];
//            for(int i=0; i< waves_.size(); i++){
//                val[i] = waves_[i].amplitude_;
//            }
//            return val;
//        }

//        float * Wave::GetWavelengths(){
//            float * val = new float[waves_.size()];
//            for(int i=0; i< waves_.size(); i++){
//                val[i] = waves_[i].wavelength_;
//            }
//            return val;
//        }

//        float * Wave::GetSpeeds(){
//            float * val = new float[waves_.size()];
//            for(int i=0; i< waves_.size(); i++){
//                val[i] = waves_[i].speed_;
//            }
//            return val;
//        }

//        float * Wave::GetDirections(){
//            float * val = new float[waves_.size()*2];
//            for(int i=0; i< waves_.size(); i++){
//                val[i*2] = waves_[i].direction_.x();
//                val[(i*2)+1] = waves_[i].direction_.y();
//            }
//            return val;
//        }

//        void Wave::BuildMesh(){

//            for(int i=0; i< height_; i++){
//                for(int j=0; j< width_; j++){
//                    mesh_.AddVertex( new Vertex (
//                                                    vec3(j*offset_, 0.0, i*offset_),
//                                                    vec3(1.0, 0.0, 1.0),
//                                                    vec2((float)j/(float)width_, (float)i/(float)height_)));

//                }
//            }

//            for(int i=0; i< height_ -1; i++){
//                for(int j = 0; j< width_-1; j++){

//                    int index = (i*width_)+j;
//                    mesh_.AddTriangle(index, index + width_ + 1, index +1);
//                    mesh_.AddTriangle(index, index + width_ , index + width_ +1);
//                }
//            }
//            renderer_   =   mesh_.BuildOpenGLMesh();

//        }

