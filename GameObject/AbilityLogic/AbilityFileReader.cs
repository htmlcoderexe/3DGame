using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameObject.AbilityLogic
{
    public class AbilityFileReader
    {
        List<ModularAbility> abilities;
        string fileName;
        FileStream stream;
        BinaryReader reader;

        public AbilityFileReader(string FileName = "")
        {
            if (FileName == "")
                FileName = "gamedata\\abilities.gdf";
            fileName = FileName;
            abilities = new List<ModularAbility>();
        }

        public List<ModularAbility> ReadFile()
        {
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
