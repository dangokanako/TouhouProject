using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Item_Dango_Sanse", menuName = "NormalBuff/Item_Dango_Sanse")]
public class Item_Dango_Sanse : item
{
    public override bool Use()
    {
        PlayerHealthControl.instance._SharpDamageRate += 0.1f;
        PlayerHealthControl.instance._SCDamageRate += 0.1f;
        PlayerHealthControl.instance._BluntDamageRate += 0.1f;
        SFXManger.instance.PlaySFX(2);
        MainPlayer.instance.PlayItemAnime(this.itemImage);
        return true;
    }
}
