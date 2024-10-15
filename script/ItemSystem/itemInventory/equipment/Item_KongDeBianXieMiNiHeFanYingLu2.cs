using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_KongDeBianXieMiNiHeFanYingLu2", menuName = "Equipment/Item_KongDeBianXieMiNiHeFanYingLu2")]
public class Item_KongDeBianXieMiNiHeFanYingLu2 : item
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
                PlayerHealthControl.instance._playerSPRecover += 0.26f;
                break;
            case ItemQuality.Normal:
                PlayerHealthControl.instance._playerSPRecover += 0.34f;
                break;
            case ItemQuality.Good:
                PlayerHealthControl.instance._playerSPRecover += 0.4f;
                break;
            case ItemQuality.Excellent:
                PlayerHealthControl.instance._playerSPRecover += 0.46f;
                break;
            case ItemQuality.Legendary:
                PlayerHealthControl.instance._playerSPRecover += 0.54f;
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
                PlayerHealthControl.instance._playerSPRecover -= 0.26f;
                break;
            case ItemQuality.Normal:
                PlayerHealthControl.instance._playerSPRecover -= 0.34f;
                break;
            case ItemQuality.Good:
                PlayerHealthControl.instance._playerSPRecover -= 0.4f;
                break;
            case ItemQuality.Excellent:
                PlayerHealthControl.instance._playerSPRecover -= 0.46f;
                break;
            case ItemQuality.Legendary:
                PlayerHealthControl.instance._playerSPRecover -= 0.54f;
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
                return "持有时提供额外26%SP恢复速度";
            case ItemQuality.Normal:
                return "持有时提供额外34%SP恢复速度";
            case ItemQuality.Good:
                return "持有时提供额外40%SP恢复速度";
            case ItemQuality.Excellent:
                return "持有时提供额外46%SP恢复速度";
            case ItemQuality.Legendary:
                return "持有时提供额外54%SP恢复速度";
            case ItemQuality.Null:
                return "持有时提供额外34%SP恢复速度";

        }
        return "itemGetQualityText()报错了";
    }
}