using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameModel
{
    public class ModelGeometryCompiler
    {

        public static string ModelBaseDir = "";
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
                values = Input.Split(new char[] { ' '}, StringSplitOptions.RemoveEmptyEntries);
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
            public Matrix NextTransform()
            {
                //default to no transform
                Matrix result = Matrix.Identity;
                if (EOL)
                    return result;
                bool done = false;
                //loop until no more transforms
                while(!EOL && !done)
                {
                    string transform = Next();
                    string cmd = transform.Substring(0, 1); //first character is either a valid transform or not
                    string rest = transform.Substring(1);
                    string[] values = rest.Split(',');
                    switch (cmd)
                    {
                        case "X": //X rotation
                            {
                                if (float.TryParse(rest, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out float angle))
                                    result *= Matrix.CreateRotationX(MathHelper.ToRadians(angle));
                                break;
                            }
                        case "Y": //Y rotation
                            {
                                if (float.TryParse(rest, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out float angle))
                                    result *= Matrix.CreateRotationY(MathHelper.ToRadians(angle));
                                break;
                            }
                        case "Z": //Z rotation
                            {
                                if (float.TryParse(rest, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out float angle))
                                    result *= Matrix.CreateRotationZ(MathHelper.ToRadians(angle));
                                break;
                            }
                        case "T": //translation
                            {
                                if (values.Count() < 3)
                                    break;
                                //no specias needed here as 0 for translation on bad value is a very sane default
                                float.TryParse(values[0], System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out float xValue);
                                float.TryParse(values[1], System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out float yValue);
                                float.TryParse(values[2], System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out float zValue);

                                result *= Matrix.CreateTranslation(xValue, yValue, zValue);
                                break;
                            }
                        case "S":
                            {
                                float xValue, yValue, zValue; //default to 1f if the entire value broken
                                if(!float.TryParse(values[0], System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out xValue))
                                    xValue=1f;
                                if(values.Count()<3) //if we can't get 3 values just use first for proportional scaling
                                {
                                    result *= Matrix.CreateScale(xValue);
                                    break;
                                }
                                //else get remaining 2 values and scale
                                if (!float.TryParse(values[1], System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out yValue))
                                    yValue = 1f;
                                if (!float.TryParse(values[2], System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out zValue))
                                    zValue = 1f;

                                result *= Matrix.CreateScale(xValue, yValue, zValue);
                                break;
                            }
                        default: //any other case
                            {
                                offset--;
                                done = true;
                                break;
                            }
                    }
                }
                return result;
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
        public List<string> Textures= new List<string>();
        string[] lines;
        public ModelPart R;
        Model Output;
        public Model ReturnOutput()
        {
            return Output;
        }
        #region data combing 
        static string[] SplitLines(string input)
        {
            input = input.Replace("\r\n", "\n");
            input = input.Replace("\t", " ");
            return input.Split(new char[] { '\n' },StringSplitOptions.RemoveEmptyEntries);
        }

        public static string ColourToCode(Color input)
        {
            byte R, G, B;
            R = input.R;
            G = input.G;
            B = input.B;
            return R + ":" + G + ":" + B;
        }
        #endregion

        public static GameModel.Model LoadModel(string name,Dictionary<string,string> Substitutes=null)
        {
            Model result;
            if (Model.TexturePool == null)
                Model.TexturePool = new Dictionary<string, Texture2D>();
            if (Model.TextureList == null)
                Model.TextureList = new List<string>();
            string modeldata= System.IO.File.ReadAllText(ModelBaseDir+"\\"+name+".mgf");
            if(Substitutes!=null)
            {
                foreach(KeyValuePair<string,string> replace in Substitutes)
                {
                    modeldata = modeldata.Replace(replace.Key, replace.Value);
                }
            }
            ModelGeometryCompiler compiler = new ModelGeometryCompiler(modeldata);
            result = compiler.ReturnOutput();
            if (compiler.Textures != null && compiler.Textures.Count > 0)
                foreach (string texname in compiler.Textures)
                {
                    if (!Model.TexturePool.ContainsKey(texname) && !Model.TextureList.Contains(texname))
                        Model.TextureList.Add(texname);
                }
            return result;
        }

        public ModelGeometryCompiler(string input)
        {
            
            this.lines=SplitLines(input);

            LineSplitter ls = new LineSplitter(lines[state.LineNumber]);
            Dictionary<string, ModelPart> parts = new Dictionary<string, ModelPart>();
            List<ModelPart> p = new List<ModelPart>();
            string choreoname = "";
            Vector3 offset= Vector3.Zero;
            float height = 0;
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
                            choreoname = ls.NextQuoted();

                            break;
                        }
                    case "#offset":
                        {
                            float X, Y, Z;
                            X = ls.NextFloat();
                            Y = ls.NextFloat();
                            Z = ls.NextFloat();
                            offset = new Vector3(X, Y, Z);
                            break;
                        }
                    case "#height":
                        {
                            float X;
                            X = ls.NextFloat();
                            height = X;
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
            string choreofname = ModelBaseDir + "\\" + choreoname + ".mcf";
            //create a dummy if doesn't exist. Dumb workaround but works for now
            if(!System.IO.File.Exists(choreofname))
            {
                System.IO.FileStream s = System.IO.File.Create(choreofname);
                s.Close();
            }
            Output.Choreo = LoadChoreo(System.IO.File.ReadAllText(choreofname));
            Output.ChoreoName = choreoname;
            Output.Offset = offset;
            Output.Height = height;
            ls = null;

        }

        ModelPart ReadBB(LineSplitter ls)
        {
            ModelPart result = new ModelPart();

            string type = ls.Next();
            int H = ls.NextInt();
            int W = ls.NextInt();
            Color c;
            string[] rgb;
            rgb = ls.Next().Split(':');
            c = new Microsoft.Xna.Framework.Color(int.Parse(rgb[0]), int.Parse(rgb[1]), int.Parse(rgb[2]));
            TestParts.PartLight p = new TestParts.PartLight(c);
            p.Width = W;
            p.Height = H;
            result = p;
            
            return result;
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
                            result.TextureName = ls.NextQuoted();
                            if (!Textures.Contains(result.TextureName))
                                Textures.Add(result.TextureName);
                            break;
                        }
                    case "#billboard": //TODO: turn into PartLight, set tex and bb type
                        {
                            result = ReadBB(ls);
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
            //read format X Y Z Colour U V W
            float X, Y, Z, U, V, W;
            Microsoft.Xna.Framework.Color c = new Microsoft.Xna.Framework.Color();
            string[] rgb;

            X = ls.NextFloat();
            Y = ls.NextFloat();
            Z = ls.NextFloat();
            rgb = ls.Next().Split(':');
            c = new Microsoft.Xna.Framework.Color(int.Parse(rgb[0]), int.Parse(rgb[1]), int.Parse(rgb[2]));
            //c.A = 127;
            U = ls.NextFloat();
            V = ls.NextFloat();
            W = 1f;
            if(!ls.EOL)
            W = ls.NextFloat();
            W += 0;
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
            //keep reading points until end command
            while(ls.Next()!="#endpoints")
            {
                ls.Reset(); //rewind because of next
                vertices.Add(ReadPoint(ls)); //read point and add to list
                //prepare for next line
                state.LineNumber++;
                ls = new LineSplitter(lines[state.LineNumber]);
            }
            //flatten and return
            return vertices.ToArray();
            
        }

        int[] ReadMesh()
        {
            LineSplitter ls = new LineSplitter(lines[state.LineNumber].Replace(',',' '));
            List<int> indices = new List<int>();
            //keep going until end mesh command
            while (ls.Next() != "#endmesh")
            {
                //rewind because of next
                ls.Reset();
               
                while(!ls.EOL) //read the rest of the line as ints
                {
                    indices.Add(ls.NextInt());
                }
                //prepare for next line
                state.LineNumber++;
                ls = new LineSplitter(lines[state.LineNumber].Replace(',', ' '));
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

        
        public static Dictionary<string, Dictionary<string, PartAnimation>> LoadChoreo(string input)
        {
            Dictionary<string, Dictionary<string, PartAnimation>> result = new Dictionary<string, Dictionary<string, PartAnimation>>();

            string[] filelines = SplitLines(input);
            int pointer = 0;
            Dictionary<string, PartAnimation> CurrentMove = new Dictionary<string, PartAnimation>();
            string CurrentMoveName="";
            string CurrentPartName="";
            PartAnimation CurrentPart = new PartAnimation();
            LineSplitter ls;
            while(pointer<filelines.Length)
            {
                ls = new LineSplitter(filelines[pointer]);
                string cmd = ls.Next();
                switch(cmd)
                {
                    case "#beginmove":
                        {
                            CurrentMove = new Dictionary<string, PartAnimation>();
                            CurrentMoveName = ls.NextQuoted();
                            break;
                        }
                    case "#endmove":
                        {
                            if (result.ContainsKey(CurrentMoveName))
                                break;
                            result.Add(CurrentMoveName, CurrentMove);
                            break;
                        }
                    case "#beginpart":
                        {
                            CurrentPart = new PartAnimation();
                            CurrentPartName = ls.NextQuoted();
                            break;
                        }
                    case "#endpart":
                        {
                            if (CurrentMove.ContainsKey(CurrentPartName))
                                break;
                            CurrentMove.Add(CurrentPartName, CurrentPart);
                            break;
                        }
                    case "#beginchoreo":
                        {
                            break;
                        }
                    case "#endchoreo":
                        {
                            break;
                        }
                    case "#moveparam":
                        {
                            break;
                        }
                    default:
                        {
                            ls.Reset();
                            float duration = ls.NextFloat();
                            Matrix transform = ls.NextTransform();
                            CurrentPart.Add(transform, duration);
                            break;
                        }
                }

                pointer++;
            }

            return result;
        }

        void AssembleModel(Dictionary<string,ModelPart> parts)
        {
            LineSplitter ls = new LineSplitter(lines[state.LineNumber]);
            ModelPart root = new ModelPart() ;
            Stack<ModelPart> TreeBuilder = new Stack<ModelPart>();
            ModelPart last;
            Model Model;
            int depth;
            while (ls.Next() != "#endassembly")
            {
                ls.Reset();
                string name = "";
                string first = ls.Next(); //first token
                
                if(first[0]=='*') //if first character is *, count
                {
                    depth = first.Length;
                }
                else //root part
                {
                    depth = 0;
                    ls.Reset(); //rewind
                }

                name = ls.NextQuoted(); //part "name" used in animation
                string partname = ls.Next();
                if (!parts.ContainsKey(partname)) //find part by name from loaded parts
                {

                    state.LineNumber++;
                    ls = new LineSplitter(lines[state.LineNumber]);
                    continue; //skip if invalid part
                }
                Matrix m = ls.NextTransform(); //get whatever transforms are there

                ModelPart next = (ModelPart)parts[partname].Clone();
                //both cool with 0 as default
                next.Phase = ls.NextFloat();
                next.BoneFactor = ls.NextFloat();
                next.Title = name;
                //determine correct parent
                    while (depth  < TreeBuilder.Count) //go back part by part until correct part is found (
                    {
                        TreeBuilder.Pop();
                    }
                    if (depth != 0) //the stack is not empty and its top is the last parent
                        TreeBuilder.Peek().Append(next, m); 
                    else
                        root = next;
                    TreeBuilder.Push(next); //put current as last parent

                
                state.LineNumber++;
                ls = new LineSplitter(lines[state.LineNumber]);
            }
            Model = new Model(root);
            Output = Model;
            R = root;
        }
    }
}
