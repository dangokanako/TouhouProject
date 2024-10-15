using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_Broom", menuName = "Inventory/Item_Broom")]
public class Item_Broom : item
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
                MainPlayer.instance.MoveSpeed += 0.1f;
                MainPlayer.instance.DashSpeed += 0.3f;
                MainPlayer.instance.DashSPRate += 0.7f;
                break;
            case ItemQuality.Normal:
                MainPlayer.instance.MoveSpeed += 0.1f;
                MainPlayer.instance.DashSpeed += 0.4f;
                MainPlayer.instance.DashSPRate += 0.6f;
                break;
            case ItemQuality.Good:
                MainPlayer.instance.MoveSpeed += 0.1f;
                MainPlayer.instance.DashSpeed += 0.5f;
                MainPlayer.instance.DashSPRate += 0.5f;
                break;
            case ItemQuality.Excellent:
                MainPlayer.instance.MoveSpeed += 0.15f;
                MainPlayer.instance.DashSpeed += 0.6f;
                MainPlayer.instance.DashSPRate += 0.5f;
                break;
            case ItemQuality.Legendary:
                MainPlayer.instance.MoveSpeed += 0.25f;
                MainPlayer.instance.DashSpeed += 0.7f;
                MainPlayer.instance.DashSPRate += 0.4f;
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
                MainPlayer.instance.MoveSpeed -= 0.1f;
                MainPlayer.instance.DashSpeed -= 0.3f;
                MainPlayer.instance.DashSPRate -= 0.7f;
                break;
            case ItemQuality.Normal:
                MainPlayer.instance.MoveSpeed -= 0.1f;
                MainPlayer.instance.DashSpeed -= 0.4f;
                MainPlayer.instance.DashSPRate -= 0.6f;
                break;
            case ItemQuality.Good:
                MainPlayer.instance.MoveSpeed -= 0.1f;
                MainPlayer.instance.DashSpeed -= 0.5f;
                MainPlayer.instance.DashSPRate -= 0.5f;
                break;
            case ItemQuality.Excellent:
                MainPlayer.instance.MoveSpeed -= 0.15f;
                MainPlayer.instance.DashSpeed -= 0.6f;
                MainPlayer.instance.DashSPRate -= 0.5f;
                break;
            case ItemQuality.Legendary:
                MainPlayer.instance.MoveSpeed -= 0.25f;
                MainPlayer.instance.DashSpeed -= 0.7f;
                MainPlayer.instance.DashSPRate -= 0.4f;
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
                return "装备\n持有时可以提供30%的奔跑速度， 以及10%的移动速度，但会增加70%的体力消耗";
            case ItemQuality.Normal:
                return "装备\n持有时可以提供40%的奔跑速度， 以及10%的移动速度，但会增加60%的体力消耗";
            case ItemQuality.Good:
                return "装备\n持有时可以提供50%的奔跑速度， 以及10%的移动速度，但会增加50%的体力消耗";
            case ItemQuality.Excellent:
                return "装备\n持有时可以提供60%的奔跑速度， 以及15%的移动速度，但会增加50%的体力消耗";
            case ItemQuality.Legendary:
                return "装备\n持有时可以提供70%的奔跑速度， 以及25%的移动速度，但会增加40%的体力消耗";
            case ItemQuality.Null:
                return "装备\n持有时可以提供40%的奔跑速度， 以及10%的移动速度，但会增加60%的体力消耗";
        }
        return "itemGetQualityText()报错了";
    }
}