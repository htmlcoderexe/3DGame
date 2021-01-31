using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI
{
    public interface ITextInput
    {
        bool MultiLine { get; set; }
        void SendCharacter(char Character);
        void Submit();
    }
}
