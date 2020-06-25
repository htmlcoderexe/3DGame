using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameObject
{
    public class CharacterTemplate
    {
        public enum MainStats
        {
            STR, DEX, INT, VIT
        }

        public string Name { get; set; }
        public string ID { get; set; }
        public SkillTree SkillTree { get; set; }
        public Dictionary<string, float> BaseStats { get; set; }
        public MainStats DamageStat { get; set; }
        public int HPperVIT { get; set; }
        public int MPperINT { get; set; }
        public int HPperLVL { get; set; }
        public int MPperLVL { get; set; }
        public Items.ItemEquip[] StarterEquipment { get; set; }
    }
}
