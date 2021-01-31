using GUI.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.Windows
{
    public class TextPrompt : ModalWindow
    {
        public string TextResult = "";
        TextBox box;
        public TextPrompt(Window Owner, string Title, string Text = "", string DefaultValue="")
        {
            this.Title = Title;
            this.Owner = Owner;
            this.Width = 280;
            this.Height = 180;
            this.TextResult = DefaultValue;
            int textheight = 100;
            RichTextDisplay text = new RichTextDisplay(this.Width, textheight, Owner.WM);
            text.SetText(Text);
            this.Controls.Add(text);
            box = new TextBox();
            box.Width = this.Width - 10;
            box.X = 0;
            box.Height = 20;
            box.Y = textheight + 5;
            box.Text = DefaultValue;
            AddControl(box);
            Button okb = new Button("OK")
            {
                Width = 80,
                Height = 20,
                Y = textheight + 5+20+5,
                X = (Width - 165) / 2
            };

            okb.OnClick += Okb_OnClick;
            AddControl(okb);
            Button cancelb = new Button("Cancel")
            {
                Width = 80,
                Height = 20,
                Y = textheight + 5 + 20 + 5,
                X = (Width - 165) / 2 + 75
            };

            cancelb.OnClick += Cancelb_OnClick;
            AddControl(cancelb);
        }



        private void Cancelb_OnClick(object sender, ClickEventArgs eventArgs)
        {
            Exit(DialogResult.Cancel);
        }

        private void Okb_OnClick(object sender, ClickEventArgs eventArgs)
        {
            this.TextResult = box.Text;
            Exit(DialogResult.OK);
        }
    }
}
