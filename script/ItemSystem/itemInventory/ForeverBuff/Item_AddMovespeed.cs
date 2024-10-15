using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Item_AddMovespeed", menuName = "Inventory_Forever/Item_AddMovespeed")]
public class Item_AddMovespeed : item
{
    public override bool Use()
    {
        // 记得增加永久数值
        GlobalControl.instance.originalMoveSpeed += 0.1f;
        MainPlayer.instance.moveSpeed += 0.1f;
        UIControl.instance.SetPlayerInfoShow();

        SFXManger.instance.PlaySFX(2);
        MainPlayer.instance.PlayItemAnime(this.itemImage);
        return true;
    }
}
