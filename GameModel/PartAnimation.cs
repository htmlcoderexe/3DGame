using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace GameModel
{
    public class PartAnimation : ICloneable
    {
        private SortedDictionary<float, Matrix> _keyframes;
        public float Time;
        public bool Loop;
        public void SetPhase(float f)
        {
            this.Time = f * this.Length;
        }
        public PartAnimation()
        {
            _keyframes = new SortedDictionary<float, Matrix>();
        }
        public float Length
        {
            get
            {
                //return zero length if there are now keyframes
                return _keyframes.Count==0?0f:_keyframes.Last().Key;
            }
        }
        public void Add(Matrix Frame, float Length)
        {
            //retrieve the highest key so far
            if(_keyframes.Count==0)
            {
                _keyframes.Add(Length, Frame);
                return;
            }
            float highest = _keyframes.Last().Key;
            _keyframes.Add(Length + highest, Frame);
        }
        public void Reset()
        {
            this.Time = 0;
        }
        public Matrix GetTransform(float dT)
        {
            this.Time += dT;
            float prev=0, next=0;
            Matrix A=Matrix.Identity, B=Matrix.Identity;
            //go through all frames
            foreach(KeyValuePair<float, Matrix> frame in _keyframes)
            {
                //current frame is before
                if(Time>frame.Key)
                {
                    //shortcut
                    if(Time>=Length)
                    {
                        if(Loop)
                        {
                            Time -= Length;
                            return GetTransform(0.0f);
                        }
                        Time = Length;
                        return frame.Value;
                    }
                    //update to this being previous frame
                    prev = frame.Key;
                    A = frame.Value;
                }
                //if frame is in the future
                else
                {
                    next = frame.Key;
                    B = frame.Value;
                    return Matrix.Lerp(A, B, (Time - prev) / (next - prev));
                }
            }
            return A;
        }

        public object Clone()
        {
            object o = this.MemberwiseClone();
            //(o as PartAnimation).Reset();
            SortedDictionary<float, Matrix> cd = new SortedDictionary<float, Matrix>();
            foreach(KeyValuePair<float,Matrix> kvp in _keyframes)
            {
                cd.Add(kvp.Key, kvp.Value);
            }
            (o as PartAnimation)._keyframes = cd;
            return o;
        }
    }
}
