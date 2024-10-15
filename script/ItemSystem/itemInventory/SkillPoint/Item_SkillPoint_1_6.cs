using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Item_SkillPoint_1_6", menuName = "SkillPoint/Item_SkillPoint_1_6")]
public class Item_SkillPoint_1_6 : item
{
    public override bool Use()
    {
        PlayerHealthControl.instance.KillEnemyRecoverSP += 0.1f;
        SFXManger.instance.PlaySFX(2);
        return true;
    }
}
