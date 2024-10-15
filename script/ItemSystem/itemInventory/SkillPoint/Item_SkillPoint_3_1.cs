using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Item_SkillPoint_3_1", menuName = "SkillPoint/Item_SkillPoint_3_1")]
public class Item_SkillPoint_3_1 : item
{
    public override bool Use()
    {
        PlayerHealthControl.instance._SharpDamageRate += 0.14f;
        SFXManger.instance.PlaySFX(2);
        return true;
    }
}
