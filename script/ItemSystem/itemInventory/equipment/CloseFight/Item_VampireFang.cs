using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_VampireFang", menuName = "CloseFight/Item_VampireFang")]
public class Item_VampireFang : item
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
                PlayerHealthControl.instance.CloseFightVampire += 1.3f;
                break;
            case ItemQuality.Normal:
                PlayerHealthControl.instance.CloseFightVampire += 1.5f;
                break;
            case ItemQuality.Good:
                PlayerHealthControl.instance.CloseFightVampire += 1.7f;
                break;
            case ItemQuality.Excellent:
                PlayerHealthControl.instance.CloseFightVampire += 1.9f;
                break;
            case ItemQuality.Legendary:
                PlayerHealthControl.instance.CloseFightVampire += 2.1f;
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
                PlayerHealthControl.instance.CloseFightVampire -= 1.3f;
                break;
            case ItemQuality.Normal:
                PlayerHealthControl.instance.CloseFightVampire -= 1.5f;
                break;
            case ItemQuality.Good:
                PlayerHealthControl.instance.CloseFightVampire -= 1.7f;
                break;
            case ItemQuality.Excellent:
                PlayerHealthControl.instance.CloseFightVampire -= 1.9f;
                break;
            case ItemQuality.Legendary:
                PlayerHealthControl.instance.CloseFightVampire -= 2.1f;
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
                return "装备\n持有时可以提供锐器和钝击（包括撞击类型）的1.3%的吸血。";
            case ItemQuality.Normal:
                return "装备\n持有时可以提供锐器和钝击（包括撞击类型）的1.5%的吸血。";
            case ItemQuality.Good:
                return "装备\n持有时可以提供锐器和钝击（包括撞击类型）的1.7%的吸血。";
            case ItemQuality.Excellent:
                return "装备\n持有时可以提供锐器和钝击（包括撞击类型）的1.9%的吸血。";
            case ItemQuality.Legendary:
                return "装备\n持有时可以提供锐器和钝击（包括撞击类型）的2.1%的吸血。";
            case ItemQuality.Null:
                return "装备\n持有时可以提供锐器和钝击（包括撞击类型）的1.5%的吸血。";

        }
        return "itemGetQualityText()报错了";
    }
}