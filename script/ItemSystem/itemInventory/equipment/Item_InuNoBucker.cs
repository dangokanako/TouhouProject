using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_InuNoBucker", menuName = "Inventory/Item_InuNoBucker")]
public class Item_InuNoBucker : item
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
                PlayerHealthControl.instance._PlayerDef += 0.3f;
                MainPlayer.instance.dashForce += 0.2f;
                break;
            case ItemQuality.Normal:
                PlayerHealthControl.instance._PlayerDef += 0.4f;
                MainPlayer.instance.dashForce += 0.22f;
                break;
            case ItemQuality.Good:
                PlayerHealthControl.instance._PlayerDef += 0.5f;
                MainPlayer.instance.dashForce += 0.25f;
                break;
            case ItemQuality.Excellent:
                PlayerHealthControl.instance._PlayerDef += 0.6f;
                MainPlayer.instance.dashForce += 0.27f;
                break;
            case ItemQuality.Legendary:
                PlayerHealthControl.instance._PlayerDef += 0.7f;
                MainPlayer.instance.dashForce += 0.3f;
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
                PlayerHealthControl.instance._PlayerDef -= 0.3f;
                MainPlayer.instance.dashForce -= 0.2f;
                break;
            case ItemQuality.Normal:
                PlayerHealthControl.instance._PlayerDef -= 0.4f;
                MainPlayer.instance.dashForce -= 0.22f;
                break;
            case ItemQuality.Good:
                PlayerHealthControl.instance._PlayerDef -= 0.5f;
                MainPlayer.instance.dashForce -= 0.25f;
                break;
            case ItemQuality.Excellent:
                PlayerHealthControl.instance._PlayerDef -= 0.6f;
                MainPlayer.instance.dashForce -= 0.27f;
                break;
            case ItemQuality.Legendary:
                PlayerHealthControl.instance._PlayerDef -= 0.7f;
                MainPlayer.instance.dashForce -= 0.3f;
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
                return "装备\n持有时可以提供0.3点防御力，同时提供20%突进力";
            case ItemQuality.Normal:
                return "装备\n持有时可以提供0.4点防御力，同时提供22%突进力";
            case ItemQuality.Good:
                return "装备\n持有时可以提供0.5点防御力，同时提供25%突进力";
            case ItemQuality.Excellent:
                return "装备\n持有时可以提供0.6点防御力，同时提供27%突进力";
            case ItemQuality.Legendary:
                return "装备\n持有时可以提供0.7点防御力，同时提供30%突进力";
            case ItemQuality.Null:
                return "null报错";

        }
        return "itemGetQualityText()报错了";
    }
}