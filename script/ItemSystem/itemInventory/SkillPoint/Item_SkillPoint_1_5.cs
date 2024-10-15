using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Item_SkillPoint_1_5", menuName = "SkillPoint/Item_SkillPoint_1_5")]
public class Item_SkillPoint_1_5 : item
{
    public override bool Use()
    {
        PlayerHealthControl.instance.KillEnemyRecoverHP += 0.1f;
        SFXManger.instance.PlaySFX(2);
        return true;
    }
}
