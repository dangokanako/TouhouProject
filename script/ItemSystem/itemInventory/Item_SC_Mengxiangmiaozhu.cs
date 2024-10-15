using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_SC_Mengxiangmiaozhu", menuName = "Inventory/Item_SC_Mengxiangmiaozhu")]
public class Item_SC_Mengxiangmiaozhu : item
{
    public override bool Use()
    {
        if (!SCAactiveControl.instance.SCActive_ASC_Mengxiangmiaozhu())
        {
            SFXManger.instance.PlaySFX(3);
            return false;
        }
        SFXManger.instance.PlaySFX(5);
        return true;
    }
}

