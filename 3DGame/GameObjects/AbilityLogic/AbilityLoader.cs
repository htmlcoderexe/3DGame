using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DGame.GameObjects.AbilityLogic
{
    public class AbilityLoader
    {
        private List<KeyValuePair<string, string>> _data = new List<KeyValuePair<string, string>>();
        private int _datapointer = 0;
        private bool _finished = false;
        public AbilityLoader(string ClassName)
        {
            FileStream fs;
            string cd = System.Reflection.Assembly.GetExecutingAssembly().Location;
            cd = System.IO.Path.GetDirectoryName(cd);
            string filename = cd + "\\gamedata\\abilitytemplates\\" + ClassName + ".gdf";
            try
            {
                fs = new FileStream(filename, FileMode.Open);
            }
            catch
            {
                Console.Write("^FF0000 Error loading ability set for ^FFFF00 " + ClassName);

                return;
            }
            StreamReader st = new StreamReader(fs);
            string line;
            int count = 0;
            while ((line = st.ReadLine()) != null)
            {
                string[] parts = line.Split(new char[]{ '='},2);
                this._data.Add(new KeyValuePair<string, string>(parts[0], parts[1]));
                
            }
            st.Close();
            st.Dispose();
            fs.Close();
            fs.Dispose();
        }
        public List<Ability> LoadAbilities()
        {
            List<Ability> result = new List<Ability>();
            // List<List<KeyValuePair<string, string>>> l = new List<List<KeyValuePair<string, string>>>();
            while (!_finished)
            {
                List<KeyValuePair<string, string>> ability = FetchAbility();
                Ability a = new Ability();
                string[] effectparts=null;
                foreach(KeyValuePair<string,string> kvp in ability)
                {
                    switch(kvp.Key)
                    {
                        case "Icon":
                            {
                                a.Icon = Int32.Parse(kvp.Value);
                                break;
                            }
                        case "Name":
                            {
                                a.Name = kvp.Value;
                                break;
                            }
                        case "Description":
                            {
                                a.Description = kvp.Value;
                                break;
                            }
                        case "Target":
                            {
                                switch(kvp.Value)
                                {
                                    case "Hostile":
                                        {
                                            a.Target = Ability.AbilityTarget.Hostile;
                                            break;
                                        }
                                    case "Self":
                                        {
                                            a.Target = Ability.AbilityTarget.Self;
                                            break;
                                        }
                                    case "Friendly":
                                        {
                                            a.Target = Ability.AbilityTarget.Friendly;
                                            break;
                                        }
                                    case "Neutral":
                                        {
                                            a.Target = Ability.AbilityTarget.Neutral;
                                            break;
                                        }
                                }
                                break;
                            }
                        case "Cast":
                            {
                                string[] castparts = kvp.Value.Split('/');
                                a.CastTime = (float)int.Parse(castparts[0]) / 10f;
                                a.CastGrowth = (float)int.Parse(castparts[1]) / 10f;
                                a.CastAnimation = new AbilityAnimation(castparts[2], Utility.GetColor(castparts[3]));

                                break;
                            }
                        case "Charge":
                            {
                                string[] castparts = kvp.Value.Split('/');
                                a.ChargeTime = (float)int.Parse(castparts[0]) / 10f;
                                a.ChargeGrowth = (float)int.Parse(castparts[1]) / 10f;
                                a.ChargeAnimation = new AbilityAnimation(castparts[2], Utility.GetColor(castparts[3]));

                                break;
                            }
                        case "LearnLevel":
                            {
                                a.LearnLevel = int.Parse(kvp.Value);

                                break;
                            }
                        case "Effect":
                            {
                                effectparts = kvp.Value.Split('/');
                                //AbilityEffect e = AbilityEffect.CreateEffect()
                                break;
                            }
                        case "EffectParams":
                            {
                                string[] eparams = kvp.Value.Split('/');
                                if(effectparts!=null)
                                {
                                    AbilityEffect e = AbilityEffect.CreateEffect(effectparts[2], eparams);
                                    string[] probs = effectparts[1].Split(',');
                                    e.Probability = (float)int.Parse(probs[0]) / 100f;
                                    e.ProbabilityGrowth = (float)int.Parse(probs[1]) / 100f;
                                    e.Animation = new AbilityAnimation(effectparts[3], Utility.GetColor(effectparts[4]));
                                    switch(effectparts[0])
                                    {
                                        case "target":
                                            {
                                                e.Target = AbilityEffect.EffectTarget.Target;
                                                break;
                                            }
                                        case "user":
                                            {
                                                e.Target = AbilityEffect.EffectTarget.User;
                                                break;
                                            }
                                        case "userarea":
                                            {
                                                e.Target = AbilityEffect.EffectTarget.UserArea;
                                                break;
                                            }
                                        case "userareaex":
                                            {
                                                e.Target = AbilityEffect.EffectTarget.UserAreaExclude;
                                                break;
                                            }
                                        case "targetarea":
                                            {
                                                e.Target = AbilityEffect.EffectTarget.TargetArea;
                                                break;
                                            }
                                        default:
                                            {
                                                e.Target = AbilityEffect.EffectTarget.Target;
                                                break;
                                            }
                                    }
                                    a.Effects.Add(e);
                                }
                                effectparts = null;
                                break;
                            }
                        case "MPCost":
                            {
                                string[] castparts = kvp.Value.Split(',');
                                a.MPCost = (float)int.Parse(castparts[0]) / 10f;
                                a.MPCostGrowth = (float)int.Parse(castparts[1]) / 10f;
                                break;
                            }
                    }
                }
                result.Add(a);
                Console.Write(a.FormatDescription());
                a.Level += 5;
                Console.Write(a.FormatDescription());

            }
            // l.Sort();
            return result;
        }
        public List<KeyValuePair<string,string>> FetchAbility()
        {
            List<KeyValuePair<string, string>> result = new List<KeyValuePair<string, string>>();
            bool _hasname = false;
            while(!_finished)
            {
                if(_datapointer>=_data.Count)
                {
                    _finished = true;
                    break;
                }
                KeyValuePair<string, string> line = _data[_datapointer];
                if(line.Key=="Name")
                {
                    if (_hasname)
                        break;
                    _hasname = true;
                }
                result.Add(line);
                _datapointer++;
            }

            return result;
        }
    }
}
