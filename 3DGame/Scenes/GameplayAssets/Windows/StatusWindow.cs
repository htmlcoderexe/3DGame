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
        private GUI.Controls.ProgressBar MPBar;
        private GUI.Controls.ProgressBar EXPBar;
        public override void Close()
        {
            
        }
        public StatusWindow(WindowManager WM, GameObjects.MapEntities.Actos.Player Player)
            {
            this.Player = Player;
            this.WM = WM;
            this.Width = 360;
            this.Height = 500;
            this.Title = "Status";
            this.OKButton = new GUI.Controls.Button("Make something!");
            this.OKButton.Clicked += OKButton_Clicked;
            this.OKButton.Width = 128;
            this.OKButton.Height = 48;
            this.OKButton.X = 64;
            this.OKButton.Y = 49;
            this.AddControl(this.OKButton);

            this.slot = new ItemSlot(null);
            this.slot.X = 0;
            this.slot.Y = 49;
            this.Controls.Add(this.slot);

            this.HPBar = new GUI.Controls.ProgressBar
            {
                DisplayLabel = true,
                Style = 0,
                Height = 16,
                Width = 192,
                Colour = new Color(255, 0, 80),
                X = 3,
                Y = 3
            };
            this.AddControl(HPBar);
            this.MPBar = new GUI.Controls.ProgressBar
            {
                DisplayLabel = true,
                Style = 0,
                Height = 16,
                Width = 192,
                Colour = new Color(25, 150, 255),
                X=3,
                Y=21
            };
            this.AddControl(MPBar);
            this.EXPBar = new GUI.Controls.ProgressBar
            {
                DisplayLabel = true,
                Style = 0,
                Height = 24,
                Width = 192,
                Colour = new Color(200, 100, 255),
                X = 3,
                Y = 39
            };
            this.AddControl(EXPBar);
            // this.AddControl(Texst);
        }

        public override void Update(float dT)
        {
            this.HPBar.Title = ((int)(this.Player.CurrentHP)).ToString() + "/" + ((int)this.Player.CalculateStat("HP")).ToString();
            this.HPBar.MaxValue = (int)this.Player.CalculateStat("HP");
            this.HPBar.MinValue = 0;
            this.HPBar.Value = (int)this.Player.CurrentHP;
            this.MPBar.Title = ((int)(this.Player.CurrentMP)).ToString() + "/" + ((int)this.Player.CalculateStat("MP")).ToString();
            this.MPBar.MaxValue = (int)this.Player.CalculateStat("MP");
            this.MPBar.MinValue = 0;
            this.MPBar.Value = (int)this.Player.CurrentMP;
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
            Enchantment enc=new Enchantment();
            int enctype = GameObjects.RNG.Next(0, 3);
            switch(enctype)
            {
                case 0:
                    {
                        enc = new Enchantment() { Effecttext = "Fire damage +{0}%", Type = "dmg_scale_fire" };
                        enc.LineColour = new Color(255, 50, 30);
                        enc.Multiplier = 0.15f;
                        break;
                    }
                case 1:
                    {
                        enc = new Enchantment() { Effecttext = "Water resistance +{0}%", Type = "element_multiplier_water" };
                        enc.LineColour = new Color(0, 50, 200);
                        enc.Multiplier = 0.05f;
                        break;
                    }
                case 2:
                    {
                        enc = new Enchantment() { Effecttext = "Reduces poisoning duration by +{0}%", Type = "element_dot_time_poison" };
                        enc.LineColour = new Color(0, 150, 20);
                        enc.Multiplier = 0.33f;
                        break;
                    }
            } 
            eq.Enchant = enc;
            eq.SubType = GameObjects.RNG.Next(0, 10);
            if (eq.SubType == 7)
                eq.SubType = 18;
            if (eq.SubType == 8)
                eq.SubType = 27;
            if (eq.SubType == 9)
                eq.SubType = 15;
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
