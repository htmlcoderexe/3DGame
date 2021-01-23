using GameObject.Interactions;
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
        void AddToBasket(int Index, int amount)
        {

        }

        public ShopWindow( NPCShop shop)
        {
            this.Width = 360;
            this.Height = 420;
            int sellX = 0;
            int sellY = 0;
            for(int y=0;y<ShopHeight;y++)
                for(int x=0;x<ShopWidth;x++)
                {
                    int index = y * ShopWidth + x;
                    ItemSlot i;
                    if(index<shop.Selling.Count)
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
            //#TODO: sell menu - BuysAnything as toggleable option, default on, if on - items on the buying list have custom (higher) price
        }
    }
}
