using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_SCBuffer4", menuName = "Inventory/Item_SCBuffer4")]
public class Item_SCBuffer4 : item
{
    public override bool Use()
    {
        SFXManger.instance.PlaySFX(3);
        return false;
    }

    public override bool Passive()
    {
        PlayerHealthControl.instance._SCDamageRate += 0.2f;
        PlayerHealthControl.instance._BluntDamageRate += 0.25f;
        return true;
    }

    public override bool discardPassive()
    {
        PlayerHealthControl.instance._SCDamageRate -= 0.2f;
        PlayerHealthControl.instance._BluntDamageRate -= 0.25f;
        return true;
    }
}