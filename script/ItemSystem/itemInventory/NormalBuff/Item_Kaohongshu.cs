using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Item_Kaohongshu", menuName = "Inventory/Item_Kaohongshu")]
public class Item_Kaohongshu : item
{
    public override bool Use()
    {
        PlayerHealthControl.instance.AddMaxHP(2);
        SFXManger.instance.PlaySFX(2);
        MainPlayer.instance.PlayItemAnime(this.itemImage);
        return true;
    }
}
