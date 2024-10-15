using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_SC_Icefall", menuName = "Inventory/Item_SC_Icefall")]
public class Item_SC_Icefall : item
{
    public override bool Use()
    {
        if (!SCAactiveControl.instance.SCActive_ASC_IcicleFall())
        {
            SFXManger.instance.PlaySFX(3);
            return false;
        }
        SFXManger.instance.PlaySFX(5);
        return true;
    }

}

