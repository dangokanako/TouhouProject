using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Item_AddHP", menuName = "Inventory_Forever/Item_AddHP")]
public class Item_AddHP : item
{
    public override bool Use()
    {
        // 记得增加永久数值
        GlobalControl.instance.originalHP += 3f;
        PlayerHealthControl.instance.AddMaxHP(3f);
        UIControl.instance.SetPlayerInfoShow();

        SFXManger.instance.PlaySFX(2);
        MainPlayer.instance.PlayItemAnime(this.itemImage);
        return true;
    }
}
