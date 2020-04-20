using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DGame.Scenes.GameplayAssets.Windows
{
    public class InventoryWindow :GUI.Window
    {
        public InventoryWindow(GUI.WindowManager WM, GameObjects.MapEntities.Actors.Player Player)
        {
            InventoryControl ic = new InventoryControl(WM, Player.Inventory);
            this.Width = ic.Width + 10;
            this.Height = ic.Height + this.Margin.Y + this.Margin.Height;
            this.Title = "Inventory";
            this.AddControl(ic);
            this.X = 600;
            this.Y = 500;
        }
    }
}
