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
