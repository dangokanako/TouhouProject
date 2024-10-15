using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Item_Dragon_Fruit", menuName = "Inventory/Item_Dragon_Fruit")]
public class Item_Dragon_Fruit : item
{
    public override bool Use()
    {
        PlayerHealthControl.instance._PlayerAtk += 2;
        SFXManger.instance.PlaySFX(2);
        MainPlayer.instance.PlayItemAnime(this.itemImage);
        return true;
    }
}
