using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Item_Haoshangyao", menuName = "Inventory/Item_Haoshangyao")]
public class Item_Haoshangyao : item
{
    public override bool Use()
    {
        if (!PlayerHealthControl.instance.changeHP(3f))
        {
            SFXManger.instance.PlaySFX(3);
            return false;
        }
        SFXManger.instance.PlaySFX(2);
        MainPlayer.instance.PlayItemAnime(this.itemImage);
        return true;
    }
}
