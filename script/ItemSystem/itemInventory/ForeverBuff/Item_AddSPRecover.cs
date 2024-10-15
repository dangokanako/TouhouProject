using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Item_AddSPRecover", menuName = "Inventory_Forever/Item_AddSPRecover")]
public class Item_AddSPRecover : item
{
    public override bool Use()
    {
        // 记得增加永久数值
        GlobalControl.instance.originalSPRecover += 0.1f;
        PlayerHealthControl.instance.playerSPRecover += 0.1f;
        UIControl.instance.SetPlayerInfoShow();

        SFXManger.instance.PlaySFX(2);
        MainPlayer.instance.PlayItemAnime(this.itemImage);
        return true;
    }
}
