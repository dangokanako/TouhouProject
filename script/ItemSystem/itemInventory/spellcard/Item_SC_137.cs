using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_SC_137", menuName = "SpellCard/Item_SC_137")]
public class Item_SC_137 : item
{
    public override bool Use()
    {
        if (!SCAactiveControl.instance.SCActive_ASC_137())
        {
            SFXManger.instance.PlaySFX(3);
            return false;
        }
        SFXManger.instance.PlaySFX(5);
        return true;
    }

}

