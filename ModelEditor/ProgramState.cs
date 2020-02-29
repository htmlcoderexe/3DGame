using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelEditor
{
    public class ProgramState
    {
        public SettingsContainer Settings;
        public GameModel.Model CurrentModel;
        public float PlayTime;
        public bool Playing;
        


        public static ProgramState State = new ProgramState();
    }
}
