using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Item_Loaf", menuName = "Inventory/Item_Loaf")]
public class Item_Loaf : item
{
    public override bool Use()
    {
        if (!PlayerHealthControl.instance.changeHP(5f))
        {
            SFXManger.instance.PlaySFX(3);
            return false;
        }
        SFXManger.instance.PlaySFX(2);
        MainPlayer.instance.PlayItemAnime(this.itemImage);
        return true;
    }
}
