using GameObject;
using GameObject.AbilityLogic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MagicEditor
{
    public partial class Form1 : Form
    {
        ModularAbility CurrentAbility;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            AbilityEffectDefinition.LoadDefinitions();
            CurrentAbility = new TestAbility();

            EffectiveAbility a = CurrentAbility.GetEffectiveAbility();

            foreach(ITimedEffect effect in a.EffectTimeline.GetList())
            {
                EffectList.Items.Add(effect.EffectType);

            }
            AbilityEffectDefinition d = AbilityEffectDefinition.Definitions.Values.First();
            CurrentAbility = new ModularAbility();
        }
    }
}
