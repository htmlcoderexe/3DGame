using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameObject
{
    public class SkillTreeEntry
    {
        public string SkillID { get; set; }
        public int LearnLevel { get; set; }
        public List<Tuple<string, int>> PreRequisiteSkills { get; set; }
        public int Column { get; set; }
        public int TrainingLevel { get; set; }
        public int MaxLevel { get; set; }
        public int ExpBase { get; set; }
        public int ExpDelta { get; set; }

        //only used for display
        public string Name { get; set; }

        public int GetLevel(int ExpTotal)
        {
            return (int)MathHelper.Clamp(
                (int)((float)(ExpTotal-ExpBase)/(float)(ExpDelta))+1,
                1,
                MaxLevel
                );
        }

        public float GetExpBar(int ExpTotal)
        {
            if (ExpTotal <= 0)
                ExpTotal = 1;
            if (ExpTotal < ExpBase)
                return (float)ExpTotal / (float)ExpBase;
            return (float)((ExpTotal - ExpBase) % ExpDelta) / (float)ExpDelta;

        }

        public Point ManualOffset { get; set; }

        public Point GetLocation()
        {
            int X = SkillTree.ICON_STRIDE + (SkillTree.ICON_STRIDE + SkillTree.ICON_WIDTH) * this.Column;
            int row = (int)((float)LearnLevel/10f);
            int Y = SkillTree.ICON_STRIDE + (SkillTree.ICON_STRIDE + SkillTree.ICON_WIDTH) * row;
            return new Point(X, Y)+ManualOffset;
        }
    }
}
