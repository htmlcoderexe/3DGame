using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameObject.MapEntities.Actors
{
    public class Player : Actor
    {
        /// <summary>
        /// Stores the skilltree used to load available skills and for binds
        /// </summary>
        public SkillTree SkillTree;
        /// <summary>
        /// Player's current equipment
        /// </summary>
        public Items.ItemEquip[] Equipment;
        /// <summary>
        /// Player's current inventory
        /// </summary>
        public Items.Inventory Inventory;
        /// <summary>
        /// Player's level
        /// </summary>
        public int Level;
        /// <summary>
        /// Player's EXP
        /// </summary>
        public int EXP;
        /// <summary>
        /// Calculates EXP to achieve specific level from previous.
        /// </summary>
        /// <param name="lvl"></param>
        /// <returns></returns>
        public int Exp4Level(int lvl)
        {
            return lvl * 10;
        }
        /// <summary>
        /// Calculates total needed to achieve specific level from 1st (0 exp).
        /// </summary>
        /// <param name="lvl"></param>
        /// <returns></returns>
        public int Total4Level(int lvl)
        {
            return lvl * Exp4Level(lvl)/2;
        }
        /// <summary>
        /// Calculates level from total EXP
        /// </summary>
        /// <param name="EXP"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Create the player
        /// </summary>
        public Player()
        {
            this.Equipment = new Items.ItemEquip[Items.ItemEquip.EquipSlot.Max];
            this.Inventory = new Items.Inventory(64);
            this.Camera.Distance = 15;
            this.Gravity = false;
            this.JumpStrength = 10;
            this.MaxJumps = 2;
            //anything below should be data pulled from character template/saved character
            this.StatBonuses.Add(new StatBonus() { FlatValue = 100, Type = "HP", Order = StatBonus.StatOrder.Template });
            this.StatBonuses.Add(new StatBonus() { FlatValue = 15, Type = "hpregen", Order = StatBonus.StatOrder.Template });
            this.StatBonuses.Add(new StatBonus() { FlatValue = 5, Type = "movement_speed", Order = StatBonus.StatOrder.Template });
        }

        public Player(CharacterTemplate CharacterClass, List<ModularAbility> skills)
        {
            this.Equipment = (Items.ItemEquip[])CharacterClass.StarterEquipment.Clone();
            this.Inventory = new Items.Inventory(64);
            foreach(KeyValuePair<string,float> basestat in CharacterClass.BaseStats)
            {
                this.StatBonuses.Add(new StatBonus() {FlatValue=basestat.Value,Type=basestat.Key,Order=StatBonus.StatOrder.Template });
            }
            this.StatBonuses.Add(new StatBonus() { FlatValue = 100, Type = "MP", Order = StatBonus.StatOrder.Template });

            this.SkillTree = CharacterClass.SkillTree;
            this.Camera.Distance = 15;
            this.Gravity = false;
            this.JumpStrength = 10;
            this.MaxJumps = 2;

            this.Abilities = new List<ModularAbility>();
            foreach(SkillTreeEntry e in CharacterClass.SkillTree.Entries)
            {
                List<ModularAbility> found = skills.Where(x => x.ID == e.SkillID).ToList();
                if (found.Count != 1)
                    continue;
                this.Abilities.Add(found[0]);
            }
        }

        static Dictionary<int, string> _equipMap;
        static Dictionary<int, Matrix> _equipTMap;
        /// <summary>
        /// Retrieves the correct parent part and translation matrix to display object for a given slot
        /// </summary>
        /// <param name="slot">Equipment slot to use</param>
        /// <returns>A Tuple containing the part name as a string (better make that unique in the model!) and the Matrix to pass to Append()</returns>
        public static Tuple<string,Matrix> GetEquipParent(int slot)
        {
            if(_equipMap==null)
            {
                _equipMap = new Dictionary<int, string>
                {
                    [0] = "HandR",
                    [1] = "HandL"
                };
            }
            if(_equipTMap==null)
            {
                //these should be eventually data coming from the model itself
                _equipTMap = new Dictionary<int, Matrix>()
                {
                    [0] = Matrix.CreateRotationY(MathHelper.PiOver2)
                    * Matrix.CreateRotationZ(-MathHelper.PiOver2)
                    * Matrix.CreateTranslation(0, 0, 0.05f),
                    [1] = Matrix.CreateRotationY(MathHelper.PiOver2)
                    * Matrix.CreateRotationZ(-MathHelper.PiOver2)
                    * Matrix.CreateTranslation(0, 0, -0.05f)
                };
            }
            if (slot >= _equipMap.Count)
                return null;
            return new Tuple<string,Matrix>(_equipMap[slot],_equipTMap[slot]);
        }

        /// <summary>
        /// Checks if specific item can be equipped by the player (based on level, class etc)
        /// </summary>
        /// <param name="Item">Item to check</param>
        /// <returns></returns>
        public bool CanEquip(Items.ItemEquip Item)
        {
            //TODO actual requirement checking
            return true;
        }
        /// <summary>
        /// Places the item in player's equipment slot, if possible
        /// </summary>
        /// <param name="Item">Item to equip</param>
        /// <param name="slot">Slot</param>
        public void EquipItem(Items.ItemEquip Item,int slot)
        {
            if (slot >= 0 && slot < this.Equipment.Length)
            {
                this.Equipment[slot] = Item;
                Tuple<string, Matrix> attach = GetEquipParent(slot);
                if (attach!=null)
                {

                    GameModel.Model m = Item.GetModel();
                    if (m != null)
                    {
                        this.Model.FindPart(attach.Item1).Append(m.Children[0], attach.Item2);
                        this.Model.Dirty = true;
                    }
                        
                }
                this.EquipItem(Item);
            }
        }
        /// <summary>
        /// Applies the stat bonuses
        /// </summary>
        /// <param name="Item"></param>
        private void EquipItem(Items.ItemEquip Item)
        {
            if (!CanEquip(Item))
                return;
            if (Item != null && Item.Bonuses!=null)
                foreach (Items.ItemBonus b in Item.Bonuses)
                    StatBonuses.Add(b);
            
        }
        /// <summary>
        /// Removes item from the corresponding equip slot
        /// </summary>
        /// <param name="Item">Item to remove - needed to undo stat bonuses</param>
        /// <param name="slot">Slot to remove from</param>
        public void UnequipItem(Items.ItemEquip Item,int slot)
        {
            if (slot >= 0 && slot < this.Equipment.Length)
            {
                this.Equipment[slot] = null;
                Tuple<string, Matrix> attach = GetEquipParent(slot);
                if (attach != null)
                {
                        this.Model.FindPart(attach.Item1).Children.Clear();

                    this.Model.Dirty = true;
                }
                this.UnequipItem(Item);
            }
        }
        /// <summary>
        /// Removes associated stat bonuses
        /// </summary>
        /// <param name="Item">Item to remove</param>
        public void UnequipItem(Items.ItemEquip Item)
        {
           
            if (Item == null)
                return;
            if(Item.Bonuses!=null)
            foreach (Items.ItemBonus b in Item.Bonuses)
                StatBonuses.Remove(b);
        }
        /// <summary>
        /// basic update logic
        /// </summary>
        /// <param name="dT">Time to update</param>
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
