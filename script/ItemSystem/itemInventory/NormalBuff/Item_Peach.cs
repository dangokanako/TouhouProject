using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Item_Peach", menuName = "Inventory/Item_Peach")]
public class Item_Peach : item
{
    public override bool Use()
    {
        PlayerHealthControl.instance._playerSPRecover += 0.1f;
        SFXManger.instance.PlaySFX(2);
        MainPlayer.instance.PlayItemAnime(this.itemImage);
        return true;
    }
}
