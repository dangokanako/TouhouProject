using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_DreamChaser", menuName = "Inventory/Item_DreamChaser")]
public class Item_DreamChaser : item
{
    public override bool Use()
    {
        if (!SCAactiveControl.instance.ASC_DreamChaser())
        {
            SFXManger.instance.PlaySFX(3);
            return false;
        }
        return true;
    }
}