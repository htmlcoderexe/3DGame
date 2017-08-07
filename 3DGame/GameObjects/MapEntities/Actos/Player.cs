using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DGame.GameObjects.MapEntities.Actos
{
    public class Player : Actor
    {
        public List<Items.ItemEquip> Equipment;
        public Player()
        {
            this.Equipment = new List<Items.ItemEquip>();
            this.StatBonuses.Add(new StatBonus() {FlatValue=100,Type="HP",Order= StatBonus.StatOrder.Template });
            this.Gravity = false;
            this.JumpStrength = 15;
            this.MaxJumps = 3;
        }
        public override float CurrentHP
        {
            get
            {
                return CalculateStat("HP");
            }

            set
            {
                base.CurrentHP = value;
            }
        }
        public bool CanEquip(Items.ItemEquip Item)
        {
            //TODO actual requirement checking
            return true;
        }
        public bool EquipItem(Items.ItemEquip Item)
        {
            if (!CanEquip(Item))
                return false;
            foreach (Items.ItemBonus b in Item.Bonuses)
                StatBonuses.Add(b);
            this.Equipment.Add(Item);
            return true;
        }
        public bool UnequipItem(Items.ItemEquip Item)
        {
            if (!this.Equipment.Contains(Item))
                return false;
            foreach (Items.ItemBonus b in Item.Bonuses)
                StatBonuses.Remove(b);
            this.Equipment.Remove(Item);
            return true;
        }
    }
}
