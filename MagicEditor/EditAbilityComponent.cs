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
    public partial class EditAbilityComponent : Form
    {
        List<Control> Inputs = new List<Control>();
        public EditAbilityComponent(ITimedEffect effect)
        {
            InitializeComponent();
            AbilityEffectDefinition aed = AbilityEffectDefinition.GetDefinition(effect.EffectType);
            this.label1.Text = aed.FriendlyName;
            string[] values = effect.GetParamValues();

            for(int i=0;i<aed.parameters.Count;i++)
            {
                string name = aed.parameters[i].Item1;
                char ptype = aed.parameters[i].Item2;
                string[] bothvalues = values[i].Split(',');
                string basevalue = bothvalues[0];
                string deltavalue = bothvalues.Length==2?bothvalues[1]:"";

                Label l = new Label();
                l.Text = name;
                l.AutoSize = true;
                l.Location = new Point(120, 160 + 80 * i);
                this.Controls.Add(l);

                TextBox t1 = new TextBox();
                t1.Width = 64;
                t1.Height = 2;
                t1.Location = new Point(320, 160 + 80 * i);
                t1.Text = basevalue;
                this.Controls.Add(t1);
                this.Inputs.Add(t1);
                TextBox t2 = new TextBox();
                t2.Width = 64;
                t2.Height = 2;
                t2.Location = new Point(460, 160 + 80 * i);
                t2.Text = basevalue;
                this.Controls.Add(t2);
                this.Inputs.Add(t2);

            }
            this.SuspendLayout();
            //add controls here!!


            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
