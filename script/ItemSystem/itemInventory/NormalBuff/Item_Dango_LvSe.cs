using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Item_Dango_LvSe", menuName = "NormalBuff/Item_Dango_LvSe")]
public class Item_Dango_LvSe : item
{
    public override bool Use()
    {
        PlayerHealthControl.instance._BluntDamageRate += 0.06f;
        SFXManger.instance.PlaySFX(2);
        MainPlayer.instance.PlayItemAnime(this.itemImage);
        return true;
    }
}
