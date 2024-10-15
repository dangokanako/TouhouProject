using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_Youmu_Weapon", menuName = "Weapon_Blade/Item_Youmu_Weapon")]
public class Item_Youmu_Weapon : item
{
    public override bool Use()
    {
        if (!SCAactiveControl.instance.SCActive_ASC_Louguanjian())
        {
            SFXManger.instance.PlaySFX(3);

            return false;
        }
        return true;
    }
}