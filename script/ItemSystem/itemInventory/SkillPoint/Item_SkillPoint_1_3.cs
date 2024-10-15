using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Item_SkillPoint_1_3", menuName = "SkillPoint/Item_SkillPoint_1_3")]
public class Item_SkillPoint_1_3 : item
{
    public int DropItemId;
    public int DropItemCount;
    public override bool Use()
    {
        for (int i = 0; i < DropItemCount; i++)
            AssetControl.instance.DropItem(PlayerHealthControl.instance.transform.position, ItemControl.instance.itemGuide[DropItemId]);

        SFXManger.instance.PlaySFX(2);
        return true;
    }
}
