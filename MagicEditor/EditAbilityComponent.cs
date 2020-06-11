using GameObject.AbilityLogic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MagicEditor
{
    public partial class EditAbilityComponent : Form
    {
        List<Control> Inputs = new List<Control>();
        ITimedEffect CurrentEffect;
        public EditAbilityComponent(ITimedEffect effect)
        {
            InitializeComponent();
            CurrentEffect = effect;
            AbilityEffectDefinition aed = AbilityEffectDefinition.GetDefinition(effect.EffectType);
            this.label1.Text = aed.FriendlyName;
            basetimevalue.Value = (decimal)CurrentEffect.BaseTime;
            basedurationvalue.Value = (decimal)CurrentEffect.BaseDuration;
            deltatimevalue.Value = (decimal)CurrentEffect.DeltaTime;
            deltadurationvalue.Value = (decimal)CurrentEffect.DeltaDuration;
            desclabel.Text = aed.Description;   
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

                Control t1 = CraftInput(basevalue, ptype);
                t1.Width = 64;
                t1.Height = 2;
                t1.Location = new Point(320, 160 + 80 * i);
                this.Controls.Add(t1);
                this.Inputs.Add(t1);
                Control t2 = CraftInput(deltavalue, ptype);
                t2.Width = 64;
                t2.Height = 2;
                t2.Location = new Point(460, 160 + 80 * i);
                this.Controls.Add(t2);
                this.Inputs.Add(t2);

            }
            this.SuspendLayout();
            //add controls here!!


            this.ResumeLayout(false);
            this.PerformLayout();

        }

        Control CraftInput(string value, char type)
        {
            //pre-convert value to make code more readable later on:

            int.TryParse(value, out int intValue);
            float.TryParse(value, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture,out float floatValue);
            float tenthsValue = (float)((int)(floatValue * 10f)) / 10f; //not sure if necessary but I don't trust floats enough

            //following types currently supported:
            //% - percentages, input int, works like an int but gets divided by 100 in game mechanics
            //n - whole numbers, input int
            //t - tenths, input float - truncate to 0.0 both ways, no other conversion
            //c - colours, in format RRR:GGG:BBB (0..255), input as string, validate on change. Possible colour picker eventually?
            //f - floating point number, no conversion except float.TryParse followed by ToString
            //s - strings, taken raw.

            switch(type)
            {
                case '%':
                    {
                        return new NumericUpDown
                        {
                            Minimum = -10000,
                            Maximum = 10000,
                            Value = intValue
                        };
                    }
                case 'n':
                    {
                        return new NumericUpDown
                        {
                            Minimum = -1000000,
                            Maximum = 1000000,
                            Value = intValue
                        };
                    }
                case 't':
                    {
                        return new NumericUpDown
                        {
                            Minimum = -1000,
                            Maximum = 1000,
                            DecimalPlaces= 1,
                            Value = (decimal)tenthsValue
                        };
                    }
                case 's':
                case 'c':
                    {
                        return new TextBox
                        {
                            Text = value
                        };
                    }
                case 'f':
                    {
                        return new NumericUpDown
                        {
                            Minimum = -1000,
                            Maximum = 1000,
                            DecimalPlaces = 3, //wayyy more than enough!
                            Value = (decimal)floatValue
                        };
                    }
                default:
                    {
                        return new TextBox
                        {
                            Text = value
                        };
                    }
            }
        }

        public void SaveAndQuit()
        {
            AbilityEffectDefinition aed = AbilityEffectDefinition.GetDefinition(CurrentEffect.EffectType);
            this.label1.Text = aed.FriendlyName;
            string[] values = new string[aed.parameters.Count];

            for (int i = 0; i < aed.parameters.Count; i++)
            {
                string name = aed.parameters[i].Item1;
                char ptype = aed.parameters[i].Item2;

                values[i] = GetValue(Inputs[i * 2]) + "," + GetValue(Inputs[i * 2 + 1]);

            }
            CurrentEffect.SetParamValues(values);

            CurrentEffect.BaseTime = (float)basetimevalue.Value;
            CurrentEffect.DeltaTime = (float)deltatimevalue.Value;
            CurrentEffect.BaseDuration = (float)basedurationvalue.Value;
            CurrentEffect.DeltaDuration = (float)deltadurationvalue.Value;

            this.Close();
        }

        public string GetValue(Control input)
        {
            if (input is TextBox tb)
                return tb.Text;
            if (input is NumericUpDown nud)
            {
                NumberFormatInfo dotdecimal = new NumberFormatInfo();
                dotdecimal.NumberDecimalSeparator = ".";
                dotdecimal.NumberGroupSeparator = "";
                return nud.Value.ToString(dotdecimal);
            }
                
            return "0";

        }

        private void okSave_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            SaveAndQuit();
        }

        private void cancelbutt_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
