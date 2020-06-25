using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameObject
{
    public class TestCharacterClass : CharacterTemplate
    {
        public TestCharacterClass()
        {
            this.Name = "Magician";
            this.ID = "mage";
            this.SkillTree = new SkillTree();

            this.DamageStat = MainStats.INT;
            this.HPperLVL = 10;
            this.HPperVIT = 5;
            this.MPperINT = 22;
            this.MPperLVL = 20;
            this.StarterEquipment = new Items.ItemEquip[Items.ItemEquip.EquipSlot.Max];

            this.BaseStats = new Dictionary<string, float>()
            {
                ["HP"]=65f,
                ["hpregen"]=5f,
                ["movement_speed"]=5f
            };

            SkillTreeEntry fb = new SkillTreeEntry
            {
                SkillID = "fireball",
                LearnLevel = 1,
                Column = 0,
                TrainingLevel = 0
            };

            SkillTreeEntry fs = new SkillTreeEntry
            {
                SkillID = "firestorm",
                LearnLevel = 10,
                Column = 0,
                TrainingLevel = 0,
                PreRequisiteSkills = new List<Tuple<string, int>> { new Tuple<string, int>("fireball", 3) }
            };
            SkillTreeEntry fs3 = new SkillTreeEntry
            {
                SkillID = "blast3",
                LearnLevel = 30,
                Column = 2,
                TrainingLevel = 0,
                PreRequisiteSkills = new List<Tuple<string, int>> { new Tuple<string, int>("firestorm", 3), new Tuple<string, int>("fireball", 3) }
            };
            this.SkillTree.Entries.Add(fb);
            this.SkillTree.Entries.Add(fs);
            this.SkillTree.Entries.Add(fs3);

        }
    }
}
