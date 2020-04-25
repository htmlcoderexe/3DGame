using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DGame.Scenes.GameplayAssets.Windows
{
    public class InventoryWindow :GUI.Window
    {
        private InventoryControl _ic;
        public InventoryWindow(GUI.WindowManager WM, GameObjects.MapEntities.Actors.Player Player)
        {
             _ic = new InventoryControl(WM, Player.Inventory);
            this.Width = _ic.Width + 10;
            this.Height = _ic.Height + this.Margin.Y + this.Margin.Height;
            this.Title = "Inventory";
            this.AddControl(_ic);
            this.X = 600;
            this.Y = 500;
        }
        public override void Update(float dT)
        {
            if (_ic.Inventory.Changed)
                RefreshItems();
            base.Update(dT);
        }
        public void RefreshItems()
        {
            _ic.Reload();
            _ic.Inventory.Changed = false;
        }
    }
}
