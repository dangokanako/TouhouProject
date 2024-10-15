using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Item_IceBlock", menuName = "Inventory/Item_IceBlock")]
public class Item_IceBlock : item
{
    public override bool Use()
    {
        if (PlayerHealthControl.instance.currentSP >= PlayerHealthControl.instance.maxSP)
        {
            SFXManger.instance.PlaySFX(3);
            return false;
        }
        PlayerHealthControl.instance.changeSP(1);
        SFXManger.instance.PlaySFX(2);
        MainPlayer.instance.PlayItemAnime(this.itemImage);
        return true;
    }
}
