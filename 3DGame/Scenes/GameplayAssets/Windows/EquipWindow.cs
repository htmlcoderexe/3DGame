using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DGame.Scenes.GameplayAssets.Windows
{
    public class EquipWindow : GUI.Window
    {
        public GameObjects.MapEntities.Actors.Player Player;
        public EquipWindow(GUI.WindowManager WM, GameObjects.MapEntities.Actors.Player Player)
        {
            this.Width = 400 + 6 + 6;
            this.Height = 400 + 6 + 16;
            this.Title = "Equipment";
            this.X = 400;
            this.Y = 400;

            GUI.Controls.TextureContainer tx = new GUI.Controls.TextureContainer(Gameplay.Textures["equipdoll"], WM);
            this.AddControl(tx);

            int y = 0;
            int x = 0;
            Point[] slots = new Point[]
            {
                new Point(314,83),
                new Point(50,83),
                new Point(175,129),
                new Point(175,244),
                new Point(104,105),
                new Point(180,346),
                new Point(89,35),
                new Point(180,10),
                new Point(270,35),
                new Point(180,74),
                new Point(314,126),
                new Point(135,74),
                new Point(230,74),
                new Point(72,190),
                new Point(288,190),

            };
            ItemSlot s;
            for(int i = 0;i<GameObjects.Items.ItemEquip.EquipSlot.Max;i++)
            {
                s = makeslot(i, Player);
                s.X = slots[i].X;
                s.Y = slots[i].Y;
                tx.AddControl(s);
            }
            
        }

        ItemSlot makeslot(int id, GameObjects.MapEntities.Actors.Player Player)
        {
            
                ItemSlot s = new ItemSlot(Player.Equipment[id]);
                s.X = 0;
                s.Y = 0;
                s.CanGrab = true;
                s.CanPut = true;
                s.BeforeItemChanged += new ItemSlot.ItemEventHandler((sender, e) =>
                {
                    GameObjects.Item item = (GameObjects.Item)(e as ItemSlot.ItemEventArgs).Item;
                    if ((item as GameObjects.Items.ItemEquip) == null)
                    {
                        e.Cancel = true;
                        if (item == null)
                        {
                            Console.Write("^FF0000 No item.");
                        }
                        else
                        {
                            List<string> ToolTip = item.GetTooltip();
                            Console.WriteEx("^BEGINLINK " + GUI.Renderer.ColourToCode(item.NameColour) + "[" + item.GetName() + "] ^ENDLINK ^FF0000 is not a suitable item.", new List<Action> { new Action(() => {GUI.ToolTipWindow tip = new GUI.ToolTipWindow(this.WM,ToolTip, WM.MouseX, WM.MouseY, false);
                WM.Add(tip); })});
                        }
                    }
                });
                s.ItemOut += new ItemSlot.ItemEventHandler((sender, e) =>
                {
                    int thisslot = id;
                    GameObjects.Items.ItemEquip item = (GameObjects.Items.ItemEquip)(e as ItemSlot.ItemEventArgs).Item;
                    List<string> ToolTip = item.GetTooltip();
                    Console.WriteEx("^BEGINLINK " + GUI.Renderer.ColourToCode(item.NameColour) + "[" + item.GetName() + "] ^ENDLINK ^FFFFFF is removed.", new List<Action> { new Action(() => {GUI.ToolTipWindow tip = new GUI.ToolTipWindow(this.WM,ToolTip, WM.MouseX, WM.MouseY, false);
                WM.Add(tip); })});
                    Player.UnequipItem(item, thisslot);
                });
                s.ItemIn += new ItemSlot.ItemEventHandler((sender, e) =>
                {
                    int thisslot = id;
                    GameObjects.Items.ItemEquip item = (GameObjects.Items.ItemEquip)(e as ItemSlot.ItemEventArgs).Item;
                    List<string> ToolTip = item.GetTooltip();
                    Console.WriteEx("^BEGINLINK " + GUI.Renderer.ColourToCode(item.NameColour) + "[" + item.GetName() + "] ^ENDLINK ^FFFFFF is equipped.", new List<Action> { new Action(() => {GUI.ToolTipWindow tip = new GUI.ToolTipWindow(this.WM,ToolTip, WM.MouseX, WM.MouseY, false);
                WM.Add(tip); })});
                    Player.EquipItem(item, thisslot);
                });

            return s;
        }
    }
}
