using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Item_Feather", menuName = "Inventory/Item_Feather")]
public class Item_Feather : item
{
    public override bool Use()
    {
        SFXManger.instance.PlaySFX(3);
        return false;
    }
}
