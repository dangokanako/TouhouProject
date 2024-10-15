using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_PassiveSC_Fengmozhen", menuName = "ForeSC/Item_PassiveSC_Fengmozhen")]
public class Item_PassiveSC_Fengmozhen : item
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
    // 攻击范围
    protected float SCRange;
    // 降低的攻击
    protected float SCReduce;
    // SP消耗
    protected float SPConsume;

    // 实例化对象
    // 原始预制体对象
    public SC_FengmozhenDamage damagerPrefab;
    // 实例化的对象
    private SC_FengmozhenDamage damagerInstance;
    public override bool Use()
    {
        SFXManger.instance.PlaySFX(3);
        return false;
    }

    public override bool Passive()
    {
        // 封魔阵仅创建对象，不需要协程。
        if (damagerInstance == null)
        {
            damagerInstance = Instantiate(damagerPrefab, MainPlayer.instance.transform);
            SetEquipmentSCStats();
        }
        else
        {
            damagerInstance.gameObject.SetActive(true);
        }
        SetEquipmentSCStats();
        return true;
    }

    public override bool discardPassive()
    {
        // 销毁与此装备物品关联的魔法阵实体
        if (damagerInstance != null)
        {
            damagerInstance.gameObject.SetActive(false);
            Destroy(damagerInstance.gameObject);
        }
        return true;
    }
    // 子类重写克隆，克隆新参数
    public override item Clone()
    {
        // 调用父类的clone()方法来复制父类的属性
        Item_PassiveSC_Fengmozhen clonedItem = (Item_PassiveSC_Fengmozhen)base.Clone();

        clonedItem.damageToEmeny = this.damageToEmeny;
        clonedItem.shotCounter = this.shotCounter;
        clonedItem.SPConsume = this.SPConsume;
        clonedItem.SCRange = this.SCRange;
        clonedItem.SCReduce = this.SCReduce;
        clonedItem.damagerInstance = this.damagerInstance;
        clonedItem.damagerPrefab = this.damagerPrefab;

        return clonedItem;
    }
    // 用于升级时重新设置参数
    public override void SetEquipmentSCStats()
    {
        maxEquipmentSCLevel = 15;
        switch (this.isEquipmentSCLevel)
        {
            case 1:
                shotCounter = 1.0f;
                damageToEmeny = 1;
                SPConsume = 0.05f;
                SCRange = 1.3f;
                SCReduce = 10f;
                break;
            case 2:
                shotCounter = 0.75f;
                damageToEmeny = 1;
                SPConsume = 0.05f;
                SCRange = 1.4f;
                SCReduce = 15f;
                break;
            case 3:
                shotCounter = 0.6f;
                damageToEmeny = 1;
                SPConsume = 0.05f;
                SCRange = 1.5f;
                SCReduce = 15f;
                break;
            case 4:
                shotCounter = 1.0f;
                damageToEmeny = 2;
                SPConsume = 0.1f;
                SCRange = 1.6f;
                SCReduce = 20f;
                break;
            case 5:
                shotCounter = 0.75f;
                damageToEmeny = 2;
                SPConsume = 0.1f;
                SCRange = 1.7f;
                SCReduce = 20f;
                break;
            case 6:
                shotCounter = 0.6f;
                damageToEmeny = 2;
                SPConsume = 0.1f;
                SCRange = 1.8f;
                SCReduce = 20f;
                break;
            case 7:
                shotCounter = 0.75f;
                damageToEmeny = 3;
                SPConsume = 0.2f;
                SCRange = 1.9f;
                SCReduce = 25f;
                break;
            case 8:
                shotCounter = 0.6f;
                damageToEmeny = 3;
                SPConsume = 0.2f;
                SCRange = 2.0f;
                SCReduce = 25f;
                break;
            case 9:
                shotCounter = 0.75f;
                damageToEmeny = 4;
                SPConsume = 0.4f;
                SCRange = 2.1f;
                SCReduce = 30f;
                break;
            case 10:
                shotCounter = 0.6f;
                damageToEmeny = 4;
                SPConsume = 0.4f;
                SCRange = 2.2f;
                SCReduce = 30f;
                break;
            case 11:
                shotCounter = 0.75f;
                damageToEmeny = 5;
                SPConsume = 0.5f;
                SCRange = 2.3f;
                SCReduce = 35f;
                break;
            case 12:
                shotCounter = 0.6f;
                damageToEmeny = 5;
                SPConsume = 0.5f;
                SCRange = 2.4f;
                SCReduce = 35f;
                break;
            case 13:
                shotCounter = 0.75f;
                damageToEmeny = 6;
                SPConsume = 0.6f;
                SCRange = 2.5f;
                SCReduce = 40f;
                break;
            case 14:
                shotCounter = 0.6f;
                damageToEmeny = 6;
                SPConsume = 0.6f;
                SCRange = 2.6f;
                SCReduce = 40f;
                break;
            case 15:
                shotCounter = 0.5f;
                damageToEmeny = 6;
                SPConsume = 0.7f;
                SCRange = 2.7f;
                SCReduce = 40f;
                break;
            default:
                break;
        }

        // 参数传递给damager
        damagerInstance.damageAmount = damageToEmeny;
        damagerInstance.timeBetweenDamage = shotCounter;
        damagerInstance.SCRange = this.SCRange * 2;
        damagerInstance.DEFreduce = SCReduce;
        damagerInstance.SPConsume = SPConsume;


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
"永续发动。\n可升级。\n每隔1.0秒消耗0.05SP，对1.3范围内敌人造成1点伤害，并降低范围内敌人10%的近战攻击。",
"永续发动。\n可升级。\n每隔0.75秒消耗0.05SP，对1.4范围内敌人造成1点伤害，并降低范围内敌人15%的近战攻击。",
"永续发动。\n可升级。\n每隔0.6秒消耗0.05SP，对1.5范围内敌人造成1点伤害，并降低范围内敌人15%的近战攻击。",
"永续发动。\n可升级。\n每隔1.0秒消耗0.1SP，对1.6范围内敌人造成2点伤害，并降低范围内敌人20%的近战攻击。",
"永续发动。\n可升级。\n每隔0.75秒消耗0.1SP，对1.7范围内敌人造成2点伤害，并降低范围内敌人20%的近战攻击。",
"永续发动。\n可升级。\n每隔0.6秒消耗0.1SP，对1.8范围内敌人造成2点伤害，并降低范围内敌人20%的近战攻击。",
"永续发动。\n可升级。\n每隔0.75秒消耗0.2SP，对1.9范围内敌人造成3点伤害，并降低范围内敌人25%的近战攻击。",
"永续发动。\n可升级。\n每隔0.6秒消耗0.2SP，对2.0范围内敌人造成3点伤害，并降低范围内敌人25%的近战攻击。",
"永续发动。\n可升级。\n每隔0.75秒消耗0.4SP，对2.1范围内敌人造成4点伤害，并降低范围内敌人30%的近战攻击。",
"永续发动。\n可升级。\n每隔0.6秒消耗0.4SP，对2.2范围内敌人造成4点伤害，并降低范围内敌人30%的近战攻击。",
"永续发动。\n可升级。\n每隔0.75秒消耗0.5SP，对2.3范围内敌人造成5点伤害，并降低范围内敌人35%的近战攻击。",
"永续发动。\n可升级。\n每隔0.6秒消耗0.5SP，对2.4范围内敌人造成5点伤害，并降低范围内敌人35%的近战攻击。",
"永续发动。\n可升级。\n每隔0.75秒消耗0.6SP，对2.5范围内敌人造成6点伤害，并降低范围内敌人40%的近战攻击。",
"永续发动。\n可升级。\n每隔0.6秒消耗0.6SP，对2.6范围内敌人造成6点伤害，并降低范围内敌人40%的近战攻击。",
"永续发动。\n可升级。\n每隔0.5秒消耗0.7SP，对2.7范围内敌人造成6点伤害，并降低范围内敌人40%的近战攻击。",
    };


}