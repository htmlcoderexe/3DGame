using GameObject.AbilityLogic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameObject.IO
{
    public class MagicFileReader
    {
       
        FileStream stream;
        BinaryReader reader;

        #region Complete gamefiles

        public List<ModularAbility> ReadAbilityFile(string FileName = "")
        {
            string fileName;
            if (FileName == "")
                FileName = "gamedata\\abilities.gdf"; //#TODO: some sort of config file that can ship with the game/editor to point to a consistent location
            fileName = FileName;
            List<ModularAbility> abilities;
            abilities = new List<ModularAbility>();
            try
            {
                stream = new FileStream(fileName, FileMode.Open);
            }
            catch(FileNotFoundException fnex) //return an empty list if no file
            {
                return abilities;
            }
            
            reader = new BinaryReader(stream);
            int count = reader.ReadInt32();
            for (int i = 0; i < count; i++)
                abilities.Add(ReadAbility());
            reader.Close();
            reader.Dispose();
            stream.Dispose();
            return abilities;
        }

        public List<CharacterTemplate> ReadClassFile(string FileName = "")
        {
            string fileName;
            if (FileName == "")
                FileName = "gamedata\\classes.gdf";
            fileName = FileName;
            List<CharacterTemplate> classes;
            classes = new List<CharacterTemplate>();
            try
            {
                stream = new FileStream(fileName, FileMode.Open);
    }
            catch(FileNotFoundException fnex) //return an empty list if no file
            {
                return classes;
            }

            reader = new BinaryReader(stream);
            int count = reader.ReadInt32();
            for (int i = 0; i<count; i++)
                classes.Add(ReadClass());
            reader.Close();
            reader.Dispose();
            stream.Dispose();
            return classes;
        }

        public List<ItemTypeDefinition> ReadItemTypeDefinitionFile(string FileName = "")
        {
            string fileName;
            if (FileName == "")
                FileName = "gamedata\\itemtypes.gdf";
            fileName = FileName;
            List<ItemTypeDefinition> itemdefs;
            itemdefs = new List<ItemTypeDefinition>();
            try
            {
                stream = new FileStream(fileName, FileMode.Open);
            }
            catch(FileNotFoundException fnex)
            {
                return itemdefs;
            }
            reader = new BinaryReader(stream);
            int count = reader.ReadInt32();
            for (int i = 0; i < count; i++)
                itemdefs.Add(ReadItemTypeDefinition());
            reader.Close();
            reader.Dispose();
            stream.Dispose();
            return itemdefs;


        }

        #endregion

        #region gamefile entries

        public ModularAbility ReadAbility()
        {
            ModularAbility result = ModularAbility.CreateEmpty(reader.ReadString());
            result.Name = reader.ReadString();
            result.DescriptionString = reader.ReadString();
            result.Icon = reader.ReadInt32();
            result.BaseValues = ReadDictionary();
            result.GrowthValues = ReadDictionary();
            int effcount = reader.ReadInt32();
            for (int i = 0; i < effcount; i++)
                result.Effects.Add(ReadEffect());
            return result;
        }

        private ItemTypeDefinition ReadItemTypeDefinition()
        {
            ItemTypeDefinition result = new ItemTypeDefinition();

            result.ID = reader.ReadString();
            result.Name = reader.ReadString();
            result.SlotID = reader.ReadByte();
            //6 floats for mainstats
            result.MainStatMultipliers = new float[]
            {
                reader.ReadSingle(),
                reader.ReadSingle(),
                reader.ReadSingle(),
                reader.ReadSingle(),
                reader.ReadSingle(),
                reader.ReadSingle()
            };
            result.Icons = ReadListOfInt();
            result.ItemCategory = (Items.ItemEquip.EquipCategories)reader.ReadInt32();

            return result;

        }

        private CharacterTemplate ReadClass()
        {
            CharacterTemplate result = CharacterTemplate.CreateEmpty(reader.ReadString());
            result.Name= reader.ReadString();
            result.Description= reader.ReadString();
            result.HPperVIT = reader.ReadInt32();
            result.MPperINT = reader.ReadInt32();
            result.HPperLVL = reader.ReadInt32();
            result.MPperLVL = reader.ReadInt32();
            CharacterTemplate.MainStats setstat;
            switch (reader.ReadInt32())
            {
                case 0:
                    {
                        setstat = CharacterTemplate.MainStats.STR;
                        break;
                    }
                case 1:
                    {
                        setstat = CharacterTemplate.MainStats.DEX;
                        break;
                    }
                case 2:
                default:
                    {
                        setstat = CharacterTemplate.MainStats.INT;
                        break;
                    }
            }
            result.DamageStat = setstat;
            result.BaseStats = ReadDictionary();
            int skillcount = reader.ReadInt32();
            for (int i = 0; i < skillcount; i++)
                result.SkillTree.Entries.Add(ReadSkillTreeEntry());
            List<string> invnentoryIDs = new List<string>();
            int inventorycount = reader.ReadInt32();
            for (int i = 0; i < inventorycount; i++)
                invnentoryIDs.Add(reader.ReadString());
            return result;
        }

        #endregion
        #region subentries

        #region Character class subentries
        /// <summary>
        /// Reads a single skilltree entry.
        /// </summary>
        /// <returns>The skilltree entry that was read.</returns>
        public SkillTreeEntry ReadSkillTreeEntry()
        {
            SkillTreeEntry result = new SkillTreeEntry();
            result.SkillID = reader.ReadString();
            result.LearnLevel = reader.ReadInt32();
            result.TrainingLevel = reader.ReadInt32();
            result.MaxLevel = reader.ReadInt32();
            result.ExpBase = reader.ReadInt32();
            result.ExpDelta = reader.ReadInt32();
            result.RequireItemID = reader.ReadString();
            result.Column = reader.ReadInt32();
            int X = reader.ReadInt32();
            int Y = reader.ReadInt32();
            result.ManualOffset = new Microsoft.Xna.Framework.Point(X,Y);
            result.PreRequisiteSkills = ReadPreRequisiteSkills();

            return result;
        }

        private List<Tuple<string, int>> ReadPreRequisiteSkills()
        {
            List<Tuple<string, int>> result = new List<Tuple<string, int>>();
            int count = reader.ReadInt32();
            if (count == 0)
                return null;
            for(int i=0;i<count;i++)
            {
                string skillid = reader.ReadString();
                int skillvl = reader.ReadInt32();
                result.Add(new Tuple<string, int>(skillid, skillvl));
            }
            return result;
        }
        #endregion
        #region Ability subentries
        /// <summary>
        /// Reads an Ability effect.
        /// </summary>
        /// <returns>Effect.</returns>
        public ITimedEffect ReadEffect()
        {
            ITimedEffect result = EffectHelper.CreateEmpty(reader.ReadString());
            result.BaseTime = reader.ReadSingle();
            result.DeltaTime = reader.ReadSingle();
            result.BaseDuration = reader.ReadSingle();
            result.DeltaDuration = reader.ReadSingle();
            int paramcount = reader.ReadInt32();
            string[] eparams = new string[paramcount];
            for (int i = 0; i < paramcount; i++)
                eparams[i] = reader.ReadString();
            result.SetParamValues(eparams);
            return result;

        }
        #endregion
        #endregion

        #region primitives
        /// <summary>
        /// Reads string based dictionary of float values.
        /// </summary>
        /// <returns>Dictionary containing float entries parsed off the stream.</returns>
        public Dictionary<string,float> ReadDictionary()
        {
            Dictionary<string, float> result = new Dictionary<string, float>();
            int count = reader.ReadInt32();
            for (int i = 0; i < count; i++)
                result.Add(reader.ReadString(), reader.ReadSingle());
            return result;
        }
        /// <summary>
        /// Reads a list of integers.
        /// </summary>
        /// <returns>List of integers.</returns>
        public List<int> ReadListOfInt()
        {
            List<int> result = new List<int>();
            int count = reader.ReadInt32();
            for(int i=0;i<count;i++)
            {
                result.Add(reader.ReadInt32());
            }
            return result;
        }
        #endregion

    }
}
