using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _3DGame.GameObjects.Items;
using GUI;
using Microsoft.Xna.Framework;

namespace _3DGame.Scenes.GameplayAssets
{
    public class StatusWindow : GUI.Window
    {
        public GUI.Controls.Button OKButton;
        public GameplayAssets.ItemSlot slot;
        public GameObjects.MapEntities.Actos.Player Player;
        private GUI.Controls.ProgressBar HPBar;
        public override void Close()
        {
            
        }
        public StatusWindow(WindowManager WM, GameObjects.MapEntities.Actos.Player Player)
            {
            this.Player = Player;
            this.WM = WM;
            this.Width = 360;
            this.Height = 200;
            this.Title = "Status";
            this.OKButton = new GUI.Controls.Button("Test button");
            this.OKButton.Clicked += OKButton_Clicked;
            this.OKButton.Width = 128;
            this.OKButton.Height = 48;
            this.OKButton.X = 64;
            this.OKButton.Y = 49;
            this.AddControl(this.OKButton);

            this.slot = new ItemSlot(GameObjects.Items.Material.MaterialTemplates.GetRandomMaterial());
            this.slot.X = 0;
            this.slot.Y = 49;
            this.Controls.Add(this.slot);

            this.HPBar = new GUI.Controls.ProgressBar();
            this.HPBar.DisplayLabel = true;
            this.HPBar.Style = 0;
            this.HPBar.Height = 16;
            this.HPBar.Width = 192;
            this.HPBar.Colour = new Color(255, 0, 80);
            this.AddControl(HPBar);
           // this.AddControl(Texst);
        }

        public override void Update(float dT)
        {
            this.HPBar.Title = ((int)(this.Player.CurrentHP)).ToString() + "/" + ((int)this.Player.CalculateStat("HP")).ToString();
            this.HPBar.MaxValue = (int)this.Player.CalculateStat("HP");
            this.HPBar.MinValue = 0;
            this.HPBar.Value = (int)this.Player.CurrentHP;
        }

        private void OKButton_Clicked(object sender, EventArgs e)
        {
            GameObjects.Item Item;
            Item = GameObjects.Items.Material.MaterialTemplates.GetRandomMaterial();
            Item.SubType = GameObjects.RNG.Next(GameObjects.Items.Material.MaterialType.Max);
            Item.Description= "A " + GameObjects.Items.Material.MaterialType.GetTypeName(Item.SubType) +" made from "+Item.Name+". Used in crafting equipment.";
            this.OKButton.Title = Item.GetName();
            this.slot.Item = Item;
            ItemEquip eq = new ItemEquip();
            BonusPool p=BonusPool.Load("heavy_0_10");
            eq.Bonuses.Add(p.PickBonus());
            eq.Bonuses.Add(p.PickBonus());
            Enchantment enc = new Enchantment() { Effecttext = "Fire damage +{0}%", Type = "dmg_scale_fire" };
            enc.LineColour = new Color(255, 50, 30);
            enc.Multiplier = 0.15f;
            eq.Enchant = enc;
            eq.SubType = GameObjects.RNG.Next(0, 5);
            eq.PrimaryMaterial = Material.MaterialTemplates.GetRandomMaterial();
            eq.SecondaryMaterial = Material.MaterialTemplates.GetRandomMaterial();
            List<string> ToolTip = eq.GetTooltip();
            this.slot.Item = eq;
            Console.WriteEx("New item is ^BEGINLINK " + Renderer.ColourToCode(eq.NameColour)+ "["+eq.GetName()+"] ^ENDLINK .^FFFFFF Click name to see more.",new List<Action> { new Action(() => {ToolTipWindow tip = new ToolTipWindow(this.WM,ToolTip, WM.MouseX, WM.MouseY, false);
                WM.Add(tip); })});
            //this.Player.EquipItem(eq);
            this.Title = this.Player.CalculateStat("HP").ToString();
        }
    }
}
