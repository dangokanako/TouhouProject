using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_YanDeZiAnBei", menuName = "Equipment/Item_YanDeZiAnBei")]
public class Item_YanDeZiAnBei : item
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
                PlayerHealthControl.instance.MaxHealth += 4;
                PlayerHealthControl.instance.changeHP(4);
                break;
            case ItemQuality.Normal:
                PlayerHealthControl.instance.MaxHealth += 5;
                PlayerHealthControl.instance.changeHP(5);
                break;
            case ItemQuality.Good:
                PlayerHealthControl.instance.MaxHealth += 6;
                PlayerHealthControl.instance.changeHP(6);
                break;
            case ItemQuality.Excellent:
                PlayerHealthControl.instance.MaxHealth += 7;
                PlayerHealthControl.instance.changeHP(7);
                break;
            case ItemQuality.Legendary:
                PlayerHealthControl.instance.MaxHealth += 8;
                PlayerHealthControl.instance.changeHP(8);
                break;
        }
        PlayerHealthControl.instance.changeHP(0);

        return true;
    }

    public override bool discardPassive()
    {
        // 根据品质调整数值
        switch (this.itemCiti)
        {
            case ItemQuality.Inferior:
                PlayerHealthControl.instance.MaxHealth -= 4;
                PlayerHealthControl.instance.changeHP(-4);
                break;
            case ItemQuality.Normal:
                PlayerHealthControl.instance.MaxHealth -= 5;
                PlayerHealthControl.instance.changeHP(-5);
                break;
            case ItemQuality.Good:
                PlayerHealthControl.instance.MaxHealth -= 6;
                PlayerHealthControl.instance.changeHP(-6);
                break;
            case ItemQuality.Excellent:
                PlayerHealthControl.instance.MaxHealth -= 7;
                PlayerHealthControl.instance.changeHP(-7);
                break;
            case ItemQuality.Legendary:
                PlayerHealthControl.instance.MaxHealth -= 8;
                PlayerHealthControl.instance.changeHP(-8);
                break;
        }

        if (PlayerHealthControl.instance.currentHealth < 0)
        {
            PlayerHealthControl.instance.TakeDamage(1000);
        }
        return true;
    }

    // 获取品质文本
    public override string GetQualityText()
    {
        switch (this.itemCiti)
        {
            case ItemQuality.Inferior:
                return "装备\n持有时可以提供4点生命最大上限";
            case ItemQuality.Normal:
                return "装备\n持有时可以提供5点生命最大上限";
            case ItemQuality.Good:
                return "装备\n持有时可以提供6点生命最大上限";
            case ItemQuality.Excellent:
                return "装备\n持有时可以提供7点生命最大上限";
            case ItemQuality.Legendary:
                return "装备\n持有时可以提供8点生命最大上限";
            case ItemQuality.Null:
                return "装备\n持有时可以提供5点生命最大上限";
        }
        return "itemGetQualityText()报错了";
    }
}