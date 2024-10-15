using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_Buckler", menuName = "Inventory/Item_Buckler")]
public class Item_Buckler : item
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
                PlayerHealthControl.instance._PlayerDef += 0.5f;
                MainPlayer.instance.rb.mass += 3;
                break;
            case ItemQuality.Normal:
                PlayerHealthControl.instance._PlayerDef += 0.5f;
                MainPlayer.instance.rb.mass += 2;
                break;
            case ItemQuality.Good:
                PlayerHealthControl.instance._PlayerDef += 0.5f;
                MainPlayer.instance.rb.mass += 2;
                break;
            case ItemQuality.Excellent:
                PlayerHealthControl.instance._PlayerDef += 0.7f;
                MainPlayer.instance.rb.mass += 2;
                break;
            case ItemQuality.Legendary:
                PlayerHealthControl.instance._PlayerDef += 1.0f;
                MainPlayer.instance.rb.mass += 1;
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
                PlayerHealthControl.instance._PlayerDef -= 0.5f;
                MainPlayer.instance.rb.mass -= 3;
                break;
            case ItemQuality.Normal:
                PlayerHealthControl.instance._PlayerDef -= 0.5f;
                MainPlayer.instance.rb.mass -= 2;
                break;
            case ItemQuality.Good:
                PlayerHealthControl.instance._PlayerDef -= 0.5f;
                MainPlayer.instance.rb.mass -= 2;
                break;
            case ItemQuality.Excellent:
                PlayerHealthControl.instance._PlayerDef -= 0.7f;
                MainPlayer.instance.rb.mass -= 2;
                break;
            case ItemQuality.Legendary:
                PlayerHealthControl.instance._PlayerDef -= 1.0f;
                MainPlayer.instance.rb.mass -= 1;
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
                return "装备\n持有时可以提供0.5点防御力，但会增加3点重量";
            case ItemQuality.Normal:
                return "装备\n持有时可以提供0.5点防御力，但会增加2点重量";
            case ItemQuality.Good:
                return "装备\n持有时可以提供0.5点防御力，但会增加2点重量";
            case ItemQuality.Excellent:
                return "装备\n持有时可以提供0.7点防御力，但会增加2点重量";
            case ItemQuality.Legendary:
                return "装备\n持有时可以提供1.0点防御力，但会增加1点重量";
            case ItemQuality.Null:
                return "装备\n持有时可以提供0.5点防御力，，但会增加2点重量";

        }
        return "itemGetQualityText()报错了";
    }
}