using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_Chain", menuName = "Inventory/Item_Chain")]
public class Item_Chain : item
{
    public override bool Use()
    {
        SFXManger.instance.PlaySFX(3);
        return false;
    }

    public override bool Passive()
    {
        // 根据品质调整数值
        switch (this.itemCiti)
        {
            case ItemQuality.Inferior:
                PlayerHealthControl.instance._PlayerAtk += 1;
                PlayerHealthControl.instance.PlayerCollsionCritical += 1;
                break;
            case ItemQuality.Normal:
                PlayerHealthControl.instance._PlayerAtk += 2;
                PlayerHealthControl.instance.PlayerCollsionCritical += 1;
                break;
            case ItemQuality.Good:
                PlayerHealthControl.instance._PlayerAtk += 3;
                PlayerHealthControl.instance.PlayerCollsionCritical += 1;
                break;
            case ItemQuality.Excellent:
                PlayerHealthControl.instance._PlayerAtk += 4;
                PlayerHealthControl.instance.PlayerCollsionCritical += 1;
                break;
            case ItemQuality.Legendary:
                PlayerHealthControl.instance._PlayerAtk += 5;
                PlayerHealthControl.instance.PlayerCollsionCritical += 1;
                break;
        }

        return true;
    }

    public override bool discardPassive()
    {
        // 根据品质调整数值
        switch (this.itemCiti)
        {
            case ItemQuality.Inferior:
                PlayerHealthControl.instance._PlayerAtk -= 1;
                PlayerHealthControl.instance.PlayerCollsionCritical -= 1;
                break;
            case ItemQuality.Normal:
                PlayerHealthControl.instance._PlayerAtk -= 2;
                PlayerHealthControl.instance.PlayerCollsionCritical -= 1;
                break;
            case ItemQuality.Good:
                PlayerHealthControl.instance._PlayerAtk -= 3;
                PlayerHealthControl.instance.PlayerCollsionCritical -= 1;
                break;
            case ItemQuality.Excellent:
                PlayerHealthControl.instance._PlayerAtk -= 4;
                PlayerHealthControl.instance.PlayerCollsionCritical -= 1;
                break;
            case ItemQuality.Legendary:
                PlayerHealthControl.instance._PlayerAtk -= 5;
                PlayerHealthControl.instance.PlayerCollsionCritical -= 1;
                break;
        }
        return true;
    }

    // 获取品质文本
    public override string GetQualityText()
    {
        switch (this.itemCiti)
        {
            case ItemQuality.Inferior:
                return "装备\n持有时碰撞必定产生暴击，提升1点碰撞攻击";
            case ItemQuality.Normal:
                return "装备\n持有时碰撞必定产生暴击，提升2点碰撞攻击";
            case ItemQuality.Good:
                return "装备\n持有时碰撞必定产生暴击，提升3点碰撞攻击";
            case ItemQuality.Excellent:
                return "装备\n持有时碰撞必定产生暴击，提升4点碰撞攻击";
            case ItemQuality.Legendary:
                return "装备\n持有时碰撞必定产生暴击，提升5点碰撞攻击";
            case ItemQuality.Null:
                return "装备\n持有时碰撞必定产生暴击，提升2点碰撞攻击";
        }
        return "itemGetQualityText()报错了";
    }
}