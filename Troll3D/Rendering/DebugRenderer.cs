using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Troll3D.Rendering.DebugRendering;

namespace Troll3D.Rendering
{
    /// <summary>
    /// Cette classe va se charger d'afficher les primitives de debug
    /// Les primitives de debug sont des objets temporaires. Une fois utilisé,
    /// la liste de primitives est vidé
    /// </summary>
    public class DebugRenderer
    {
        /// <summary>
        /// Implémentation du Lazy Singleton
        /// </summary>
        public static DebugRenderer Instance{
            get{
                if ( m_instance == null )
                {
                    m_instance = new DebugRenderer();
                }
                return m_instance;
            }
        }

        private static DebugRenderer m_instance;

        /// <summary>
        /// Construit le Debug renderer. Le constructeur est en privé puisqu'il s'agit d'un singleton
        /// </summary>
        private DebugRenderer()
        {
            m_debugMaterial = new MaterialDX11();
        }

        /// <summary>
        /// Affiche les primitives qui ont été demandé par la scène/l'utilisateur, puis, vide
        /// la liste
        /// </summary>
        public void Update()
        {
            for ( int i = 0; i < m_primitives.Count; i++ )
            {
                m_primitives[i].Render( m_debugMaterial );
            }
            m_primitives.Clear();
        }

        /// <summary>
        /// Ajoute une primitive de Debug à la liste 
        /// </summary>
        /// <param name="primitive"> Primitive à afficher</param>
        public void AddPrimitive( DebugPrimitive primitive )
        {
            m_primitives.Add( primitive );
        }

        private MaterialDX11            m_debugMaterial;
        private List<DebugPrimitive>    m_primitives = new List<DebugPrimitive>();
    }
}
