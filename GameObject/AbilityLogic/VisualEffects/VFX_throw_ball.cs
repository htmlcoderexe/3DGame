using GameObject.MapEntities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameObject.AbilityLogic.VisualEffects
{
    class VFX_throw_ball : AbilityVFX
    {
        public override string EffectType { get { return "VFX_throw_ball"; } }
        public Color Colour { get; set; }
        public float Size { get; set; }
        string[] _rawparams;
        public override void Apply(Actor Source, Actor Target, int Level)
        {
            MapEntities.Particles.LightBall ball = new MapEntities.Particles.LightBall(this.Colour, this.Size);
            MapEntities.ParticleGroup g = new MapEntities.ParticleGroup
            {
                Speed = 15f,
                Position = Source.Position + new Vector3(0, 0.9f, 0),
                WorldSpawn = Source.WorldSpawn,
                Gravity = false,
                OnGround = false
            };
            g.Model = null;
            g.TTL = this.GetDuration(Level)+20;
            g.Expires = true;
            g.Target = Target;
            g.FizzleOnGround = true;
            g.FizzleOnTarget = true;
            g.Particles.Add(ball);

            Source.WorldSpawn.Entities.Add(g);
        }
        public VFX_throw_ball() { }
        public VFX_throw_ball(string[] parameters)
        {
            this.ParamValues = parameters;
        }
        public override string[] ParamValues
        {
            set
            {
                this._rawparams = value;
                _rawparams[0] = _rawparams[0].Split(',')[0];
                _rawparams[1] = _rawparams[1].Split(',')[0];
                this.Colour = Utility.GetColor(_rawparams[0]);
                this.Size = Utility.GetFloat(_rawparams[1]);
            }
            get
            {
                return _rawparams;
            }
        }
    }
}
