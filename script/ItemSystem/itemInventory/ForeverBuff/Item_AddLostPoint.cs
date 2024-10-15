using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Item_AddLostPoint", menuName = "Inventory_Forever/Item_AddLostPoint")]
public class Item_AddLostPoint : item
{
    public override bool Use()
    {
        GlobalControl.instance.deathLoss -= 0.05f;

        SFXManger.instance.PlaySFX(2);
        MainPlayer.instance.PlayItemAnime(this.itemImage);
        return true;
    }
}
