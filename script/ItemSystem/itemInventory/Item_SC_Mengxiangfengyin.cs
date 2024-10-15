using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_SC_Mengxiangfengyin", menuName = "Inventory/Item_SC_Mengxiangfengyin")]
public class Item_SC_Mengxiangfengyin : item
{
    public override bool Use()
    {
        if (!SCAactiveControl.instance.SCActive_ASC_Mengxiangfengyin())
        {
            SFXManger.instance.PlaySFX(3);
            return false;
        }
        SFXManger.instance.PlaySFX(5);
        return true;
    }
}

