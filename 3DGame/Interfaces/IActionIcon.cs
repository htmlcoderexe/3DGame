using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace _3DGame.Interfaces
{
    public interface IActionIcon
    {
        float CooldownPercentage { get; set; }
        void PrimaryAction();
        void SecondaryAction();
        void Render(int X, int Y, GraphicsDevice device, GUI.Renderer Renderer, bool RenderCooldown = false, bool RenderEXP = false);
        string GetTooltip();
    }
}
