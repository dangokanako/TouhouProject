using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_AyaEquipment4", menuName = "Equipment/Item_AyaEquipment4")]
public class Item_AyaEquipment4 : item
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
                PlayerHealthControl.instance.BulletSpeedRate += 0.13f;
                break;
            case ItemQuality.Normal:
                PlayerHealthControl.instance.BulletSpeedRate += 0.17f;
                break;
            case ItemQuality.Good:
                PlayerHealthControl.instance.BulletSpeedRate += 0.2f;
                break;
            case ItemQuality.Excellent:
                PlayerHealthControl.instance.BulletSpeedRate += 0.23f;
                break;
            case ItemQuality.Legendary:
                PlayerHealthControl.instance.BulletSpeedRate += 0.27f;
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
                PlayerHealthControl.instance.BulletSpeedRate -= 0.13f;
                break;
            case ItemQuality.Normal:
                PlayerHealthControl.instance.BulletSpeedRate -= 0.17f;
                break;
            case ItemQuality.Good:
                PlayerHealthControl.instance.BulletSpeedRate -= 0.2f;
                break;
            case ItemQuality.Excellent:
                PlayerHealthControl.instance.BulletSpeedRate -= 0.23f;
                break;
            case ItemQuality.Legendary:
                PlayerHealthControl.instance.BulletSpeedRate -= 0.27f;
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
                return "持有时提供额外13%主动弹幕初始速度";
            case ItemQuality.Normal:
                return "持有时提供额外17%主动弹幕初始速度";
            case ItemQuality.Good:
                return "持有时提供额外20%主动弹幕初始速度";
            case ItemQuality.Excellent:
                return "持有时提供额外23%主动弹幕初始速度";
            case ItemQuality.Legendary:
                return "持有时提供额外27%主动弹幕初始速度";
            case ItemQuality.Null:
                return "持有时提供额外17%主动弹幕初始速度";

        }
        return "itemGetQualityText()报错了";
    }
}