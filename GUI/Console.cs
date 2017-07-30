using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI
{
    public abstract class Console
    {
        public static System.Action<string> WriteCallback;
        public static void Write(string Text)
        {
            if (WriteCallback == null)
                return;
            WriteCallback(Text);
        }
    }
}
