using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GUI
{
    public class Window : Control//, Interfaces.IWindow
    {


        private bool CloseHot;
        public Rectangle Margin;

        public override void MouseDown(float X, float Y)
        {
            WM.Top(this);
            if (Y < 16)
            {
                if (X < (this.Width - 16))
                {
                    WM.MoveX = X;
                    WM.MoveY = Y;
                    
                    WM.MovingWindow = this;
                }
                else
                {
                    //  this.CloseButton.Action();
                }

            }
            else
            {
                Y -= 16;

                base.MouseDown(X - Margin.X, Y - Margin.Y);
            }
        }

        public override void MouseUp(float X, float Y)
        {
            if (Y < 16)
            {
                if (X < (this.Width - 16))
                {
                    WM.MovingWindow = null;
                }
                else
                {
                    //  this.CloseButton.Action();
                }

            }
            else
            {
                

                base.MouseUp(X-Margin.X, Y-Margin.Y);
            }

        }

        public override void Click(float X, float Y)
        {
            if (Y < 16)
            {
                if (X < (this.Width - 16))
                {
                    WM.MovingWindow = this;
                }
                else
                {
                    //  this.CloseButton.Action();
                }

            }
            else
            {

                base.Click(X - Margin.X, Y - Margin.Y);
            }


        }

        public override void RightClick()
        {
            //throw new NotImplementedException();
        }

        public override void MouseMove(float X, float Y)
        {
            if (Y < 16)
            {
                if (X < (this.Width - 16))
                {
                    this.CloseHot = false;
                }
                else
                {
                    this.CloseHot = true;
                }

            }
            else
            {
                this.CloseHot = false;
                base.MouseMove(X - Margin.X, Y - Margin.Y); 
            }
        }

        public override void Render(GraphicsDevice device, Renderer Renderer, int X, int Y)
        {
            Renderer.RenderFrame(device, this.X, this.Y, this.Width, this.Height);
            Renderer.RenderSmallText(device, this.X + 5, this.Y, this.Title, Color.White);
            Renderer.RenderCloseButton(device, this.X + this.Width - 16, this.Y, this.CloseHot);
            base.Render(device, Renderer, Margin.X+X, Margin.Y+Y);
        }

        public Window()
        {
            this.Controls = new List<Control>();
            this.Title = "GUIWindow";
            this.Margin = new Rectangle(3+3, 16 + 3, 3+3, 3+3);
           // this.WM.GetType();
        }
    }

}
