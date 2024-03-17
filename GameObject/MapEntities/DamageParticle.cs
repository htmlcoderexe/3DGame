using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameObject.MapEntities
{
     public class DamageParticle : MapEntity
     {
        public float TTL;
        public Color Colour;
        public string Text;
        
        public override void Render(GraphicsDevice device, float dT, Vector2 Reference, bool Alpha)
        {
          //don't
        }
        public DamageParticle(MapEntity Parent, Interfaces.WorldPosition Position,string Text,Color Colour,float TTL)
        {
            this.WorldSpawn = Parent.WorldSpawn;
            this.Position = Position;
            this.Text = Text;
            this.Colour = Colour;
            this.TTL = TTL;
            this.DisplayName = " "; //lol 
        }
        public override void Update(float dT)
        {
            base.Update(dT);
            this.Position += new Vector3(0, 0.5f * dT, 0);
            this.TTL -= dT;
            if (this.TTL < 0f)
                Die();
        }
    }
}
