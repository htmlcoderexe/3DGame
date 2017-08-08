using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GUI
{
    /// <summary>
    /// Deals with displaying and interaction of windows - all GUI action happens here.
    /// </summary>
    public class WindowManager
    {
        public List<Window> Windows;
        public Window MovingWindow;
        public float MoveX;
        public float MoveY;
        public int MouseX;
        public int MouseY;
        public Vector2 Screen;
      //  public Interfaces.ITextInput FocusedText;
       // public ToolTip ToolTip;
        public float MouseStillSeconds;
        public IActionIcon MouseGrab;
        public Control LastClickedControl;
        public string NotificationText;
        public float NotificationTimeout;
        float NotificationTime = 100;
        public Renderer Renderer;
        public KeyboardState PreviousKbState { get; set; }

        public MouseState PreviousMouseState { get; set; }
        private List<Window> closedwindows;
        public WindowManager()
        {
            this.Windows = new List<Window>();
            this.closedwindows = new List<Window>();
          //  this.ToolTip = new ToolTip(); fuck tooltips!
        }
        /// <summary>
        /// Updates timers, events etc
        /// </summary>
        /// <param name="seconds">Seconds since last update. Might not be accurate.</param>
        public void Update(float seconds)
        {
            if (this.MovingWindow != null)
            {

                this.MovingWindow.X = (int)(this.MouseX - this.MoveX);
                this.MovingWindow.Y = (int)(this.MouseY - this.MoveY);

            }
            this.NotificationTimeout = Math.Max(0, this.NotificationTimeout - seconds);
            closedwindows.Clear();
            foreach (Window w in this.Windows)
            {
                if (w.Closed)
                    closedwindows.Add(w);
                else
                    w.Update(seconds);
            }
            foreach (Window w in closedwindows)
                this.Windows.Remove(w);
        }
        public void Notify(string Text, bool Override = false)
        {
            if (Override || this.NotificationTimeout == 0)
            {
                this.NotificationText = Text;
                this.NotificationTimeout = NotificationTime;

            }
        }
        public void UpdateAnchors(int dX, int dY)
        {
            foreach(Window w in this.Windows)
            {
                if (w.AnchorRight)
                    w.X += dX;
                if (w.AnchorBottom)
                    w.Y += dY;
            }
        }
        public void ScreenResized(int W, int H)
        {
            int dX = W - (int)this.Screen.X;
            int dY = H - (int)this.Screen.Y;
            this.Screen.X = W;
            this.Screen.Y = H;
            UpdateAnchors(dX, dY);
        }
        public void Render(GraphicsDevice device)
        {
            foreach (Window Window in this.Windows)
            {
                if(Window.Visible)
                Window.Render(device, Renderer, 0, 0);
            }
            /*
            if (this.ToolTip != null)
            {
                if (MouseStillSeconds >= 70f)
                {
                    if (this.ToolTip.Visible == false)
                    {
                        this.ToolTip.X = this.MouseX;
                        this.ToolTip.Y = this.MouseY;
                        this.ToolTip.Visible = true;
                    }
                    this.ToolTip.Render(device);
                }
                else
                {
                    this.ToolTip.Visible = false;
                }
            }

            //*/
            if (this.MouseGrab != null)
            {
                this.MouseGrab.Render(MouseX - 16, MouseY - 16,device, this.Renderer, false,false);

            }
            if (this.NotificationText != null && this.NotificationTimeout > 0)
            {
                int CX = (int)((float)device.Viewport.Width / 2f);
                int CY = (int)((float)device.Viewport.Height / 2f);
                float scale = NotificationTime / 2.0f;
                float A = Math.Min(1.0f, this.NotificationTimeout / this.NotificationTime);
                Color c = new Color(1.0f, 1.0f, 0.9f, A);
              //TODO  GUIDraw.RenderBigText(device, CX, CY, this.NotificationText, c, true);
            }
        }
        public void Add(Window Window)
        {
            Window.WM = this;
            this.Windows.Add(Window);
        }
        public bool HandleMouse(MouseState Mouse, float dT)
        {
            int stillness = (Math.Abs(Mouse.X - PreviousMouseState.X)) + (Math.Abs(Mouse.Y - PreviousMouseState.Y));
            int mousethreshold = 1;
            if (stillness > mousethreshold)
            {
                MouseStillSeconds = 0;
            }
            else
            {
                MouseStillSeconds+=dT;
            }
            Window Window = GetWindow(Mouse.X, Mouse.Y);
            if (Window == null)
            {
                if (this.MovingWindow != null)
                {

                    PreviousMouseState = Mouse;
                    return true;
                }
                if (this.MouseGrab != null)
                {

                    PreviousMouseState = Mouse;
                    return true;
                }
                PreviousMouseState = Mouse;
                return false;
            }
            Window.MouseMove(MouseX - Window.X, MouseY - Window.Y);
            bool MouseIsDown = Mouse.LeftButton == ButtonState.Pressed;
            if (MouseIsDown)
            {
                if (PreviousMouseState.LeftButton== ButtonState.Released) //the mouse was up last time
                {
                    Window.MouseDown(MouseX - Window.X, MouseY - Window.Y);
                }

            }
            else
            {
                if (PreviousMouseState.LeftButton == ButtonState.Pressed) //the mouse was down last time
                {
                    Window.MouseUp(MouseX - Window.X, MouseY - Window.Y);
                }
                if (this.MovingWindow != null)
                    this.MovingWindow = null;

            }
            PreviousMouseState = Mouse;

            return true;
        }
        public void Click(Window Window, float X, float Y)
        {

            // this.Windows[idxc] = tmp;
            Window.Click(X - Window.X, Y - Window.Y);
            MoveX = X - Window.X;
            MoveY = Y - Window.Y;

        }
        public void Top(Window Window)
        {
            Window tmp;
            int idxc = this.Windows.LastIndexOf(Window);
            tmp = this.Windows[idxc];
            for (int i = idxc; i < this.Windows.Count - 1; i++)
            {
                this.Windows[i] = this.Windows[i + 1];
            }
            this.Windows[this.Windows.Count - 1] = Window;
        }
        
        public void MouseMove(float X, float Y)
        {
            Window Window = this.GetWindow(X, Y);
            /*
            if (Window == null)
            {
                this.ToolTip = null;
                return;
            }
        //*/
            Window.MouseMove(X - Window.X, Y - Window.Y);
        }
        public Window GetWindow(float X, float Y)
        {
            Window wnd = null;

            for (int i = this.Windows.Count - 1; i >= 0; i--)
            {
                if (this.Windows[i].CheckCollision(X, Y))
                    return this.Windows[i];
            }

            return wnd;
        }
    }

    public static class WindowSettings
    {
        public static Color WindowColour = new Color(102, 102, 202);

    }

}
