using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_AyaEquipment2", menuName = "Equipment/Item_AyaEquipment2")]
public class Item_AyaEquipment2 : item
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
                MainPlayer.instance.MoveSpeed += 0.35f;
                MainPlayer.instance.DashSpeed += 0.12f;
                PlayerHealthControl.instance._PlayerDodge += 12f;
                MainPlayer.instance.DashSPRate += 0.7f;
                break;
            case ItemQuality.Normal:
                MainPlayer.instance.MoveSpeed += 0.45f;
                MainPlayer.instance.DashSpeed += 0.14f;
                PlayerHealthControl.instance._PlayerDodge += 14f;
                MainPlayer.instance.DashSPRate += 0.6f;
                break;
            case ItemQuality.Good:
                MainPlayer.instance.MoveSpeed += 0.55f;
                MainPlayer.instance.DashSpeed += 0.18f;
                PlayerHealthControl.instance._PlayerDodge += 16f;
                MainPlayer.instance.DashSPRate += 0.5f;
                break;
            case ItemQuality.Excellent:
                MainPlayer.instance.MoveSpeed += 0.65f;
                MainPlayer.instance.DashSpeed += 0.22f;
                PlayerHealthControl.instance._PlayerDodge += 18f;
                MainPlayer.instance.DashSPRate += 0.5f;
                break;
            case ItemQuality.Legendary:
                MainPlayer.instance.MoveSpeed += 0.75f;
                MainPlayer.instance.DashSpeed += 0.27f;
                PlayerHealthControl.instance._PlayerDodge += 20f;
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
                MainPlayer.instance.MoveSpeed -= 0.35f;
                MainPlayer.instance.DashSpeed -= 0.12f;
                PlayerHealthControl.instance._PlayerDodge -= 12f;
                MainPlayer.instance.DashSPRate -= 0.7f;
                break;
            case ItemQuality.Normal:
                MainPlayer.instance.MoveSpeed -= 0.45f;
                MainPlayer.instance.DashSpeed -= 0.14f;
                PlayerHealthControl.instance._PlayerDodge -= 14f;
                MainPlayer.instance.DashSPRate -= 0.6f;
                break;
            case ItemQuality.Good:
                MainPlayer.instance.MoveSpeed -= 0.55f;
                MainPlayer.instance.DashSpeed -= 0.18f;
                PlayerHealthControl.instance._PlayerDodge -= 16f;
                MainPlayer.instance.DashSPRate -= 0.5f;
                break;
            case ItemQuality.Excellent:
                MainPlayer.instance.MoveSpeed -= 0.65f;
                MainPlayer.instance.DashSpeed -= 0.22f;
                PlayerHealthControl.instance._PlayerDodge -= 18f;
                MainPlayer.instance.DashSPRate -= 0.5f;
                break;
            case ItemQuality.Legendary:
                MainPlayer.instance.MoveSpeed -= 0.75f;
                MainPlayer.instance.DashSpeed -= 0.27f;
                PlayerHealthControl.instance._PlayerDodge -= 20f;
                MainPlayer.instance.DashSPRate -= 0.4f;
                break;
        }
        return true;
    }

    // 获取品质文本
    /*
    持有时可以提供35%的奔跑速度， 12%的移动速度，以及13%回避率，但会增加70%的体力消耗
持有时可以提供45%的奔跑速度， 14%的移动速度，以及18%回避率，但会增加60%的体力消耗
持有时可以提供55%的奔跑速度， 18%的移动速度，以及24%回避率，但会增加50%的体力消耗
持有时可以提供65%的奔跑速度， 22%的移动速度，以及29%回避率，但会增加50%的体力消耗
持有时可以提供75%的奔跑速度， 27%的移动速度，以及35%回避率，但会增加40%的体力消耗
*/
    public override string GetQualityText()
    {
        switch (this.itemCiti)
        {
            case ItemQuality.Inferior:
                return "持有时可以提供35%的奔跑速度， 12%的移动速度，以及12%回避率，但会增加70%的体力消耗";
            case ItemQuality.Normal:
                return "持有时可以提供45%的奔跑速度， 14%的移动速度，以及14%回避率，但会增加60%的体力消耗";
            case ItemQuality.Good:
                return "持有时可以提供55%的奔跑速度， 18%的移动速度，以及16%回避率，但会增加50%的体力消耗";
            case ItemQuality.Excellent:
                return "持有时可以提供65%的奔跑速度， 22%的移动速度，以及18%回避率，但会增加50%的体力消耗";
            case ItemQuality.Legendary:
                return "持有时可以提供75%的奔跑速度， 27%的移动速度，以及20%回避率，但会增加40%的体力消耗";
            case ItemQuality.Null:
                return "持有时可以提供45%的奔跑速度， 14%的移动速度，以及14%回避率，但会增加60%的体力消耗";
        }
        return "itemGetQualityText()报错了";
    }
}