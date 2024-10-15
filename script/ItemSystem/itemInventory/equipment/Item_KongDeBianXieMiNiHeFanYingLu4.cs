using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_KongDeBianXieMiNiHeFanYingLu4", menuName = "Equipment/Item_KongDeBianXieMiNiHeFanYingLu4")]
public class Item_KongDeBianXieMiNiHeFanYingLu4 : item
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
                PlayerHealthControl.instance._playerSPRecover += 1.04f;
                break;
            case ItemQuality.Normal:
                PlayerHealthControl.instance._playerSPRecover += 1.36f;
                break;
            case ItemQuality.Good:
                PlayerHealthControl.instance._playerSPRecover += 1.6f;
                break;
            case ItemQuality.Excellent:
                PlayerHealthControl.instance._playerSPRecover += 1.84f;
                break;
            case ItemQuality.Legendary:
                PlayerHealthControl.instance._playerSPRecover += 2.16f;
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
                PlayerHealthControl.instance._playerSPRecover -= 1.04f;
                break;
            case ItemQuality.Normal:
                PlayerHealthControl.instance._playerSPRecover -= 1.36f;
                break;
            case ItemQuality.Good:
                PlayerHealthControl.instance._playerSPRecover -= 1.6f;
                break;
            case ItemQuality.Excellent:
                PlayerHealthControl.instance._playerSPRecover -= 1.84f;
                break;
            case ItemQuality.Legendary:
                PlayerHealthControl.instance._playerSPRecover -= 2.16f;
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
                return "持有时提供额外104%SP恢复速度";
            case ItemQuality.Normal:
                return "持有时提供额外136%SP恢复速度";
            case ItemQuality.Good:
                return "持有时提供额外160%SP恢复速度";
            case ItemQuality.Excellent:
                return "持有时提供额外184%SP恢复速度";
            case ItemQuality.Legendary:
                return "持有时提供额外216%SP恢复速度";
            case ItemQuality.Null:
                return "持有时提供额外136%SP恢复速度";

        }
        return "itemGetQualityText()报错了";
    }
}