using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GUI
{
    public class Renderer

    {
        public Effect GUIEffect;
        public Texture2D WindowSkin;
        public Texture2D InventoryPartsMap;
        public Texture2D AbilityMap;
        public Texture2D SmallIconMap;
        public SpriteFont UIFont;
        public SpriteFont FloatFont;
            private SpriteBatch batch;





        public Renderer(GraphicsDevice device)
        {
            this.batch = new SpriteBatch(device);
        }
        /// <summary>
        /// Sets texture for the GUIEffect. If the texture being set is the same one, nothing happens.
        /// </summary>
        /// <param name="texture">Any Texture2D.</param>
        public void SetTexture2(Texture2D texture)
        {
            // if (GUIEffect.Texture != texture)
            {
                // GUIEffect.Texture = texture;
                //  GUIEffect.DiffuseColor = new Vector3(0, 1, 0);
                // GUIEffect.
                foreach (EffectPass p in GUIEffect.CurrentTechnique.Passes)
                {
                    p.Apply();
                }
            }

        }
        public void SetTexture(Texture2D texture)
        {
            // if (GUIEffect.Texture != texture)
            {
                GUIEffect.Parameters["IconMap"].SetValue(texture);

                // GUIEffect.
                foreach (EffectPass p in GUIEffect.CurrentTechnique.Passes)
                {
                    p.Apply();
                }
            }

        }
        public void SetColour(Color Colour, int Alpha = 127)
        {
            // if (GUIEffect.Texture != texture)
            Colour.A = (byte)Alpha;
            {
                GUIEffect.Parameters["Colour"].SetValue(Colour.ToVector4());

                // GUIEffect.
                foreach (EffectPass p in GUIEffect.CurrentTechnique.Passes)
                {
                    p.Apply();
                }
            }

        }
        public struct Rect
        {
            public int Left { get; set; }
            public int Right { get; set; }
            public int Top { get; set; }
            public int Bottom { get; set; }
            public Rect(int X, int Y, int Width, int Height) : this()
            {
                this.Left = X;
                this.Right = X + Width;
                this.Top = Y;
                this.Bottom = Y + Height;
            }

        }
        public struct Slice9
        {
            public Rect Outer { get; set; }
            public Rect Inner { get; set; }
            public Slice9(int X, int Y, int Width, int Height, int Left, int Right, int Top, int Bottom) : this()
            {
                this.Outer = new Rect(X, Y, Width, Height);
                this.Inner = new Rect(X + Left, Y + Top, Width - (Right + Left), Height - (Bottom + Top));
            }
            public Slice9(int X, int Y, int Width, int Height, int Margin) : this()
            {
                this.Outer = new Rect(X, Y, Width, Height);
                this.Inner = new Rect(X + Margin, Y + Margin, Width - Margin * 2, Height - Margin * 2);
            }
            #region Matrix
            #region A
            public Vector2 AA
            {
                get
                {
                    return new Vector2(this.Outer.Left, this.Outer.Top);
                }

            }
            public Vector2 AB
            {
                get
                {
                    return new Vector2(this.Inner.Left, this.Outer.Top);
                }

            }
            public Vector2 AC
            {
                get
                {
                    return new Vector2(this.Inner.Right, this.Outer.Top);
                }

            }
            public Vector2 AD
            {
                get
                {
                    return new Vector2(this.Outer.Right, this.Outer.Top);
                }

            }
            #endregion
            #region B
            public Vector2 BA
            {
                get
                {
                    return new Vector2(this.Outer.Left, this.Inner.Top);
                }

            }
            public Vector2 BB
            {
                get
                {
                    return new Vector2(this.Inner.Left, this.Inner.Top);
                }

            }
            public Vector2 BC
            {
                get
                {
                    return new Vector2(this.Inner.Right, this.Inner.Top);
                }

            }
            public Vector2 BD
            {
                get
                {
                    return new Vector2(this.Outer.Right, this.Inner.Top);
                }

            }
            #endregion
            #region C
            public Vector2 CA
            {
                get
                {
                    return new Vector2(this.Outer.Left, this.Inner.Bottom);
                }

            }
            public Vector2 CB
            {
                get
                {
                    return new Vector2(this.Inner.Left, this.Inner.Bottom);
                }

            }
            public Vector2 CC
            {
                get
                {
                    return new Vector2(this.Inner.Right, this.Inner.Bottom);
                }

            }
            public Vector2 CD
            {
                get
                {
                    return new Vector2(this.Outer.Right, this.Inner.Bottom);
                }

            }
            #endregion
            #region D
            public Vector2 DA
            {
                get
                {
                    return new Vector2(this.Outer.Left, this.Outer.Bottom);
                }

            }
            public Vector2 DB
            {
                get
                {
                    return new Vector2(this.Inner.Left, this.Outer.Bottom);
                }

            }
            public Vector2 DC
            {
                get
                {
                    return new Vector2(this.Inner.Right, this.Outer.Bottom);
                }

            }
            public Vector2 DD
            {
                get
                {
                    return new Vector2(this.Outer.Right, this.Outer.Bottom);
                }

            }
            #endregion
            #endregion
            public int Top
            {
                get
                {
                    return Inner.Top - Outer.Top;

                }
            }
            public int Bottom
            {
                get
                {
                    return Outer.Bottom - Inner.Bottom;
                }
            }
            public int Left
            {
                get
                {
                    return Inner.Left - Outer.Left;
                }
            }
            public int Right
            {
                get
                {
                    return Outer.Right - Inner.Right;
                }

            }
        }
        public static Vector2 TexelToCoord(int X, int Y, Texture2D Texture)
        {
            Vector2 output;
            float TextureX = (float)X / (float)Texture.Width;
            float TextureY = (float)Y / (float)Texture.Height;
            output = new Vector2(TextureX, TextureY);
            return output;

        }
        public static Vector2 TexelToCoord(Vector2 Coord, Texture2D Texture)
        {
            // Coord.X += 0.5f;
            // Coord.Y += 0.5f;
            Coord.X /= Texture.Width;
            Coord.Y /= Texture.Height;
            return Coord;

        }

        #region Colours

        public static Color ColourBlue = new Color(0, 189, 255);
        // publstatic ic Color ColourGreen = new Color(0, 255, 100);
        public static Color ColourGreen = new Color(0, 206, 0);
        public static Color ColourGold = new Color(255, 200, 12);
        public static Color ColourViolet = new Color(255, 12, 200);
        public static Color ColourOrange = new Color(255, 100, 0);
        public static Color ColourGray = new Color(200, 200, 200);
        public static Color ColourYellow = new Color(255, 255, 0);
        public static Color ColourRed = new Color(255, 20, 50);
        public static Color ColourNeon = new Color(0, 240, 255);


        public static string ColourToCode(Color Colour)
        {
            string s = "";
            int r = Colour.R;
            int g = Colour.G;
            int b = Colour.B;
            s =HexByte(r) +HexByte(g) +HexByte(b);
            s = "^" + s + " ";
            return s;
        }

        #endregion
        public void RenderTexturedQuad(GraphicsDevice device, float X, float Y, float W, float H, Texture2D Texture)
        {
            SetTexture(Texture);
            VertexPositionTexture[] c = GFXUtility.Quad(device, X, Y, W, H);
            device.DrawUserPrimitives<VertexPositionTexture>(PrimitiveType.TriangleList, c, 0, c.Length / 3);

        }
        public void RenderClock(GraphicsDevice device, float X, float Y, float Progress, int Size = 32)
        {
            // GUIEffect.DiffuseColor = new Vector3(0.0f, 0.0f, 0.0f);
            // GUIEffect.Alpha = 0.7f;
            SetTexture(WindowSkin);
            SetColour(new Color(0, 0, 0), 0);
            VertexPositionTexture[] c = GFXUtility.Clock(device, X, Y, Progress, Size);
            device.DrawUserPrimitives<VertexPositionTexture>(PrimitiveType.TriangleList, c, 0, c.Length / 3);
            SetColour(Color.Gray);

            // GUIEffect.DiffuseColor = new Vector3(1,1,1);
            // GUIEffect.Alpha = 2.0f;
        }
        /// <summary>
        /// Renders a progress bar
        /// </summary>
        /// <param name="device">Device to render to</param>
        /// <param name="X">Location X</param>
        /// <param name="Y">Location Y</param>
        /// <param name="Width">Width of the bar</param>
        /// <param name="Height">Height of the bar</param>
        /// <param name="Progress">Amount of progress, floating point number between 0.0f (empty) and 1.0f (full)</param>
        /// <param name="skin">Skin used for the bar</param>
        public void RenderBar(GraphicsDevice device, float X, float Y, float Width, float Height, float Progress, int skin)
        {
            //SetTexture(BarTexture);
            SetTexture(WindowSkin);
            VertexPositionTexture[] bar = GFXUtility.Bar(device, X, Y, Width, Height, Progress, skin);
            device.DrawUserPrimitives<VertexPositionTexture>(PrimitiveType.TriangleList, bar, 0, 4);
        }
        public void RenderSmallText(GraphicsDevice device, float X, float Y, string Text, Color Colour, bool Centered = false, bool outlined = false)
        {
            if (Text == null)
                return;
            if (Centered)
            {
                Vector2 sd = UIFont.MeasureString(Text);
                X -= sd.X / 2;
                X = (float)Math.Round(X);

            }
            batch.Begin();
            if (outlined)
            {
                batch.DrawString(UIFont, Text, new Microsoft.Xna.Framework.Vector2(X, Y), Colour);
            }
            else
            {
                batch.DrawString(UIFont, Text, new Microsoft.Xna.Framework.Vector2(X, Y), Colour);
            }
            batch.End();

        }
        public void RenderBigText(GraphicsDevice device, float X, float Y, string Text, Color Colour, bool Centered = false)
        {
            if (Text == null)
                return;
            if (Centered)
            {
                Vector2 sd = FloatFont.MeasureString(Text);
                X -= sd.X / 2;
                X = (float)Math.Round(X);

            }
            batch.Begin();

            batch.DrawString(FloatFont, Text, new Microsoft.Xna.Framework.Vector2(X, Y), Colour);

            batch.End();
        }
        string ReadToSymbol(string Text, string Symbol, int Offset, out int NewOffset)
        {
            //NewOffset = 0;
            int off = Text.IndexOf(Symbol, Offset);
            if (off == -1)
            {
                NewOffset = Text.Length;
                return Text.Substring(Offset);
            }
            NewOffset = off + 1;
            return Text.Substring(Offset, off - Offset);
        }
        public void RenderRichText(GraphicsDevice device, float X, float Y, string Text,  bool Outline = false)
        {
            if (Text == null)
                return;
            Color Colour = Color.White;
            int offset = 0;
            float dX = 0;
            string s = "";
            string cstring;
            batch.Begin();
            while (offset < Text.Length)
            {
                int o2 = 0;
                s = ReadToSymbol(Text, "^", offset, out o2);
                offset = o2;
                cstring = ReadToSymbol(Text, " ", offset, out offset);

                batch.DrawString(UIFont, s, new Vector2(X + dX, Y), Colour);
                if (cstring.Length == 6)
                {
                    int R, G, B;
                    R = int.Parse(cstring.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
                    G = int.Parse(cstring.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
                    B = int.Parse(cstring.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
                    Colour = new Color(R, G, B);
                }
                dX += UIFont.MeasureString(s).X;
            }
            batch.End();
        }
        public void RenderFrame(GraphicsDevice device, float X, float Y, float Width, float Height, Slice9? slice=null)
        {
            SetTexture(WindowSkin);
            SetColour(Color.LightGray);
            if(!slice.HasValue)
            slice = new Slice9(0, 0, 48, 48, 5, 5, 17, 5);
            VertexPositionTexture[] frame = GFXUtility.SlicedQuad(device, X, Y, Width, Height, slice.GetValueOrDefault(), WindowSkin);// GFXUtility.Frame(device, X, Y, Width, Height);
            device.DrawUserPrimitives<VertexPositionTexture>(PrimitiveType.TriangleList, frame, 0, 18);
            SetColour(Color.Gray);

        }
        public void RenderButton(GraphicsDevice device, float X, float Y, float Width, float Height, bool Hot = false)
        {
            SetTexture(WindowSkin);
            Slice9 slice = new Slice9(80, Hot ? 24 : 0, 24, 24, 4);
            VertexPositionTexture[] frame = GFXUtility.SlicedQuad(device, X, Y, Width, Height, slice, WindowSkin);
            device.DrawUserPrimitives<VertexPositionTexture>(PrimitiveType.TriangleList, frame, 0, 18);

        }
        public void RenderCloseButton(GraphicsDevice device, float X, float Y, bool Hot = false)
        {
            SetTexture(WindowSkin);
            Rect r = new Rect(48, Hot ? 16 : 0, 16, 16);
            VertexPositionTexture[] butt = GFXUtility.Quad(device, X, Y, 16, 16, r,WindowSkin);   // GFXUtility.CloseButton(device, X, Y, Hot);
            device.DrawUserPrimitives<VertexPositionTexture>(PrimitiveType.TriangleList, butt, 0, 2);
        }
        public void RenderQuad(GraphicsDevice device, float X, float Y, float Width, float Height, Renderer.Rect TexMap)
        {
            SetTexture(WindowSkin);
            VertexPositionTexture[] butt = GFXUtility.Quad(device, X, Y, Width, Height, TexMap, WindowSkin);
            device.DrawUserPrimitives<VertexPositionTexture>(PrimitiveType.TriangleList, butt, 0, 2);
        }
        public void RenderBigIcon(GraphicsDevice device, float X, float Y, int Icon,Texture2D Texture)
        {
            SetTexture(Texture);
            VertexPositionTexture[] i = GFXUtility.Icon(device, X, Y, Icon);
            device.DrawUserPrimitives<VertexPositionTexture>(PrimitiveType.TriangleList, i, 0, 2);
        }
        public void RenderIconEx(GraphicsDevice device, float X, float Y, int Icon)
        {
            // SetTexture(bagIcons);
            VertexPositionTexture[] i = GFXUtility.Icon(device, X, Y, Icon);
            device.DrawUserPrimitives<VertexPositionTexture>(PrimitiveType.TriangleList, i, 0, 2);
        }
        public void RenderSmallIcon(GraphicsDevice device, float X, float Y, int Icon)
        {
            SetTexture(SmallIconMap);
            VertexPositionTexture[] j = GFXUtility.Icon(device, X, Y, Icon, 16);

            device.DrawUserPrimitives<VertexPositionTexture>(PrimitiveType.TriangleList, j, 0, 2);
        }
        public void RenderBox(GraphicsDevice device, float X, float Y, float Width, float Height)
        {
            SetTexture(WindowSkin);
            VertexPositionTexture[] b = GFXUtility.Box(device, X, Y, Width, Height);
            device.DrawUserPrimitives<VertexPositionTexture>(PrimitiveType.TriangleList, b, 0, 6);
        }


        public static string Pad(string String, int Length, string PadChr = "0")
        {
            string tstr = "";
            for (int i = 1; i <= Length - String.Length; i++)
            {
                tstr += PadChr;
            }
            return tstr + String;
        }
        public static string Hex(int Number)
        {
            return Number.ToString("X");

        }
        public static string HexByte(int Number)
        {
            string num = Hex(Number);
            num = Pad(num, 2);
            return num;
        }
    }

}
