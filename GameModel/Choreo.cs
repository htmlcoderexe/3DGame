using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameModel
{
    public class Choreo
    {
        Dictionary<string, Dictionary<string, PartAnimation>> AnimationData;

        public Choreo(Dictionary<string, Dictionary<string, PartAnimation>> Data)
        {
            this.AnimationData = Data;
        }

        public Dictionary<string, PartAnimation> GetMove(string Name)
        {
            if (!this.AnimationData.ContainsKey(Name))
                return null;
            return this.AnimationData[Name];
        }

        public float GetAnimationLength(string Name)
        {
            float max = 0;

            Dictionary<string, PartAnimation> Move = GetMove(Name);
            if (Move != null)
                foreach (PartAnimation p in Move.Values.ToList())
                    if (p.Length > max)
                        max = p.Length;

            return max;
        }


    }
}
