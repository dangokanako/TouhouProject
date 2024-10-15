using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_FlandreDeHuDieJie", menuName = "Equipment/Item_FlandreDeHuDieJie")]
public class Item_FlandreDeHuDieJie : item
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
                PlayerHealthControl.instance.CriticalRate += 0.04f;
                PlayerHealthControl.instance.CriticalDamage += 0.1f;
                break;
            case ItemQuality.Normal:
                PlayerHealthControl.instance.CriticalRate += 0.05f;
                PlayerHealthControl.instance.CriticalDamage += 0.12f;
                break;
            case ItemQuality.Good:
                PlayerHealthControl.instance.CriticalRate += 0.06f;
                PlayerHealthControl.instance.CriticalDamage += 0.14f;
                break;
            case ItemQuality.Excellent:
                PlayerHealthControl.instance.CriticalRate += 0.07f;
                PlayerHealthControl.instance.CriticalDamage += 0.16f;
                break;
            case ItemQuality.Legendary:
                PlayerHealthControl.instance.CriticalRate += 0.08f;
                PlayerHealthControl.instance.CriticalDamage += 0.18f;
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
                PlayerHealthControl.instance.CriticalRate -= 0.04f;
                PlayerHealthControl.instance.CriticalDamage -= 0.1f;
                break;
            case ItemQuality.Normal:
                PlayerHealthControl.instance.CriticalRate -= 0.05f;
                PlayerHealthControl.instance.CriticalDamage -= 0.12f;
                break;
            case ItemQuality.Good:
                PlayerHealthControl.instance.CriticalRate -= 0.06f;
                PlayerHealthControl.instance.CriticalDamage -= 0.14f;
                break;
            case ItemQuality.Excellent:
                PlayerHealthControl.instance.CriticalRate -= 0.07f;
                PlayerHealthControl.instance.CriticalDamage -= 0.16f;
                break;
            case ItemQuality.Legendary:
                PlayerHealthControl.instance.CriticalRate -= 0.08f;
                PlayerHealthControl.instance.CriticalDamage -= 0.18f;
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
                return "持有时提供4%暴击率和10%暴击伤害";
            case ItemQuality.Normal:
                return "持有时提供5%暴击率和12%暴击伤害";
            case ItemQuality.Good:
                return "持有时提供6%暴击率和14%暴击伤害";
            case ItemQuality.Excellent:
                return "持有时提供7%暴击率和16%暴击伤害";
            case ItemQuality.Legendary:
                return "持有时提供8%暴击率和18%暴击伤害";
            case ItemQuality.Null:
                return "持有时提供5%暴击率和12%暴击伤害";

        }
        return "itemGetQualityText()报错了";
    }
}