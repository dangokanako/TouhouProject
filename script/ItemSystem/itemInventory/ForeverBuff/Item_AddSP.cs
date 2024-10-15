using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Item_AddSP", menuName = "Inventory_Forever/Item_AddSP")]
public class Item_AddSP : item
{
    public override bool Use()
    {
        // 记得增加永久数值
        GlobalControl.instance.originalSP += 0.5f;
        PlayerHealthControl.instance.AddMaxSP(0.5f);
        UIControl.instance.SetPlayerInfoShow();

        SFXManger.instance.PlaySFX(2);
        MainPlayer.instance.PlayItemAnime(this.itemImage);
        return true;
    }
}
