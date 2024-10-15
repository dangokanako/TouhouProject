using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_Youmu_SC1", menuName = "Weapon_Blade/Item_Youmu_SC1")]
public class Item_Youmu_SC1 : item
{
    public override bool Use()
    {
        if (!SCAactiveControl.instance.SCActive_ASC_Wuruhuanxiang())
        {
            SFXManger.instance.PlaySFX(3);
            return false;
        }
        return true;
    }
}