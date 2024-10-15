using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Item_SkillPoint_1_4", menuName = "SkillPoint/Item_SkillPoint_1_4")]
public class Item_SkillPoint_1_4 : item
{
    public override bool Use()
    {
        PlayerHealthControl.instance.AddMaxSP(0.5f);
        PlayerHealthControl.instance.changeSP(PlayerHealthControl.instance.maxSP);
        SFXManger.instance.PlaySFX(2);
        return true;
    }
}
