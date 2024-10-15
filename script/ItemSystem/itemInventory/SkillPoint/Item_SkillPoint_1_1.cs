using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Item_SkillPoint_1_1", menuName = "SkillPoint/Item_SkillPoint_1_1")]
public class Item_SkillPoint_1_1 : item
{
    public override bool Use()
    {
        PlayerHealthControl.instance._PlayerDef += 0.25f;
        SFXManger.instance.PlaySFX(2);
        return true;
    }
}
