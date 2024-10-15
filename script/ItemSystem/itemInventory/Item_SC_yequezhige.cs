using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_SC_yequezhige", menuName = "Inventory/Item_SC_yequezhige")]
public class Item_SC_yequezhige : item
{
    public override bool Use()
    {
        if (!SCAactiveControl.instance.SCActive_ASC_Yequezhige())
        {
            SFXManger.instance.PlaySFX(3);
            return false;
        }
        return true;
    }
}

