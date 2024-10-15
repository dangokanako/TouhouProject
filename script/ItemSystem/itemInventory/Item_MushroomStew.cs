using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Item_MushroomStew", menuName = "Inventory/Item_MushroomStew")]
public class Item_MushroomStew : item
{
    public override bool Use()
    {
        if (!PlayerHealthControl.instance.changeHP(12f))
        {
            SFXManger.instance.PlaySFX(3);
            return false;
        }
        SFXManger.instance.PlaySFX(2);
        MainPlayer.instance.PlayItemAnime(this.itemImage);
        return true;
    }
}
