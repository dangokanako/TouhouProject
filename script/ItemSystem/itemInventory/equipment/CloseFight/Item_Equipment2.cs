using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_Equipment2", menuName = "CloseFight/Item_Equipment2")]
public class Item_Equipment2 : item
{
    public override bool Use()
    {
        SFXManger.instance.PlaySFX(3);
        return false;
    }

    public override bool Passive()
    {
        // 根据品质调整数值
        switch (this.itemCiti)
        {
            case ItemQuality.Inferior:
                PlayerHealthControl.instance._PlayerAtk += 12;
                PlayerHealthControl.instance.PlayerCollsionCritical += 1;
                PlayerHealthControl.instance.PlayerCollsionDef += 4;
                PlayerHealthControl.instance.HowManyInvincibleTime += 0.3f;
                break;
            case ItemQuality.Normal:
                PlayerHealthControl.instance._PlayerAtk += 14;
                PlayerHealthControl.instance.PlayerCollsionCritical += 1;
                PlayerHealthControl.instance.PlayerCollsionDef += 4.5f;
                PlayerHealthControl.instance.HowManyInvincibleTime += 0.4f;
                break;
            case ItemQuality.Good:
                PlayerHealthControl.instance._PlayerAtk += 16;
                PlayerHealthControl.instance.PlayerCollsionCritical += 1;
                PlayerHealthControl.instance.PlayerCollsionDef += 5f;
                PlayerHealthControl.instance.HowManyInvincibleTime += 0.5f;
                break;
            case ItemQuality.Excellent:
                PlayerHealthControl.instance._PlayerAtk += 18;
                PlayerHealthControl.instance.PlayerCollsionCritical += 1;
                PlayerHealthControl.instance.PlayerCollsionDef += 5.5f;
                PlayerHealthControl.instance.HowManyInvincibleTime += 0.6f;
                break;
            case ItemQuality.Legendary:
                PlayerHealthControl.instance._PlayerAtk += 20;
                PlayerHealthControl.instance.PlayerCollsionCritical += 1;
                PlayerHealthControl.instance.PlayerCollsionDef += 6;
                PlayerHealthControl.instance.HowManyInvincibleTime += 0.7f;
                break;
        }

        return true;
    }

    public override bool discardPassive()
    {
        // 根据品质调整数值
        switch (this.itemCiti)
        {
            case ItemQuality.Inferior:
                PlayerHealthControl.instance._PlayerAtk -= 12;
                PlayerHealthControl.instance.PlayerCollsionCritical -= 1;
                PlayerHealthControl.instance.PlayerCollsionDef -= 4;
                PlayerHealthControl.instance.HowManyInvincibleTime -= 0.3f;
                break;
            case ItemQuality.Normal:
                PlayerHealthControl.instance._PlayerAtk -= 14;
                PlayerHealthControl.instance.PlayerCollsionCritical -= 1;
                PlayerHealthControl.instance.PlayerCollsionDef -= 4.5f;
                PlayerHealthControl.instance.HowManyInvincibleTime -= 0.4f;
                break;
            case ItemQuality.Good:
                PlayerHealthControl.instance._PlayerAtk -= 16;
                PlayerHealthControl.instance.PlayerCollsionCritical -= 1;
                PlayerHealthControl.instance.PlayerCollsionDef -= 5f;
                PlayerHealthControl.instance.HowManyInvincibleTime -= 0.5f;
                break;
            case ItemQuality.Excellent:
                PlayerHealthControl.instance._PlayerAtk -= 18;
                PlayerHealthControl.instance.PlayerCollsionCritical -= 1;
                PlayerHealthControl.instance.PlayerCollsionDef -= 5.5f;
                PlayerHealthControl.instance.HowManyInvincibleTime -= 0.6f;
                break;
            case ItemQuality.Legendary:
                PlayerHealthControl.instance._PlayerAtk -= 20;
                PlayerHealthControl.instance.PlayerCollsionCritical -= 1;
                PlayerHealthControl.instance.PlayerCollsionDef -= 6;
                PlayerHealthControl.instance.HowManyInvincibleTime -= 0.7f;
                break;
        }
        return true;
    }

    // 获取品质文本
    public override string GetQualityText()
    {
        switch (this.itemCiti)
        {
            case ItemQuality.Inferior:
                return "装备\n持有时碰撞必定产生暴击，提升12点碰撞攻击，减少4点来自敌人的碰撞攻击，受到伤害后的无敌时间增加0.3秒";
            case ItemQuality.Normal:
                return "装备\n持有时碰撞必定产生暴击，提升14点碰撞攻击，减少4.5点来自敌人的碰撞攻击，受到伤害后的无敌时间增加0.4秒";
            case ItemQuality.Good:
                return "装备\n持有时碰撞必定产生暴击，提升16点碰撞攻击，减少5点来自敌人的碰撞攻击，受到伤害后的无敌时间增加0.5秒";
            case ItemQuality.Excellent:
                return "装备\n持有时碰撞必定产生暴击，提升18点碰撞攻击，减少5.5点来自敌人的碰撞攻击，受到伤害后的无敌时间增加0.6秒";
            case ItemQuality.Legendary:
                return "装备\n持有时碰撞必定产生暴击，提升20点碰撞攻击，减少6点来自敌人的碰撞攻击，受到伤害后的无敌时间增加0.7秒";
            case ItemQuality.Null:
                return "装备\n持有时碰撞必定产生暴击，提升15点碰撞攻击，减少4.5点来自敌人的碰撞攻击，受到伤害后的无敌时间增加0.6秒";
        }
        return "itemGetQualityText()报错了";
    }
}