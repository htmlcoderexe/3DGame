using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GUI.Controls;

namespace GUI.Windows
{
    public class MessageBox : ModalWindow
    {
        public enum ButtonOptions
        {
            OKOnly,OKCancel,YesNo
        }

        public MessageBox(Window Owner, string Title, string Text="", ButtonOptions options= ButtonOptions.OKOnly)
        {
            this.Title = Title;
            this.Owner = Owner;
            this.Width = 280;
            this.Height = 180;
            RichTextDisplay text = new RichTextDisplay(this.Width, 120, Owner.WM);
            text.SetText(Text);
            this.Controls.Add(text);
            switch(options)
            {
                case ButtonOptions.OKCancel:
                    {

                        Button okb = new Button("OK")
                        {
                            Width = 80,
                            Height = 20,
                            Y = 120,
                            X = (Width - 165) / 2
                        };

                        okb.OnClick += Okb_OnClick;
                        AddControl(okb);
                        Button cancelb = new Button("Cancel")
                        {
                            Width = 80,
                            Height = 20,
                            Y = 120,
                            X = (Width - 165) / 2 + 80
                        };

                        cancelb.OnClick += Cancelb_OnClick;
                        AddControl(cancelb);

                        break;


                    }
                default: //OKOnly
                    {
                        Button okb = new Button("OK")
                        {
                            Width = 60,
                            Height = 20,
                            Y = 120,
                            X = (Width - 60) / 2
                        };

                        okb.OnClick += Okb_OnClick;
                        AddControl(okb);

                        break;
                    }
            }
        }

        private void Cancelb_OnClick(object sender, ClickEventArgs eventArgs)
        {
            Exit(DialogResult.Cancel);
        }

        private void Okb_OnClick(object sender, ClickEventArgs eventArgs)
        {
            Exit(DialogResult.OK);
        }
    }
}
