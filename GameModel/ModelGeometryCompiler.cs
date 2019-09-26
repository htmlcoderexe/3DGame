using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameModel
{
    public class ModelGeometryCompiler
    {
        public class LineSplitter
        {
            int offset;
            string[] values;
            string original;
            public bool EOL;
            public LineSplitter(string Input)
            {
                original = Input;
                values = Input.Split(new char[] { ' ',','}, StringSplitOptions.RemoveEmptyEntries);
                offset = 0;
                EOL = false;
            }
            public void Reset()
            {
                offset = 0;
                EOL = false;
            }
            public string Next()
            {
                if (EOL)
                    return "";
                offset++;
                if (offset >= values.Length)
                    EOL = true;
                return values[offset - 1];
            }
            public int NextInt()
            {
                if (EOL)
                    return 0;
                string s = Next();
                bool yes = false;
                int result = 0;
                yes = int.TryParse(s, out result);
                if (yes)
                    return result;
                return 0;
            }
        }
        struct CompilerState
        {
            public bool ModelBegun;
            public string CurrentPartName;

            public int LineNumber;
        }
        CompilerState state;
        ModelPart CurrentPart;
        string[] lines;

        #region data combing 
        void SplitLines(string input)
        {
            input = input.Replace("\r\n", "\n");
            this.lines= input.Split('\n');
        }
        #endregion

        ModelPart ReadPart()
        {
            return new ModelPart();
        }

        ModelVertex ReadPoint(LineSplitter ls)
        {
            ModelVertex v = new ModelVertex();
            //TODO: read format X Y Z Colour U V B

            return v;
        }

        ModelVertex[] ReadPoints()
        {
            LineSplitter ls = new LineSplitter(lines[state.LineNumber]);
            List<ModelVertex> vertices = new List<ModelVertex>();
            while(ls.Next()!="#endpoints")
            {
                ls.Reset();
                vertices.Add(ReadPoint(ls));
                state.LineNumber++;
                ls = new LineSplitter(lines[state.LineNumber]);
            }
            return vertices.ToArray();
            
        }

        int[] ReadMesh()
        {
            LineSplitter ls = new LineSplitter(lines[state.LineNumber]);
            List<int> indices = new List<int>();
            while (ls.Next() != "#endmesh")
            {
                ls.Reset();
               
                while(!ls.EOL)
                {
                    indices.Add(ls.NextInt());
                }
            }
            return indices.ToArray();
        }
    }
}
