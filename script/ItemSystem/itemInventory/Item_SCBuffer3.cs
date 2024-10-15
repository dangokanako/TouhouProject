using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_SCBuffer3", menuName = "Inventory/Item_SCBuffer3")]
public class Item_SCBuffer3 : item
{
    public override bool Use()
    {
        SFXManger.instance.PlaySFX(3);
        return false;
    }

    public override bool Passive()
    {
        PlayerHealthControl.instance._SharpDamageRate += 0.3f;
        return true;
    }

    public override bool discardPassive()
    {
        PlayerHealthControl.instance._SharpDamageRate -= 0.3f;
        return true;
    }
}