using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_Yinyangyu_3", menuName = "MainRoute/Item_Yinyangyu_3")]
public class Item_Yinyangyu_3 : item
{
    public override bool Use()
    {
        if (!SCAactiveControl.instance.SCActive_ASC_Yinyangyu_3())
        {
            SFXManger.instance.PlaySFX(3);
            return false;
        }
        return true;
    }
}