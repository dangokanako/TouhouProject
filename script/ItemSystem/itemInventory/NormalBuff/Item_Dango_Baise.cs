using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Item_Dango_Baise", menuName = "NormalBuff/Item_Dango_Baise")]
public class Item_Dango_Baise : item
{
    public override bool Use()
    {
        PlayerHealthControl.instance._SharpDamageRate += 0.07f;
        SFXManger.instance.PlaySFX(2);
        MainPlayer.instance.PlayItemAnime(this.itemImage);
        return true;
    }
}
