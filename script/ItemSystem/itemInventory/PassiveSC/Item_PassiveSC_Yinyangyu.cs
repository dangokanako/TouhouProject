using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_PassiveSC_Yinyangyu", menuName = "ForeSC/Item_PassiveSC_Yinyangyu")]
public class Item_PassiveSC_Yinyangyu : item
{
    // 生成一个全局唯一的ID
    private string uniqueId = System.Guid.NewGuid().ToString();

    /*
    以下变量为单个用
    */

    // 攻击伤害
    protected float damageToEmeny;
    // 阴阳玉个数
    protected int Amount;
    // 阴阳玉旋转速度
    protected float rotateSpeed;
    // 阴阳玉质量
    protected float mass;
    // SP消耗
    protected float SPConsume;

    // 原始预制体对象
    public SC_Yinyangyu damagerPrefab;
    // 实例化的对象
    private SC_Yinyangyu damagerInstance;

    public override bool Use()
    {
        SFXManger.instance.PlaySFX(3);
        return false;
    }

    public override bool Passive()
    {
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
        // 销毁与此装备物品关联的实体
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
        Item_PassiveSC_Yinyangyu clonedItem = (Item_PassiveSC_Yinyangyu)base.Clone();
        clonedItem.damageToEmeny = damageToEmeny;
        clonedItem.Amount = Amount;
        clonedItem.rotateSpeed = rotateSpeed;
        clonedItem.mass = mass;
        clonedItem.SPConsume = SPConsume;
        clonedItem.damagerPrefab = damagerPrefab;
        clonedItem.damagerInstance = damagerInstance;

        return clonedItem;
    }
    // 用于升级时重新设置参数
    public override void SetEquipmentSCStats()
    {
        /*
        永续发动。\n可升级。\n每隔1.0秒消耗0.2SP，在附近维持一个能对敌人造成7点伤害70质量1.0转速的阴阳玉。
永续发动。\n可升级。\n每隔1.0秒消耗0.2SP，在附近维持一个能对敌人造成8点伤害80质量1.1转速的阴阳玉。
永续发动。\n可升级。\n每隔1.0秒消耗0.2SP，在附近维持一个能对敌人造成9点伤害90质量1.2转速的阴阳玉。
永续发动。\n可升级。\n每隔1.0秒消耗0.2SP，在附近维持一个能对敌人造成10点伤害100质量1.3转速的阴阳玉。
永续发动。\n可升级。\n每隔1.0秒消耗0.4SP，在附近维持两个能对敌人造成7点伤害70质量1.3转速的阴阳玉。
永续发动。\n可升级。\n每隔1.0秒消耗0.4SP，在附近维持两个能对敌人造成8点伤害80质量1.4转速的阴阳玉。
永续发动。\n可升级。\n每隔1.0秒消耗0.4SP，在附近维持两个能对敌人造成9点伤害90质量1.5转速的阴阳玉。
永续发动。\n可升级。\n每隔1.0秒消耗0.4SP，在附近维持两个能对敌人造成10点伤害100质量1.5转速的阴阳玉。
永续发动。\n可升级。\n每隔1.0秒消耗0.6SP，在附近维持三个能对敌人造成7点伤害70质量1.5转速的阴阳玉。
永续发动。\n可升级。\n每隔1.0秒消耗0.6SP，在附近维持三个能对敌人造成8点伤害80质量1.6转速的阴阳玉。
永续发动。\n可升级。\n每隔1.0秒消耗0.6SP，在附近维持三个能对敌人造成9点伤害90质量1.7转速的阴阳玉。
永续发动。\n可升级。\n每隔1.0秒消耗0.6SP，在附近维持三个能对敌人造成10点伤害100质量1.8转速的阴阳玉。
永续发动。\n可升级。\n每隔1.0秒消耗0.6SP，在附近维持四个能对敌人造成7点伤害70质量1.8转速的阴阳玉。
永续发动。\n可升级。\n每隔1.0秒消耗0.6SP，在附近维持四个能对敌人造成8点伤害80质量1.9转速的阴阳玉。
永续发动。\n可升级。\n每隔1.0秒消耗0.6SP，在附近维持四个能对敌人造成9点伤害100质量2.0转速的阴阳玉。

        */
        maxEquipmentSCLevel = 15;
        switch (this.isEquipmentSCLevel)
        {
            case 0: Debug.Log("报错了，没有0级，笨~蛋~"); break;
            case 1:
                Amount = 1;
                SPConsume = 0.2f;
                damageToEmeny = 7;
                mass = 70;
                rotateSpeed = 1.0f;
                break;
            case 2:
                Amount = 1;
                SPConsume = 0.2f;
                damageToEmeny = 8;
                mass = 80;
                rotateSpeed = 1.1f;
                break;
            case 3:
                Amount = 1;
                SPConsume = 0.2f;
                damageToEmeny = 9;
                mass = 90;
                rotateSpeed = 1.2f;
                break;
            case 4:
                Amount = 1;
                SPConsume = 0.2f;
                damageToEmeny = 10;
                mass = 100;
                rotateSpeed = 1.3f;
                break;
            case 5:
                Amount = 2;
                SPConsume = 0.4f;
                damageToEmeny = 7;
                mass = 70;
                rotateSpeed = 1.3f;
                break;
            case 6:
                Amount = 2;
                SPConsume = 0.4f;
                damageToEmeny = 8;
                mass = 80;
                rotateSpeed = 1.4f;
                break;
            case 7:
                Amount = 2;
                SPConsume = 0.4f;
                damageToEmeny = 9;
                mass = 90;
                rotateSpeed = 1.5f;
                break;
            case 8:
                Amount = 2;
                SPConsume = 0.4f;
                damageToEmeny = 10;
                mass = 100;
                rotateSpeed = 1.5f;
                break;
            case 9:
                Amount = 3;
                SPConsume = 0.6f;
                damageToEmeny = 7;
                mass = 70;
                rotateSpeed = 1.5f;
                break;
            case 10:
                Amount = 3;
                SPConsume = 0.6f;
                damageToEmeny = 8;
                mass = 80;
                rotateSpeed = 1.6f;
                break;
            case 11:
                Amount = 3;
                SPConsume = 0.6f;
                damageToEmeny = 9;
                mass = 90;
                rotateSpeed = 1.7f;
                break;
            case 12:
                Amount = 3;
                SPConsume = 0.6f;
                damageToEmeny = 10;
                mass = 100;
                rotateSpeed = 1.8f;
                break;
            case 13:
                Amount = 4;
                SPConsume = 0.6f;
                damageToEmeny = 7;
                mass = 70;
                rotateSpeed = 1.8f;
                break;
            case 14:
                Amount = 4;
                SPConsume = 0.6f;
                damageToEmeny = 8;
                mass = 80;
                rotateSpeed = 1.9f;
                break;
            case 15:
                Amount = 4;
                SPConsume = 0.6f;
                damageToEmeny = 9;
                mass = 100;
                rotateSpeed = 2.0f;
                break;
        }
        damagerInstance.amount = Amount;
        damagerInstance.SPConsume = SPConsume;
        damagerInstance.damageToEmeny = damageToEmeny;
        damagerInstance.mass = mass;
        damagerInstance.rotateSpeed = rotateSpeed;

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
        "永续发动。\n可升级。\n每隔1.0秒消耗0.2SP，在附近维持一个能对敌人造成7点伤害70质量1.0转速的阴阳玉。",
        "永续发动。\n可升级。\n每隔1.0秒消耗0.2SP，在附近维持一个能对敌人造成8点伤害80质量1.1转速的阴阳玉。",
        "永续发动。\n可升级。\n每隔1.0秒消耗0.2SP，在附近维持一个能对敌人造成9点伤害90质量1.2转速的阴阳玉。",
        "永续发动。\n可升级。\n每隔1.0秒消耗0.2SP，在附近维持一个能对敌人造成10点伤害100质量1.3转速的阴阳玉。",
        "永续发动。\n可升级。\n每隔1.0秒消耗0.4SP，在附近维持两个能对敌人造成7点伤害70质量1.3转速的阴阳玉。",
        "永续发动。\n可升级。\n每隔1.0秒消耗0.4SP，在附近维持两个能对敌人造成8点伤害80质量1.4转速的阴阳玉。",
        "永续发动。\n可升级。\n每隔1.0秒消耗0.4SP，在附近维持两个能对敌人造成9点伤害90质量1.5转速的阴阳玉。",
        "永续发动。\n可升级。\n每隔1.0秒消耗0.4SP，在附近维持两个能对敌人造成10点伤害100质量1.5转速的阴阳玉。",
        "永续发动。\n可升级。\n每隔1.0秒消耗0.6SP，在附近维持三个能对敌人造成7点伤害70质量1.5转速的阴阳玉。",
        "永续发动。\n可升级。\n每隔1.0秒消耗0.6SP，在附近维持三个能对敌人造成8点伤害80质量1.6转速的阴阳玉。",
        "永续发动。\n可升级。\n每隔1.0秒消耗0.6SP，在附近维持三个能对敌人造成9点伤害90质量1.7转速的阴阳玉。",
        "永续发动。\n可升级。\n每隔1.0秒消耗0.6SP，在附近维持三个能对敌人造成10点伤害100质量1.8转速的阴阳玉。",
        "永续发动。\n可升级。\n每隔1.0秒消耗0.6SP，在附近维持四个能对敌人造成7点伤害70质量1.8转速的阴阳玉。",
        "永续发动。\n可升级。\n每隔1.0秒消耗0.6SP，在附近维持四个能对敌人造成8点伤害80质量1.9转速的阴阳玉。",
        "永续发动。\n可升级。\n每隔1.0秒消耗0.6SP，在附近维持四个能对敌人造成9点伤害100质量2.0转速的阴阳玉。",
    };


}