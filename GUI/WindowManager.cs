using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI
{
    public class WindowManager
    {
        internal Control LastClickedControl;

        public float MoveX { get; internal set; }
        public float MoveY { get; internal set; }
        public Window MovingWindow { get; internal set; }

        internal void Top(Window window)
        {
            throw new NotImplementedException();
        }
    }
}
