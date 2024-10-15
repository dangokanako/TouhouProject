using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Item_SkillPoint_2_1", menuName = "SkillPoint/Item_SkillPoint_2_1")]
public class Item_SkillPoint_2_1 : item
{
    public override bool Use()
    {
        MainPlayer.instance.MoveSpeed += 0.12f;
        SFXManger.instance.PlaySFX(2);
        return true;
    }
}
