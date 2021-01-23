using GameObject.Interactions;
using GameObject.Interactions.NPCCommands;
using GUI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DGame.Scenes.GameplayAssets.Windows
{
    //basically reskinned button
    public class NPCMenuItem : Control
    {
        bool Hot = false;
        bool md = false;
        public Color Colour = new Color(102, 25, 0);
        public Color HotColour = new Color(173,43,0);   
        void OpenShop(NPCShop shop)
        {
            ShopWindow w = new ShopWindow(shop);
            Window p = this.GetParentWindow();
            if(p!=null)
            {
                w.X = p.X;
                w.Y = p.Y;
                p.Close();
            }
            WM.Add(w);
            w.Visible = true;
        }
        //Exit option at the end of any menu
        public static NPCMenuItem Close()
        {
            NPCMenuItem close = new NPCMenuItem("Exit");
            close.OnClick += new ClickEventHandler((sender, m) => close.GetParentWindow()?.Close());
            return close;
        }

        public NPCMenuItem(string Label)
        {
            this.Title = Label;
            this.Height = 20;
            this.Width = 300;

        }

        public NPCMenuItem(NPCCommand command)
        {
            this.Title = command.Label;
            this.Height = 20;
            this.Width = 300;
            if (command is OpenShop shop)
            {
                this.OnClick += new ClickEventHandler((sender, m) => OpenShop(shop.Shop));
            }
        }

        public override void MouseMove(float X, float Y)
        {
            this.Hot = true;
            base.MouseMove(X, Y);
        }
        public override void MouseDown(float X, float Y)
        {
            this.md = true;
            base.MouseDown(X, Y);
        }
        public override void MouseUp(float X, float Y)
        {
            this.md = false;
            base.MouseUp(X, Y);
        }
        public override void Render(GraphicsDevice device, Renderer Renderer, int X, int Y)
        {

            float textwidth = GFXUtility.StrW(this.Title, this.WM.Renderer.UIFont);
            float offsetX = 3f;
            float offsetY = (this.Height - 16f) / 2f;
            Renderer.SetColour(Hot ? HotColour : Colour);
            Renderer.RenderBar(device, X+this.X, Y+this.Y, this.Width, this.Height, 1f, 2);
            Renderer.SetColour(Color.Gray);
            Renderer.RenderSmallText(device, offsetX + X + this.X, offsetY + Y + this.Y, this.Title, Microsoft.Xna.Framework.Color.White, false, true);
            this.Hot = false;
        }
    }
}
