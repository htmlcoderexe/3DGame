using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace GUI.Controls.RichText
{
    public class RichTextMultiLine
    {
        public List<string> Lines;
        public int MaxCount = 10;
        public int MaxWidth = 128;
        private SpriteFont Font;
        private string LastControlWord = "";
        public RichTextMultiLine()
        {
            this.Lines = new List<string>();
            this.Lines.Add("Test ^FF0000 Red string");
            this.Lines.Add("Test ^00FF00 Green string");
            this.Lines.Add("Test ^0000FF Blue string");
        }
        private void AddLine(string Line)
        {
            this.Lines.Add(Line);
            if (this.Lines.Count > MaxCount)
                this.Lines.RemoveAt(0);
        }
        private void Clear()
        {
            this.Lines.Clear();
            this.LastControlWord = "";
        }
        public void AppendText(string Text, bool ContinueLine = true)
        {

            string[] l = Text.Split(' ');
            string CurrentWord = "";
            string CurrentLine = "";
            float Xoffset = 0;
            if (ContinueLine)
            {
                CurrentLine = Lines.Last();
                Lines.Remove(CurrentLine);
                Regex regex = new Regex(@"\^\w+\s");
                string CurrentLinePure = regex.Replace(CurrentLine, "");
                Xoffset = GFXUtility.StrW(CurrentLinePure + " ", Font);
            }
            float CurrentLength;
            float spacewidth = GFXUtility.StrW(" ", Font);
            for (int i = 0; i < l.Length; i++)
            {
                CurrentWord = l[i];
                if (CurrentWord == "")
                    continue;
                //control words start with ^ and are never displayed
                if (CurrentWord[0] == '^')
                {
                    CurrentLine += CurrentWord + " ";
                    LastControlWord = CurrentWord;
                    continue;
                }
                CurrentLength = GFXUtility.StrW(CurrentWord + " ", Font);
                if ((Xoffset + CurrentLength) < MaxWidth + spacewidth)
                {
                    CurrentLine += CurrentWord + " ";
                    Xoffset += CurrentLength;
                }
                else
                {
                    AddLine(CurrentLine);
                    CurrentLine = "";
                    if (LastControlWord != "")
                        CurrentLine += LastControlWord + " ";
                    CurrentLine += CurrentWord + " ";
                    Xoffset = CurrentLength;

                }
            }
            AddLine(CurrentLine);
        }
        public RichTextMultiLine(string Text, SpriteFont Font, int MaxWidth)
        {
            this.Lines = new List<string>();
            this.MaxWidth = MaxWidth;
            this.Font = Font;
            this.AppendText(Text, false);
        }
        public void SetText(string Text)
        {
            this.Clear();
            this.AppendText(Text, false);
        }
    }
}
