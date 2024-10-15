using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_VampireWing", menuName = "CloseFight/Item_VampireWing")]
public class Item_VampireWing : item
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
                MainPlayer.instance.dashForce += 0.5f;
                PlayerHealthControl.instance._PlayerAtk += 2;
                PlayerHealthControl.instance.CloseFightAnimation += 1;
                break;
            case ItemQuality.Normal:
                MainPlayer.instance.dashForce += 0.6f;
                PlayerHealthControl.instance._PlayerAtk += 3;
                PlayerHealthControl.instance.CloseFightAnimation += 1;
                break;
            case ItemQuality.Good:
                MainPlayer.instance.dashForce += 0.7f;
                PlayerHealthControl.instance._PlayerAtk += 4;
                PlayerHealthControl.instance.CloseFightAnimation += 1;
                break;
            case ItemQuality.Excellent:
                MainPlayer.instance.dashForce += 0.8f;
                PlayerHealthControl.instance._PlayerAtk += 5;
                PlayerHealthControl.instance.CloseFightAnimation += 1;
                break;
            case ItemQuality.Legendary:
                MainPlayer.instance.dashForce += 0.9f;
                PlayerHealthControl.instance._PlayerAtk += 6;
                PlayerHealthControl.instance.CloseFightAnimation += 1;
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
                MainPlayer.instance.dashForce -= 0.5f;
                PlayerHealthControl.instance._PlayerAtk -= 2;
                PlayerHealthControl.instance.CloseFightAnimation -= 1;
                break;
            case ItemQuality.Normal:
                MainPlayer.instance.dashForce -= 0.6f;
                PlayerHealthControl.instance._PlayerAtk -= 3;
                PlayerHealthControl.instance.CloseFightAnimation -= 1;
                break;
            case ItemQuality.Good:
                MainPlayer.instance.dashForce -= 0.7f;
                PlayerHealthControl.instance._PlayerAtk -= 4;
                PlayerHealthControl.instance.CloseFightAnimation -= 1;
                break;
            case ItemQuality.Excellent:
                MainPlayer.instance.dashForce -= 0.8f;
                PlayerHealthControl.instance._PlayerAtk -= 5;
                PlayerHealthControl.instance.CloseFightAnimation -= 1;
                break;
            case ItemQuality.Legendary:
                MainPlayer.instance.dashForce -= 0.9f;
                PlayerHealthControl.instance._PlayerAtk -= 6;
                PlayerHealthControl.instance.CloseFightAnimation -= 1;
                break;
        }
        return true;
    }

    // 获取品质文本
    public override string GetQualityText()
    {
        /*
        持有时提供额外50%的力度突进，2点碰撞攻击。
持有时提供额外60%的力度突进，3点碰撞攻击。
持有时提供额外70%的力度突进，4点碰撞攻击。
持有时提供额外80%的力度突进，5点碰撞攻击。
持有时提供额外90%的力度突进，6点碰撞攻击。
        */
        switch (this.itemCiti)
        {
            case ItemQuality.Inferior:
                return "持有时提供额外50%的突进力，2点碰撞攻击。";
            case ItemQuality.Normal:
                return "持有时提供额外60%的突进力，3点碰撞攻击。";
            case ItemQuality.Good:
                return "持有时提供额外70%的突进力，4点碰撞攻击。";
            case ItemQuality.Excellent:
                return "持有时提供额外80%的突进力，5点碰撞攻击。";
            case ItemQuality.Legendary:
                return "持有时提供额外90%的突进力，6点碰撞攻击。";
            case ItemQuality.Null:
                return "持有时提供额外60%的突进力，3点碰撞攻击。";


        }
        return "itemGetQualityText()报错了";
    }
}