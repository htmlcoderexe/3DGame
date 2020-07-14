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

        public Dictionary<string,float> ReadDictionary()
        {
            Dictionary<string, float> result = new Dictionary<string, float>();
            int count = reader.ReadInt32();
            for (int i = 0; i < count; i++)
                result.Add(reader.ReadString(), reader.ReadSingle());
            return result;
        }

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
    }
}
