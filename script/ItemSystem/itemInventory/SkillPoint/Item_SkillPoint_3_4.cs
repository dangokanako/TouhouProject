using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Item_SkillPoint_3_4", menuName = "SkillPoint/Item_SkillPoint_3_4")]
public class Item_SkillPoint_3_4 : item
{
    public override bool Use()
    {
        PlayerHealthControl.instance._CriticalRate += 0.04f;
        PlayerHealthControl.instance._CriticalDamage += 0.1f;
        SFXManger.instance.PlaySFX(2);
        return true;
    }
}
