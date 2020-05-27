using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameObject.AbilityLogic
{
    public class EffectTimeline
    {
        SortedDictionary<float, List<ITimedEffect>> Effects;
        /// <summary>
        /// Adds a single effect to the internal collection
        /// </summary>
        /// <param name="effect"></param>
        public void Add(ITimedEffect effect)
        {
            if(!Effects.ContainsKey(effect.Time))
            {
                Effects.Add(effect.Time, new List<ITimedEffect>());
            }
            Effects[effect.Time].Add(effect);
        }
        /// <summary>
        /// Returns total amount of elements
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            int result = 0;
            foreach (List<ITimedEffect> l in this.Effects.Values)
                result += l.Count;
            return result;
        }
        /// <summary>
        /// Returns a flat list
        /// </summary>
        /// <returns></returns>
        public List<ITimedEffect> GetList()
        {
            List<ITimedEffect> result = new List<ITimedEffect>();
            foreach (List<ITimedEffect> l in this.Effects.Values)
                result.AddRange(l);
            return result;
        }
        /// <summary>
        /// Generates a new instance from a list of effects
        /// </summary>
        /// <param name="effectList"></param>
        public EffectTimeline(List<ITimedEffect> effectList)
        {
            this.Effects = new SortedDictionary<float, List<ITimedEffect>>();
            foreach (ITimedEffect effect in effectList)
                Add(effect);
        }
        public EffectTimeline()
        {
            this.Effects = new SortedDictionary<float, List<ITimedEffect>>();
        }
        public List<float> Keys
        { get { return this.Effects.Keys.ToList(); } }

        public void Remove(float index)
        {
            this.Effects.Remove(index);
        }
        public List<ITimedEffect> Get(float index)
        {
            return this.Effects[index];
        }
    }
}
