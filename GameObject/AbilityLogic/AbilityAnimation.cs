using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace GameObject.AbilityLogic
{
    public class AbilityAnimation
    {
        public string Name;
        public Color Colour;
        public AbilityAnimation(string Name, Color Colour)
        {
            this.Name = Name;
            this.Colour = Colour;
        }
    }
}
