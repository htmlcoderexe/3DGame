using GameObject.AbilityLogic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameObject.IO
{
    /// <summary>
    /// Writes various files used by the game systems.
    /// </summary>
    public class MagicFileWriter
    {
        FileStream stream;
        BinaryWriter writer;
        public MagicFileWriter()
        {


        }
        /// <summary>
        /// Writes a list of modular abilities to a binary file.
        /// </summary>
        /// <param name="Abilities">List of modular abilities to write.</param>
        /// <param name="FileName">Filename if not default.</param>
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
            WriteClassFileVersion1(Classes, FileName);
        }

        public void WriteItemTypeDefinitionFile(List<ItemTypeDefinition> ItemTypeDefinitions, string FileName = "")
        {
            WriteItemTypeDefinitionFile1(ItemTypeDefinitions, FileName);
        }

        public void WriteItemAddonDefinitionFile(List<ItemAddonEntry> Addons, string FileName ="")
        {
            WriteItemAddonDefinitionFile1(Addons, FileName);
        }

        public void WriteClassFileVersion0(List<CharacterTemplate> Classes, string FileName = "")
        {
            string fileName;
            if (FileName == "")
                FileName = "gamedata\\classes.gdf";
            fileName = FileName;
            stream = new FileStream(fileName, FileMode.OpenOrCreate);
            writer = new BinaryWriter(stream);
            writer.Write(Classes.Count);
            foreach (CharacterTemplate a in Classes)
                WriteClass0(a);
            writer.Close();
            writer.Dispose();
            stream.Dispose();
        }
        public void WriteClassFileVersion1(List<CharacterTemplate> Classes, string FileName = "")
        {
            string fileName;
            if (FileName == "")
                FileName = "gamedata\\classes.gdf";
            fileName = FileName;
            stream = new FileStream(fileName, FileMode.OpenOrCreate);
            writer = new BinaryWriter(stream);
            writer.Write("MAGICFILE");
            writer.Write(1);
            writer.Write("classdata");
            writer.Write(Classes.Count);
            foreach (CharacterTemplate a in Classes)
                WriteClass1(a);
            writer.Close();
            writer.Dispose();
            stream.Dispose();
        }


        public void WriteItemTypeDefinitionFile0(List<ItemTypeDefinition> ItemTypeDefinitions, string FileName = "")
        {
            string fileName;
            if (FileName == "")
                FileName = "gamedata\\itemtypes.gdf";
            fileName = FileName;
            stream = new FileStream(fileName, FileMode.OpenOrCreate);
            writer = new BinaryWriter(stream);
            writer.Write(ItemTypeDefinitions.Count);
            foreach (ItemTypeDefinition def in ItemTypeDefinitions)
                WriteItemTypeDefinition0(def);
            writer.Close();
            writer.Dispose();
            stream.Dispose();

        }
        public void WriteItemTypeDefinitionFile1(List<ItemTypeDefinition> ItemTypeDefinitions, string FileName = "")
        {
            string fileName;
            if (FileName == "")
                FileName = "gamedata\\itemtypes.gdf";
            fileName = FileName;
            stream = new FileStream(fileName, FileMode.OpenOrCreate);
            writer = new BinaryWriter(stream);
            writer.Write("MAGICFILE");
            writer.Write(1);
            writer.Write("itemdefdata");
            writer.Write(ItemTypeDefinitions.Count);
            foreach (ItemTypeDefinition def in ItemTypeDefinitions)
                WriteItemTypeDefinition1(def);
            writer.Close();
            writer.Dispose();
            stream.Dispose();

        }
        public void WriteItemAddonDefinitionFile1(List<ItemAddonEntry> ItemAddons, string FileName = "")
        {
            string fileName;
            if (FileName == "")
                FileName = "gamedata\\itemaddons.gdf";
            fileName = FileName;
            stream = new FileStream(fileName, FileMode.OpenOrCreate);
            writer = new BinaryWriter(stream);
            writer.Write("MAGICFILE");
            writer.Write(1);
            writer.Write("itemaddondata");
            writer.Write(ItemAddons.Count);
            foreach (ItemAddonEntry def in ItemAddons)
                WriteAddonDefinition1(def);
            writer.Close();
            writer.Dispose();
            stream.Dispose();

        }

        public void WriteItemMasterTemplateFile(List<ItemMasterTemplate> Templates, string FileName = "")
        {
            string fileName;
            if (FileName == "")
                FileName = "gamedata\\itemmasters.gdf";
            fileName = FileName;
            stream = new FileStream(fileName, FileMode.OpenOrCreate);
            writer = new BinaryWriter(stream);
            writer.Write(Templates.Count);
            foreach (ItemMasterTemplate a in Templates)
                WriteItemMasterTemplate(a);
            writer.Close();
            writer.Dispose();
            stream.Dispose();
        }
        


        public void WriteItemMasterTemplate(ItemMasterTemplate a)
        {

        }


        private void WriteClass0(CharacterTemplate a)
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
            for (int i = 0; i < a.StarterEquipment.Length; i++)
            {
                if (a.StarterEquipment[i] == null)
                {
                    writer.Write("null");
                }
                else
                {
                    writer.Write(a.StarterEquipment[i].ID);
                }
            }
        }
        private void WriteClass1(CharacterTemplate a)
        {
            writer.Write(a.ID);
            writer.Write(a.Name);
            writer.Write(a.Description);
            writer.Write(a.HPperVIT);
            writer.Write(a.MPperINT);
            writer.Write(a.HPperLVL);
            writer.Write(a.MPperLVL);
            writer.Write((int)a.DamageStat);
            writer.Write((int)a.ArmourPreference);
            WriteListOfString(a.WeaponPreferenceIDs);
            WriteDictionary(a.BaseStats);
            writer.Write(a.SkillTree.Entries.Count);
            foreach (SkillTreeEntry e in a.SkillTree.Entries)
                WriteSkillTreeEntry(e);
            writer.Write(a.StarterEquipment.Length);
            for (int i = 0; i < a.StarterEquipment.Length; i++)
            {
                if (a.StarterEquipment[i] == null)
                {
                    writer.Write("null");
                }
                else
                {
                    writer.Write(a.StarterEquipment[i].ID);
                }
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

        void WriteItemTypeDefinition0(ItemTypeDefinition typedef)
        {
            writer.Write(typedef.ID);
            writer.Write(typedef.Name);
            writer.Write((byte)typedef.SlotID);
            for (int i = 0; i < 6; i++)
                writer.Write(typedef.MainStatMultipliers[i]);
            WriteListOfInt(typedef.Icons);
            writer.Write((int)typedef.ItemCategory);
        }
        void WriteItemTypeDefinition1(ItemTypeDefinition typedef)
        {
            writer.Write(typedef.ID);
            writer.Write(typedef.Name);
            writer.Write((byte)typedef.SlotID);
            for (int i = 0; i < 6; i++)
                writer.Write(typedef.MainStatMultipliers[i]);
            writer.Write(typedef.AttributeRequirements[0]);
            writer.Write(typedef.AttributeRequirements[1]);
            writer.Write(typedef.AttributeRequirements[2]);
            writer.Write(typedef.AttributeRequirements[3]);
            WriteListOfInt(typedef.Icons);
            writer.Write((int)typedef.ItemCategory);
        }


        private void WriteAddonDefinition1(ItemAddonEntry entry)
        {
            writer.Write(entry.StatType);
            writer.Write(entry.BaseValue);
            writer.Write(entry.GrowthValue);
            writer.Write(entry.IsPercentage);
            writer.Write(entry.EffectString);
            writer.Write(entry.Rareness);
            writer.Write(entry.MinLevelTier);
            writer.Write(entry.LoreKeyword);
            WriteListOfString(entry.ItemTypes);
            WriteListOfString(entry.SetTypes);
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


        void WriteDictionary(Dictionary<string, float> dictionary)
        {
            writer.Write(dictionary.Count);
            foreach (KeyValuePair<string, float> entry in dictionary)
            {
                writer.Write(entry.Key);
                writer.Write(entry.Value);
            }
        }

        void WriteListOfInt(List<int> list)
        {
            writer.Write(list.Count);
            foreach (int i in list)
                writer.Write(i);
        }
        void WriteListOfString(List<string> list)
        {
            writer.Write(list.Count);
            foreach (string i in list)
                writer.Write(i);
        }
    }
}
