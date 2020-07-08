using GameObject.AbilityLogic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameObject.IO
{
    public class MagicFileWriter
    {
        FileStream stream;
        BinaryWriter writer;
        public MagicFileWriter()
        {


        }

        public void WriteAbilityFile(List<ModularAbility> Abilities, string FileName = "")
        {
            string fileName;
            if (FileName == "")
                FileName = "gamedata\\abilities.gdf";
            fileName = FileName;
            stream = new FileStream(fileName, FileMode.OpenOrCreate);
            writer = new BinaryWriter(stream);
            writer.Write(Abilities.Count);
            foreach (ModularAbility a in Abilities)
                WriteAbility(a);
            writer.Close();
            writer.Dispose();
            stream.Dispose();
        }

        public void WriteClassFile(List<CharacterTemplate> Classes, string FileName = "")
        {
            string fileName;
            if (FileName == "")
                FileName = "gamedata\\classes.gdf";
            fileName = FileName;
            stream = new FileStream(fileName, FileMode.OpenOrCreate);
            writer = new BinaryWriter(stream);
            writer.Write(Classes.Count);
            foreach (CharacterTemplate a in Classes)
                WriteClass(a);
            writer.Close();
            writer.Dispose();
            stream.Dispose();
        }

        private void WriteClass(CharacterTemplate a)
        {
            writer.Write(a.ID);
            writer.Write(a.Name);
            writer.Write(a.Description);
            writer.Write(a.HPperVIT);
            writer.Write(a.MPperINT);
            writer.Write(a.HPperLVL);
            writer.Write(a.MPperLVL);
            writer.Write((int)a.DamageStat);
            WriteDictionary(a.BaseStats);
            writer.Write(a.SkillTree.Entries.Count);
            foreach (SkillTreeEntry e in a.SkillTree.Entries)
                WriteSkillTreeEntry(e);
            writer.Write(a.StarterEquipment.Length);
            for(int i=0;i<a.StarterEquipment.Length;i++)
            {
                if(a.StarterEquipment[i]==null)
                {
                    writer.Write("null");
                }
                else
                {
                    writer.Write(a.StarterEquipment[i].ID);
                }
            }
        }

        private void WriteSkillTreeEntry(SkillTreeEntry e)
        {
            writer.Write(e.SkillID);
            writer.Write(e.LearnLevel);
            writer.Write(e.TrainingLevel);
            writer.Write(e.MaxLevel);
            writer.Write(e.ExpBase);
            writer.Write(e.ExpDelta);
            writer.Write(e.RequireItemID??"null");
            writer.Write(e.Column);
            writer.Write(e.ManualOffset.X);
            writer.Write(e.ManualOffset.Y);
            WritePreRequisiteSkills(e.PreRequisiteSkills);
        }

        private void WritePreRequisiteSkills(List<Tuple<string, int>> preRequisiteSkills)
        {
            if(preRequisiteSkills==null)
            {
                writer.Write(0);
                return;
            }
            writer.Write(preRequisiteSkills.Count);
            foreach(Tuple<string,int> prereq in preRequisiteSkills)
            {
                writer.Write(prereq.Item1);
                writer.Write(prereq.Item2);
            }
        }

        void WriteAbility(ModularAbility ability)
        {
            writer.Write(ability.ID);
            writer.Write(ability.Name);
            writer.Write(ability.DescriptionString);
            writer.Write(ability.Icon);
            WriteDictionary(ability.BaseValues);
            WriteDictionary(ability.GrowthValues);
            writer.Write(ability.GetModules().Count);
            foreach (ITimedEffect effect in ability.GetModules())
                WriteEffect(effect);
        }

        void WriteDictionary(Dictionary<string,float> dictionary)
        {
            writer.Write(dictionary.Count);
            foreach(KeyValuePair<string,float> entry in dictionary)
            {
                writer.Write(entry.Key);
                writer.Write(entry.Value);
            }
        }
        
        void WriteEffect(ITimedEffect effect)
        {
            writer.Write(effect.EffectType);
            writer.Write(effect.BaseTime);
            writer.Write(effect.DeltaTime);
            writer.Write(effect.BaseDuration);
            writer.Write(effect.DeltaDuration);
            writer.Write(effect.GetParamValues().Length);
            foreach (string s in effect.GetParamValues())
                writer.Write(s);
        }
    }
}
