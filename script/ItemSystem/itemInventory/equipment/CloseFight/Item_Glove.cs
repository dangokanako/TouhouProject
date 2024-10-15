using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_Glove", menuName = "Inventory/Item_Glove")]
public class Item_Glove : item
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
                PlayerHealthControl.instance._PlayerAtk += 5;
                PlayerHealthControl.instance.PlayerCollsionDef += 1;
                break;
            case ItemQuality.Normal:
                PlayerHealthControl.instance._PlayerAtk += 6;
                PlayerHealthControl.instance.PlayerCollsionDef += 1.3f;
                break;
            case ItemQuality.Good:
                PlayerHealthControl.instance._PlayerAtk += 7;
                PlayerHealthControl.instance.PlayerCollsionDef += 1.5f;
                break;
            case ItemQuality.Excellent:
                PlayerHealthControl.instance._PlayerAtk += 8;
                PlayerHealthControl.instance.PlayerCollsionDef += 1.7f;
                break;
            case ItemQuality.Legendary:
                PlayerHealthControl.instance._PlayerAtk += 9;
                PlayerHealthControl.instance.PlayerCollsionDef += 2f;
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
                PlayerHealthControl.instance._PlayerAtk -= 5;
                PlayerHealthControl.instance.PlayerCollsionDef -= 1;
                break;
            case ItemQuality.Normal:
                PlayerHealthControl.instance._PlayerAtk -= 6;
                PlayerHealthControl.instance.PlayerCollsionDef -= 1.3f;
                break;
            case ItemQuality.Good:
                PlayerHealthControl.instance._PlayerAtk -= 7;
                PlayerHealthControl.instance.PlayerCollsionDef -= 1.5f;
                break;
            case ItemQuality.Excellent:
                PlayerHealthControl.instance._PlayerAtk -= 8;
                PlayerHealthControl.instance.PlayerCollsionDef -= 1.7f;
                break;
            case ItemQuality.Legendary:
                PlayerHealthControl.instance._PlayerAtk -= 9;
                PlayerHealthControl.instance.PlayerCollsionDef -= 2f;
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
                return "装备\n持有时提升5点碰撞攻击，减少1点来自敌人的碰撞攻击";
            case ItemQuality.Normal:
                return "装备\n持有时提升6点碰撞攻击，减少1.3点来自敌人的碰撞攻击";
            case ItemQuality.Good:
                return "装备\n持有时提升7点碰撞攻击，减少1.5点来自敌人的碰撞攻击。";
            case ItemQuality.Excellent:
                return "装备\n持有时提升8点碰撞攻击，减少1.7点来自敌人的碰撞攻击。";
            case ItemQuality.Legendary:
                return "装备\n持有时提升9点碰撞攻击，减少2点来自敌人的碰撞攻击。";
            case ItemQuality.Null:
                return "装备\n持有时提升6点碰撞攻击，减少1.3点来自敌人的碰撞攻击";

        }
        return "itemGetQualityText()报错了";
    }
}