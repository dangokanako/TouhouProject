using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_Yinyangyu_2", menuName = "MainRoute/Item_Yinyangyu_2")]
public class Item_Yinyangyu_2 : item
{
    public override bool Use()
    {
        if (!SCAactiveControl.instance.SCActive_ASC_Yinyangyu_2())
        {
            SFXManger.instance.PlaySFX(3);
            return false;
        }
        return true;
    }
}