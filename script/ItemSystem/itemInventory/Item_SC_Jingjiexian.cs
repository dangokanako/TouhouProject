using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_SC_Jingjiexian", menuName = "Inventory/Item_SC_Jingjiexian")]
public class Item_SC_Jingjiexian : item
{
    public override bool Use()
    {
        if (!SCAactiveControl.instance.SCActive_ASC_Jingjiexian())
        {
            SFXManger.instance.PlaySFX(3);
            return false;
        }
        return true;
    }
}

