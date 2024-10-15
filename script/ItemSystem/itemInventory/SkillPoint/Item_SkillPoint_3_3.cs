using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Item_SkillPoint_3_3", menuName = "SkillPoint/Item_SkillPoint_3_3")]
public class Item_SkillPoint_3_3 : item
{
    public override bool Use()
    {
        PlayerHealthControl.instance._BluntDamageRate += 0.12f;
        SFXManger.instance.PlaySFX(2);
        return true;
    }
}
