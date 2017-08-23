using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DGame.GameObjects.AbilityLogic
{
    public struct EffectTemplate
    {
        public enum EffectTarget
        {
            Target,
            User
        }

        public EffectTarget Target;
        public int Probability;
        public int ProbabilityGrowth;
        public string Type;
        public string Animation;
        public Microsoft.Xna.Framework.Color Colour;

        public EffectTemplate(string line)
        {
            string[] parts = line.Split('/');
            this.Target = parts[0] == "target" ? EffectTarget.Target : EffectTarget.User;
            this.Probability = Int32.Parse(parts[1].Split(',')[0]);
            this.ProbabilityGrowth = Int32.Parse(parts[1].Split(',')[1]);
            this.Type = parts[2];
            this.Animation = parts[3];

            this.Colour = Utility.GetColor(parts[4]);
        }
    }
}
