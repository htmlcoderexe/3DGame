using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelEditor
{
    public class SettingsContainer : ICloneable
    {
        public Microsoft.Xna.Framework.Color ViewerBackgroundColor;

        public object Clone()
        {
            object result= this.MemberwiseClone();
            return result;
        }
    }
}
