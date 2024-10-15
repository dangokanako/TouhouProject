using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_TheFristStep", menuName = "Inventory/Item_TheFristStep")]
public class Item_TheFristStep : item
{
    public override bool Use()
    {
        if (!SCAactiveControl.instance.ASC_TheFristStepBullet())
        {
            SFXManger.instance.PlaySFX(3);
            return false;
        }
        return true;
    }
}