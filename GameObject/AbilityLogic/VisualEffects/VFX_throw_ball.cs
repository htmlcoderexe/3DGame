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
        public Color Colour;
        public override void Apply(Actor Source, Actor Target, int Level)
        {
            MapEntities.Particles.LightBall ball = new MapEntities.Particles.LightBall(this.Colour, 0.5f);
            MapEntities.ParticleGroup g = new MapEntities.ParticleGroup
            {
                Speed = 15f,
                Position = Source.Position + new Vector3(0, 0.9f, 0),
                WorldSpawn = Source.WorldSpawn,
                Gravity = false,
                OnGround = false
            };
            g.Model = null;
            g.TTL = this.Duration;
            g.Expires = true;
            g.Target = Target;
            g.FizzleOnGround = true;
            g.Particles.Add(ball);

            Source.WorldSpawn.Entities.Add(g);
        }
    }
}
