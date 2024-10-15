using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_Equipment1", menuName = "Inventory/Item_Equipment1")]
public class Item_Equipment1 : item
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
                PlayerHealthControl.instance._PlayerAtk += 10;
                PlayerHealthControl.instance.PlayerCollsionCritical += 1;
                PlayerHealthControl.instance.PlayerCollsionDef += 2;
                break;
            case ItemQuality.Normal:
                PlayerHealthControl.instance._PlayerAtk += 11;
                PlayerHealthControl.instance.PlayerCollsionCritical += 1;
                PlayerHealthControl.instance.PlayerCollsionDef += 2f;
                break;
            case ItemQuality.Good:
                PlayerHealthControl.instance._PlayerAtk += 12;
                PlayerHealthControl.instance.PlayerCollsionCritical += 1;
                PlayerHealthControl.instance.PlayerCollsionDef += 2.5f;
                break;
            case ItemQuality.Excellent:
                PlayerHealthControl.instance._PlayerAtk += 13;
                PlayerHealthControl.instance.PlayerCollsionCritical += 1;
                PlayerHealthControl.instance.PlayerCollsionDef += 2.5f;
                break;
            case ItemQuality.Legendary:
                PlayerHealthControl.instance._PlayerAtk += 14;
                PlayerHealthControl.instance.PlayerCollsionCritical += 1;
                PlayerHealthControl.instance.PlayerCollsionDef += 3f;
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
                PlayerHealthControl.instance._PlayerAtk -= 10;
                PlayerHealthControl.instance.PlayerCollsionCritical -= 1;
                PlayerHealthControl.instance.PlayerCollsionDef -= 2;
                break;
            case ItemQuality.Normal:
                PlayerHealthControl.instance._PlayerAtk -= 11;
                PlayerHealthControl.instance.PlayerCollsionCritical -= 1;
                PlayerHealthControl.instance.PlayerCollsionDef -= 2f;
                break;
            case ItemQuality.Good:
                PlayerHealthControl.instance._PlayerAtk -= 12;
                PlayerHealthControl.instance.PlayerCollsionCritical -= 1;
                PlayerHealthControl.instance.PlayerCollsionDef -= 2.5f;
                break;
            case ItemQuality.Excellent:
                PlayerHealthControl.instance._PlayerAtk -= 13;
                PlayerHealthControl.instance.PlayerCollsionCritical -= 1;
                PlayerHealthControl.instance.PlayerCollsionDef -= 2.5f;
                break;
            case ItemQuality.Legendary:
                PlayerHealthControl.instance._PlayerAtk -= 14;
                PlayerHealthControl.instance.PlayerCollsionCritical -= 1;
                PlayerHealthControl.instance.PlayerCollsionDef -= 3f;
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
                return "装备\n持有时碰撞必定产生暴击，提升10点碰撞攻击，减少2点来自敌人的碰撞攻击";
            case ItemQuality.Normal:
                return "装备\n持有时碰撞必定产生暴击，提升11点碰撞攻击，减少2点来自敌人的碰撞攻击";
            case ItemQuality.Good:
                return "装备\n持有时碰撞必定产生暴击，提升12点碰撞攻击，减少2.5点来自敌人的碰撞攻击";
            case ItemQuality.Excellent:
                return "装备\n持有时碰撞必定产生暴击，提升13点碰撞攻击，减少2.5点来自敌人的碰撞攻击";
            case ItemQuality.Legendary:
                return "装备\n持有时碰撞必定产生暴击，提升14点碰撞攻击，减少3点来自敌人的碰撞攻击";
            case ItemQuality.Null:
                return "装备\n持有时碰撞必定产生暴击，提升12点碰撞攻击，减少2.5点来自敌人的碰撞攻击";
        }
        return "itemGetQualityText()报错了";
    }
}