using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.Controls
{
    public class TabbedView : Control
    {
        List<Tuple<string, Container>> Tabs = new List<Tuple<string, Container>>();
        List<Tuple<Rectangle, Container>> ClickAreas= new List<Tuple<Rectangle, Container>>();
        int TabOffset = 0;
        //3px padding around tab text
        int pad = 3;
        public int ActiveIndex;
        void RegenerateTabs()
        {
            //reset/create list of rectangle targets
            ClickAreas = new List<Tuple<Rectangle, Container>>();
            //wipe subcontrols
            this.Controls.Clear();
            //start in 0,0 of the control
            int XOffset = 0;
            int YOffset = 0;
            //offset the tabs by textheight + 2px pad around
            int YStride = (int)WM.Renderer.UIFont.MeasureString(" ").Y + pad + pad; 
            //go thru each tab text
            for(int i=0;i<Tabs.Count;i++)
            {
                //get tab text
                string aa = Tabs[i].Item1;
                //calculate tab width (text width + padding both sides)
                int w =(int)GUI.GFXUtility.StrW(aa, WM.Renderer.UIFont)+pad+pad;
                //if tab would go outside control's border, wrap to next tab row
                if(XOffset+w>this.Width)
                {
                    //tab origin offset by tab height
                    YOffset += YStride;
                    //reset horisontal offset back to 0
                    XOffset = 0;
                }
                //add a rectangle with origin in x/y offset, width of text+ padding and height of text + padding
                ClickAreas.Add(new Tuple<Rectangle, Container>(new Rectangle(XOffset, YOffset, w, YStride), Tabs[i].Item2));
                //add container as child control
                AddControl(Tabs[i].Item2);
                //advance the tab origin
                XOffset += w;
            }
            //set control offset to bottom of lowest tab
            TabOffset = YOffset + YStride;
        }

        public void AddTab(string Name, List<Control> Controls, bool ReloadTabs=true)
        {
            Container c = new Container();
            foreach (Control cc in Controls)
                c.AddControl(cc);
            this.Tabs.Add(new Tuple<string, Container>(Name, c));

            if (ReloadTabs)
                RegenerateTabs();
        }

        public void SetActiveTab(int Index)
        {
            ActiveIndex = Index;
            for(int i=0;i<ClickAreas.Count;i++)
            {
                if(i==ActiveIndex)
                {
                    ClickAreas[i].Item2.Visible = true;
                    //lazily set the Y offset here so it draws nicely after tabs
                    ClickAreas[i].Item2.Y = TabOffset;
                    ClickAreas[i].Item2.Width = Width;
                    ClickAreas[i].Item2.Height = Height-TabOffset;
                }
                else
                {
                    ClickAreas[i].Item2.Visible = false;
                }
            }
        }
        public override void Click(float X, float Y)
        {
            //go thru each rectange until active found
            for(int i=0;i<ClickAreas.Count;i++)
            {
                if(ClickAreas[i].Item1.Contains(X,Y))
                {
                    SetActiveTab(i);
                }
            }
            base.Click(X, Y);
        }
        public override void Render(GraphicsDevice device, Renderer Renderer, int X, int Y)
        {
            //draw tabs
            for(int i=0;i<Tabs.Count;i++)
            {
                bool a = i == ActiveIndex;
                Renderer.Slice9 tabframe = new Renderer.Slice9(0, 80, 48, 16, pad);
                Rectangle r = ClickAreas[i].Item1;
                Renderer.RenderFrame(device, X + this.X + r.X, Y + this.Y + r.Y, r.Width, r.Height, tabframe);
                Renderer.RenderSmallText(device, X + this.X + r.X + pad, Y + this.Y + r.Y + pad, Tabs[i].Item1, a ? Color.Yellow : Color.White);
            }

            base.Render(device, Renderer, X, Y);
        }

    }
}
