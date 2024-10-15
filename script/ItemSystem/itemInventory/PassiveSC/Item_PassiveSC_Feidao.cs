using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_PassiveSC_Feidao", menuName = "ForeSC/Item_PassiveSC_Feidao")]
public class Item_PassiveSC_Feidao : item
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
    // 攻击检测距离
    protected float SCRange;
    // 敌人layer
    public LayerMask whatIsEnemyLayer;
    // 飞刀丢的个数
    protected int Amount;
    // SP消耗
    protected float SPConsume;
    // 实例化对象
    public EnemyDamagerClass damager;
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
        Item_PassiveSC_Feidao clonedItem = (Item_PassiveSC_Feidao)base.Clone();

        clonedItem.shotCounter = this.shotCounter;
        clonedItem.damageToEmeny = this.damageToEmeny;
        clonedItem.SCRange = this.SCRange;
        clonedItem.whatIsEnemyLayer = this.whatIsEnemyLayer;
        clonedItem.Amount = this.Amount;
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
                {
                    SCRange = 3;
                    Amount = 1;
                    SPConsume = 0.1f;
                    damageToEmeny = 3;
                    shotCounter = 1.8f;
                    break;
                }
            case 2:
                {
                    SCRange = 3;
                    Amount = 1;
                    SPConsume = 0.1f;
                    damageToEmeny = 4;
                    shotCounter = 1.6f;
                    break;
                }
            case 3:
                {
                    SCRange = 3;
                    Amount = 1;
                    SPConsume = 0.1f;
                    damageToEmeny = 5;
                    shotCounter = 1.4f;
                    break;
                }
            case 4:
                {
                    SCRange = 3;
                    Amount = 2;
                    SPConsume = 0.2f;
                    damageToEmeny = 5;
                    shotCounter = 2.5f;
                    break;
                }
            case 5:
                {
                    SCRange = 3.5f;
                    Amount = 2;
                    SPConsume = 0.2f;
                    damageToEmeny = 5;
                    shotCounter = 2f;
                    break;
                }
            case 6:
                {
                    SCRange = 3.5f;
                    Amount = 2;
                    SPConsume = 0.2f;
                    damageToEmeny = 5;
                    shotCounter = 1.6f;
                    break;
                }
            case 7:
                {
                    SCRange = 4;
                    Amount = 2;
                    SPConsume = 0.2f;
                    damageToEmeny = 5;
                    shotCounter = 1.3f;
                    break;
                }
            case 8:
                {
                    SCRange = 4;
                    Amount = 2;
                    SPConsume = 0.2f;
                    damageToEmeny = 6;
                    shotCounter = 1.3f;
                    break;
                }
            case 9:
                {
                    SCRange = 4;
                    Amount = 2;
                    SPConsume = 0.2f;
                    damageToEmeny = 6;
                    shotCounter = 1.1f;
                    break;
                }
            case 10:
                {
                    SCRange = 4.5f;
                    Amount = 2;
                    SPConsume = 0.2f;
                    damageToEmeny = 7;
                    shotCounter = 1.1f;
                    break;
                }
            case 11:
                {
                    SCRange = 4.5f;
                    Amount = 3;
                    SPConsume = 0.3f;
                    damageToEmeny = 7;
                    shotCounter = 1.4f;
                    break;
                }
            case 12:
                {
                    SCRange = 5;
                    Amount = 3;
                    SPConsume = 0.3f;
                    damageToEmeny = 7;
                    shotCounter = 1.2f;
                    break;
                }
            case 13:
                {
                    SCRange = 5;
                    Amount = 3;
                    SPConsume = 0.3f;
                    damageToEmeny = 7;
                    shotCounter = 1f;
                    break;
                }
            case 14:
                {
                    SCRange = 5;
                    Amount = 3;
                    SPConsume = 0.3f;
                    damageToEmeny = 8;
                    shotCounter = 1f;
                    break;
                }
            case 15:
                {
                    SCRange = 5;
                    Amount = 3;
                    SPConsume = 0.3f;
                    damageToEmeny = 9;
                    shotCounter = 1f;
                    break;
                }
            default: Debug.Log("报错，等级溢出。"); break;
        }
    }
    // 用于根据等级获取文本
    public override string GetEquipmentSCText()
    {
        switch (this.isEquipmentSCLevel)
        {
            case 0: return "报错了，没有0级，笨~蛋~";
            case 1:
                return "永续发动。\n可升级。\n每隔1.8秒消耗0.1SP，向周围的敌人丢出一枚3点锐器伤害的飞刀。";
            case 2:
                return "永续发动。\n可升级。\n每隔1.6秒消耗0.1SP，向周围的敌人丢出一枚4点锐器伤害的飞刀。";
            case 3:
                return "永续发动。\n可升级。\n每隔1.4秒消耗0.1SP，向周围的敌人丢出一枚5点锐器伤害的飞刀。";
            case 4:
                return "永续发动。\n可升级。\n每隔2.5秒消耗0.2SP，向周围的敌人丢出两枚5点锐器伤害的飞刀。";
            case 5:
                return "永续发动。\n可升级。\n每隔2秒消耗0.2SP，向周围的敌人丢出两枚5点锐器伤害的飞刀。";
            case 6:
                return "永续发动。\n可升级。\n每隔1.6秒消耗0.2SP，向周围的敌人丢出两枚5点锐器伤害的飞刀。";
            case 7:
                return "永续发动。\n可升级。\n每隔1.3秒消耗0.2SP，向周围的敌人丢出两枚5点锐器伤害的飞刀。";
            case 8:
                return "永续发动。\n可升级。\n每隔1.3秒消耗0.2SP，向周围的敌人丢出两枚6点锐器伤害的飞刀。";
            case 9:
                return "永续发动。\n可升级。\n每隔1.1秒消耗0.2SP，向周围的敌人丢出两枚6点锐器伤害的飞刀。";
            case 10:
                return "永续发动。\n可升级。\n每隔1.1秒消耗0.2SP，向周围的敌人丢出两枚7点锐器伤害的飞刀。";
            case 11:
                return "永续发动。\n可升级。\n每隔1.4秒消耗0.3SP，向周围的敌人丢出三枚7点锐器伤害的飞刀。";
            case 12:
                return "永续发动。\n可升级。\n每隔1.2秒消耗0.3SP，向周围的敌人丢出三枚7点锐器伤害的飞刀。";
            case 13:
                return "永续发动。\n可升级。\n每隔1秒消耗0.3SP，向周围的敌人丢出三枚7点锐器伤害的飞刀。";
            case 14:
                return "永续发动。\n可升级。\n每隔1秒消耗0.3SP，向周围的敌人丢出三枚8点锐器伤害的飞刀。";
            case 15:
                return "永续发动。\n可升级。\n每隔1秒消耗0.3SP，向周围的敌人丢出三枚9点锐器伤害的飞刀。";
            default:
                return "报错，等级溢出。";

        }
    }
    // 主要内容协程
    private IEnumerator MainCon()
    {
        SetEquipmentSCStats();

        while (true)
        {
            yield return new WaitForSeconds(shotCounter);

            // if (statsUpdate)
            // {
            //     statsUpdate = false;
            //     SetEquipmentSCStats();
            // }

            if (GlobalControl.instance.isBattle)
            {

                // 如果范围里有敌人才发射飞刀
                Collider2D[] enemies = Physics2D.OverlapCircleAll(MainPlayer.instance.transform.position, this.SCRange,
                this.whatIsEnemyLayer);
                if (enemies.Length > 0)
                {
                    // 消耗体力，不重置READY时间
                    if (PlayerHealthControl.instance.UseSP(SPConsume, false, false))
                    {
                        SFXManger.instance.PlaySFX(29);
                        for (int i = 0; i < this.Amount; i++)
                        {
                            Vector3 targetPosition = enemies[UnityEngine.Random.Range(0, enemies.Length)].transform.position;
                            Vector3 direction = targetPosition - MainPlayer.instance.transform.position;
                            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                            angle -= 90;
                            // angle加-15到+15的随机范围
                            angle += UnityEngine.Random.Range(-15, 15);
                            damager.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                            damager.damageAmount = damageToEmeny;
                            Instantiate(damager, MainPlayer.instance.transform.position, damager.transform.rotation).gameObject.SetActive(true);
                        }
                    }
                }
            }

        }
    }

}