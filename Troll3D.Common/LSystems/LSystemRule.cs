
namespace Troll3D.Common.LSystems
{

    /// <summary>
    /// Une règle d'un LSystem
    /// </summary>
    public class LSystemRule
    {
        public LSystemRule() { }

        public LSystemRule( char val, string rule )
        {
            val_ = val;
            rule_ = rule;
        }

        public virtual string GetRule()
        {
            return rule_;
        }

        public char val_;
        public string rule_;
    }

}