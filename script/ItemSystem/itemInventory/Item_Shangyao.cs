using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Item_Shangyao", menuName = "Inventory/Item_Shangyao")]
public class Item_Shangyao : item
{
    public override bool Use()
    {
        if (!PlayerHealthControl.instance.changeHP(1f))
        {
            SFXManger.instance.PlaySFX(3);
            return false;
        }
        SFXManger.instance.PlaySFX(2);
        MainPlayer.instance.PlayItemAnime(this.itemImage);
        return true;
    }
}
