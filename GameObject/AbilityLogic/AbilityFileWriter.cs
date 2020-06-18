using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameObject.AbilityLogic
{
    public class AbilityFileWriter
    {
        List<ModularAbility> abilities;
        string fileName;
        FileStream stream;
        BinaryWriter writer;
        public AbilityFileWriter(List<ModularAbility> Abilities, string FileName="")
        {
            if (FileName == "")
                FileName = "gamedata\\abilities.gdf";
            fileName = FileName;
            abilities = Abilities;

        }
        
        public void WriteFile()
        {
            stream = new FileStream(fileName, FileMode.OpenOrCreate);
            writer = new BinaryWriter(stream);
            writer.Write(abilities.Count);
            foreach (ModularAbility a in this.abilities)
                WriteAbility(a);
            writer.Close();
            writer.Dispose();
            stream.Dispose();
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
