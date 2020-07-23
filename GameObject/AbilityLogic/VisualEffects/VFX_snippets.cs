using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameObject.AbilityLogic.VisualEffects
{
    class VFX_snippets
    {

        /*
           if (1==2)
                { 

                World.Player.Model.ApplyAnimation("ChargeHands");
                World.Player.SetPlayOnce();
                Color c = new Color(100, 255, 200);
                /*
                GameObject.MapEntities.ParticleGroups.Ring r =
                    new GameObject.MapEntities.ParticleGroups.Ring(0.5f, 0.4f, c);
                r.Speed = 8f;
                r.Position = World.Player.Position;
                r.Target = World.Player.Target;
                r.WorldSpawn = World;
                r.Gravity = false;
                r.OnGround = false;
                r.FizzleOnTarget = true;
                World.Entities.Add(r);
                //* /
        //GameObject.MapEntities.Particles.LightRay ray = new GameObject.MapEntities.Particles.LightRay(World.Player, World.Player.Target, new Color(0, 254, 100),1f);
        //ray.Expires = true;
        GameObject.MapEntities.Particles.LightBall ball = new GameObject.MapEntities.Particles.LightBall(c, 0.5f);
        GameObject.MapEntities.ParticleGroup g = new GameObject.MapEntities.ParticleGroup
        {
            Speed = 15f,
            Position = World.Player.Position + new Vector3(0, 0.9f, 0),
            WorldSpawn = World,
            Gravity = false,
            OnGround = false
        };
        g.Model = null;
                g.TTL = 10.4f;
                g.Expires = true;
                g.Target = World.Player.Target;
                g.FizzleOnGround = true;
                g.Particles.Add(ball);

                World.Entities.Add(g);
                World.Player.Target.Target = World.Player;
               // World.Player.Executor = new GameObject.AbilityLogic.AbilityExecutor(World.Player.Abilities[0],World.Player,World.Player.Target);
            }
         */

        /*

        if (false && kb.IsKeyDown(Keys.F1) && PreviousKbState.IsKeyUp(Keys.F1) && World.Player.Target!=null && !World.Player.Target.IsDead)
            {
                World.Player.Model.ApplyAnimation("StrikeBladeShortRight");
                World.Player.SetPlayOnce();
                World.Player.Target.Target = World.Player;
                World.Player.Target.Hit(World.Player.CalculateStat("p_atk") + RNG.Next(0, 30), true, 0);
                // World.Player.Model.Animation.
            }
            if (false && kb.IsKeyDown(Keys.F2) && PreviousKbState.IsKeyUp(Keys.F2) && World.Player.Target!=null && !World.Player.Target.IsDead)
            {
                Color c = new Color(255, 100, 20);
                /*
                for (int i = 0; i < 1; i++)
                {
                    GameObject.MapEntities.Particles.Homing p = new GameObject.MapEntities.Particles.Homing(c, 2.0f);
                    p.WorldSpawn = World;
                    p.Parent = World.Player.Target;
                    Vector3 v = new Vector3(0, 3.6f, -2.0f+(1.0f*(float)i));
                    v = Vector3.Transform(v, Matrix.CreateRotationY((World.Player.Heading+90)*MathHelper.Pi/180f));
                    p.Position = World.Player.Position + v;
                   
                    p.TTL = 100;
                    p.Speed = 8f;
                    p.Gravity = false;
                    World.Entities.Add(p);
                    p = null;
                }
                //* /

        GameObject.MapEntities.ParticleGroup g = new GameObject.MapEntities.ParticleGroup
        {
            Target = World.Player.Target,
            Position = World.Player.Target.Position + new Vector3(0, 6 + 15, 0),// + offset;
            TTL = 10.0f,
            Expires = true,
            Speed = 2f,
            Gravity = false,
            Model = null,
            //                g.Pitch = -89f;
            FizzleOnTarget = true,
            FlatAim = true
        };
                for (int i=0;i<11;i++)
                {
                    float angle = MathHelper.ToRadians(RNG.Next(0, 360));
        float delta = (float)((float)RNG.Next(0, 600) / 100f);
        float delta2 = (float)((float)RNG.Next(0, 600) / 100f);
        Vector3 offset = new Vector3(delta, delta2 - 15, 0);
        offset = Vector3.Transform(offset, Matrix.CreateRotationY(angle));
                    GameObject.MapEntities.Particle p = new GameObject.MapEntities.Particle
                    {
                        Offset = new WorldPosition() + offset,
                        Model = null
                    };
        GameObject.MapEntities.Particle p2 = new GameObject.MapEntities.Particle
        {
            Offset = new WorldPosition() + new Vector3(0, 2, 0) + offset,
            Model = null
        };
        GameObject.MapEntities.Particles.LightRay r = new GameObject.MapEntities.Particles.LightRay(p, p2, new Color(255, 50, 0), 1f);

        g.Particles.Add(p);
                    g.Particles.Add(p2);
                    g.Particles.Add(r);
                    World.Entities.Add(g);
                }
    World.Player.Target.Target = World.Player;
                World.Player.Target.Hit(World.Player.CalculateStat("p_atk") + RNG.Next(0, 30), true, 0);
                List<MapEntity> targets = World.LocateNearby(World.Player.Target);
                foreach(MapEntity e in targets)
                {
                    if(e is GameObject.MapEntities.Actors.Monster mon)
                    {
                        if(((Vector3)(World.Player.Target.Position-mon.Position)).Length()<10f)
                        {
                            mon.Target = World.Player;
                            mon.Hit(World.Player.CalculateStat("p_atk") + RNG.Next(0, 30), true, 0);
                        }
                    }
                }

                //World.Player.Target.
            }

        */
    }
}
