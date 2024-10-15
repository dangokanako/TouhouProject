using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_FengshenshaonvFeather", menuName = "Equipment/Item_FengshenshaonvFeather")]
public class Item_FengshenshaonvFeather : item
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
                MainPlayer.instance.MoveSpeed += 0.15f;
                PlayerHealthControl.instance.SPReadyTime -= 0.2f;
                break;
            case ItemQuality.Normal:
                MainPlayer.instance.MoveSpeed += 0.20f;
                PlayerHealthControl.instance.SPReadyTime -= 0.2f;
                break;
            case ItemQuality.Good:
                MainPlayer.instance.MoveSpeed += 0.25f;
                PlayerHealthControl.instance.SPReadyTime -= 0.2f;
                break;
            case ItemQuality.Excellent:
                MainPlayer.instance.MoveSpeed += 0.30f;
                PlayerHealthControl.instance.SPReadyTime -= 0.2f;
                break;
            case ItemQuality.Legendary:
                MainPlayer.instance.MoveSpeed += 0.35f;
                PlayerHealthControl.instance.SPReadyTime -= 0.2f;
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
                MainPlayer.instance.MoveSpeed -= 0.15f;
                PlayerHealthControl.instance.SPReadyTime += 0.2f;
                break;
            case ItemQuality.Normal:
                MainPlayer.instance.MoveSpeed -= 0.20f;
                PlayerHealthControl.instance.SPReadyTime += 0.2f;
                break;
            case ItemQuality.Good:
                MainPlayer.instance.MoveSpeed -= 0.25f;
                PlayerHealthControl.instance.SPReadyTime += 0.2f;
                break;
            case ItemQuality.Excellent:
                MainPlayer.instance.MoveSpeed -= 0.30f;
                PlayerHealthControl.instance.SPReadyTime += 0.2f;
                break;
            case ItemQuality.Legendary:
                MainPlayer.instance.MoveSpeed -= 0.35f;
                PlayerHealthControl.instance.SPReadyTime += 0.2f;
                break;
        }
        return true;
    }

    // 获取品质文本
    public override string GetQualityText()
    {
        /*
        持有时体力恢复不再需要疲劳时间，并且提供15%的移动速度。
持有时体力恢复不再需要疲劳时间，并且提供20%的移动速度。
持有时体力恢复不再需要疲劳时间，并且提供25%的移动速度。
持有时体力恢复不再需要疲劳时间，并且提供30%的移动速度。
持有时体力恢复不再需要疲劳时间，并且提供35%的移动速度。

        */
        switch (this.itemCiti)
        {
            case ItemQuality.Inferior:
                return "持有时体力恢复疲劳所需时间减少0.2秒，并且提供15%的移动速度。";
            case ItemQuality.Normal:
                return "持有时体力恢复疲劳所需时间减少0.2秒，并且提供20%的移动速度。";
            case ItemQuality.Good:
                return "持有时体力恢复疲劳所需时间减少0.2秒，并且提供25%的移动速度。";
            case ItemQuality.Excellent:
                return "持有时体力恢复疲劳所需时间减少0.2秒，并且提供30%的移动速度。";
            case ItemQuality.Legendary:
                return "持有时体力恢复疲劳所需时间减少0.2秒，并且提供35%的移动速度。";
            case ItemQuality.Null:
                return "持有时体力恢复疲劳所需时间减少0.2秒，并且提供20%的移动速度。";
        }
        return "itemGetQualityText()报错了";
    }
}