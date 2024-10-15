using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Item_AddCriRate", menuName = "Inventory_Forever/Item_AddCriRate")]
public class Item_AddCriRate : item
{
    public override bool Use()
    {
        // 记得增加永久数值
        GlobalControl.instance.originalCriticalRate += 0.03f;
        PlayerHealthControl.instance.CriticalRate += 0.03f;
        UIControl.instance.SetPlayerInfoShow();



        SFXManger.instance.PlaySFX(2);
        MainPlayer.instance.PlayItemAnime(this.itemImage);
        return true;
    }
}
