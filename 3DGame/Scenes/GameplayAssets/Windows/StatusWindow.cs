using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameObject.Items;
using GUI;
using Microsoft.Xna.Framework;

namespace _3DGame.Scenes.GameplayAssets
{
    public class StatusWindow : GUI.Window
    {
        public GUI.Controls.Button OKButton;
        public GameplayAssets.ItemSlot slot;
        public GameObject.MapEntities.Actors.Player Player;
        private GUI.Controls.ProgressBar HPBar;
        private GUI.Controls.ProgressBar MPBar;
        private GUI.Controls.ProgressBar EXPBar;
        public override void Close()
        {
            
        }
        public StatusWindow(WindowManager WM, GameObject.MapEntities.Actors.Player Player)
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

            int lvl, target, leftover;
            lvl = Player.CalculateLvl(Player.EXP);
            target = Player.Exp4Level(lvl + 1);
            leftover = Player.EXP - Player.Total4Level(lvl);

            this.EXPBar.Title = leftover.ToString() + "/" + target.ToString();
            this.EXPBar.MaxValue = target;
            this.EXPBar.MinValue = 0;
            this.EXPBar.Value = leftover;


        }

        GameObject.Item MakeRandomMat()
        {
            GameObject.Item Item;
            Item = GameObject.Items.Material.MaterialTemplates.GetRandomMaterial();
            Item.SubType = GameObject.RNG.Next(GameObject.Items.Material.MaterialType.Max);
            Item.Description = "A " + GameObject.Items.Material.MaterialType.GetTypeName(Item.SubType) + " made from " + Item.Name + ". Used in crafting equipment.";
            return Item;
        }

        GameObject.Item MakeRandomEquip()
        {
            ItemEquip eq = new ItemEquip();
            BonusPool p = BonusPool.Load("heavy_0_10");
            eq.Bonuses.Add(p.PickBonus());
            eq.Bonuses.Add(p.PickBonus());
            Enchantment enc = new Enchantment();
            int enctype = GameObject.RNG.Next(0, 3);
            switch (enctype)
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
            eq.SubType = GameObject.RNG.Next(0, 10);
            if (eq.SubType == 7)
                eq.SubType = 18;
            if (eq.SubType == 8)
                eq.SubType = 27;
            if (eq.SubType == 9)
                eq.SubType = 15;
            eq.PrimaryMaterial = Material.MaterialTemplates.GetRandomMaterial();
            eq.SecondaryMaterial = Material.MaterialTemplates.GetRandomMaterial();
            return eq;
        }

        GameObject.Item MakeRandomGem()
        {
            int Level = GameObject.RNG.Next(8);
            string stat = ItemGemstone.StatGemColours.Keys.ToList()[GameObject.RNG.Next(ItemGemstone.StatGemColours.Keys.Count())];
            ItemGemstone gem = new ItemGemstone(stat,Level+1);

            return gem;
        }

        private void OKButton_Clicked(object sender, EventArgs e)
        {
            // this.slot.Item = Item;

            GUI.Windows.MessageBox mb = new GUI.Windows.MessageBox(this, "MessageBox test", "This is a test", GUI.Windows.MessageBox.ButtonOptions.OKCancel);
            //so modals don't quite work like in winforms but should do the trick in our case:
            //an active modal window prevents the mouse from doing anything except in the modal window,
            //effectively ensuring the code in the callback attached to the event that fires when
            //the window is closed by any means is executed before any other GUI actions can happen
            //for example, a callback for a yes/no box may check the result and apply an action to its owner window
            //such as proceeding/not proceeding with an action or making a choice
            //here we simply change the title of this window based on which button was clicked
            //a different modal window may ask for a text input or for a number
            mb.ModalWindowClosed += Mb_ModalWindowClosed;
            //lambdas are just as good
            //mb.ModalWindowClosed+= new ModalWindow.ModalWindowClosedHandler((mbx,result,owner)=> owner.Title = result == ModalWindow.DialogResult.OK ? "Clicked OK" : "Clicked Cancel");
            //this shows the modal, this MUST be the last line in the calling method unless you know what you're doing
            WM.Add(mb);
            //anything after this WILL execute BEFORE the modal is closed.
        }

        private void Mb_ModalWindowClosed(object sender, ModalWindow.DialogResult result, Window owner)
        {
            if(result == ModalWindow.DialogResult.OK)
            {
                GameObject.Item Item = MakeRandomMat();

                Item = MakeRandomEquip();
                Item = MakeRandomGem();
                List<string> ToolTip = Item.GetTooltip();
                this.slot.Item = Item;
                Console.WriteEx("New item is ^BEGINLINK " + Renderer.ColourToCode(Item.NameColour) + "[" + Item.GetName() + "] ^ENDLINK .^FFFFFF Click name to see more.", new List<Action> { new Action(() => {ToolTipWindow tip = new ToolTipWindow(this.WM,ToolTip, WM.MouseX, WM.MouseY, false);
                WM.Add(tip); })});
            }
        }
    }
}
