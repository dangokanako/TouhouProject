using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemSakuyaSC_1", menuName = "Inventory/ItemSakuyaSC_1")]
public class ItemSakuyaSC_1 : item
{
    public override bool Use()
    {
        // 发动符卡
        if (!SCAactiveControl.instance.SCActive_ASC_IndiscriminateSword())
        {
            SFXManger.instance.PlaySFX(3);
            return false;
        }
        return true;
    }
}
