using System;
using System.Diagnostics;

namespace Troll3D{

    /// <summary>
    /// Classe contenant quelques méthodes utiles, comme le temps écoulé depuis le démarrage de l'application,
    /// le temps écoulé entre 2 frame
    /// </summary>
    public class TimeHelper{

        // Public

            // Static Data

                public static TimeHelper Instance;

            // Lifecycle

                public TimeHelper(){
                    Instance    = this;
                    m_Time      = new Stopwatch();
                    m_FrameTime = new Stopwatch();
                }

            // Methods

                public void Start(){
                    m_Time.Start();
                    m_FrameTime.Start();
                }

                public void Update(){
                    m_ElapsedTime = m_FrameTime.ElapsedMilliseconds;
                    m_FrameTime.Restart();
                }

                /// <summary>
                /// Retourne en milliseconde le temps écoulé depuis le dernier rafraichissement de l'image
                /// </summary>
                /// <returns></returns>
                public long GetElapsedTime(){
                    return m_ElapsedTime;
                }

                /// <summary>
                /// Retourne en milliseconde le temps écoulé depuis le démarrage de l'application
                /// </summary>
                /// <returns></returns>
                public long GetTimeSinceStart(){
                    return m_Time.ElapsedMilliseconds;
                }

        // Private

            // Datas

                private Stopwatch   m_Time;
                private Stopwatch   m_FrameTime;

                private long        m_ElapsedTime;
    }
}
