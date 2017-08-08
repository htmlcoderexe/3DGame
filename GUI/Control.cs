using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI
{
    /// <summary>
    /// Base Control class. Handles rendering and mouse events.
    /// </summary>
    public abstract class Control
    {
        List<Control> _controls;
        WindowManager _wm;
        /// <summary>
        /// If control is not visible, it loses collision and is not rendered.
        /// </summary>
        public bool Visible { get; set; }
        /// <summary>
        /// The window manager this control is running on.
        /// Used to notify the WM about focus and events.
        /// </summary>
        public WindowManager WM
        {
            get
            {

                return _wm;
            }


            set
            {
                _wm = value;
                //assigns all the child controls. Recursvie.
                if (Controls.Count != 0)
                {
                    foreach (Control c in this.Controls)
                    {
                        c.WM = value;
                    }
                }
            }
        }
        /// <summary>
        /// Child controls of this control.
        /// </summary>
        public List<Control> Controls
        {
            get
            {
                //lazy initialization, ho!
                if (_controls == null)
                {
                    _controls = new List<Control>();
                }
                return _controls;
            }


            set
            {
                _controls = value;
            }
        }
        /// <summary>
        /// Wrapper method for adding controls. Handles WM parenting.
        /// </summary>
        /// <param name="c">The control to add.</param>
        public void AddControl(Control c)
        {
            if (this.Controls == null)
                this.Controls = new List<Control>();
            c.Parent = (Control)this;
            c.WM = this.WM;
            this.Controls.Add(c);
        }
        /// <summary>
        /// Generic Constructor.
        /// </summary>
        public Control()
        {
            this.Controls = new List<Control>();
            this.Visible = true;
        }
        /// <summary>
        /// Parent control - used to propagate events.
        /// Can be null for top level controls (mostly windows).
        /// </summary>
        public Control Parent { get; set; }
        /// <summary>
        /// Position from left in pixels.
        /// </summary>
        public virtual int X { get; set; }
        /// <summary>
        /// Position from top in pixels.
        /// </summary>
        public virtual int Y { get; set; }
        /// <summary>
        /// Width in pixels.
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// Height in pixels.
        /// </summary>
        public int Height { get; set; }
        /// <summary>
        /// The title of the control.
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Dummy method to override for click events.
        /// </summary>
        /// <param name="X">X</param>
        /// <param name="Y">Y</param>
        public virtual void Click(float X, float Y)
        {
            

        }
        /// <summary>
        /// TODO: Right clicking behaviour.
        /// </summary>
        public virtual void RightClick()
        {


        }
        /// <summary>
        /// Override to define mouse down behaviour. Fires once and won't fire until mouse button has been released and pressed again.
        /// </summary>
        /// <param name="X">X</param>
        /// <param name="Y">Y</param>
        public virtual void MouseDown(float X, float Y)
        {
            //  Volatile.Console.Write("mousedown event - " + X.ToString() + "," + Y.ToString() + "@" + this.Title);
            this.WM.LastClickedControl = this;
            foreach (Control c in this.Controls)
            {
                if (c.CheckCollision(X, Y))
                {

                    c.MouseDown(X - c.X, Y - c.Y);


                    break;
                }
            }

        }
        /// <summary>
        /// Override to define mouse up behaviour. Fires once per mouse release.
        /// </summary>
        /// <param name="X">X</param>
        /// <param name="Y">Y</param>
        public virtual void MouseUp(float X, float Y)
        {
            //  Volatile.Console.Write("mouseup event - " + X.ToString() + "," + Y.ToString() + "@" + this.Title);

            //this more or less emulates Windows behaviour
            //you can mousedown, move out and back in again and releasing will click the control
            //otherwise mouseup wastes the click
            if (this.WM.LastClickedControl != null && this.WM.LastClickedControl == this)
            {

                this.Click(X, Y);
            }
            foreach (Control c in this.Controls)
            {
                if (c.CheckCollision(X, Y))
                {
                    c.MouseUp(X - c.X, Y - c.Y);


                    break;
                }
            }
            this.WM.LastClickedControl = null;

        }
        /// <summary>
        /// Override to define mouse move behaviour. Fires as long as mouse collides with the control.
        /// </summary>
        /// <param name="X">X</param>
        /// <param name="Y">Y</param>
        public virtual void MouseMove(float X, float Y)
        {
            foreach (Control c in this.Controls)
            {
                if (c.CheckCollision(X, Y))
                {

                    c.MouseMove(X - c.X, Y - c.Y);


                    break;
                }
            }

        }
        /// <summary>
        /// Draws the control. Base behaviour propagates the coordinates 
        /// so that the controls are positioned relative to parent.
        /// </summary>
        /// <param name="device">GraphicsDevice that the control is rendered to.</param>
        /// <param name="X">X in pixels.</param>
        /// <param name="Y">Y in pixels.</param>
        public virtual void Render(Microsoft.Xna.Framework.Graphics.GraphicsDevice device, Renderer Renderer, int X, int Y)
        {
            
            foreach (Control c in this.Controls)
            {
                if(c.Visible)
                c.Render(device, Renderer, this.X + X, this.Y + Y);
            }

        }
        /// <summary>
        /// Checks whether a set of coordinates is within this control's area.
        /// </summary>
        /// <param name="X">X in pixels.</param>
        /// <param name="Y">Y in pixels.</param>
        /// <returns></returns>
        public bool CheckCollision(float X, float Y)
        {
            bool r = false;
            if (this.Visible && X < this.X + Width && X > this.X && Y < this.Y + this.Height && Y > this.Y)
                r = true;

            return r;


        }
    }

}
