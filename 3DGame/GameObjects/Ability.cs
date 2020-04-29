using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GUI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _3DGame.GameObjects
{
    public class Ability : GUI.IActionIcon
    {
        public float CoolDown { get; set; } 
        public int Icon { get; set; }
        public float MaxCoolDown { get; set; }
        public string Name { get; set; }
        public int StackSize { get; set; }
        public int Level=1;
        public string Description;
        public AbilityTarget Target;
        public int LearnLevel;
        public float ChargeTime;
        public float ChargeGrowth;
        public AbilityLogic.AbilityAnimation ChargeAnimation;
        public float CastTime;
        public float CastGrowth;
        public AbilityLogic.AbilityAnimation CastAnimation;

        public List<AbilityLogic.AbilityEffect> Effects=new List<AbilityLogic.AbilityEffect>();

        public float MPCost;
        public float MPCostGrowth;
        public float GetCurrentMPCost()
        {
            return this.MPCost + this.MPCostGrowth * (this.Level - 1);
        }

        public enum AbilityTarget
        {
            Self,
            Friendly,
            Neutral,
            Hostile
        }

        public List<string> GetTooltip()
        {
            List<string> s = new List<string>();
            s.Add(this.Name);
            s.Add("^DFFFFF Charge time: " + (this.ChargeTime + this.ChargeGrowth * (this.Level-1)) + "s");
            s.Add("^FFEFDF Cast time: " + (this.CastTime + this.CastGrowth * (this.Level-1)) + "s");
            s.Add("^FFFFFF " + this.FormatDescription());
            return s;
        }

        public float GetCurrentCastTime()
        {
            return this.CastTime + this.CastGrowth * (this.Level - 1);
        }

        public float GetCurrentChargeTime()
        {
            return this.ChargeTime + this.ChargeGrowth * (this.Level - 1);
        }
        public virtual string FormatDescription()
        {

            List<string> eparams = new List<string>();
            foreach(AbilityLogic.AbilityEffect e in this.Effects)
            {
                eparams.AddRange(e.GetParams(this.Level-1));
            }
            string[] s = eparams.ToArray();
            return string.Format(this.Description,s);
        }

        public void Render(int X, int Y, GraphicsDevice device, Renderer Renderer, bool RenderCooldown = false, bool RenderEXP = false)
        {

            Renderer.SetTexture(Renderer.AbilityMap);
            Renderer.SetColour(Color.Gray);
            Renderer.RenderIconEx(device, X, Y, this.Icon);
        }

        public void Use(MapEntities.Actor Source, MapEntities.Actor Target)
        {
            foreach(AbilityLogic.AbilityEffect e in this.Effects)
            {
                e.Apply(Source, Target,this.Level);
            }
            
        }
    }
}
