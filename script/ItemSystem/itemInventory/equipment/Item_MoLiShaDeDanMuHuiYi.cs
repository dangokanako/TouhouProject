using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_MoLiShaDeDanMuHuiYi", menuName = "Equipment/Item_MoLiShaDeDanMuHuiYi")]
public class Item_MoLiShaDeDanMuHuiYi : item
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
                PlayerHealthControl.instance._SCDamageRate += 0.10f;
                break;
            case ItemQuality.Normal:
                PlayerHealthControl.instance._SCDamageRate += 0.12f;
                break;
            case ItemQuality.Good:
                PlayerHealthControl.instance._SCDamageRate += 0.14f;
                break;
            case ItemQuality.Excellent:
                PlayerHealthControl.instance._SCDamageRate += 0.16f;
                break;
            case ItemQuality.Legendary:
                PlayerHealthControl.instance._SCDamageRate += 0.18f;
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
                PlayerHealthControl.instance._SCDamageRate -= 0.10f;
                break;
            case ItemQuality.Normal:
                PlayerHealthControl.instance._SCDamageRate -= 0.12f;
                break;
            case ItemQuality.Good:
                PlayerHealthControl.instance._SCDamageRate -= 0.14f;
                break;
            case ItemQuality.Excellent:
                PlayerHealthControl.instance._SCDamageRate -= 0.16f;
                break;
            case ItemQuality.Legendary:
                PlayerHealthControl.instance._SCDamageRate -= 0.18f;
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
                return "持有时提供额外10%符卡类伤害";
            case ItemQuality.Normal:
                return "持有时提供额外12%符卡类伤害";
            case ItemQuality.Good:
                return "持有时提供额外14%符卡类伤害";
            case ItemQuality.Excellent:
                return "持有时提供额外16%符卡类伤害";
            case ItemQuality.Legendary:
                return "持有时提供额外18%符卡类伤害";
            case ItemQuality.Null:
                return "持有时提供额外12%符卡类伤害";

        }
        return "itemGetQualityText()报错了";
    }
}