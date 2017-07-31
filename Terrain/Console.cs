using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terrain
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

        public static System.Action<string, List<System.Action>> WriteCallbackEx;
        public static void WriteEx(string Text,List<System.Action> Links)
        {
            if (WriteCallbackEx == null)
                return;
            WriteCallbackEx(Text,Links);
        }
    }
}
