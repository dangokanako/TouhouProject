using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Item_Dango_FenSe", menuName = "NormalBuff/Item_Dango_FenSe")]
public class Item_Dango_FenSe : item
{
    public override bool Use()
    {
        PlayerHealthControl.instance._SCDamageRate += 0.05f;
        SFXManger.instance.PlaySFX(2);
        MainPlayer.instance.PlayItemAnime(this.itemImage);
        return true;
    }
}
