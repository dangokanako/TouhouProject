using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Item_SkillPoint_2_3", menuName = "SkillPoint/Item_SkillPoint_2_3")]
public class Item_SkillPoint_2_3 : item
{
    public override bool Use()
    {
        PlayerHealthControl.instance.SPConsumptionReduction += 0.07f;
        SFXManger.instance.PlaySFX(2);
        return true;
    }
}
