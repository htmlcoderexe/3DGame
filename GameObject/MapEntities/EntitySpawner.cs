using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameObjects;
using Microsoft.Xna.Framework;

namespace GameObject.MapEntities
{
    public class EntitySpawner : MapEntity
    {
        public MapEntity Entity;
        public float Interval;
        public float CountDown;
        public int MaxCount;
        public int Count;
        public System.Action<MapEntity> SpawnCallback;
        public Microsoft.Xna.Framework.BoundingBox SpawningVolume;

        public override void Update(float dT)
        {
            this.DisplayName = Count.ToString() + "/" + MaxCount.ToString();
            if (this.Count >= MaxCount)
                return;
            base.Update(dT);
            this.CountDown -= dT;
            this.DisplayName = this.DisplayName + " - Next in: " + this.CountDown;
            if(this.CountDown<0f )
            {
                DoSpawn();
                this.CountDown += this.Interval;
            }
        }
        private void DoSpawn()
        {
            MapEntity e = (MapEntity)Entity.Clone();
            float X, Y, Z;
            X = (float)RNG.Next((int)(SpawningVolume.Min.X * 100), (int)(SpawningVolume.Max.X * 100)) / 100f;
            Y = (float)RNG.Next((int)(SpawningVolume.Min.Y * 100), (int)(SpawningVolume.Max.Y * 100)) / 100f;
            Z = (float)RNG.Next((int)(SpawningVolume.Min.Z * 100), (int)(SpawningVolume.Max.Z * 100)) / 100f;
            Interfaces.WorldPosition p = new Interfaces.WorldPosition();
            p += new Vector3(X, Y, Z);
            e.Position = p+this.Position;
            e.Heading = RNG.Next(359);
            e.WorldSpawn = this.WorldSpawn;
            SpawnCallback?.Invoke(e);
            Count++;
        }
        private void DeathNotify()
        {
            Count--;
        }
    }
}
