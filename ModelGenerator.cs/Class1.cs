using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameModel;
using Microsoft.Xna.Framework;

namespace ModelGenerator
{
    public class PrimitiveGenerator
    {
        public static ModelPart GenerateTube(float Length, float Radius, int Sections, bool StartCap, bool EndCap)
        {
            ModelPart result = new ModelPart();

            int ringSize = 6;

            float Delta = Length / (float)Sections;
            Vector3 Offset = new Vector3(Radius, 0, 0);

            ModelVertex[] vertices = new ModelVertex[ringSize*(Sections-1)];
            for (int j = 0; j < Sections + 1; j++)
            {

                for (int i = 0; i < ringSize; i++)
                {
                    ModelVertex v = new ModelVertex();
                    v.Position = Vector3.Transform(
                                    Vector3.Transform(
                                            Offset,
                                            Matrix.CreateRotationY(MathHelper.TwoPi * ((float)i / (float)(ringSize)))
                                    ),
                                    Matrix.CreateTranslation(new Vector3(0, Delta * j, 0))
                                 );
                    v.BoneWeightData.X = (float)j / (float)Sections;
                    vertices[i * ringSize + i] = v;
                }
            }

            int[] indices = new int[(ringSize * 2) * Sections * 3];
            int[] quad = { 0, ringSize, ringSize + 1, 1 };
            for(int i=0;i<Sections;i++)
            {
                for (int j=0;j<ringSize-1;j++)
                {
                    int di = i * ringSize*6 + j*6;
                    indices[di++] = quad[0] + (i * ringSize)+j;
                    indices[di++] = quad[1] + (i * ringSize)+j;
                    indices[di++] = quad[2] + (i * ringSize)+j;
                    indices[di++] = quad[2] + (i * ringSize)+j;
                    indices[di++] = quad[3] + (i * ringSize)+j;
                    indices[di++] = quad[0] + (i * ringSize)+j;
                }
                int index = i * ringSize * 6 + (ringSize-1) * 6;
                indices[index++] = i * ringSize + ringSize - 1;
                indices[index++] = ringSize + i * ringSize + ringSize - 1;
                indices[index++] = ringSize + 1 + i * ringSize + ringSize - 1 - ringSize;
                indices[index++] = ringSize + 1 + i * ringSize + ringSize - 1 - ringSize;
                indices[index++] = i * ringSize + ringSize - 1-ringSize+1;
                indices[index++] = i * ringSize + ringSize - 1;
            }
            result.SetVertices(vertices);
            return result;
        }        
    }
}
