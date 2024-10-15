using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Item_Bamuman", menuName = "Inventory/Item_Bamuman")]
public class Item_Bamuman : item
{
    public override bool Use()
    {
        PlayerHealthControl.instance._CriticalRate += 0.04f;
        SFXManger.instance.PlaySFX(2);
        MainPlayer.instance.PlayItemAnime(this.itemImage);
        return true;
    }
}
