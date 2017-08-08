using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace GUI
{
    public interface IActionIcon
    {
        float CoolDown { get; set; }
        float MaxCoolDown { get; set; }
        void Use();
        int Icon { get; set; }
        int StackSize { get; set; }
        string Name { get; set; }
        void Render(int X, int Y, GraphicsDevice device, GUI.Renderer Renderer, bool RenderCooldown = false, bool RenderEXP = false);
        List<string> GetTooltip();
    }
}
