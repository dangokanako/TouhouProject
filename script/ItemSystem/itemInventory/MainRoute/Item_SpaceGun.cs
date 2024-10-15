using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_SpaceGun", menuName = "Inventory/Item_SpaceGun")]
public class Item_SpaceGun : item
{
    public override bool Use()
    {
        if (!SCAactiveControl.instance.ASC_SpaceGunBullet())
        {
            SFXManger.instance.PlaySFX(3);
            return false;
        }
        return true;
    }
}