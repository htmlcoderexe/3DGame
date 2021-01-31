using GameObject.Interactions;
using GameObject.MapEntities.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DGame.Scenes.GameplayAssets.Windows
{
    public class ShopWindow : GUI.Window
    {
        const int ShopWidth = 8;
        const int ShopHeight = 4;

        List<Tuple<GameObject.Item,int>> _basket;

        ItemSlot[] _basketslots = new ItemSlot[ShopWidth];
        private Player Player;
        GUI.Controls.RichTextDisplay TotalLabel;

        NPCShop shop;

        void RefreshBasket()
        {
            int tally=0;
            for(int i=0;i<ShopWidth;i++)
            {
                if(i>=_basket.Count)
                {
                    _basketslots[i].Item = null;
                }
                else
                {
                    _basketslots[i].Item = _basket[i].Item1;
                    tally += _basket[i].Item2 * _basket[i].Item1.StackSize;
                }

            }
            string moneystring = GameObject.Item.FormatMoney(tally);
            if (tally > Player.Money)
                moneystring = "^FF0000 " + GameObject.Item.FormatMoney(tally,false);

            TotalLabel.SetText(moneystring);
        }

        void AddToBasket(int Index, int amount)
        {
            GameObject.Item soldItem = (GameObject.Item)shop.Selling[Index].Item1.Clone();
            int price = shop.Selling[Index].Item2 - ((int)(shop.Selling[Index].Item2 * (float)shop.Selling[Index].Item3/100f));
            Tuple<GameObject.Item, int> removed = null;
            bool stacked = false;
            foreach (Tuple<GameObject.Item, int> lineItem in _basket)
            {
                if(lineItem.Item1.CanStackWith(soldItem))
                {
                    lineItem.Item1.StackSize += amount;
                    stacked = true;
                    //if stack size is below 1, mark item to remove from basket - should never happen but you never know...
                    if (lineItem.Item1.StackSize <= 0)
                        removed = lineItem;
                    break;
                }
            }
            //if new item and not full yet, add to basket
            if(!stacked && _basket.Count<=ShopWidth)
            {
                _basket.Add(new Tuple<GameObject.Item, int>(soldItem, price));
            }
            //remove zeroed item
            if (removed != null)
                _basket.Remove(removed);

            RefreshBasket();
        }

        void RemoveFromBasket(int Index, int amount)
        {
            ItemSlot slot = _basketslots[Index];
            if (slot.Item == null)
                return;
            Tuple<GameObject.Item, int> lineItem = _basket[Index];
            lineItem.Item1.StackSize -= amount;

            if (lineItem.Item1.StackSize <= 0)
                _basket.Remove(lineItem);
            RefreshBasket();
        }

        void SetupWares()
        {

            int sellX = 0;
            int sellY = 0;
            for (int y = 0; y < ShopHeight; y++)
                for (int x = 0; x < ShopWidth; x++)
                {
                    int index = y * ShopWidth + x;
                    ItemSlot i;
                    if (index < shop.Selling.Count)
                    {
                        i = new ItemSlot(shop.Selling[index].Item1);
                        i.OnClick += new ClickEventHandler((sender, m) => AddToBasket(index, 1));
                    }
                    else
                    {
                        i = new ItemSlot(null);
                    }
                    i.CanGrab = false;
                    i.CanPut = false;
                    i.X = x * i.Width + sellX;
                    i.Y = y * i.Height + sellY;

                    AddControl(i);
                }
        }

        void SetupBasket()
        {
            for(int i=0;i<8;i++)
            {
                int index = i;
                _basketslots[i] = new ItemSlot(null);
                _basketslots[i].OnClick += new ClickEventHandler((sender, m) => RemoveFromBasket(index, 1));
                _basketslots[i] .Y = ShopHeight * _basketslots[i].Height + 30;
                _basketslots[i] .X = i * _basketslots[i].Width;
                _basketslots[i] .CanGrab = false;
                _basketslots[i] .CanPut = false;
                AddControl(_basketslots[i]);
            }
            TotalLabel = new GUI.Controls.RichTextDisplay("0", 320, 16, WM);
            TotalLabel.X = 0;
            TotalLabel.Y = (ShopHeight+1) * _basketslots[0].Height+30+16;
            AddControl(TotalLabel);
        }

        public void Buy()
        {
            int tally = 0;
            List<GameObject.Item> basketitems = new List<GameObject.Item>();
            for (int i = 0; i < _basket.Count; i++)
            {
                
                basketitems.Add( _basket[i].Item1);
                tally += _basket[i].Item2 * _basket[i].Item1.StackSize;
                

            }
            if(tally>Player.Money)
            {
                Console.Write("^FF0000 Insufficient funds");
                return;
            }
            Player.Inventory.Prepare();
            bool full = false;
            foreach(GameObject.Item item in basketitems)
            {
                if (Player.Inventory.AddItem(item) != null)
                    full = true;
            }
            if(full)
            {
                Console.Write("^FF0000 Inventory full");
                return;
            }
            Player.Money -= tally;
            Player.Inventory.Commit();
            _basket.Clear();
            RefreshBasket();
        }

        public ShopWindow( NPCShop shop,GameObject.MapEntities.Actors.Player Player)
        {
            this.Width = 360;
            this.Height = 420;
            this.shop = shop;
            this.WM = Main.CurrentScene.WindowManager;
            this._basket = new List<Tuple<GameObject.Item, int>>();
            this._basketslots = new ItemSlot[8];
            this.Player = Player;
            SetupWares();
            SetupBasket();

            GUI.Controls.Button BuyButton = new GUI.Controls.Button("Purchase");
            BuyButton.Width = 150;
            BuyButton.Height = 32;
            BuyButton.X = 3;
            BuyButton.Y = (ShopHeight + 1) * _basketslots[0].Height + 30 + 16 + TotalLabel.Height + 5;
            BuyButton.OnClick += new ClickEventHandler((sender, m) => Buy());
            AddControl(BuyButton);
            //#TODO: sell menu - BuysAnything as toggleable option, default on, if on - items on the buying list have custom (higher) price
        }
    }
}
