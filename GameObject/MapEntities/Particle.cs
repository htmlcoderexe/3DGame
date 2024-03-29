﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameObject;
using Microsoft.Xna.Framework;

namespace GameObject.MapEntities
{
    public class Particle : MapEntity
    {
        public float TTL;
        public MapEntity Parent;
        public Color Colour;
        public Interfaces.WorldPosition Origin;
        public Interfaces.WorldPosition Offset;
        public bool Expires=false;
        private void DoExpire(float dT)
        {

            this.TTL -= dT;
            if (this.TTL <= 0)
                this.Die();
        }
        public override void Update(float dT)
        {

            if (Expires)
                DoExpire(dT);
            if(this.Parent is ParticleGroup && ((ParticleGroup)(this.Parent)).FlatAim)
                this.Position = Origin + Vector3.Transform(Offset, Matrix.CreateRotationY(MathHelper.ToRadians(-this.Heading)));
            else
                this.Position = Origin + Vector3.Transform(Vector3.Transform(Offset, Matrix.CreateRotationZ(MathHelper.ToRadians(this.Pitch))),Matrix.CreateRotationY(MathHelper.ToRadians(-this.Heading)));
        }
    }
}
