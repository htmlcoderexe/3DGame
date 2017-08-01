using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DGame.GameObjects.Items
{
    /// <summary>
    /// An add-on template with fluctuating values
    /// </summary>
    public class BonusTemplate
    {
        /// <summary>
        /// Type of bonus, for stat bonuses matches the stat name
        /// </summary>
        public string Type;
        /// <summary>
        /// Minimum value of the fixed part
        /// </summary>
        public int FlatMin;
        /// <summary>
        /// Maximum value of the fixed part
        /// </summary>
        public int FlatMax;
        /// <summary>
        /// Minimum balue of the percentage part
        /// </summary>
        public int MultiMin;
        /// <summary>
        /// Maximum value of the percentage part
        /// </summary>
        public int MultiMax;
        /// <summary>
        /// The string that appears in the item's tooltip
        /// </summary>
        public string Effectstring;
        /// <summary>
        /// Likelihood of this template being picked from a pool
        /// </summary>
        public int Weight;
        /// <summary>
        /// Initializes a template from a |-separated line
        /// </summary>
        /// <param name="Line">A line containing a definition</param>
        public BonusTemplate(string Line)
        {
            string[] parts = Line.Split('|');
            this.Type = parts[0];
            this.Effectstring = parts[1];
            string[] Flat = parts[2].Split('~');
            this.FlatMin = Int32.Parse(Flat[0]);
            this.FlatMax = Int32.Parse(Flat[1]);
            string[] Multi = parts[3].Split('~');
            this.MultiMin = Int32.Parse(Multi[0]);
            this.MultiMax = Int32.Parse(Multi[1]);
            this.Weight = Int32.Parse(parts[4]);
        }
        /// <summary>
        /// Generates an ItemBonus with random values.
        /// </summary>
        /// <returns>An ItemBonus with values within the template's ranges.</returns>
        public ItemBonus Generate()
        {
            float Flat = RNG.Next(this.FlatMin, this.FlatMax);
            float Multi = RNG.Next(this.MultiMin, this.MultiMax) / 100.0f;
            ItemBonus b = new ItemBonus() { FlatValue = Flat, Multiplier = Multi, Effecttext = this.Effectstring, LineColour = GUI.Renderer.ColourBlue, Type = this.Type };
            return b;
        }
    }
}
