using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Item_SkillPoint_2_4", menuName = "SkillPoint/Item_SkillPoint_2_4")]
public class Item_SkillPoint_2_4 : item
{
    public override bool Use()
    {
        MainPlayer.instance.isFreeDash = true;
        SFXManger.instance.PlaySFX(2);
        return true;
    }
}
