using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Item_AddWarehouse", menuName = "Inventory_Forever/Item_AddWarehouse")]
public class Item_AddWarehouse : item
{
    public override bool Use()
    {
        ItemControl.instance.AddOneWarehouse();
        SFXManger.instance.PlaySFX(2);
        MainPlayer.instance.PlayItemAnime(this.itemImage);
        return true;
    }
}
