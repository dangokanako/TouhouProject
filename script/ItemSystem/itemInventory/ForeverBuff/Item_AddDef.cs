using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Item_AddDef", menuName = "Inventory_Forever/Item_AddDef")]
public class Item_AddDef : item
{
    public override bool Use()
    {
        // 记得增加永久数值
        GlobalControl.instance.originalDEF += 0.3f;
        PlayerHealthControl.instance.PlayerDef += 0.3f;
        UIControl.instance.SetPlayerInfoShow();

        SFXManger.instance.PlaySFX(2);
        MainPlayer.instance.PlayItemAnime(this.itemImage);
        return true;
    }
}
