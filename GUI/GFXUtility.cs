using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GUI
{
    /// <summary>
    /// This class contains functions to create primitives used by the GUI engine.
    /// It generates vertices necessary to display the primitives and works out the screen coordinates.
    /// </summary>
    public static class GFXUtility
    {
        /// <summary>
        /// Pixel coordinates to screen coordinates.
        /// </summary>
        /// <param name="device">Device used for the viewport</param>
        /// <param name="X">X</param>
        /// <param name="Y">Y</param>
        /// <returns></returns>
        public static Vector3 PixelToScreen(GraphicsDevice device, float X, float Y)
        {
           // X -= 0.5f;
            //Y -= 0.5f;
            float xscale = (float)device.Viewport.Width / 2;
            float yscale = (float)device.Viewport.Height / 2;
            return new Vector3((X / xscale) - 1f, 1f - (Y / yscale), 0);
        }
        public static Vector2 ScreenToPixel(GraphicsDevice device, Vector3 Coord)
        {
            Vector2 val;
            float xscale = (float)device.Viewport.Width / 2;
            float yscale = (float)device.Viewport.Height / 2;
            val = new Vector2(((1 + Coord.X) * xscale), ((1 - Coord.Y) * xscale));
          //  val.X += 0.5f;
          //  val.Y += 0.5f;
            val.X = (int)val.X;
            val.Y = (int)val.Y;
            return val;
        }
        /// <summary>
        /// Converts a numeric ID into an X,Y coordinate for a table
        /// </summary>
        /// <param name="Width">Width of the table, in cells</param>
        /// <param name="Height">Height of the table, in cells</param>
        /// <param name="ID">ID of the cell</param>
        /// <returns>Vector2 coordinates of the cell.</returns>
        public static Vector2 GetXYFromInt(int Width, int Height, int ID)
        {
            int X = ID % Width;
            int Y = (int)(ID / Width);
            Vector2 v = new Vector2(X, Y);
            return v;
        }
        public static VertexPositionTexture[] Icon(GraphicsDevice device, float X, float Y, int i, int Size = 64)
        {
            VertexPositionTexture[] f = new VertexPositionTexture[6];
            float stride = 0.0625f;
            Vector3 TL = PixelToScreen(device, X, Y); Vector2 TLt = GetXYFromInt(16, 16, i);
            Vector3 TR = PixelToScreen(device, X + Size, Y); Vector2 TRt = TLt + new Vector2(1, 0);
            Vector3 BL = PixelToScreen(device, X, Y + Size); Vector2 BLt = TLt + new Vector2(0, 1);
            Vector3 BR = PixelToScreen(device, X + Size, Y + Size); Vector2 BRt = TLt + new Vector2(1, 1);
            f[0] = new VertexPositionTexture(TL, TLt * stride);
            f[1] = new VertexPositionTexture(TR, TRt * stride);
            f[2] = new VertexPositionTexture(BL, BLt * stride);
            f[3] = new VertexPositionTexture(TR, TRt * stride);
            f[4] = new VertexPositionTexture(BR, BRt * stride);
            f[5] = new VertexPositionTexture(BL, BLt * stride);

            return f;
        }
        public static VertexPositionTexture[] Quad(GraphicsDevice device, float X, float Y, float W, float H, float Rotation = 0.0f)
        {
            VertexPositionTexture[] f = new VertexPositionTexture[6];
            f[0] = new VertexPositionTexture(PixelToScreen(device, X, Y), new Vector2(0, 0));//10
            f[1] = new VertexPositionTexture(PixelToScreen(device, X + W, Y + H), new Vector2(1, 1)); //01
            f[2] = new VertexPositionTexture(PixelToScreen(device, X, Y + H), new Vector2(0, 1));//00
            f[3] = new VertexPositionTexture(PixelToScreen(device, X, Y), new Vector2(0, 0));//01
            f[4] = new VertexPositionTexture(PixelToScreen(device, X + W, Y), new Vector2(1, 0));//11
            f[5] = new VertexPositionTexture(PixelToScreen(device, X + W, Y + H), new Vector2(1, 1));//01
            Matrix r = Matrix.CreateRotationZ(Rotation);
            for (int i = 0; i < 6; i++)
            {
                f[i].Position = Vector3.Transform(f[i].Position, r);
            }
            return f;
        }
        public static VertexPositionTexture[] Quad(GraphicsDevice device, float X, float Y, float Width, float Height, Renderer.Rect TexMap, Texture2D WindowSkin)
        {
            VertexPositionTexture[] f = new VertexPositionTexture[6];
            int ML = TexMap.Left;
            int MR = TexMap.Right;
            int MT = TexMap.Top;
            int MB = TexMap.Bottom;

            Vector3 AA = PixelToScreen(device, X, Y);
            Vector3 AB = PixelToScreen(device, X + Width, Y);
            Vector3 BA = PixelToScreen(device, X, Y + Height);
            Vector3 BB = PixelToScreen(device, X + Width, Y + Height);

            VertexPositionTexture VAA = new VertexPositionTexture(AA, Renderer.TexelToCoord(ML, MT, WindowSkin));
            VertexPositionTexture VAB = new VertexPositionTexture(AB, Renderer.TexelToCoord(MR, MT, WindowSkin));
            VertexPositionTexture VBA = new VertexPositionTexture(BA, Renderer.TexelToCoord(ML, MB, WindowSkin));
            VertexPositionTexture VBB = new VertexPositionTexture(BB, Renderer.TexelToCoord(MR, MB, WindowSkin));

            f[0] = VAA;
            f[1] = VAB;
            f[2] = VBA;
            f[3] = VAB;
            f[4] = VBB;
            f[5] = VBA;

            return f;
        }
        /*
           public static VertexPositionTexture[] Bar(GraphicsDevice device, float X, float Y, float Width, float Height, float Progress, int skin)
           {
               // VertexPositionTexture[] dt = new VertexPositionTexture[6];
               float TA = 0;
               float TB = 1;
               float sxt = 0.0625f / 4f;
               Vector3 TL = GFXUtility.PixelToScreen(device, X, Y);
               Vector3 BL = GFXUtility.PixelToScreen(device, X, Y + Height);
               Vector3 TR = GFXUtility.PixelToScreen(device, X + Width, Y);
               Vector3 BR = GFXUtility.PixelToScreen(device, X + Width, Y + Height);
               Vector3 TM = GFXUtility.PixelToScreen(device, X + (int)(Progress * Width), Y);
               Vector3 BM = GFXUtility.PixelToScreen(device, X + (int)(Progress * Width), Y + Height);
               VertexPositionTexture[] dt = new VertexPositionTexture[12];

               dt[0] = new VertexPositionTexture(TL, new Vector2(sxt * (1 + skin * 4), TA));
               dt[1] = new VertexPositionTexture(TM, new Vector2(sxt * (1 + skin * 4), TA));
               dt[2] = new VertexPositionTexture(BL, new Vector2(sxt * (1 + skin * 4), TB));
               dt[3] = new VertexPositionTexture(TM, new Vector2(sxt * (1 + skin * 4), TA));
               dt[4] = new VertexPositionTexture(BM, new Vector2(sxt * (1 + skin * 4), TB));
               dt[5] = new VertexPositionTexture(BL, new Vector2(sxt * (1 + skin * 4), TB));
               dt[6] = new VertexPositionTexture(TM, new Vector2(sxt * (3 + skin * 4), TA));
               dt[7] = new VertexPositionTexture(TR, new Vector2(sxt * (3 + skin * 4), TA));
               dt[8] = new VertexPositionTexture(BM, new Vector2(sxt * (3 + skin * 4), TB));
               dt[9] = new VertexPositionTexture(TR, new Vector2(sxt * (3 + skin * 4), TA));
               dt[10] = new VertexPositionTexture(BR, new Vector2(sxt * (3 + skin * 4), TB));
               dt[11] = new VertexPositionTexture(BM, new Vector2(sxt * (3 + skin * 4), TB));


               //eventually
               return dt;
           }
           //*/
        public static VertexPositionTexture[] Bar(GraphicsDevice device, float X, float Y, float Width, float Height, float Progress, int skin)
        {
            // VertexPositionTexture[] dt = new VertexPositionTexture[6];
            float TA = 0.5f;
            float TB = TA + 1.0f / 8.0f;
            // TA = 0;
            // TB = 1;
            float sxt = 1f / 128f;//;0.0625f / 8f;
            Vector3 TL = GFXUtility.PixelToScreen(device, X, Y);
            Vector3 BL = GFXUtility.PixelToScreen(device, X, Y + Height);
            Vector3 TR = GFXUtility.PixelToScreen(device, X + Width, Y);
            Vector3 BR = GFXUtility.PixelToScreen(device, X + Width, Y + Height);
            Vector3 TM = GFXUtility.PixelToScreen(device, X + (int)(Progress * Width), Y);
            Vector3 BM = GFXUtility.PixelToScreen(device, X + (int)(Progress * Width), Y + Height);
            VertexPositionTexture[] dt = new VertexPositionTexture[12];

            dt[0] = new VertexPositionTexture(TL, new Vector2(sxt * (1 + skin * 4), TA));
            dt[1] = new VertexPositionTexture(TM, new Vector2(sxt * (1 + skin * 4), TA));
            dt[2] = new VertexPositionTexture(BL, new Vector2(sxt * (1 + skin * 4), TB));
            dt[3] = new VertexPositionTexture(TM, new Vector2(sxt * (1 + skin * 4), TA));
            dt[4] = new VertexPositionTexture(BM, new Vector2(sxt * (1 + skin * 4), TB));
            dt[5] = new VertexPositionTexture(BL, new Vector2(sxt * (1 + skin * 4), TB));
            dt[6] = new VertexPositionTexture(TM, new Vector2(sxt * (3 + skin * 4), TA));
            dt[7] = new VertexPositionTexture(TR, new Vector2(sxt * (3 + skin * 4), TA));
            dt[8] = new VertexPositionTexture(BM, new Vector2(sxt * (3 + skin * 4), TB));
            dt[9] = new VertexPositionTexture(TR, new Vector2(sxt * (3 + skin * 4), TA));
            dt[10] = new VertexPositionTexture(BR, new Vector2(sxt * (3 + skin * 4), TB));
            dt[11] = new VertexPositionTexture(BM, new Vector2(sxt * (3 + skin * 4), TB));


            //eventually
            return dt;
        }
        public static VertexPositionTexture[] Box(GraphicsDevice device, float X, float Y, float Width, float Height)
        {
            VertexPositionTexture[] b = new VertexPositionTexture[18];
            VertexPositionTexture[] f = new VertexPositionTexture[54];
            float qt = 0.25f;
            Vector3 TLC = PixelToScreen(device, X, Y);
            Vector3 TLM = PixelToScreen(device, X + 16, Y);
            Vector3 TRM = PixelToScreen(device, X + Width - 16, Y);
            Vector3 TRC = PixelToScreen(device, X + Width, Y);

            Vector3 BLC = PixelToScreen(device, X, Y + 16);
            Vector3 BLM = PixelToScreen(device, X + 16, Y + 16);
            Vector3 BRM = PixelToScreen(device, X + Width - 16, Y + 16);
            Vector3 BRC = PixelToScreen(device, X + Width, Y + 16);
            b[0] = new VertexPositionTexture(TLC, new Vector2(0, qt * 3));
            b[1] = new VertexPositionTexture(TLM, new Vector2(qt, qt * 3));
            b[2] = new VertexPositionTexture(BLC, new Vector2(0, qt * 4));
            b[3] = new VertexPositionTexture(TLM, new Vector2(qt, qt * 3));
            b[4] = new VertexPositionTexture(BLM, new Vector2(qt, qt * 4));
            b[5] = new VertexPositionTexture(BLC, new Vector2(0, qt * 4));
            //top
            b[6] = new VertexPositionTexture(TLM, new Vector2(qt, qt * 3));
            b[7] = new VertexPositionTexture(TRM, new Vector2(qt * 2, qt * 3));
            b[8] = new VertexPositionTexture(BLM, new Vector2(qt, qt * 4));
            //*
            b[9] = new VertexPositionTexture(TRM, new Vector2(qt * 2, qt * 3));
            b[10] = new VertexPositionTexture(BRM, new Vector2(qt * 2, qt * 4));
            b[11] = new VertexPositionTexture(BLM, new Vector2(qt, qt * 4));
            //*/
            //top right corner
            b[12] = new VertexPositionTexture(TRM, new Vector2(qt * 2, qt * 3));
            b[13] = new VertexPositionTexture(TRC, new Vector2(qt * 3, qt * 3));
            b[14] = new VertexPositionTexture(BRM, new Vector2(qt * 2, qt * 4));
            b[15] = new VertexPositionTexture(TRC, new Vector2(qt * 3, qt * 3));
            b[16] = new VertexPositionTexture(BRC, new Vector2(qt * 3, qt * 4));
            b[17] = new VertexPositionTexture(BRM, new Vector2(qt * 2, qt * 4));
            //middle left

            return b;
        }

        /// <summary>
        /// Sets up the vertices for a frame (window).
        /// </summary>
        /// <param name="device">The graphics device, as required by most functions in this class.</param>
        /// <param name="X">X coordinate of the upper left corner.</param>
        /// <param name="Y">Y coordinate of the upper left corner.</param>
        /// <param name="Width">Width of the window.</param>
        /// <param name="Height">Height of the window.</param>
        /// <returns>All necessary vertices including texture coordinates that can be passed to the renderer.</returns>
        public static VertexPositionTexture[] Frame(GraphicsDevice device, float X, float Y, float Width, float Height)
        {
            VertexPositionTexture[] f = new VertexPositionTexture[54];
            float qt = 0.125f;
            Vector3 TLC = PixelToScreen(device, X, Y);
            Vector3 TLM = PixelToScreen(device, X + 16, Y);
            Vector3 TRM = PixelToScreen(device, X + Width - 16, Y);
            Vector3 TRC = PixelToScreen(device, X + Width, Y);

            Vector3 TMLC = PixelToScreen(device, X, Y + 16);
            Vector3 TMLM = PixelToScreen(device, X + 16, Y + 16);
            Vector3 TMRM = PixelToScreen(device, X + Width - 16, Y + 16);
            Vector3 TMRC = PixelToScreen(device, X + Width, Y + 16);

            Vector3 BMLC = PixelToScreen(device, X, Y + Height - 16);
            Vector3 BMLM = PixelToScreen(device, X + 16, Y + Height - 16);
            Vector3 BMRM = PixelToScreen(device, X + Width - 16, Y + Height - 16);
            Vector3 BMRC = PixelToScreen(device, X + Width, Y + Height - 16);

            Vector3 BLC = PixelToScreen(device, X, Y + Height);
            Vector3 BLM = PixelToScreen(device, X + 16, Y + Height);
            Vector3 BRM = PixelToScreen(device, X + Width - 16, Y + Height);
            Vector3 BRC = PixelToScreen(device, X + Width, Y + Height);

            //top left corner
            f[0] = new VertexPositionTexture(TLC, new Vector2(0, 0));
            f[1] = new VertexPositionTexture(TLM, new Vector2(qt, 0));
            f[2] = new VertexPositionTexture(TMLC, new Vector2(0, qt));
            f[3] = new VertexPositionTexture(TLM, new Vector2(qt, 0));
            f[4] = new VertexPositionTexture(TMLM, new Vector2(qt, qt));
            f[5] = new VertexPositionTexture(TMLC, new Vector2(0, qt));
            //top
            f[6] = new VertexPositionTexture(TLM, new Vector2(qt, 0));
            f[7] = new VertexPositionTexture(TRM, new Vector2(qt * 2, 0));
            f[8] = new VertexPositionTexture(TMLM, new Vector2(qt, qt));
            //*
            f[9] = new VertexPositionTexture(TRM, new Vector2(qt * 2, 0));
            f[10] = new VertexPositionTexture(TMRM, new Vector2(qt * 2, qt));
            f[11] = new VertexPositionTexture(TMLM, new Vector2(qt, qt));
            //*/
            //top right corner
            f[12] = new VertexPositionTexture(TRM, new Vector2(qt * 2, 0));
            f[13] = new VertexPositionTexture(TRC, new Vector2(qt * 3, 0));
            f[14] = new VertexPositionTexture(TMRM, new Vector2(qt * 2, qt));
            f[15] = new VertexPositionTexture(TRC, new Vector2(qt * 3, 0));
            f[16] = new VertexPositionTexture(TMRC, new Vector2(qt * 3, qt));
            f[17] = new VertexPositionTexture(TMRM, new Vector2(qt * 2, qt));
            //middle left
            f[18] = new VertexPositionTexture(TMLC, new Vector2(0, qt));
            f[19] = new VertexPositionTexture(TMLM, new Vector2(qt, qt));
            f[20] = new VertexPositionTexture(BMLC, new Vector2(0, qt * 2));
            f[21] = new VertexPositionTexture(TMLM, new Vector2(qt, qt));
            f[22] = new VertexPositionTexture(BMLM, new Vector2(qt, qt * 2));
            f[23] = new VertexPositionTexture(BMLC, new Vector2(0, qt * 2));
            //*/
            //middle
            f[24] = new VertexPositionTexture(TMLM, new Vector2(0 + qt, qt));
            f[25] = new VertexPositionTexture(TMRM, new Vector2(0 + qt * 2, qt));
            f[26] = new VertexPositionTexture(BMLM, new Vector2(0 + qt, qt * 2));
            f[27] = new VertexPositionTexture(TMRM, new Vector2(0 + qt * 2, qt));
            f[28] = new VertexPositionTexture(BMRM, new Vector2(0 + qt * 2, qt * 2));
            f[29] = new VertexPositionTexture(BMLM, new Vector2(0 + qt, qt * 2));
            //middle right
            f[30] = new VertexPositionTexture(TMRM, new Vector2(qt * 2, qt));
            f[31] = new VertexPositionTexture(TMRC, new Vector2(qt * 3, qt));
            f[32] = new VertexPositionTexture(BMRM, new Vector2(qt * 2, qt * 2));
            f[33] = new VertexPositionTexture(TMRC, new Vector2(qt * 3, qt));
            f[34] = new VertexPositionTexture(BMRC, new Vector2(qt * 3, qt * 2));
            f[35] = new VertexPositionTexture(BMRM, new Vector2(qt * 2, qt * 2));
            //bottom left
            f[36] = new VertexPositionTexture(BMLC, new Vector2(0, qt * 2));
            f[37] = new VertexPositionTexture(BMLM, new Vector2(qt, qt * 2));
            f[38] = new VertexPositionTexture(BLC, new Vector2(0, qt * 3));
            f[39] = new VertexPositionTexture(BMLM, new Vector2(qt, qt * 2));
            f[40] = new VertexPositionTexture(BLM, new Vector2(qt, qt * 3));
            f[41] = new VertexPositionTexture(BLC, new Vector2(0, qt * 3));
            //bottom
            f[42] = new VertexPositionTexture(BMLM, new Vector2(qt, qt * 2));
            f[43] = new VertexPositionTexture(BMRM, new Vector2(qt * 2, qt * 2));
            f[44] = new VertexPositionTexture(BLM, new Vector2(qt, qt * 3));
            f[45] = new VertexPositionTexture(BMRM, new Vector2(qt * 2, qt * 2));
            f[46] = new VertexPositionTexture(BRM, new Vector2(qt * 2, qt * 3));
            f[47] = new VertexPositionTexture(BLM, new Vector2(qt, qt * 3));
            //bottom right
            f[48] = new VertexPositionTexture(BMRM, new Vector2(qt * 2, qt * 2));
            f[49] = new VertexPositionTexture(BMRC, new Vector2(qt * 3, qt * 2));
            f[50] = new VertexPositionTexture(BRM, new Vector2(qt * 2, qt * 3));
            f[51] = new VertexPositionTexture(BMRC, new Vector2(qt * 3, qt * 2));
            f[52] = new VertexPositionTexture(BRC, new Vector2(qt * 3, qt * 3));
            f[53] = new VertexPositionTexture(BRM, new Vector2(qt * 2, qt * 3));
            //eventually
            return f;
        }
        public static VertexPositionTexture[] SlicedQuad(GraphicsDevice device, float X, float Y, float Width, float Height, Renderer.Slice9 TexMap, Texture2D WindowSkin)
        {
            VertexPositionTexture[] f = new VertexPositionTexture[54];
            int ML = TexMap.Left;
            int MR = TexMap.Right;
            int MT = TexMap.Top;
            int MB = TexMap.Bottom;

            Vector3 AA = PixelToScreen(device, X, Y);
            Vector3 AB = PixelToScreen(device, X + ML, Y);
            Vector3 AC = PixelToScreen(device, X + Width - MR, Y);
            Vector3 AD = PixelToScreen(device, X + Width, Y);

            Vector3 BA = PixelToScreen(device, X, Y + MT);
            Vector3 BB = PixelToScreen(device, X + ML, Y + MT);
            Vector3 BC = PixelToScreen(device, X + Width - MR, Y + MT);
            Vector3 BD = PixelToScreen(device, X + Width, Y + MT);

            Vector3 CA = PixelToScreen(device, X, Y + Height - MB);
            Vector3 CB = PixelToScreen(device, X + ML, Y + Height - MB);
            Vector3 CC = PixelToScreen(device, X + Width - MR, Y + Height - MB);
            Vector3 CD = PixelToScreen(device, X + Width, Y + Height - MB);

            Vector3 DA = PixelToScreen(device, X, Y + Height);
            Vector3 DB = PixelToScreen(device, X + ML, Y + Height);
            Vector3 DC = PixelToScreen(device, X + Width - MR, Y + Height);
            Vector3 DD = PixelToScreen(device, X + Width, Y + Height);

            //top left corner
            //Vector2 TTLC = Renderer.TexelToCoord(64, 0, WindowSkin);


            VertexPositionTexture VAA = new VertexPositionTexture(AA, Renderer.TexelToCoord(TexMap.AA, WindowSkin));
            VertexPositionTexture VAB = new VertexPositionTexture(AB, Renderer.TexelToCoord(TexMap.AB, WindowSkin));
            VertexPositionTexture VAC = new VertexPositionTexture(AC, Renderer.TexelToCoord(TexMap.AC, WindowSkin));
            VertexPositionTexture VAD = new VertexPositionTexture(AD, Renderer.TexelToCoord(TexMap.AD, WindowSkin));

            VertexPositionTexture VBA = new VertexPositionTexture(BA, Renderer.TexelToCoord(TexMap.BA, WindowSkin));
            VertexPositionTexture VBB = new VertexPositionTexture(BB, Renderer.TexelToCoord(TexMap.BB, WindowSkin));
            VertexPositionTexture VBC = new VertexPositionTexture(BC, Renderer.TexelToCoord(TexMap.BC, WindowSkin));
            VertexPositionTexture VBD = new VertexPositionTexture(BD, Renderer.TexelToCoord(TexMap.BD, WindowSkin));

            VertexPositionTexture VCA = new VertexPositionTexture(CA, Renderer.TexelToCoord(TexMap.CA, WindowSkin));
            VertexPositionTexture VCB = new VertexPositionTexture(CB, Renderer.TexelToCoord(TexMap.CB, WindowSkin));
            VertexPositionTexture VCC = new VertexPositionTexture(CC, Renderer.TexelToCoord(TexMap.CC, WindowSkin));
            VertexPositionTexture VCD = new VertexPositionTexture(CD, Renderer.TexelToCoord(TexMap.CD, WindowSkin));

            VertexPositionTexture VDA = new VertexPositionTexture(DA, Renderer.TexelToCoord(TexMap.DA, WindowSkin));
            VertexPositionTexture VDB = new VertexPositionTexture(DB, Renderer.TexelToCoord(TexMap.DB, WindowSkin));
            VertexPositionTexture VDC = new VertexPositionTexture(DC, Renderer.TexelToCoord(TexMap.DC, WindowSkin));
            VertexPositionTexture VDD = new VertexPositionTexture(DD, Renderer.TexelToCoord(TexMap.DD, WindowSkin));






            f[0] = VAA;
            f[1] = VAB;
            f[2] = VBA;
            f[3] = VAB;
            f[4] = VBB;
            f[5] = VBA;
            //top
            f[6] = VAB;// new VertexPositionTexture(TLM, new Vector2(buttonoffset + qt, 0 + delta));
            f[7] = VAC;// new VertexPositionTexture(TRM, new Vector2(buttonoffset + qt * 2, 0 + delta));
            f[8] = VBB;// new VertexPositionTexture(TMLM, new Vector2(buttonoffset + qt, qt + delta));
            //*
            f[9] = VAC;// new VertexPositionTexture(TRM, new Vector2(buttonoffset + qt * 2, 0 + delta));
            f[10] = VBC;// new VertexPositionTexture(TMRM, new Vector2(buttonoffset + qt * 2, qt + delta));
            f[11] = VBB;// new VertexPositionTexture(TMLM, new Vector2(buttonoffset + qt, qt + delta));
            //*/
            //top right corner
            f[12] = VAC;// new VertexPositionTexture(TRM, new Vector2(buttonoffset + qt * 2, 0 + delta));
            f[13] = VAD;// new VertexPositionTexture(TRC, new Vector2(buttonoffset + qt * 3, 0 + delta));
            f[14] = VBC;// new VertexPositionTexture(TMRM, new Vector2(buttonoffset + qt * 2, qt + delta));
            f[15] = VAD;// new VertexPositionTexture(TRC, new Vector2(buttonoffset + qt * 3, 0 + delta));
            f[16] = VBD;// new VertexPositionTexture(TMRC, new Vector2(buttonoffset + qt * 3, qt + delta));
            f[17] = VBC;// new VertexPositionTexture(TMRM, new Vector2(buttonoffset + qt * 2, qt + delta));
            //middle left
            f[18] = VBA;// new VertexPositionTexture(TMLC, new Vector2(buttonoffset + 0, qt + delta));
            f[19] = VBB;// new VertexPositionTexture(TMLM, new Vector2(buttonoffset + qt, qt + delta));
            f[20] = VCA;// new VertexPositionTexture(BMLC, new Vector2(buttonoffset + 0, qt * 2 + delta));
            f[21] = VBB;// new VertexPositionTexture(TMLM, new Vector2(buttonoffset + qt, qt + delta));
            f[22] = VCB;// new VertexPositionTexture(BMLM, new Vector2(buttonoffset + qt, qt * 2 + delta));
            f[23] = VCA;// new VertexPositionTexture(BMLC, new Vector2(buttonoffset + 0, qt * 2 + delta));
            //*/
            //middle
            f[24] = VBB;// new VertexPositionTexture(TMLM, new Vector2(buttonoffset + 0 + qt, qt + delta));
            f[25] = VBC;// new VertexPositionTexture(TMRM, new Vector2(buttonoffset + 0 + qt * 2, qt + delta));
            f[26] = VCB;// new VertexPositionTexture(BMLM, new Vector2(buttonoffset + 0 + qt, qt * 2 + delta));
            f[27] = VBC;// new VertexPositionTexture(TMRM, new Vector2(buttonoffset + 0 + qt * 2, qt + delta));
            f[28] = VCC;// VertexPositionTexture(BMRM, new Vector2(buttonoffset + 0 + qt * 2, qt * 2 + delta));
            f[29] = VCB;// VertexPositionTexture(BMLM, new Vector2(buttonoffset + 0 + qt, qt * 2 + delta));
            //middle right
            f[30] = VBC;// new VertexPositionTexture(TMRM, new Vector2(buttonoffset + qt * 2, qt + delta));
            f[31] = VBD;// new VertexPositionTexture(TMRC, new Vector2(buttonoffset + qt * 3, qt + delta));
            f[32] = VCC;// new VertexPositionTexture(BMRM, new Vector2(buttonoffset + qt * 2, qt * 2 + delta));
            f[33] = VBD;// new VertexPositionTexture(TMRC, new Vector2(buttonoffset + qt * 3, qt + delta));
            f[34] = VCD;// VertexPositionTexture(BMRC, new Vector2(buttonoffset + qt * 3, qt * 2 + delta));
            f[35] = VCC;// VertexPositionTexture(BMRM, new Vector2(buttonoffset + qt * 2, qt * 2 + delta));
            //bottom left
            f[36] = VCA;// new VertexPositionTexture(BMLC, new Vector2(buttonoffset + 0, qt * 2 + delta));
            f[37] = VCB;// VertexPositionTexture(BMLM, new Vector2(buttonoffset + qt, qt * 2 + delta));
            f[38] = VDA;// VertexPositionTexture(BLC, new Vector2(buttonoffset + 0, qt * 3 + delta));
            f[39] = VCB;// VertexPositionTexture(BMLM, new Vector2(buttonoffset + qt, qt * 2 + delta));
            f[40] = VDB;// VertexPositionTexture(BLM, new Vector2(buttonoffset + qt, qt * 3 + delta));
            f[41] = VDA;// VertexPositionTexture(BLC, new Vector2(buttonoffset + 0, qt * 3 + delta));
            //bottom
            f[42] = VCB;// new VertexPositionTexture(BMLM, new Vector2(buttonoffset + qt, qt * 2 + delta));
            f[43] = VCC;// new VertexPositionTexture(BMRM, new Vector2(buttonoffset + qt * 2, qt * 2 + delta));
            f[44] = VDB;// new VertexPositionTexture(BLM, new Vector2(buttonoffset + qt, qt * 3 + delta));
            f[45] = VCC;// new VertexPositionTexture(BMRM, new Vector2(buttonoffset + qt * 2, qt * 2 + delta));
            f[46] = VDC;// new VertexPositionTexture(BRM, new Vector2(buttonoffset + qt * 2, qt * 3 + delta));
            f[47] = VDB;// new VertexPositionTexture(BLM, new Vector2(buttonoffset + qt, qt * 3 + delta));
            //bottom right
            f[48] = VCC;// new VertexPositionTexture(BMRM, new Vector2(buttonoffset + qt * 2, qt * 2 + delta));
            f[49] = VCD;// new VertexPositionTexture(BMRC, new Vector2(buttonoffset + qt * 3, qt * 2 + delta));
            f[50] = VDC;// new VertexPositionTexture(BRM, new Vector2(buttonoffset + qt * 2, qt * 3 + delta));
            f[51] = VCD;// new VertexPositionTexture(BMRC, new Vector2(buttonoffset + qt * 3, qt * 2 + delta));
            f[52] = VDD;// new VertexPositionTexture(BRC, new Vector2(buttonoffset + qt * 3, qt * 3 + delta));
            f[53] = VDC;// new VertexPositionTexture(BRM, new Vector2(buttonoffset + qt * 2, qt * 3 + delta));
            //eventually
            return f;
        }
        public static VertexPositionTexture[] Button(GraphicsDevice device, float X, float Y, float Width, float Height,Texture2D WindowSkin, bool Hot = false)
        {
            VertexPositionTexture[] f = new VertexPositionTexture[54];
            float qt = 0.125f / 4f;
            int blocksize = 4;
            float buttonoffset = 0.5f;// 0.375f;
            int bo = 64;
            float delta = Hot ? 0.125f : 0;
            int d = Hot ? 16 : 0;
            Vector3 TLC = PixelToScreen(device, X, Y);
            Vector3 TLM = PixelToScreen(device, X + blocksize, Y);
            Vector3 TRM = PixelToScreen(device, X + Width - blocksize, Y);
            Vector3 TRC = PixelToScreen(device, X + Width, Y);

            Vector3 TMLC = PixelToScreen(device, X, Y + blocksize);
            Vector3 TMLM = PixelToScreen(device, X + blocksize, Y + blocksize);
            Vector3 TMRM = PixelToScreen(device, X + Width - blocksize, Y + blocksize);
            Vector3 TMRC = PixelToScreen(device, X + Width, Y + blocksize);

            Vector3 BMLC = PixelToScreen(device, X, Y + Height - blocksize);
            Vector3 BMLM = PixelToScreen(device, X + blocksize, Y + Height - blocksize);
            Vector3 BMRM = PixelToScreen(device, X + Width - blocksize, Y + Height - blocksize);
            Vector3 BMRC = PixelToScreen(device, X + Width, Y + Height - blocksize);

            Vector3 BLC = PixelToScreen(device, X, Y + Height);
            Vector3 BLM = PixelToScreen(device, X + blocksize, Y + Height);
            Vector3 BRM = PixelToScreen(device, X + Width - blocksize, Y + Height);
            Vector3 BRC = PixelToScreen(device, X + Width, Y + Height);

            //top left corner
            Vector2 TTLC = Renderer.TexelToCoord(64, 0, WindowSkin);







            f[0] = new VertexPositionTexture(TLC, Renderer.TexelToCoord(bo, d, WindowSkin));
            //  f[0].TextureCoordinate = TTLC;
            f[1] = new VertexPositionTexture(TLM, Renderer.TexelToCoord(bo + blocksize, d, WindowSkin));
            f[2] = new VertexPositionTexture(TMLC, Renderer.TexelToCoord(bo, d + blocksize, WindowSkin));
            f[3] = new VertexPositionTexture(TLM, new Vector2(buttonoffset + qt, 0 + delta));
            f[4] = new VertexPositionTexture(TMLM, new Vector2(buttonoffset + qt, qt + delta));
            f[5] = new VertexPositionTexture(TMLC, new Vector2(buttonoffset + 0, qt + delta));
            //top
            f[6] = new VertexPositionTexture(TLM, new Vector2(buttonoffset + qt, 0 + delta));
            f[7] = new VertexPositionTexture(TRM, new Vector2(buttonoffset + qt * 2, 0 + delta));
            f[8] = new VertexPositionTexture(TMLM, new Vector2(buttonoffset + qt, qt + delta));
            //*
            f[9] = new VertexPositionTexture(TRM, new Vector2(buttonoffset + qt * 2, 0 + delta));
            f[10] = new VertexPositionTexture(TMRM, new Vector2(buttonoffset + qt * 2, qt + delta));
            f[11] = new VertexPositionTexture(TMLM, new Vector2(buttonoffset + qt, qt + delta));
            //*/
            //top right corner
            f[12] = new VertexPositionTexture(TRM, new Vector2(buttonoffset + qt * 2, 0 + delta));
            f[13] = new VertexPositionTexture(TRC, new Vector2(buttonoffset + qt * 3, 0 + delta));
            f[14] = new VertexPositionTexture(TMRM, new Vector2(buttonoffset + qt * 2, qt + delta));
            f[15] = new VertexPositionTexture(TRC, new Vector2(buttonoffset + qt * 3, 0 + delta));
            f[16] = new VertexPositionTexture(TMRC, new Vector2(buttonoffset + qt * 3, qt + delta));
            f[17] = new VertexPositionTexture(TMRM, new Vector2(buttonoffset + qt * 2, qt + delta));
            //middle left
            f[18] = new VertexPositionTexture(TMLC, new Vector2(buttonoffset + 0, qt + delta));
            f[19] = new VertexPositionTexture(TMLM, new Vector2(buttonoffset + qt, qt + delta));
            f[20] = new VertexPositionTexture(BMLC, new Vector2(buttonoffset + 0, qt * 2 + delta));
            f[21] = new VertexPositionTexture(TMLM, new Vector2(buttonoffset + qt, qt + delta));
            f[22] = new VertexPositionTexture(BMLM, new Vector2(buttonoffset + qt, qt * 2 + delta));
            f[23] = new VertexPositionTexture(BMLC, new Vector2(buttonoffset + 0, qt * 2 + delta));
            //*/
            //middle
            f[24] = new VertexPositionTexture(TMLM, new Vector2(buttonoffset + 0 + qt, qt + delta));
            f[25] = new VertexPositionTexture(TMRM, new Vector2(buttonoffset + 0 + qt * 2, qt + delta));
            f[26] = new VertexPositionTexture(BMLM, new Vector2(buttonoffset + 0 + qt, qt * 2 + delta));
            f[27] = new VertexPositionTexture(TMRM, new Vector2(buttonoffset + 0 + qt * 2, qt + delta));
            f[28] = new VertexPositionTexture(BMRM, new Vector2(buttonoffset + 0 + qt * 2, qt * 2 + delta));
            f[29] = new VertexPositionTexture(BMLM, new Vector2(buttonoffset + 0 + qt, qt * 2 + delta));
            //middle right
            f[30] = new VertexPositionTexture(TMRM, new Vector2(buttonoffset + qt * 2, qt + delta));
            f[31] = new VertexPositionTexture(TMRC, new Vector2(buttonoffset + qt * 3, qt + delta));
            f[32] = new VertexPositionTexture(BMRM, new Vector2(buttonoffset + qt * 2, qt * 2 + delta));
            f[33] = new VertexPositionTexture(TMRC, new Vector2(buttonoffset + qt * 3, qt + delta));
            f[34] = new VertexPositionTexture(BMRC, new Vector2(buttonoffset + qt * 3, qt * 2 + delta));
            f[35] = new VertexPositionTexture(BMRM, new Vector2(buttonoffset + qt * 2, qt * 2 + delta));
            //bottom left
            f[36] = new VertexPositionTexture(BMLC, new Vector2(buttonoffset + 0, qt * 2 + delta));
            f[37] = new VertexPositionTexture(BMLM, new Vector2(buttonoffset + qt, qt * 2 + delta));
            f[38] = new VertexPositionTexture(BLC, new Vector2(buttonoffset + 0, qt * 3 + delta));
            f[39] = new VertexPositionTexture(BMLM, new Vector2(buttonoffset + qt, qt * 2 + delta));
            f[40] = new VertexPositionTexture(BLM, new Vector2(buttonoffset + qt, qt * 3 + delta));
            f[41] = new VertexPositionTexture(BLC, new Vector2(buttonoffset + 0, qt * 3 + delta));
            //bottom
            f[42] = new VertexPositionTexture(BMLM, new Vector2(buttonoffset + qt, qt * 2 + delta));
            f[43] = new VertexPositionTexture(BMRM, new Vector2(buttonoffset + qt * 2, qt * 2 + delta));
            f[44] = new VertexPositionTexture(BLM, new Vector2(buttonoffset + qt, qt * 3 + delta));
            f[45] = new VertexPositionTexture(BMRM, new Vector2(buttonoffset + qt * 2, qt * 2 + delta));
            f[46] = new VertexPositionTexture(BRM, new Vector2(buttonoffset + qt * 2, qt * 3 + delta));
            f[47] = new VertexPositionTexture(BLM, new Vector2(buttonoffset + qt, qt * 3 + delta));
            //bottom right
            f[48] = new VertexPositionTexture(BMRM, new Vector2(buttonoffset + qt * 2, qt * 2 + delta));
            f[49] = new VertexPositionTexture(BMRC, new Vector2(buttonoffset + qt * 3, qt * 2 + delta));
            f[50] = new VertexPositionTexture(BRM, new Vector2(buttonoffset + qt * 2, qt * 3 + delta));
            f[51] = new VertexPositionTexture(BMRC, new Vector2(buttonoffset + qt * 3, qt * 2 + delta));
            f[52] = new VertexPositionTexture(BRC, new Vector2(buttonoffset + qt * 3, qt * 3 + delta));
            f[53] = new VertexPositionTexture(BRM, new Vector2(buttonoffset + qt * 2, qt * 3 + delta));
            //eventually
            return f;
        }
        public static VertexPositionTexture[] CloseButton(GraphicsDevice device, float X, float Y, bool hot = false)
        {
            VertexPositionTexture[] buttonframe = new VertexPositionTexture[6];
            float qt = 0.125f;
            Vector3 TL = PixelToScreen(device, X, Y);
            Vector3 TR = PixelToScreen(device, X + 16, Y);
            Vector3 BL = PixelToScreen(device, X, Y + 16);
            Vector3 BR = PixelToScreen(device, X + 16, Y + 16);
            float delta = hot ? 0.0f : 0.125f;
            buttonframe[0] = new VertexPositionTexture(TL, new Vector2(qt * 3, 0 + delta));
            buttonframe[1] = new VertexPositionTexture(TR, new Vector2(qt * 4, 0 + delta));
            buttonframe[2] = new VertexPositionTexture(BL, new Vector2(qt * 3, qt + delta));

            buttonframe[3] = new VertexPositionTexture(TR, new Vector2(qt * 4, 0 + delta));
            buttonframe[4] = new VertexPositionTexture(BR, new Vector2(qt * 4, qt + delta));
            buttonframe[5] = new VertexPositionTexture(BL, new Vector2(qt * 3, qt + delta));
            return buttonframe;

        }
        /// <summary>
        /// Sets up the vertices for a "clock"-style cooldown/progress overlay. Fucking pain in the ass to code, too.
        /// </summary>
        /// <param name="device">Device to use for screenspace calculations</param>
        /// <param name="X">X in pixels</param>
        /// <param name="Y">Y in pixels</param>
        /// <param name="Progress">0.0 to 1.0, 0 being full circle, 1 being invisible.</param>
        /// <param name="Size">Width and height of the clock, in pixels.</param>
        /// <returns></returns>
        public static VertexPositionTexture[] Clock(GraphicsDevice device, float X, float Y, float Progress, int Size = 32)
        {
            int vertcount = 15;
            float EI = 0.125f;
            Vector3 TL = PixelToScreen(device, X, Y); Vector2 TLt = new Vector2(0.75f, 0.25f);
            Vector3 TR = PixelToScreen(device, X + Size, Y); Vector2 TRt = TLt + new Vector2(0.25f, 0);
            Vector3 BL = PixelToScreen(device, X, Y + Size); Vector2 BLt = TLt + new Vector2(0, 0.25f);
            Vector3 BR = PixelToScreen(device, X + Size, Y + Size); Vector2 BRt = TLt + new Vector2(0.25f, 0.25f);
            Vector3 TC = Vector3.Lerp(TL, TR, 0.5f);
            Vector3 M = Vector3.Lerp(Vector3.Lerp(BL, BR, 0.5f), TC, 0.5f);

            if (Progress > EI)
            {
                vertcount = 12;

            }

            if (Progress > EI * 3)
            {
                vertcount = 9;

            }
            if (Progress > EI * 5)
            {
                vertcount = 6;

            }
            if (Progress > EI * 7)
            {
                vertcount = 3;

            }
            VertexPositionTexture[] c = new VertexPositionTexture[vertcount];
            if (Progress > EI * 7)
            {
                Progress -= EI * 7;
                Progress /= EI;
                c[0].Position = Vector3.Lerp(TL, TC, Progress); c[0].TextureCoordinate = TLt;
                c[1].Position = TC; c[1].TextureCoordinate = TLt;
                c[2].Position = M; c[2].TextureCoordinate = TLt;
                return c;
            }
            if (Progress > EI * 5)
            {
                Progress -= EI * 5;
                Progress /= EI * 2;
                c[0].Position = TL; c[0].TextureCoordinate = TLt;
                c[1].Position = TC; c[1].TextureCoordinate = TLt;
                c[2].Position = M; c[2].TextureCoordinate = TLt;
                c[3].Position = TL; c[3].TextureCoordinate = TLt;
                c[4].Position = M; c[4].TextureCoordinate = TLt;
                c[5].Position = Vector3.Lerp(BL, TL, Progress); c[5].TextureCoordinate = TLt;

                return c;
            }
            if (Progress > EI * 3)
            {
                Progress -= EI * 3;
                Progress /= EI * 2;
                c[0].Position = TL; c[0].TextureCoordinate = TLt;
                c[1].Position = TC; c[1].TextureCoordinate = TLt;
                c[2].Position = M; c[2].TextureCoordinate = TLt;

                c[3].Position = TL; c[3].TextureCoordinate = TLt;
                c[4].Position = M; c[4].TextureCoordinate = TLt;
                c[5].Position = BL; c[5].TextureCoordinate = TLt;

                c[6].Position = BL; c[6].TextureCoordinate = TLt;
                c[7].Position = M; c[7].TextureCoordinate = TLt;
                c[8].Position = Vector3.Lerp(BR, BL, Progress); c[8].TextureCoordinate = TLt;

                return c;
            }
            if (Progress > EI)
            {
                Progress -= EI;
                Progress /= EI * 2;
                c[0].Position = TL; c[0].TextureCoordinate = TLt;
                c[1].Position = TC; c[1].TextureCoordinate = TLt;
                c[2].Position = M; c[2].TextureCoordinate = TLt;

                c[3].Position = TL; c[3].TextureCoordinate = TLt;
                c[4].Position = M; c[4].TextureCoordinate = TLt;
                c[5].Position = BL; c[5].TextureCoordinate = TLt;

                c[6].Position = BL; c[6].TextureCoordinate = TLt;
                c[7].Position = M; c[7].TextureCoordinate = TLt;
                c[8].Position = BR; c[8].TextureCoordinate = TLt;

                c[9].Position = BR; c[9].TextureCoordinate = TLt;
                c[10].Position = M; c[10].TextureCoordinate = TLt;
                c[11].Position = Vector3.Lerp(TR, BR, Progress); c[11].TextureCoordinate = TLt;

                return c;
            }
            Progress *= 8;
            c[0].Position = TL; c[0].TextureCoordinate = TLt;
            c[1].Position = TC; c[1].TextureCoordinate = TLt;
            c[2].Position = M; c[2].TextureCoordinate = TLt;

            c[3].Position = TL; c[3].TextureCoordinate = TLt;
            c[4].Position = M; c[4].TextureCoordinate = TLt;
            c[5].Position = BL; c[5].TextureCoordinate = TLt;

            c[6].Position = BL; c[6].TextureCoordinate = TLt;
            c[7].Position = M; c[7].TextureCoordinate = TLt;
            c[8].Position = BR; c[8].TextureCoordinate = TLt;

            c[9].Position = BR; c[9].TextureCoordinate = TLt;
            c[10].Position = M; c[10].TextureCoordinate = TLt;
            c[11].Position = TR; c[11].TextureCoordinate = TLt;

            c[12].Position = TR; c[12].TextureCoordinate = TLt;
            c[13].Position = M; c[13].TextureCoordinate = TLt;
            c[14].Position = Vector3.Lerp(TC, TR, Progress); c[14].TextureCoordinate = TLt;
            return c;
        }
        public static float StrW(string str, SpriteFont font)
        {
            return font.MeasureString(str).X;

        }
    }
}
