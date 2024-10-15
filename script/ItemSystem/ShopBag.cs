using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Runtime.CompilerServices;
// using Microsoft.Unity.VisualStudio.Editor;

public class ShopBag : ItemBag
{
    [Header("商店显示组件")]
    // 商店物品的价格
    [SerializeField] private TMP_Text shopPriceText;
    // 普通P点和超级P点的图标
    [SerializeField] private UnityEngine.UI.Image priceImage;
    // 一个指向ShopPanel的引用
    public ShopPanel shopPanel;

    public int currencytype = 0;
    // 设置价格
    public void SetPrice(int price)
    {
        shopPriceText.text = price.ToString();
    }
    // 设置价格颜色
    public void SetPriceColor(bool isEnough)
    {
        shopPriceText.color = isEnough ? Color.black : Color.gray;
    }
    void Start()
    {
    }

    // 点击商店页面的物品
    public void OnClickShop()
    {
        // 如果鼠标上有物品，那么直接返回
        if (ItemControl.instance.hoverItem.itemId != 0)
            return;


        // 如果鼠标上没有物品，目标上有物品，那么执行购买操作
        if (ItemControl.instance.hoverItem.itemId == 0 && this.itemInfo.itemId != 0)
        {

            // 计算价格是否能购买
            int price = int.Parse(shopPriceText.text);
            // 货币类型 普通P点情况
            // 获取priceImage路径
            if (currencytype == 0)
            {
                if (price > AssetControl.instance.currentPoint)
                {
                    SFXManger.instance.PlaySFX(3);
                    return;
                }
            }
            else if (currencytype == 1)
            {
                if (price > AssetControl.instance.currentSuperPoint)
                {
                    SFXManger.instance.PlaySFX(3);
                    return;
                }
            }
            // 播放音效
            SFXManger.instance.PlaySFX(7);

            // 扣除P点
            if (currencytype == 0)
                AssetControl.instance.ReducePoint(price);
            else if (currencytype == 1)
                AssetControl.instance.ReduceSuperPoint(price);


            // 如果是角色升级，那么直接执行物品使用操作，并返回。
            if (this.itemInfo.isUseImmediately)
            {
                this.itemInfo.Use();

                // 移除物品
                this.isShowItemInfo = false;
                this.gameObject.SetActive(false);

                // 在商店列表中移除永久升级品
                // 这里仅靠ITEMID和PRICE锁定对象。应该不会有问题……吧。
                var ShopItem = this.shopPanel.shopItems.Find(x => x.item.itemId == this.itemInfo.itemId && x.price == price);
                // 设置已经卖出的标志
                ShopControl.instance.PatchouliShopData[ShopItem.positionX][ShopItem.positionY] = true;
                // 移除对象 
                shopPanel.shopItems.Remove(ShopItem);


                // TODO 重新加载商店，耍赖写法，以后要改
                ShopControl.instance.LoadShop(ShopControl.instance.PatchouliShop);

                return;
            }


            // 设置鼠标上的物品
            // ItemControl.instance.hoverItem = this.itemInfo;
            // ItemControl.instance.SethoverItemCountText(this.itemCount);
            AssetControl.instance.DropItem(PlayerHealthControl.instance.transform.position, ItemControl.instance.itemGuide[this.itemInfo.itemId]);



            // 图片切换
            ItemControl.instance.hoverImage.sprite = this.itemInfo.itemImage;

            // 移除物品
            this.isShowItemInfo = false;
            this.gameObject.SetActive(false);
            // this.SetNoItem();


            // TODO 重新加载商店，耍赖写法，以后要改
            ShopControl.instance.LoadShop(ShopControl.instance.SakuyaShop);

        }
    }

    internal void SetCurrencyType(int currencyType)
    {
        currencytype = currencyType;

        if (currencyType == 0)
            // 设置为路径文件夹Asset/img/point的图片
            priceImage.sprite = Resources.Load<Sprite>("item/point");
        else if (currencyType == 1)
            // 设置为路径文件夹Asset/img/superPoint的图片
            priceImage.sprite = Resources.Load<Sprite>("item/superPoint");
    }
}

