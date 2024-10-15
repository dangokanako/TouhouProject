using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Item_Sickle", menuName = "Inventory/Item_Sickle")]
public class Item_Sickle : item
{
    public override bool Use()
    {

        // 还没做 TODO
        if (!SCAactiveControl.instance.ASC_Sickle())
        {
            SFXManger.instance.PlaySFX(3);
            return false;
        }
        return true;
    }
}
