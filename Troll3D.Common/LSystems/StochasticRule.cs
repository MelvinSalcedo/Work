using System;

namespace Troll3D.Common.LSystems
{
    /// <summary>
    ///  Une règle Stochastique est une règle soumise à une probabilité. 
    /// </summary>
    public class StochasticRule : LSystemRule
    {
        /// <summary>
        /// On précise la règle 1 et la règle 2. si le test de probabilité renvoie true,
        /// on utilisera la première règle, autrement, on utilisera la deuxième
        /// </summary>
        public StochasticRule( char c, string rulea, string ruleb, float probability, Random rand )
        {
            rulea_ = rulea;
            ruleb_ = ruleb;
            probability_ = probability;
            val_ = c;
            m_random = rand;
        }

        public override string GetRule()
        {
            float val = ( float )m_random.NextDouble();
            if ( val > probability_ )
            {
                return rulea_;
            }
            return ruleb_;

        }

        string rulea_;
        string ruleb_;
        float probability_;

        Random m_random;
    }
}
