using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_SCBuffer2", menuName = "Inventory/Item_SCBuffer2")]
public class Item_SCBuffer2 : item
{
    public override bool Use()
    {
        SFXManger.instance.PlaySFX(3);
        return false;
    }

    public override bool Passive()
    {
        PlayerHealthControl.instance._BluntDamageRate += 0.25f;
        return true;
    }

    public override bool discardPassive()
    {
        PlayerHealthControl.instance._BluntDamageRate -= 0.25f;
        return true;
    }
}