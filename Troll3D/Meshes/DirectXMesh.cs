using System;
using System.Collections.Generic;
using SharpDX;
using SharpDX.Direct3D11;

using D3D11 = SharpDX.Direct3D11;

namespace Troll3D
{

    /// <summary>
    /// Transforme un maillage dans un format interprétable par 
    /// DirectX. Cette classe n'est pas faite pour être manipulé mais simplement utilisé pour l'affichage des maillages
    /// </summary>
    public class DirectXMesh<T>
    {
        public DirectXMesh( T mesh ) { }

        /// <summary>
        ///  Buffers (tableau) contenant les informations qui seront véritablement envoyé au GPU via DirectX
        /// </summary>
        D3D11.Buffer m_Vertexbuffer;
        D3D11.Buffer m_Indexbuffer;
    }
}
