using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Item_RoShiALieBa", menuName = "NormalBuff/Item_RoShiALieBa")]
public class Item_RoShiALieBa : item
{
    public override bool Use()
    {
        PlayerHealthControl.instance._PlayerDef += 0.2f;
        SFXManger.instance.PlaySFX(2);
        MainPlayer.instance.PlayItemAnime(this.itemImage);
        return true;
    }
}
