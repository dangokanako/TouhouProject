using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Item_RedTea", menuName = "Inventory/Item_RedTea")]
public class Item_RedTea : item
{
    public override bool Use()
    {
        PlayerHealthControl.instance.AddMaxSP(0.5f);
        SFXManger.instance.PlaySFX(2);
        MainPlayer.instance.PlayItemAnime(this.itemImage);
        return true;
    }
}
