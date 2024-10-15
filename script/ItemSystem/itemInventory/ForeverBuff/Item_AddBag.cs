using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Item_AddBag", menuName = "Inventory/Item_AddBag")]
public class Item_AddBag : item
{
    public override bool Use()
    {
        ItemControl.instance.AddOneBag();
        SFXManger.instance.PlaySFX(2);
        MainPlayer.instance.PlayItemAnime(this.itemImage);
        return true;
    }
}
