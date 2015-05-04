using System;
using System.Collections.Generic;
using System.IO;

namespace Troll3D.Common.LSystems
{

    /// <summary>
    ///  Les LSystem permettent de construire des formes fractales à partir d'alphabet et d'axiomes
    ///  L'idée générale étant qu'un LSystem est composé de 2 parties. d'un alphabete
    /// </summary>
    public class LSystem
    {
        public LSystem( string alphabet, string constantes, string axiome, int seed = -1 )
        {
            rules_ = new List<LSystemRule>();

            alphabet_       = alphabet;
            constantes_     = constantes;
            startaxiome_    = axiome;
            current_        = startaxiome_;

            if ( seed == -1 )
            {
                rand = new Random();
            }
            else
            {
                rand = new Random( seed );
            }

        }

        void Reset()
        {
            current_ = startaxiome_;
        }

        /// <summary>
        /// Ajoute une règle au LSystème en cours
        /// </summary>
        public void AddRule( char c, string rule )
        {
            rules_.Add( new LSystemRule( c, rule ) );
        }

        public void AddStochasticRule( char c, string rulea, string ruleb, float prob )
        {
            rules_.Add( new StochasticRule( c, rulea, ruleb, prob, rand ) );
        }

        /// <summary>
        /// On applique les règles
        /// </summary>
        public void ApplyRules()
        {

            string newstring = "";

            Console.WriteLine( current_.Length );

            for ( int i = 0; i < current_.Length; i++ )
            {

                bool found = false;

                for ( int j = 0; j < rules_.Count && found == false; j++ )
                {

                    if ( current_[i] == rules_[j].val_ )
                    {
                        found = true;
                        newstring += rules_[j].GetRule();
                    }
                }
                if ( found == false )
                {
                    newstring += current_[i];
                }
            }
            current_ = newstring;
        }

        // Datas

        public string current_;

        public string alphabet_;
        public string constantes_;
        public string startaxiome_;
        public List<LSystemRule> rules_;

        public float initialAngle;
        public float angleValue;
        public Random rand;

    }
}
