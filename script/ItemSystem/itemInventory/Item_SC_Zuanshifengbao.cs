using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_SC_Zuanshifengbao", menuName = "Inventory/Item_SC_Zuanshifengbao")]
public class Item_SC_Zuanshifengbao : item
{
    public override bool Use()
    {
        if (!SCAactiveControl.instance.SCActive_ASC_Zuanshifengbao())
        {
            SFXManger.instance.PlaySFX(3);
            return false;
        }
        SFXManger.instance.PlaySFX(5);
        return true;
    }
}

