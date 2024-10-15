using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_PassiveSC_Fuzha", menuName = "ForeSC/Item_PassiveSC_Fuzha")]
public class Item_PassiveSC_Fuzha : item
{
    // 生成一个全局唯一的ID
    private string uniqueId = System.Guid.NewGuid().ToString();

    // 是否更新

    /*
    以下变量为单个用
    */

    // 攻击间隔
    protected float shotCounter;
    // 攻击伤害
    protected float damageToEmeny;
    // BUFF
    protected float SCReduce;
    // SP消耗
    protected float SPConsume;
    // 实例化对象
    public SC_Fuzha damager;
    public override bool Use()
    {
        SFXManger.instance.PlaySFX(3);
        return false;
    }

    public override bool Passive()
    {
        // 装备后即开始线程
        BuffCoroutineControl.instance.StartCoroutine(uniqueId, MainCon());

        return true;
    }

    public override bool discardPassive()
    {
        // 装备取下时终止线程
        BuffCoroutineControl.instance.StopTheCoroutine(uniqueId);
        return true;
    }

    // 子类重写克隆，克隆新参数
    public override item Clone()
    {
        // 调用父类的clone()方法来复制父类的属性
        Item_PassiveSC_Fuzha clonedItem = (Item_PassiveSC_Fuzha)base.Clone();

        clonedItem.shotCounter = this.shotCounter;
        clonedItem.damageToEmeny = this.damageToEmeny;
        clonedItem.SCReduce = this.SCReduce;
        clonedItem.SPConsume = this.SPConsume;
        clonedItem.damager = this.damager;

        return clonedItem;
    }
    // 用于升级时重新设置参数
    public override void SetEquipmentSCStats()
    {
        maxEquipmentSCLevel = 15;
        switch (this.isEquipmentSCLevel)
        {
            case 0: Debug.Log("报错了，没有0级，笨~蛋~"); break;
            case 1:
                shotCounter = 2;
                SPConsume = 0.3f;
                damageToEmeny = 5;
                SCReduce = 10f;
                break;
            case 2:
                shotCounter = 2;
                SPConsume = 0.3f;
                damageToEmeny = 6;
                SCReduce = 10f;
                break;
            case 3:
                shotCounter = 2;
                SPConsume = 0.3f;
                damageToEmeny = 7;
                SCReduce = 10f;
                break;
            case 4:
                shotCounter = 2;
                SPConsume = 0.3f;
                damageToEmeny = 7;
                SCReduce = 15f;
                break;
            case 5:
                shotCounter = 2;
                SPConsume = 0.3f;
                damageToEmeny = 8;
                SCReduce = 15f;
                break;
            case 6:
                shotCounter = 2;
                SPConsume = 0.3f;
                damageToEmeny = 9;
                SCReduce = 15f;
                break;
            case 7:
                shotCounter = 2;
                SPConsume = 0.3f;
                damageToEmeny = 9;
                SCReduce = 20f;
                break;
            case 8:
                shotCounter = 2;
                SPConsume = 0.6f;
                damageToEmeny = 10;
                SCReduce = 20f;
                break;
            case 9:
                shotCounter = 2;
                SPConsume = 0.6f;
                damageToEmeny = 10;
                SCReduce = 25f;
                break;
            case 10:
                shotCounter = 2;
                SPConsume = 0.6f;
                damageToEmeny = 11;
                SCReduce = 25f;
                break;
            case 11:
                shotCounter = 2;
                SPConsume = 0.6f;
                damageToEmeny = 11;
                SCReduce = 30f;
                break;
            case 12:
                shotCounter = 2;
                SPConsume = 0.6f;
                damageToEmeny = 12;
                SCReduce = 30f;
                break;
            case 13:
                shotCounter = 2;
                SPConsume = 0.6f;
                damageToEmeny = 12;
                SCReduce = 35f;
                break;
            case 14:
                shotCounter = 2;
                SPConsume = 0.6f;
                damageToEmeny = 13;
                SCReduce = 35f;
                break;
            case 15:
                shotCounter = 2;
                SPConsume = 0.6f;
                damageToEmeny = 13;
                SCReduce = 40f;
                break;
            default:
                Debug.Log("报错，等级溢出。");
                break;
        }
    }
    // 用于根据等级获取文本
    public override string GetEquipmentSCText()
    {
        if (this.isEquipmentSCLevel < 0 || this.isEquipmentSCLevel >= LevelTexts.Length)
        {
            return "报错，等级溢出。";
        }

        return LevelTexts[this.isEquipmentSCLevel];
    }

