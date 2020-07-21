using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameObject
{
    public class SkillTree
    {
        public const int ICON_STRIDE = 16;
        public const int ICON_WIDTH = 40;
        public List<SkillTreeEntry> Entries { get; set; }

        public SkillTree()
        {
            Entries = new List<SkillTreeEntry>();
        }

        public SkillTreeEntry Find(string ID)
        {
            foreach (SkillTreeEntry a in this.Entries)
                if (a.SkillID == ID)
                    return a;
            return null;
        }
    }
}
