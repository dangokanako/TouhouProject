using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Item_NoodlesPan", menuName = "Inventory/Item_NoodlesPan")]
public class Item_NoodlesPan : item
{
    public override bool Use()
    {
        if (!PlayerHealthControl.instance.changeHP(25f))
        {
            SFXManger.instance.PlaySFX(3);
            return false;
        }
        SFXManger.instance.PlaySFX(2);
        MainPlayer.instance.PlayItemAnime(this.itemImage);
        return true;
    }
}
