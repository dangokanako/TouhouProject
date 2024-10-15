using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Item_Yingbing", menuName = "NormalBuff/Item_Yingbing")]
public class Item_Yingbing : item
{
    public override bool Use()
    {
        MainPlayer.instance.DashSpeed += 0.1f;
        SFXManger.instance.PlaySFX(2);
        MainPlayer.instance.PlayItemAnime(this.itemImage);
        return true;
    }
}
