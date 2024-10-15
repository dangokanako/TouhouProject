using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_IronBuckler", menuName = "Inventory/Item_IronBuckler")]
public class Item_IronBuckler : item
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
                PlayerHealthControl.instance._PlayerDef += 1.0f;
                MainPlayer.instance.rb.mass += 6;
                break;
            case ItemQuality.Normal:
                PlayerHealthControl.instance._PlayerDef += 1.3f;
                MainPlayer.instance.rb.mass += 5;
                break;
            case ItemQuality.Good:
                PlayerHealthControl.instance._PlayerDef += 1.5f;
                MainPlayer.instance.rb.mass += 5;
                break;
            case ItemQuality.Excellent:
                PlayerHealthControl.instance._PlayerDef += 1.5f;
                MainPlayer.instance.rb.mass += 5;
                break;
            case ItemQuality.Legendary:
                PlayerHealthControl.instance._PlayerDef += 1.7f;
                MainPlayer.instance.rb.mass += 4;
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
                PlayerHealthControl.instance._PlayerDef -= 1.0f;
                MainPlayer.instance.rb.mass -= 6;
                break;
            case ItemQuality.Normal:
                PlayerHealthControl.instance._PlayerDef -= 1.3f;
                MainPlayer.instance.rb.mass -= 5;
                break;
            case ItemQuality.Good:
                PlayerHealthControl.instance._PlayerDef -= 1.5f;
                MainPlayer.instance.rb.mass -= 5;
                break;
            case ItemQuality.Excellent:
                PlayerHealthControl.instance._PlayerDef -= 1.5f;
                MainPlayer.instance.rb.mass -= 5;
                break;
            case ItemQuality.Legendary:
                PlayerHealthControl.instance._PlayerDef -= 1.7f;
                MainPlayer.instance.rb.mass -= 4;
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
                return "装备\n持有时可以提供1.0点防御力，但会增加6点重量";
            case ItemQuality.Normal:
                return "装备\n持有时可以提供1.3点防御力，但会增加5点重量";
            case ItemQuality.Good:
                return "装备\n持有时可以提供1.5点防御力，但会增加5点重量";
            case ItemQuality.Excellent:
                return "装备\n持有时可以提供1.5点防御力，但会增加5点重量";
            case ItemQuality.Legendary:
                return "装备\n持有时可以提供1.7点防御力，但会增加4点重量";
            case ItemQuality.Null:
                return "装备\n持有时可以提供1.3点防御力，但会增加5点重量";
        }
        return "itemGetQualityText()报错了";
    }
}
