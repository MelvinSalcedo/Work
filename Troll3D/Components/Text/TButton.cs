using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.Direct3D11;
using SharpDX;

using Troll3D.Components;

namespace Troll3D
{
    /// <summary> Un bouton active un event lors de son activation. Pour le moment, un bouton est simplement composé de plusieurs images
    /// en fonction de l'état "released/pressed/highlited</summary>
    public class TButton : Entity
    {
        public TButton(Resource resource, float width = 9.0f, float height = 3.0f)
        {
            m_width = width;
            m_height = height;
            SetImage(resource);
            UpdateLayout();
        }

        public void SetImage(Resource image)
        {
            if (image == null)
            {
                throw new Exception("La ressource Image est null");
            }
            else
            {
                if (m_image == null)
                {
                    m_image = new Entity();
                    //m_image.modelrenderer_ = new MeshRenderer(new MaterialDX11(), Quad.GetMesh());
                    //m_image.modelrenderer_.material_.AddTexture(image);    
                    Append(m_image);
                }
                else
                {
                    //m_image.modelrenderer_.material_.SetTexture(0,image);
                }
                
            }
        }

        public void SetText(string text)
        {
            m_textQuad.SetText(text);
        }

        //public override void OnMouseDown(MouseEvent e)
        //{
        //    if (TRaycast.FireRayFromMouse().GetEntity() == m_background)
        //    {
        //        OnClick();
        //    }
        //}

        public float Width
        {
            get
            {
                return m_width;
            }
            set
            {
                m_width = value;
                UpdateLayout();
            }
        }
        public float Height
        {
            get
            {
                return m_height;
            }
            set
            {
                m_height = value;
                UpdateLayout();
            }
        }

        public event EventHandler DoubleClicked;
        public event EventHandler Clicked;

        /// <summary> Se déclenche lorsque le bouton de la souris est relaché</summary>
        public event EventHandler Released;
        /// <summary> Se déclenche lorsque le bouton est appuyé</summary>
        public event EventHandler Pressed;

        /// <summary> Se déclenche lorsque la souris rentre dans l'espace du bouton </summary>
        public event EventHandler MouseEnter;   
        /// <summary> Se déclenche lorsque la souris sort de l'espace du bouton</summary>
        public event EventHandler MouseExit;

        private void OnClick()
        {
            if (Clicked != null)
            {
                Clicked.Invoke(this, new EventArgs());
            } 
        }

        /// <summary> Met à jour le bouton </summary>
        private void UpdateLayout()
        {
            m_image.transform_.SetScale(Width, Height, 1.0f);
        }


        private float m_width;
        private float m_height;

        private TextQuad    m_textQuad;
        private Entity      m_background;
        private Entity      m_image;
        private FontAtlas   m_font;
    }
}
