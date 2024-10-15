using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Item_SkillPoint_1_2", menuName = "SkillPoint/Item_SkillPoint_1_2")]
public class Item_SkillPoint_1_2 : item
{
    public override bool Use()
    {
        PlayerHealthControl.instance.AddMaxHP(5);
        PlayerHealthControl.instance.changeHP(PlayerHealthControl.instance.maxHealth * 0.5f);
        SFXManger.instance.PlaySFX(2);
        return true;
    }
}
