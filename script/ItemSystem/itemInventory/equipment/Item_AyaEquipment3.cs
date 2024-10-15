using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_AyaEquipment3", menuName = "Equipment/Item_AyaEquipment3")]
public class Item_AyaEquipment3 : item
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
                MainPlayer.instance.MoveSpeed += 0.4f;
                MainPlayer.instance.DashSpeed += 0.15f;
                PlayerHealthControl.instance._PlayerDodge += 15;
                PlayerHealthControl.instance.InvincibleTime += 0.4f;
                break;
            case ItemQuality.Normal:
                MainPlayer.instance.MoveSpeed += 0.5f;
                MainPlayer.instance.DashSpeed += 0.2f;
                PlayerHealthControl.instance._PlayerDodge += 20;
                PlayerHealthControl.instance.InvincibleTime += 0.6f;
                break;
            case ItemQuality.Good:
                MainPlayer.instance.MoveSpeed += 0.6f;
                MainPlayer.instance.DashSpeed += 0.25f;
                PlayerHealthControl.instance._PlayerDodge += 25;
                PlayerHealthControl.instance.InvincibleTime += 0.8f;
                break;
            case ItemQuality.Excellent:
                MainPlayer.instance.MoveSpeed += 0.7f;
                MainPlayer.instance.DashSpeed += 0.3f;
                PlayerHealthControl.instance._PlayerDodge += 30;
                PlayerHealthControl.instance.InvincibleTime += 1.0f;
                break;
            case ItemQuality.Legendary:
                MainPlayer.instance.MoveSpeed += 0.8f;
                MainPlayer.instance.DashSpeed += 0.35f;
                PlayerHealthControl.instance._PlayerDodge += 35;
                PlayerHealthControl.instance.InvincibleTime += 1.2f;
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
                MainPlayer.instance.MoveSpeed -= 0.4f;
                MainPlayer.instance.DashSpeed -= 0.15f;
                PlayerHealthControl.instance._PlayerDodge -= 15;
                PlayerHealthControl.instance.InvincibleTime -= 0.4f;
                break;
            case ItemQuality.Normal:
                MainPlayer.instance.MoveSpeed -= 0.5f;
                MainPlayer.instance.DashSpeed -= 0.2f;
                PlayerHealthControl.instance._PlayerDodge -= 20;
                PlayerHealthControl.instance.InvincibleTime -= 0.6f;
                break;
            case ItemQuality.Good:
                MainPlayer.instance.MoveSpeed -= 0.6f;
                MainPlayer.instance.DashSpeed -= 0.25f;
                PlayerHealthControl.instance._PlayerDodge -= 25;
                PlayerHealthControl.instance.InvincibleTime -= 0.8f;
                break;
            case ItemQuality.Excellent:
                MainPlayer.instance.MoveSpeed -= 0.7f;
                MainPlayer.instance.DashSpeed -= 0.3f;
                PlayerHealthControl.instance._PlayerDodge -= 30;
                PlayerHealthControl.instance.InvincibleTime -= 1.0f;
                break;
            case ItemQuality.Legendary:
                MainPlayer.instance.MoveSpeed -= 0.8f;
                MainPlayer.instance.DashSpeed -= 0.35f;
                PlayerHealthControl.instance._PlayerDodge -= 35;
                PlayerHealthControl.instance.InvincibleTime -= 1.2f;
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
                return "持有时可以提供40%的奔跑速度， 15%的移动速度，以及15%回避率，受到伤害后的无敌时间增加0.4秒";
            case ItemQuality.Normal:
                return "持有时可以提供50%的奔跑速度， 20%的移动速度，以及20%回避率，受到伤害后的无敌时间增加0.6秒";
            case ItemQuality.Good:
                return "持有时可以提供60%的奔跑速度， 25%的移动速度，以及25%回避率，受到伤害后的无敌时间增加0.8秒";
            case ItemQuality.Excellent:
                return "持有时可以提供70%的奔跑速度， 30%的移动速度，以及30%回避率，受到伤害后的无敌时间增加1.0秒";
            case ItemQuality.Legendary:
                return "持有时可以提供80%的奔跑速度， 35%的移动速度，以及35%回避率，受到伤害后的无敌时间增加1.2秒";
            case ItemQuality.Null:
                return "持有时可以提供50%的奔跑速度， 20%的移动速度，以及20%回避率，受到伤害后的无敌时间增加1秒";

        }
        return "itemGetQualityText()报错了";
    }
}