    private static readonly string[] LevelTexts = new string[]
    {
        "报错了，没有0级，笨~蛋~",
"永续发动。\n可升级。\n每隔2.0秒消耗0.4SP，在面前放出一个能对敌人造成5点钝系伤害的符札，并降低敌人10%的移动速度。并且会爆炸。",
"永续发动。\n可升级。\n每隔2.0秒消耗0.4SP，在面前放出一个能对敌人造成6点钝系伤害的符札，并降低敌人10%的移动速度。并且会爆炸。",
"永续发动。\n可升级。\n每隔2.0秒消耗0.4SP，在面前放出一个能对敌人造成7点钝系伤害的符札，并降低敌人10%的移动速度。并且会爆炸。",
"永续发动。\n可升级。\n每隔2.0秒消耗0.4SP，在面前放出一个能对敌人造成7点钝系伤害的符札，并降低敌人15%的移动速度。并且会爆炸。",
"永续发动。\n可升级。\n每隔2.0秒消耗0.4SP，在面前放出一个能对敌人造成8点钝系伤害的符札，并降低敌人15%的移动速度。并且会爆炸。",
"永续发动。\n可升级。\n每隔2.0秒消耗0.4SP，在面前放出一个能对敌人造成9点钝系伤害的符札，并降低敌人15%的移动速度。并且会爆炸。",
"永续发动。\n可升级。\n每隔2.0秒消耗0.4SP，在面前放出一个能对敌人造成9点钝系伤害的符札，并降低敌人20%的移动速度。并且会爆炸。",
"永续发动。\n可升级。\n每隔2.0秒消耗0.6SP，在面前放出一个能对敌人造成10点钝系伤害的符札，并降低敌人20%的移动速度。并且会爆炸。",
"永续发动。\n可升级。\n每隔2.0秒消耗0.6SP，在面前放出一个能对敌人造成10点钝系伤害的符札，并降低敌人25%的移动速度。并且会爆炸。",
"永续发动。\n可升级。\n每隔2.0秒消耗0.6SP，在面前放出一个能对敌人造成11点钝系伤害的符札，并降低敌人25%的移动速度。并且会爆炸。",
"永续发动。\n可升级。\n每隔2.0秒消耗0.6SP，在面前放出一个能对敌人造成11点钝系伤害的符札，并降低敌人30%的移动速度。并且会爆炸。",
"永续发动。\n可升级。\n每隔2.0秒消耗0.6SP，在面前放出一个能对敌人造成12点钝系伤害的符札，并降低敌人30%的移动速度。并且会爆炸。",
"永续发动。\n可升级。\n每隔2.0秒消耗0.6SP，在面前放出一个能对敌人造成12点钝系伤害的符札，并降低敌人35%的移动速度。并且会爆炸。",
"永续发动。\n可升级。\n每隔2.0秒消耗0.6SP，在面前放出一个能对敌人造成13点钝系伤害的符札，并降低敌人35%的移动速度。并且会爆炸。",
"永续发动。\n可升级。\n每隔2.0秒消耗0.6SP，在面前放出一个能对敌人造成13点钝系伤害的符札，并降低敌人40%的移动速度。并且会爆炸。",
    };

    // 主要内容协程
    private IEnumerator MainCon()
    {
        SetEquipmentSCStats();

        while (true)
        {
            yield return new WaitForSeconds(shotCounter);

            if (GlobalControl.instance.isBattle)
            {
                if (PlayerHealthControl.instance.UseSP(SPConsume, false, false))
                {
                    if (MainPlayer.instance.facingDirection == -1)
                    {
                        damager.transform.rotation = UnityEngine.Quaternion.Euler(0f, 0f, 180f);
                    }
                    else
                    {
                        damager.transform.rotation = UnityEngine.Quaternion.identity;
                    }
                    damager.SPEDreduce = SCReduce;
                    damager.damageAmount = damageToEmeny;
                    Instantiate(damager, MainPlayer.instance.transform);
                }
            }
        }

    }
}
