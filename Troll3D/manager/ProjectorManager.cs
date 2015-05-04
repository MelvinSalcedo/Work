using System;
using System.Collections.Generic;
using SharpDX;
using SharpDX.Direct3D11;

namespace Troll3D
{
    /// <summary>
    /// Cette classe se charge de gérer l'utilisation des projecteurs à l'intérieur du pipeline graphique
    /// </summary>
    public class ProjectorManager{

        // Public

            // Static Datas

                public static ProjectorManager Instance;

            // Lifecycle

                public ProjectorManager(){
                    Instance = this;
                    description_ = new ProjectorsDesc{
                        ProjectorCount= 0,
                        Projectors = new ProjectorDesc[10]
                    };

                    constantBuffer_ = new CBuffer<ProjectorsDesc>(3, description_);
                }

            // Methods


                /// <summary>
                /// Met à jour les projecteurs et charge leur données
                /// </summary>
                public void Bind(){

                    int projectorCount = 0;
                    for (int i = 0; i < projectors_.Count; i++)
                    {
                        if (projectors_[i].IsActive){
                            projectors_[i].UpdateMatrix();
                            
                            description_.Projectors[projectorCount] = projectors_[i].projectorDesc;
                            
                            ApplicationDX11.Instance.devicecontext_.VertexShader.SetShaderResource(10   +projectorCount,   projectors_[i].m_SRV);
                            ApplicationDX11.Instance.devicecontext_.PixelShader.SetShaderResource(10 + projectorCount, projectors_[i].m_SRV);
                            projectorCount++;
                        }
                    }

                    description_.ProjectorCount = projectorCount;

                    constantBuffer_.UpdateStruct(description_);
                    constantBuffer_.Send();
                }

                /// <summary>
                /// La méthode UnBind va se charger de mettre à null les ShaderResourceView 
                /// </summary>
                public void UnBind(){
                    for (int i = 10; i < 20; i++){
                        ApplicationDX11.Instance.devicecontext_.VertexShader.SetShaderResource(i,null);
                        ApplicationDX11.Instance.devicecontext_.PixelShader.SetShaderResource(i, null);
                    }
                }

                public void AddProjector(Projector projector){
                    projectors_.Add(projector);
                }

                public void RemoveProjector(Projector projector){

                }

                public void SendProjectors(){

                }

                public int ProjectorCount{
                    get { return projectors_.Count;}
                }
        
        // Private

            // Datas

                
                private List<Projector>             projectors_ = new List<Projector>();
                private ProjectorsDesc              description_;
                private CBuffer<ProjectorsDesc>     constantBuffer_;
    }
}
