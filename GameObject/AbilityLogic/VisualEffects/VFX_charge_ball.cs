using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameObject.MapEntities;
using Microsoft.Xna.Framework;

namespace GameObject.AbilityLogic.VisualEffects
{
    public class VFX_charge_ball : AbilityVFX
    {
        public override string EffectType{get{return "VFX_charge_ball";}}
        public Color Colour;
        public float Size;
        string[] _rawparams;
        public override void Apply(Actor Source, Actor Target, int Level)
        {
            MapEntities.Particles.LightBall ball = new MapEntities.Particles.LightBall(this.Colour,this.Size);
            MapEntities.ParticleGroup g = new MapEntities.ParticleGroup
            {
                Speed = 0f,
                Position = Source.Position + new Vector3(0, 0.9f, 0),
                TargetOffset= new Vector3(0, 0.91f, 0),
                FizzleOnTarget=false,
                WorldSpawn = Source.WorldSpawn,
                Gravity = false,
                OnGround = false
            };
            g.Model = null;
            g.TTL = this.GetDuration(Level);
            g.Expires = true;
            g.Target = Source;
            //g.FizzleOnGround = true;
            g.Particles.Add(ball);
            
            Source.WorldSpawn.Entities.Add(g);
        }

        public VFX_charge_ball(string[] parameters)
        {
            this.ParamValues = parameters;
        }
        public override string[] ParamValues
        {
            set
            {
                this._rawparams = (string[])value.Clone();
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
