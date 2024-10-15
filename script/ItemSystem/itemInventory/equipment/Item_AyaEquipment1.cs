using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_AyaEquipment1", menuName = "Equipment/Item_AyaEquipment1")]
public class Item_AyaEquipment1 : item
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
                PlayerHealthControl.instance._PlayerDodge += 11;
                break;
            case ItemQuality.Normal:
                PlayerHealthControl.instance._PlayerDodge += 13;
                break;
            case ItemQuality.Good:
                PlayerHealthControl.instance._PlayerDodge += 15;
                break;
            case ItemQuality.Excellent:
                PlayerHealthControl.instance._PlayerDodge += 17;
                break;
            case ItemQuality.Legendary:
                PlayerHealthControl.instance._PlayerDodge += 20;
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
                PlayerHealthControl.instance._PlayerDodge -= 11;
                break;
            case ItemQuality.Normal:
                PlayerHealthControl.instance._PlayerDodge -= 13;
                break;
            case ItemQuality.Good:
                PlayerHealthControl.instance._PlayerDodge -= 15;
                break;
            case ItemQuality.Excellent:
                PlayerHealthControl.instance._PlayerDodge -= 17;
                break;
            case ItemQuality.Legendary:
                PlayerHealthControl.instance._PlayerDodge -= 20;
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
                return "持有时可以提供11%回避率（所有伤害）（但最大回避率为50%）";
            case ItemQuality.Normal:
                return "持有时可以提供13%回避率（所有伤害）（但最大回避率为50%）";
            case ItemQuality.Good:
                return "持有时可以提供15%回避率（所有伤害）（但最大回避率为50%）";
            case ItemQuality.Excellent:
                return "持有时可以提供17%回避率（所有伤害）（但最大回避率为50%）";
            case ItemQuality.Legendary:
                return "持有时可以提供20%回避率（所有伤害）（但最大回避率为50%）";
            case ItemQuality.Null:
                return "持有时可以提供13%回避率（所有伤害）（但最大回避率为50%）";

        }
        return "itemGetQualityText()报错了";
    }
}