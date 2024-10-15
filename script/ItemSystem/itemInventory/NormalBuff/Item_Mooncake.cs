using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Item_Mooncake", menuName = "Inventory/Item_Mooncake")]
public class Item_Mooncake : item
{
    public override bool Use()
    {
        MainPlayer.instance.moveSpeed += 0.1f;
        SFXManger.instance.PlaySFX(2);
        MainPlayer.instance.PlayItemAnime(this.itemImage);
        return true;
    }
}
