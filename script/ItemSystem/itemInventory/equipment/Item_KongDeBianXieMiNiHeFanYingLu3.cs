using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_KongDeBianXieMiNiHeFanYingLu3", menuName = "Equipment/Item_KongDeBianXieMiNiHeFanYingLu3")]
public class Item_KongDeBianXieMiNiHeFanYingLu3 : item
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
                PlayerHealthControl.instance._playerSPRecover += 0.52f;
                break;
            case ItemQuality.Normal:
                PlayerHealthControl.instance._playerSPRecover += 0.68f;
                break;
            case ItemQuality.Good:
                PlayerHealthControl.instance._playerSPRecover += 0.8f;
                break;
            case ItemQuality.Excellent:
                PlayerHealthControl.instance._playerSPRecover += 0.92f;
                break;
            case ItemQuality.Legendary:
                PlayerHealthControl.instance._playerSPRecover += 1.08f;
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
                PlayerHealthControl.instance._playerSPRecover -= 0.52f;
                break;
            case ItemQuality.Normal:
                PlayerHealthControl.instance._playerSPRecover -= 0.68f;
                break;
            case ItemQuality.Good:
                PlayerHealthControl.instance._playerSPRecover -= 0.8f;
                break;
            case ItemQuality.Excellent:
                PlayerHealthControl.instance._playerSPRecover -= 0.92f;
                break;
            case ItemQuality.Legendary:
                PlayerHealthControl.instance._playerSPRecover -= 1.08f;
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
                return "持有时提供额外52%SP恢复速度";
            case ItemQuality.Normal:
                return "持有时提供额外68%SP恢复速度";
            case ItemQuality.Good:
                return "持有时提供额外80%SP恢复速度";
            case ItemQuality.Excellent:
                return "持有时提供额外92%SP恢复速度";
            case ItemQuality.Legendary:
                return "持有时提供额外108%SP恢复速度";
            case ItemQuality.Null:
                return "持有时提供额外68%SP恢复速度";

        }
        return "itemGetQualityText()报错了";
    }
}