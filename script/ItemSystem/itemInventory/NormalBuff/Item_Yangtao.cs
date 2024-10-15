using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Item_Yangtao", menuName = "NormalBuff/Item_Yangtao")]
public class Item_Yangtao : item
{
    public override bool Use()
    {
        PlayerHealthControl.instance._PlayerDodge += 2;
        SFXManger.instance.PlaySFX(2);
        MainPlayer.instance.PlayItemAnime(this.itemImage);
        return true;
    }
}
