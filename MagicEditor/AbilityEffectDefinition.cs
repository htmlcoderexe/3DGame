using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicEditor
{
    public class AbilityEffectDefinition
    {
        public string EffectType;
        public int Icon;
        public string FriendlyName;
        public string Description;
        public List<Tuple<string, char>> parameters;
        public static Dictionary<string, AbilityEffectDefinition> Definitions;
        public static void LoadDefinitions()
        {
            Definitions = new Dictionary<string, AbilityEffectDefinition>();
            string data = System.IO.File.ReadAllText("cfg\\ability_components.gdf");
            string[] lines = data.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            foreach(string line in lines)
            {
                string[] parts = line.Split('|');
                AbilityEffectDefinition def = new AbilityEffectDefinition
                {
                    Icon = GameObject.Utility.GetInt(parts[0]),
                    EffectType = parts[1],
                    FriendlyName = parts[2],
                    Description = parts[3],
                    parameters = new List<Tuple<string, char>>()
                };
                for (int i=4;i<parts.Length;i++)
                {
                    string[] parts2 = parts[i].Split(',');
                    def.parameters.Add(new Tuple<string, char>(parts2[0], parts2[1][0]));//just take whatever the first char is
                }
                Definitions.Add(def.EffectType, def);
            }
        }
        public static AbilityEffectDefinition GetDefinition(string typename)
        {
            return Definitions.ContainsKey(typename) ? Definitions[typename] : null;
        }
    }
}
