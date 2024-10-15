using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Item_Noodles", menuName = "Inventory/Item_Noodles")]
public class Item_Noodles : item
{
    public override bool Use()
    {
        if (!PlayerHealthControl.instance.changeHP(7f))
        {
            SFXManger.instance.PlaySFX(3);
            return false;
        }
        SFXManger.instance.PlaySFX(2);
        MainPlayer.instance.PlayItemAnime(this.itemImage);
        return true;
    }
}
