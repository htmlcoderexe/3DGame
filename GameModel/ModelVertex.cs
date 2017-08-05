using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameModel
{
    public struct ModelVertex : IVertexType
    {
        public Vector3 Position;
        public Color Color;
        public Vector2 TextureCoordinate;
        //public Vector3 Normal;
        public Vector2 BoneWeightData;
        public readonly static VertexDeclaration VertexDeclaration = new VertexDeclaration
    (
        new VertexElement(0, VertexElementFormat.Vector3, VertexElementUsage.Position, 0),    //vertex position
        new VertexElement(sizeof(float) * 3, VertexElementFormat.Color, VertexElementUsage.Color, 0), //vertex colour
        new VertexElement(sizeof(float) * 3 + sizeof(uint), VertexElementFormat.Vector2, VertexElementUsage.TextureCoordinate, 0), //vertex texcoord
                                                                                                                                   //new VertexElement(sizeof(float) * 5 + sizeof(uint), VertexElementFormat.Vector3, VertexElementUsage.Normal, 0),
        new VertexElement(sizeof(float) * 5 + sizeof(uint), VertexElementFormat.Vector2, VertexElementUsage.TextureCoordinate, 1)

    );

        VertexDeclaration IVertexType.VertexDeclaration { get { return VertexDeclaration; } }
        public ModelVertex(Vector3 Position, Color Colour)
        {
            this.Position = Position;
            this.Color = Colour;
            this.TextureCoordinate = new Vector2(0, 0);
            this.BoneWeightData = new Vector2(1, 1);
        }
    };
        
}
