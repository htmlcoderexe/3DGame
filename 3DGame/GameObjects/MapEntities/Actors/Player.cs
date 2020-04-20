using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DGame.GameObjects.MapEntities.Actors
{
    public class Player : Actor
    {
        public Items.ItemEquip[] Equipment;
        public Scenes.GameplayAssets.Inventory Inventory;
        public int Level;
        public int EXP;
        public int Exp4Level(int lvl)
        {
            return lvl * 10;
        }
        public int Total4Level(int lvl)
        {
            return lvl * Exp4Level(lvl)/2;
        }
        public int CalculateLvl(int EXP)
        {
            int curlvl = 1;
            while(Total4Level(curlvl)<EXP)
            {
                curlvl++;
            }
            curlvl -= 1;
            return curlvl;
        }
        public Player()
        {
            this.Equipment = new Items.ItemEquip[Items.ItemEquip.EquipSlot.Max];
            this.StatBonuses.Add(new StatBonus() { FlatValue = 100, Type = "HP", Order = StatBonus.StatOrder.Template });
            this.StatBonuses.Add(new StatBonus() { FlatValue = 15, Type = "hpregen", Order = StatBonus.StatOrder.Template });
            this.StatBonuses.Add(new StatBonus() { FlatValue = 5, Type = "movement_speed", Order = StatBonus.StatOrder.Template });
            this.Gravity = false;
            this.JumpStrength = 10;
            this.MaxJumps = 2;
            this.Inventory = new Scenes.GameplayAssets.Inventory(64);
            this.Camera.Distance = 15;
        }
        
        public bool CanEquip(Items.ItemEquip Item)
        {
            //TODO actual requirement checking
            return true;
        }
        
        public void EquipItem(Items.ItemEquip Item,int slot)
        {
            if (slot >= 0 && slot < this.Equipment.Length)
            {
                this.Equipment[slot] = Item;
                this.EquipItem(Item);
            }
        }

        public void EquipItem(Items.ItemEquip Item)
        {
            if (!CanEquip(Item))
                return;
            if (Item != null && Item.Bonuses!=null)
                foreach (Items.ItemBonus b in Item.Bonuses)
                    StatBonuses.Add(b);
            
        }
        public void UnequipItem(Items.ItemEquip Item,int slot)
        {
            if (slot >= 0 && slot < this.Equipment.Length)
            {
                this.Equipment[slot] = null;
                this.UnequipItem(Item);
            }
        }
        public void UnequipItem(Items.ItemEquip Item)
        {
           
            if (Item == null)
                return;
            if(Item.Bonuses!=null)
            foreach (Items.ItemBonus b in Item.Bonuses)
                StatBonuses.Remove(b);
        }
        public override void Update(float dT)
        {

            if (Walking)
            {
                this.Speed = this.GetMovementSpeed();

                StepToTarget(dT);
            }
            base.Update(dT);
        }
    }
}
