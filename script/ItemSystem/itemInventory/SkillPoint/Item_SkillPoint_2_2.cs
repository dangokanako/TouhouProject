using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Item_SkillPoint_2_2", menuName = "SkillPoint/Item_SkillPoint_2_2")]
public class Item_SkillPoint_2_2 : item
{
    public override bool Use()
    {
        PlayerHealthControl.instance.SPReadyTime -= 0.12f;
        SFXManger.instance.PlaySFX(2);
        return true;
    }
}
