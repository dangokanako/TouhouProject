using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemSakuyaSC_2", menuName = "Inventory/ItemSakuyaSC_2")]
public class ItemSakuyaSC_2 : item
{
    public override bool Use()
    {
        // 发动符卡
        if (!SCAactiveControl.instance.SCActive_ASC_Sharenwanou())
        {
            SFXManger.instance.PlaySFX(3);
            return false;
        }
        return true;
    }
}
