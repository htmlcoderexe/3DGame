using Microsoft.Xna.Framework;
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
            public static Dictionary<string, string> SymbolTable;
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
                if (SymbolTable == null)
                    SymbolTable = new Dictionary<string, string>();
            }
            public void Reset()
            {
                offset = 0;
                EOL = false;
            }
            public string Next(bool literal = false)
            {
                if (EOL)
                    return "";
                if (values.Length < 1)
                    return "";
                offset++;
                if (offset >= values.Length)
                    EOL = true;
                string result= values[offset - 1];
                if(!literal && result[0]=='$')
                {
                    if (SymbolTable.ContainsKey(result))
                        result = SymbolTable[result];
                }
                return result;
            }
            public string NextQuoted()
            {
                string raw = Next();
                return raw.Substring(1, raw.Length - 2);
            }
            public int NextInt()
            {
                if (EOL)
                    return 0;
                string s = Next();
                bool yes = false;
                yes = int.TryParse(s, out int result);
                if (yes)
                    return result;
                return 0;
            }
            public float NextFloat()
            {
                if (EOL)
                    return 0f;
                string s = Next();
                bool yes = false;
                //make sure it uses the . as decimal separator
                yes = float.TryParse(s, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out float result);
                if (yes)
                    return result;
                return 0f;
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
        public ModelPart R;

        #region data combing 
        void SplitLines(string input)
        {
            input = input.Replace("\r\n", "\n");
            input = input.Replace("\t", " ");
            this.lines= input.Split(new char[] { '\n' },StringSplitOptions.RemoveEmptyEntries);
        }
        #endregion

        public ModelGeometryCompiler(string input)
        {
            SplitLines(input);

            LineSplitter ls = new LineSplitter(lines[state.LineNumber]);
            Dictionary<string, ModelPart> parts = new Dictionary<string, ModelPart>();
            List<ModelPart> p = new List<ModelPart>();
            string command = ls.Next();
            while(command!="#endmodel")
            {
                switch(command)
                {
                    case "#beginpart":
                        {
                            state.LineNumber++;
                            parts.Add(ls.NextQuoted(),ReadPart());
                            break;
                        }
                    case "#beginassembly":
                        {
                            AssembleModel(parts);
                            break;
                        }
                    case "#beginsymbols":
                        {
                            state.LineNumber++;
                            BuildSymbolTable();
                            break;
                        }
                    case "#choreo":
                        {
                            break;
                        }
                    default:
                        {
                            //throw error here or something
                            break;
                        }
                }
                state.LineNumber++;
                ls = new LineSplitter(lines[state.LineNumber]);
                command = ls.Next();
            }
            ls = null;

        }

        ModelPart ReadPart()
        {
            ModelPart result = new ModelPart();
            
            LineSplitter ls = new LineSplitter(lines[state.LineNumber]);
            List<ModelVertex> vertices = new List<ModelVertex>();
            while (ls.Next() != "#endpart")
            {
                ls.Reset();
                switch(ls.Next())
                {
                    case "#beginpoints":
                        {
                            state.LineNumber++;
                            result.SetVertices(this.ReadPoints());
                            break;
                        }
                    case "#beginmesh":
                        {
                            state.LineNumber++;
                            result.SetIndices(this.ReadMesh());
                            break;
                        }
                    case "#texture": //TODO: set part's tex
                        {
                            break;
                        }
                    case "#billboard": //TODO: turn into PartLight, set tex and bb type
                        {
                            break;
                        }
                    default: //throw an error or something lol
                        {
                            break;
                        }
                }
                state.LineNumber++;
                ls = new LineSplitter(lines[state.LineNumber]);
            }

            return result;
        }

        ModelVertex ReadPoint(LineSplitter ls)
        {
            ModelVertex v = new ModelVertex();
            //TODO: read format X Y Z Colour U V W
            float X, Y, Z, U, V, W;
            Microsoft.Xna.Framework.Color c = new Microsoft.Xna.Framework.Color();
            string[] rgb;

            X = ls.NextFloat();
            Y = ls.NextFloat();
            Z = ls.NextFloat();
            rgb = ls.Next().Split(':');
            c = new Microsoft.Xna.Framework.Color(int.Parse(rgb[0]), int.Parse(rgb[1]), int.Parse(rgb[2]));
            U = ls.NextFloat();
            V = ls.NextFloat();
            W = 1f;
            if(!ls.EOL)
            W = ls.NextFloat();
            v.Position = new Microsoft.Xna.Framework.Vector3(X, Y, Z);
            v.Color = c;
            v.TextureCoordinate = new Microsoft.Xna.Framework.Vector2(U, V);
            v.BoneWeightData.X = W;
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
                state.LineNumber++;
                ls = new LineSplitter(lines[state.LineNumber]);
            }
            return indices.ToArray();
        }

        void BuildSymbolTable()
        {
            LineSplitter ls = new LineSplitter(lines[state.LineNumber]);
            Dictionary<string, string> SymbolTable = new Dictionary<string, string>();
            while (ls.Next() != "#endsymbols")
            {
                ls.Reset();
                //split a name=value pair
                string[] kvp = ls.Next(true).Split('=');
                //check if exactly 2 pieces exist (name and value), this also conveniently ignores empty values
                if(kvp.Length==2)
                {
                    //if symbol exists, redefine it, else add to table
                    if (SymbolTable.ContainsKey(kvp[0]))
                        SymbolTable[kvp[0]] = kvp[1];
                    else
                        SymbolTable.Add(kvp[0], kvp[1]);
                }
                state.LineNumber++;
                ls = new LineSplitter(lines[state.LineNumber]);
            }
            //make this table the current table
            LineSplitter.SymbolTable = SymbolTable;
        }

        

        void AssembleModel(Dictionary<string,ModelPart> parts)
        {
            LineSplitter ls = new LineSplitter(lines[state.LineNumber]);
            ModelPart root = new ModelPart() ;
            Stack<ModelPart> TreeBuilder = new Stack<ModelPart>();
            ModelPart last;
            int depth;
            while (ls.Next() != "#endassembly")
            {
                ls.Reset();
                string name = "";
                string first = ls.Next();
                
                if(first[0]=='*')
                {
                    depth = first.Length;
                }
                else
                {
                    depth = 0;
                    ls.Reset();
                }

                name = ls.NextQuoted();
                string partname = ls.Next();
                if (!parts.ContainsKey(partname))
                {

                    state.LineNumber++;
                    ls = new LineSplitter(lines[state.LineNumber]);
                    continue; //skip if invalid part
                }
                Matrix m = Matrix.Identity;

                ModelPart next = (ModelPart)parts[partname].Clone();
                next.Title = name;
                if (depth == TreeBuilder.Count)
                {
                    if (depth != 0)
                    {
                        ModelPart parent = TreeBuilder.Peek();
                        parent.Append(next, m);
                    }
                    else
                    { 
                        root = next;
                }
                    TreeBuilder.Push(next);
                }
                else
                {
                    ModelPart parent;
                    while (depth  < TreeBuilder.Count)
                    {
                        parent = TreeBuilder.Pop();
                    }
                    parent = TreeBuilder.Peek();
                    parent.Append(next, m);
                    TreeBuilder.Push(next);
                }
                state.LineNumber++;
                ls = new LineSplitter(lines[state.LineNumber]);
            }
            R = root;
        }
    }
}
