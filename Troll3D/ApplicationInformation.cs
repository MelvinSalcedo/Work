using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Troll3D
{

    /// <summary>Cette classe contient les différentes informations concernant l'application en cours d'éxécution : 
    /// Nombre de triangles affichés, nombre de drawCall, Fps etc
    /// </summary>
    public class ApplicationInformation
    {
        public static ApplicationInformation Instance;

        public ApplicationInformation()
        {
            Instance = this;
            m_FPSRefresh = 500;
        }

        public void Update()
        {

            m_FrameCount++;

            m_FPSCounter += TimeHelper.Instance.GetElapsedTime();

            if ( TimeHelper.Instance.GetTimeSinceStart() > m_LastFPSCount + m_FPSRefresh )
            {
                m_FPS_ = ( float )m_FrameCount / ( float )m_FPSCounter;
                m_FPS_ *= 1000.0f;
                // On met à jour la dernière fois que le fps a été mis à jour
                m_LastFPSCount = TimeHelper.Instance.GetTimeSinceStart();
                m_FPSCounter = 0;
                m_FrameCount = 0;
                Console.WriteLine( "Fps :" + m_FPS_ );
                Console.WriteLine( " DrawCalls :" + m_DrawCalls );
                Console.WriteLine( " Triangles : " + m_TriangleCount );
            }
            m_DrawCalls = 0;
            m_TriangleCount = 0;
        }


        /// <summary> Retourne le nombre de Draw Calls qui ont été effectué </summary>
        /// <returns></returns>
        public int GetDrawCallsCount()
        {
            return m_DrawCalls;
        }

        public void AddTriangles( int trianglecount )
        {
            m_TriangleCount += trianglecount;
        }

        public void AddDrawCall( int drawCall )
        {
            m_DrawCalls += drawCall;
        }

        public float frametime;

        private int m_DrawCalls;
        private int m_TriangleCount;

        private int m_FrameCount;
        private float m_FPS_; // Frame per seconds
        private long m_FPSCounter;
        private long m_FPSRefresh;   // Rafraichis le FPS toutes les x millisecondes
        private long m_LastFPSCount;

    }
}
