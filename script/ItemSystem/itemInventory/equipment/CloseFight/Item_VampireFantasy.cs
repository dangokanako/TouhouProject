using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_VampireFantasy", menuName = "CloseFight/Item_VampireFantasy")]
public class Item_VampireFantasy : item
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
                MainPlayer.instance.dashForce += 0.8f;
                PlayerHealthControl.instance._PlayerAtk += 4;
                PlayerHealthControl.instance.CloseFightAnimation += 1;
                PlayerHealthControl.instance.CloseFightVampire += 1.5f;
                break;
            case ItemQuality.Normal:
                MainPlayer.instance.dashForce += 0.9f;
                PlayerHealthControl.instance._PlayerAtk += 5;
                PlayerHealthControl.instance.CloseFightAnimation += 1;
                PlayerHealthControl.instance.CloseFightVampire += 1.7f;
                break;
            case ItemQuality.Good:
                MainPlayer.instance.dashForce += 1.0f;
                PlayerHealthControl.instance._PlayerAtk += 6;
                PlayerHealthControl.instance.CloseFightAnimation += 1;
                PlayerHealthControl.instance.CloseFightVampire += 1.9f;
                break;
            case ItemQuality.Excellent:
                MainPlayer.instance.dashForce += 1.1f;
                PlayerHealthControl.instance._PlayerAtk += 7;
                PlayerHealthControl.instance.CloseFightAnimation += 1;
                PlayerHealthControl.instance.CloseFightVampire += 2.1f;
                break;
            case ItemQuality.Legendary:
                MainPlayer.instance.dashForce += 1.2f;
                PlayerHealthControl.instance._PlayerAtk += 8;
                PlayerHealthControl.instance.CloseFightAnimation += 1;
                PlayerHealthControl.instance.CloseFightVampire += 2.3f;
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
                MainPlayer.instance.dashForce -= 0.8f;
                PlayerHealthControl.instance._PlayerAtk -= 4;
                PlayerHealthControl.instance.CloseFightAnimation -= 1;
                PlayerHealthControl.instance.CloseFightVampire -= 1.5f;
                break;
            case ItemQuality.Normal:
                MainPlayer.instance.dashForce -= 0.9f;
                PlayerHealthControl.instance._PlayerAtk -= 5;
                PlayerHealthControl.instance.CloseFightAnimation -= 1;
                PlayerHealthControl.instance.CloseFightVampire -= 1.7f;
                break;
            case ItemQuality.Good:
                MainPlayer.instance.dashForce -= 1.0f;
                PlayerHealthControl.instance._PlayerAtk -= 6;
                PlayerHealthControl.instance.CloseFightAnimation -= 1;
                PlayerHealthControl.instance.CloseFightVampire -= 1.9f;
                break;
            case ItemQuality.Excellent:
                MainPlayer.instance.dashForce -= 1.1f;
                PlayerHealthControl.instance._PlayerAtk -= 7;
                PlayerHealthControl.instance.CloseFightAnimation -= 1;
                PlayerHealthControl.instance.CloseFightVampire -= 2.1f;
                break;
            case ItemQuality.Legendary:
                MainPlayer.instance.dashForce -= 1.2f;
                PlayerHealthControl.instance._PlayerAtk -= 8;
                PlayerHealthControl.instance.CloseFightAnimation -= 1;
                PlayerHealthControl.instance.CloseFightVampire -= 2.3f;
                break;
        }
        return true;
    }

    // 获取品质文本
    public override string GetQualityText()
    {
        /*
        持有时提供额外80%的力度突进，4点碰撞攻击，2.0%的锐钝吸血。
        持有时提供额外90%的力度突进，5点碰撞攻击，2.5%的锐钝吸血。
        持有时提供额外100%的力度突进，6点碰撞攻击，3.0%的锐钝吸血。
        持有时提供额外110%的力度突进，7点碰撞攻击，3.5%的锐钝吸血。
        持有时提供额外120%的力度突进，8点碰撞攻击，4.0%的锐钝吸血。
        */
        switch (this.itemCiti)
        {
            case ItemQuality.Inferior:
                return "持有时提供额外80%的突进力，4点碰撞攻击，1.5%的锐钝吸血。";
            case ItemQuality.Normal:
                return "持有时提供额外90%的突进力，5点碰撞攻击，1.7%的锐钝吸血。";
            case ItemQuality.Good:
                return "持有时提供额外100%的突进力，6点碰撞攻击，1.9%的锐钝吸血。";
            case ItemQuality.Excellent:
                return "持有时提供额外110%的突进力，7点碰撞攻击，2.1%的锐钝吸血。";
            case ItemQuality.Legendary:
                return "持有时提供额外120%的突进力，8点碰撞攻击，2.3%的锐钝吸血。";
            case ItemQuality.Null:
                return "持有时提供额外90%的突进力，5点碰撞攻击，1.7%的锐钝吸血。";
        }
        return "itemGetQualityText()报错了";
    }
}