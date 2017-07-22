using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace GUI
{
    public interface IAction
    {
        float CoolDown { get; set; }
        float MaxCoolDown { get; set; }
        void Use();
        int Icon { get; set; }
        int StackSize { get; set; }
        string Name { get; set; }
        void Render(GraphicsDevice device, int X, int Y, bool RenderCooldown = false, bool RenderProgress = false);
        string GetTip();
    }
}
