using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_NullCard", menuName = "MainRoute/Item_NullCard")]
public class Item_NullCard : item
{
    public override bool Use()
    {
        if (!SCAactiveControl.instance.ASC_NullBullet())
        {
            SFXManger.instance.PlaySFX(3);
            return false;
        }
        return true;
    }
}