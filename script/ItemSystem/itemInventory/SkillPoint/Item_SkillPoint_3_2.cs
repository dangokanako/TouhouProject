using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Item_SkillPoint_3_2", menuName = "SkillPoint/Item_SkillPoint_3_2")]
public class Item_SkillPoint_3_2 : item
{
    public override bool Use()
    {
        PlayerHealthControl.instance._SCDamageRate += 0.1f;
        SFXManger.instance.PlaySFX(2);
        return true;
    }
}
