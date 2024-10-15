using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;

public class ShopControl : MonoBehaviour
{
    // 商店格子
    public ShopBag[] shopsBag;

    public static ShopControl instance;
    void Awake()
    {
        instance = this;

        // 初始化商店
        SakuyaShop.shopItems = new List<ShopItem>
        {
            new ShopItem() { positionX = 0, positionY = 0, currencyType = 0, price = 5, item = ItemControl.instance.itemGuide[87], itemCount = 1 },
            new ShopItem() { positionX = 0, positionY = 1, currencyType = 0, price = 15, item = ItemControl.instance.itemGuide[36], itemCount = 1 },
            new ShopItem() { positionX = 0, positionY = 2, currencyType = 0, price = 2, item = ItemControl.instance.itemGuide[1], itemCount = 1 },
            new ShopItem() { positionX = 0, positionY = 3, currencyType = 0, price = 3, item = ItemControl.instance.itemGuide[2], itemCount = 1 },
            new ShopItem() { positionX = 0, positionY = 4, currencyType = 0, price = 3, item = ItemControl.instance.itemGuide[28], itemCount = 1 },
            new ShopItem() { positionX = 0, positionY = 5, currencyType = 0, price = 1, item = ItemControl.instance.itemGuide[45], itemCount = 1 },
            new ShopItem() { positionX = 0, positionY = 6, currencyType = 0, price = 800, item = ItemControl.instance.itemGuide[109], itemCount = 1 },

            new ShopItem() { positionX = 1, positionY = 0, currencyType = 0, price = 3, item = ItemControl.instance.itemGuide[19], itemCount = 1 },
            new ShopItem() { positionX = 1, positionY = 1, currencyType = 0, price = 12, item = ItemControl.instance.itemGuide[20], itemCount = 1 },
            new ShopItem() { positionX = 1, positionY = 2, currencyType = 0, price = 3, item = ItemControl.instance.itemGuide[3], itemCount = 1 },
            new ShopItem() { positionX = 1, positionY = 3, currencyType = 0, price = 12, item = ItemControl.instance.itemGuide[39], itemCount = 1 },
            new ShopItem() { positionX = 1, positionY = 4, currencyType = 0, price = 2, item = ItemControl.instance.itemGuide[13], itemCount = 1 },
            new ShopItem() { positionX = 1, positionY = 5, currencyType = 0, price = 12, item = ItemControl.instance.itemGuide[21], itemCount = 1 },

            new ShopItem() { positionX = 2, positionY = 0, currencyType = 0, price = 95, item = ItemControl.instance.itemGuide[37], itemCount = 1 },
            new ShopItem() { positionX = 2, positionY = 1, currencyType = 0, price = 2, item = ItemControl.instance.itemGuide[12], itemCount = 1 },
            new ShopItem() { positionX = 2, positionY = 2, currencyType = 0, price = 12, item = ItemControl.instance.itemGuide[38], itemCount = 1 },

            new ShopItem() { positionX = 2, positionY = 3, currencyType = 0, price = 100, item = ItemControl.instance.itemGuide[11], itemCount = 1 },
            new ShopItem() { positionX = 2, positionY = 4, currencyType = 0, price = 100, item = ItemControl.instance.itemGuide[22], itemCount = 1 },
            new ShopItem() { positionX = 2, positionY = 5, currencyType = 0, price = 100, item = ItemControl.instance.itemGuide[23], itemCount = 1 },


            new ShopItem() { positionX = 3, positionY = 1, currencyType = 0, price = 5, item = ItemControl.instance.itemGuide[31], itemCount = 1 },
            new ShopItem() { positionX = 3, positionY = 0, currencyType = 0, price = 5, item = ItemControl.instance.itemGuide[42], itemCount = 1 },
            new ShopItem() { positionX = 3, positionY = 2, currencyType = 0, price = 30, item = ItemControl.instance.itemGuide[8], itemCount = 1 },
            new ShopItem() { positionX = 3, positionY = 3, currencyType = 0, price = 20, item = ItemControl.instance.itemGuide[79], itemCount = 1 },
            new ShopItem() { positionX = 3, positionY = 4, currencyType = 0, price = 120, item = ItemControl.instance.itemGuide[76], itemCount = 1 },
            new ShopItem() { positionX = 3, positionY = 5, currencyType = 0, price = 120, item = ItemControl.instance.itemGuide[81], itemCount = 1 },

            // new ShopItem() { positionX = 3, positionY = 3, currencyType = 0, price = 1, item = ItemControl.instance.itemGuide[32], itemCount = 1 },


        };

        PatchouliShop.shopItems = new List<ShopItem>
        {
            new ShopItem() { positionX = 0, positionY = 0, currencyType = 1, price = 20, item = ItemControl.instance.itemGuide[34], itemCount = 1 },
            new ShopItem() { positionX = 0, positionY = 1, currencyType = 1, price = 80, item = ItemControl.instance.itemGuide[34], itemCount = 1 },
            new ShopItem() { positionX = 0, positionY = 2, currencyType = 1, price = 160, item = ItemControl.instance.itemGuide[34], itemCount = 1 },
            new ShopItem() { positionX = 0, positionY = 3, currencyType = 1, price = 320, item = ItemControl.instance.itemGuide[34], itemCount = 1 },
            new ShopItem() { positionX = 0, positionY = 4, currencyType = 1, price = 20, item = ItemControl.instance.itemGuide[35], itemCount = 1 },
            new ShopItem() { positionX = 0, positionY = 5, currencyType = 1, price = 60, item = ItemControl.instance.itemGuide[35], itemCount = 1 },
            new ShopItem() { positionX = 0, positionY = 6, currencyType = 1, price = 240, item = ItemControl.instance.itemGuide[35], itemCount = 1 },


            new ShopItem() { positionX = 1, positionY = 0, currencyType = 1, price = 20, item = ItemControl.instance.itemGuide[41], itemCount = 1 },
            new ShopItem() { positionX = 1, positionY = 1, currencyType = 1, price = 40, item = ItemControl.instance.itemGuide[41], itemCount = 1 },
            new ShopItem() { positionX = 1, positionY = 2, currencyType = 1, price = 80, item = ItemControl.instance.itemGuide[41], itemCount = 1 },
            new ShopItem() { positionX = 1, positionY = 3, currencyType = 1, price = 160, item = ItemControl.instance.itemGuide[41], itemCount = 1 },
            new ShopItem() { positionX = 1, positionY = 4, currencyType = 1, price = 30, item = ItemControl.instance.itemGuide[40], itemCount = 1 },
            new ShopItem() { positionX = 1, positionY = 5, currencyType = 1, price = 150, item = ItemControl.instance.itemGuide[40], itemCount = 1 },
            new ShopItem() { positionX = 1, positionY = 6, currencyType = 1, price = 600, item = ItemControl.instance.itemGuide[40], itemCount = 1 },



            new ShopItem() { positionX = 2, positionY = 1, currencyType = 1, price = 100, item = ItemControl.instance.itemGuide[115], itemCount = 1 },
            new ShopItem() { positionX = 2, positionY = 2, currencyType = 1, price = 500, item = ItemControl.instance.itemGuide[115], itemCount = 1 },
            new ShopItem() { positionX = 2, positionY = 0, currencyType = 1, price = 20, item = ItemControl.instance.itemGuide[24], itemCount = 1 },
            new ShopItem() { positionX = 2, positionY = 3, currencyType = 1, price = 20, item = ItemControl.instance.itemGuide[111], itemCount = 1 },
            new ShopItem() { positionX = 2, positionY = 4, currencyType = 1, price = 60, item = ItemControl.instance.itemGuide[111], itemCount = 1 },
            new ShopItem() { positionX = 2, positionY = 5, currencyType = 1, price = 180, item = ItemControl.instance.itemGuide[111], itemCount = 1 },
            new ShopItem() { positionX = 2, positionY = 6, currencyType = 1, price = 540, item = ItemControl.instance.itemGuide[111], itemCount = 1 },


            new ShopItem() { positionX = 3, positionY = 0, currencyType = 1, price = 20, item = ItemControl.instance.itemGuide[43], itemCount = 1 },
            new ShopItem() { positionX = 3, positionY = 1, currencyType = 1, price = 70, item = ItemControl.instance.itemGuide[43], itemCount = 1 },
            new ShopItem() { positionX = 3, positionY = 2, currencyType = 1, price = 150, item = ItemControl.instance.itemGuide[43], itemCount = 1 },

            new ShopItem() { positionX = 3, positionY = 3, currencyType = 1, price = 20, item = ItemControl.instance.itemGuide[110], itemCount = 1 },
            new ShopItem() { positionX = 3, positionY = 4, currencyType = 1, price = 50, item = ItemControl.instance.itemGuide[110], itemCount = 1 },
            new ShopItem() { positionX = 3, positionY = 5, currencyType = 1, price = 150, item = ItemControl.instance.itemGuide[110], itemCount = 1 },



        };
    }
    // 帕秋莉商店标识。如果为TRUE说明已经卖出去了。（在读档时使用这个 ，刷新商店哪些已经卖出去了）
    [SerializeField]
    public bool[][] PatchouliShopData = new bool[4][]{
        new bool[7],
        new bool[7],
        new bool[7],
        new bool[7]
    };
    public ShopPanel SakuyaShop = new ShopPanel();
    public ShopPanel PatchouliShop = new ShopPanel();
    // 根据输入，展示商店面板
    public void LoadShop(ShopPanel shopPanel)
    {
        // 清空所有格子
        for (int i = 0; i < shopsBag.Count(); i++)
        {
            shopsBag[i].gameObject.SetActive(false);
        }



        // 根据shopPanel显示对应的格子，并设置对应的物品
        for (int i = 0; i < shopPanel.shopItems.Count; i++)
        {



            shopsBag[shopPanel.shopItems[i].GetIndex()].gameObject.SetActive(true);

            var shopBag = shopsBag[shopPanel.shopItems[i].GetIndex()];

            // 清空原有物品
            shopBag.deleteItem();

            // 从存档数据排除已经买过的东西
            if (shopPanel == PatchouliShop)
            {
                if (PatchouliShopData[shopPanel.shopItems[i].positionX][shopPanel.shopItems[i].positionY])
                {
                    shopsBag[shopPanel.shopItems[i].GetIndex()].gameObject.SetActive(false);
                    Debug.Log("111");
                    continue;
                }
            }


            // 设置价格
            shopBag.SetPrice(shopPanel.shopItems[i].price);
            if (shopPanel.shopItems[i].currencyType == 0 && shopPanel.shopItems[i].price > AssetControl.instance.currentPoint)
            {
                shopBag.SetPriceColor(false);
            }
            else if (shopPanel.shopItems[i].currencyType == 1 && shopPanel.shopItems[i].price > AssetControl.instance.currentSuperPoint)
            {
                shopBag.SetPriceColor(false);
            }
            else
            {
                shopBag.SetPriceColor(true);
            }

            // 设置货币类型
            shopBag.SetCurrencyType(shopPanel.shopItems[i].currencyType);

            // 设置物品
            shopBag.AddItem(shopPanel.shopItems[i].item, shopPanel.shopItems[i].itemCount);

            // 指向引用
            shopBag.shopPanel = shopPanel;
        }



    }
}


// 商店面板格式
public class ShopPanel
{
    public string shopName;
    public List<ShopItem> shopItems;
}

public class ShopItem
{
    public int GetIndex()
    {
        return positionX * 7 + positionY;
    }
    // 在商店的位置
    public int positionX;
    public int positionY;
    // 货币种类 0为通常P点，1为超级P点
    public int currencyType;
    // 价格
    public int price;
    // 物品
    public item item;
    // 物品个数
    public int itemCount;
}