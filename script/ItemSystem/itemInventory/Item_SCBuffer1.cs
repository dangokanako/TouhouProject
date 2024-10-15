using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_SCBuffer1", menuName = "Inventory/Item_SCBuffer1")]
public class Item_SCBuffer1 : item
{
    public override bool Use()
    {
        SFXManger.instance.PlaySFX(3);
        return false;
    }

    public override bool Passive()
    {
        PlayerHealthControl.instance._SCDamageRate += 0.2f;
        return true;
    }

    public override bool discardPassive()
    {
        PlayerHealthControl.instance._SCDamageRate -= 0.2f;
        return true;
    }
}