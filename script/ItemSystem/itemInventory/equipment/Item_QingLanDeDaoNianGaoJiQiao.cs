using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_QingLanDeDaoNianGaoJiQiao", menuName = "Equipment/Item_QingLanDeDaoNianGaoJiQiao")]
public class Item_QingLanDeDaoNianGaoJiQiao : item
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
                PlayerHealthControl.instance._BluntDamageRate += 0.11f;
                break;
            case ItemQuality.Normal:
                PlayerHealthControl.instance._BluntDamageRate += 0.13f;
                break;
            case ItemQuality.Good:
                PlayerHealthControl.instance._BluntDamageRate += 0.15f;
                break;
            case ItemQuality.Excellent:
                PlayerHealthControl.instance._BluntDamageRate += 0.17f;
                break;
            case ItemQuality.Legendary:
                PlayerHealthControl.instance._BluntDamageRate += 0.19f;
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
                PlayerHealthControl.instance._BluntDamageRate -= 0.11f;
                break;
            case ItemQuality.Normal:
                PlayerHealthControl.instance._BluntDamageRate -= 0.13f;
                break;
            case ItemQuality.Good:
                PlayerHealthControl.instance._BluntDamageRate -= 0.15f;
                break;
            case ItemQuality.Excellent:
                PlayerHealthControl.instance._BluntDamageRate -= 0.17f;
                break;
            case ItemQuality.Legendary:
                PlayerHealthControl.instance._BluntDamageRate -= 0.19f;
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
                return "持有时提供额外11%钝器类伤害";
            case ItemQuality.Normal:
                return "持有时提供额外13%钝器类伤害";
            case ItemQuality.Good:
                return "持有时提供额外15%钝器类伤害";
            case ItemQuality.Excellent:
                return "持有时提供额外17%钝器类伤害";
            case ItemQuality.Legendary:
                return "持有时提供额外19%钝器类伤害";
            case ItemQuality.Null:
                return "持有时提供额外13%钝器类伤害";

        }
        return "itemGetQualityText()报错了";
    }
}