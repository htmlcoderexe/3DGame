using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameObject.Items
{
    public class Inventory
    {
        public Item[] Items;
        Item[] Backup;

        public bool Changed;
        public Inventory(int Size)
        {
            this.Items = new Item[Size];
            this.Backup = new Item[Size];
        }
        public void Prepare()
        {
            this.Items.CopyTo(Backup, 0);

        }
        public void Rollback()
        {
            this.Items.CopyTo(Backup, 0);

        }
        public void Commit()
        {
            for(int i=0;i<this.Backup.Length;i++)
            {
                Item slot = this.Backup[i];
                if (slot == null)
                    continue;
                if (slot.StackSize < 1)
                    this.Backup[i] = null;
            }
            this.Backup.CopyTo(Items, 0);
            this.Changed = true;

        }
        public Item AddItem(Item Item)
        {
            int firstempty = -1;
            for (int i = 0; i < this.Backup.Length; i++)
            {
                if (this.Backup[i] == null)
                {
                    firstempty = i;
                    break;
                }

            }
            int firstofkind = -1;
            for (int i = 0; i < this.Backup.Length; i++)
            {
                if (this.Backup[i] != null && this.Backup[i].CanStackWith(Item))
                {
                    firstofkind = i;
                    break;
                }


            }
            if (firstofkind != -1)
            {
                this.Backup[firstofkind].StackSize += Item.StackSize;
                return null;
            }
            if (firstempty != -1)
            {
                this.Backup[firstempty] = Item;
                return null;
            }
            return Item;
        }
        public Item AddItem(Item Item, int Position)
        {
            if (Position >= this.Items.Length)
                return Item;
            if (Position < 0)
                return Item;
            if (this.Backup[Position] == null)
            {
                this.Backup[Position] = Item;
                return null;
            }
            else
            {
                if (this.Backup[Position].CanStackWith(Item))
                {
                    int totalstack = this.Backup[Position].StackSize + Item.StackSize;
                    // if(totalstack<=Item.
                    this.Backup[Position].StackSize = totalstack;
                    return null;
                }
                return Item;

            }
            return null;
        }

        public Item FindItem(Item Item)
        {
            int firstofkind = -1;
            for (int i = 0; i < this.Backup.Length; i++)
            {
                if (this.Backup[i] != null && this.Backup[i].CanStackWith(Item))
                {
                    firstofkind = i;
                    break;
                }


            }
            if (firstofkind != -1)
            {
                return this.Backup[firstofkind];
            }
            return null;
        }

        public int CountItem(Item Item)
        {
            int result = 0;
            for (int i = 0; i < this.Backup.Length; i++)
            {
                if (this.Backup[i] != null && this.Backup[i].CanStackWith(Item))
                {
                    result += this.Backup[i].StackSize;
                }
            }
            return result;
        }
        public bool RemoveItem(Item Item, int Amount)
        {
            //check if inventory in fact contains enough of the item
            if (CountItem(Item) < Amount)
                return false;


            for (int i = 0; i < this.Backup.Length; i++)
            {   //check if we can take from current slot
                if (this.Backup[i] != null && this.Backup[i].CanStackWith(Item))
                {
                    int current= this.Backup[i].StackSize;
                    //this stack is enough
                    if (current>Amount)
                    {
                        this.Backup[i].StackSize -= Amount;
                        Amount = 0;
                        return true;
                    }
                    //else kill this stack and carry on with removing the rest
                    Amount -= current;
                    this.Backup[i] = null;
                }
            }

            return true;
        }
    }
}
