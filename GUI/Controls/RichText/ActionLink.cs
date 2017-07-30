using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.Controls.RichText
{
    public class ActionLink
    {
        public Action ClickHandler;
        public int LineID;
        public int LinkStart;
        public int LinkEnd;
        public void Click()
        {
            if (this.ClickHandler != null)
                this.ClickHandler();
        }
        public bool Check(int X)
        {
            return (X > LinkStart && X < LinkEnd);
        }
    }
}
