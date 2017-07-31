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
        public List<ActionLink> Links;
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
            while (this.Lines.Count > MaxCount)
            {
                foreach(ActionLink l in this.Links)
                {
                    l.LineID--;
                }
                this.Lines.RemoveAt(0);
            }
            
        }
        private void Clear()
        {
            this.Lines.Clear();
            this.LastControlWord = "";
        }
        public void AppendMultiline(string Text,List<System.Action> LinkAction=null)
        {
            string[] ss = Text.Split('\n');
            foreach(string s in ss)
            {
                this.AppendText(s, false, LinkAction);
            }
        }
        public void AppendText(string Text, bool ContinueLine = false, List<System.Action> LinkAction=null)
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
            else
            {
                LastControlWord = "";
            }
            float CurrentLength;
            float spacewidth = GFXUtility.StrW(" ", Font);
            ActionLink currentlink = null;
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
                    if(CurrentWord=="^BEGINLINK")
                    {
                        currentlink = new ActionLink();
                        
                        if(LinkAction!=null && LinkAction.Count>0)
                        {
                            currentlink.ClickHandler = LinkAction.First();
                            LinkAction.Remove(LinkAction.First());
                        }
                        currentlink.LinkStart = (int)Xoffset;
                        currentlink.LineID = this.Lines.Count;
                    }
                    if(CurrentWord=="^ENDLINK")
                    {

                        currentlink.LinkEnd = (int)Xoffset;
                        this.Links.Add(currentlink);
                    }
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
                    if (LastControlWord == "^BEGINLINK" || (currentlink!=null && currentlink.LinkEnd==0))
                    {
                        CurrentLine+= "^ENDLINK";
                        currentlink.LinkEnd = (int)Xoffset;
                        this.Links.Add(currentlink);
                        System.Action tmp = currentlink.ClickHandler;
                        currentlink = new ActionLink();
                        currentlink.ClickHandler = tmp;
                        currentlink.LinkStart = 0;
                        currentlink.LineID = this.Lines.Count + 1;
                    }
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
            this.Links = new List<ActionLink>();
            this.MaxWidth = MaxWidth;
            this.Font = Font;
            this.AppendText(Text, false);
        }
        public RichTextMultiLine(SpriteFont Font, int MaxWidth)
        {
            this.Lines = new List<string>();
            this.Links = new List<ActionLink>();
            this.MaxWidth = MaxWidth;
            this.Font = Font;
        }
        public void SetText(string Text)
        {
            this.Clear();
            this.AppendText(Text, false);
        }
        public bool TryAction(int Line, int X)
        {
            List<ActionLink> a = Links.FindAll(l => l.LineID == Line);
            if (a == null)
                return false;
            foreach(ActionLink l in a)
            {
                if(l.Check(X))
                {
                    l.Click();
                    return true;
                }
            }
            return false;
        }
    }
}
