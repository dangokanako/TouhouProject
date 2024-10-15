using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;




[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item")]
public class item : ScriptableObject
{
    public ItemSlotEnum itemSlot;

    public int itemId;
    public string itemName;
    // 物品是否可堆叠 1可堆叠 0不可堆叠
    public int isDuidie;
    // 最大堆叠数量
    public int MaxDuidie;
    public Color itemColor;
    public Sprite itemImage;

    // 物品价格(在商店时可读取该价格？)
    public float price;

    // 物品描述
    public string itemInfoText;
    // 物品备注
    public string itemRemarkText;

    // 是否为消耗品。使用后减少数量。
    public bool isXiaohao;
    // 是否为被动装备。如果是被动装备务必勾选。
    public bool isPassive;
    // 是否为材料，参与合成。实际上只影响是否能放入合成栏。
    public bool isMaterial;
    // 是否是蓄力使用武器
    public bool isChargeWeapon;

    // 是否是升级品，可以立即使用（仅商店永久升级使用）
    public bool isUseImmediately;


    [Header("装备品质相关")]
    // 是否有品质值。有品质的装备使用。
    public bool hasQuality;
    // 物品品质  0-99的数字
    // 0-24 劣质
    // 25-59 普通
    // 60-84 良好
    // 85-94 优秀
    // 95-99 传说
    public int Quality;
    // 物品词条
    public ItemQuality itemCiti;
    public virtual bool Use()
    {
        return false;
        // 物品使用函数
        // 由子类重写
    }

    /// <summary>
    /// 蓄力物品使用函数，请注意，该函数为每帧执行！
    /// </summary>
    /// <returns></returns>
    public virtual bool UseChargePower()
    {
        return false;
        // 蓄力物品使用函数，请注意，该函数为帧执行！
        // 由子类重写
    }

    public virtual bool Passive()
    {
        // 被动装备函数
        // 由子类重写
        return false;
    }
    public virtual bool discardPassive()
    {
        // 被动装备函数
        // 由子类重写
        return false;
    }
    public virtual string GetQualityText()
    {
        // 品质相关获取文本
        // 由子类重写
        return "";
    }

    [Header("永续符卡装备")]
    // 永续符卡相关
    public bool isEquipmentSC;
    public int isEquipmentSCLevel;
    public int maxEquipmentSCLevel;
    protected bool statsUpdate = true;
    public virtual string GetEquipmentSCText()
    {
        // 永续符卡获取文本
        // 由子类重写
        return "";
    }
    public virtual void SetEquipmentSCStats()
    {
        // 永续符卡获取文本
        // 由子类重写
    }
    public virtual bool UpgradeEquipmentSC()
    {
        // 永续符卡升级函数
        // 由子类重写
        if (!isEquipmentSC)
            return false;

        if (isEquipmentSCLevel >= maxEquipmentSCLevel)
            return false;

        isEquipmentSCLevel++;
        this.SetEquipmentSCStats();
        // statsUpdate = true;
        return true;
    }
    public virtual item Clone()
    {
        item newItem = ScriptableObject.CreateInstance(this.GetType()) as item;

        newItem.itemSlot = this.itemSlot;
        newItem.itemId = this.itemId;
        newItem.itemName = string.Copy(this.itemName);
        newItem.isDuidie = this.isDuidie;
        newItem.MaxDuidie = this.MaxDuidie;
        newItem.itemColor = this.itemColor;
        newItem.itemImage = this.itemImage;
        newItem.itemInfoText = string.Copy(this.itemInfoText);
        newItem.itemRemarkText = string.Copy(this.itemRemarkText);
        newItem.isXiaohao = this.isXiaohao;
        newItem.isPassive = this.isPassive;
        newItem.isMaterial = this.isMaterial;
        newItem.isUseImmediately = this.isUseImmediately;
        newItem.hasQuality = this.hasQuality;
        newItem.Quality = this.Quality;
        newItem.itemCiti = this.itemCiti;
        newItem.isEquipmentSC = this.isEquipmentSC;
        newItem.isEquipmentSCLevel = this.isEquipmentSCLevel;
        newItem.maxEquipmentSCLevel = this.maxEquipmentSCLevel;
        newItem.statsUpdate = this.statsUpdate;
        newItem.price = this.price;


        return newItem;
    }
}


public enum ItemSlotEnum
{
    // 默认0
    normal = 0,
    // ALL，所有，没错，手上也包括，道具栏也包括
    All = 1,
    // ALL，包括手上，除了道具栏
    AllExceptWarehouse = 2,
    // ALL，除了道具栏和手上。也就是所有装备栏
    AllEquipment = 3,
    // 仅限红色 
    OnlyRed = 4,
    // 仅限蓝色
    OnlyBlue = 5,
    // 仅限绿色
    OnlyGreen = 6,
}


public enum ItemQuality
{
    Null = 0,

    // 棕色 #544742
    Inferior = 1,


    Normal = 2,


    // 绿色 #7DFA00
    Good = 3,

    // 蓝色 #1C14FF
    Excellent = 4,

    // 橙色 #FFDB57
    Legendary = 5,
}