using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameModel
{
    public class ModelPart
    {
        public int VertexOffset;
        public int VertexLength;
        public int IndexOffset;
        public int IndexLength;
        private VertexPositionColor[] _vertices;
        private int[] _indices;
        public void Render(GraphicsDevice device, float dT, Matrix World)
        {

        }
    }
}
