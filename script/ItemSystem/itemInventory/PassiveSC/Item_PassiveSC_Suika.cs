using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_PassiveSC_Suika", menuName = "ForeSC/Item_PassiveSC_Suika")]
public class Item_PassiveSC_Suika : item
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
    // 西瓜刀大小
    protected float SCScale;
    // 西瓜刀重量
    protected float SCWeight;
    // 西瓜刀丢的个数
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
        Item_PassiveSC_Suika clonedItem = (Item_PassiveSC_Suika)base.Clone();


        clonedItem.damageToEmeny = this.damageToEmeny;
        clonedItem.shotCounter = this.shotCounter;
        clonedItem.SCScale = this.SCScale;
        clonedItem.Amount = this.Amount;
        clonedItem.damager = this.damager;

        return clonedItem;
    }
    // 用于升级时重新设置参数
    public override void SetEquipmentSCStats()
    {
        maxEquipmentSCLevel = 15;
        switch (this.isEquipmentSCLevel)
        {

            case 0:
                Debug.Log("报错了，没有0级，笨~蛋~"); break;


            case 1:
                SPConsume = 0.3f;
                shotCounter = 1.5f;
                Amount = 1;
                damageToEmeny = 6;
                SCWeight = 60f;
                break;

            case 2:
                SPConsume = 0.3f;
                shotCounter = 1.5f;
                Amount = 1;
                damageToEmeny = 7;
                SCWeight = 70f;
                break;

            case 3:
                SPConsume = 0.3f;
                shotCounter = 1.5f;
                Amount = 1;
                damageToEmeny = 8;
                SCWeight = 80f;
                break;

            case 4:
                SPConsume = 0.6f;
                shotCounter = 1.5f;
                Amount = 2;
                damageToEmeny = 6;
                SCWeight = 60f;
                break;

            case 5:
                SPConsume = 0.6f;
                shotCounter = 1.5f;
                Amount = 2;
                damageToEmeny = 7;
                SCWeight = 70f;
                break;

            case 6:
                SPConsume = 0.6f;
                shotCounter = 1.3f;
                Amount = 2;
                damageToEmeny = 7;
                SCWeight = 70f;
                break;

            case 7:
                SPConsume = 0.6f;
                shotCounter = 1.5f;
                Amount = 2;
                damageToEmeny = 8;
                SCWeight = 80f;
                break;

            case 8:
                SPConsume = 0.6f;
                shotCounter = 1.3f;
                Amount = 2;
                damageToEmeny = 8;
                SCWeight = 80f;
                break;

            case 9:
                SPConsume = 0.6f;
                shotCounter = 1.5f;
                Amount = 2;
                damageToEmeny = 9;
                SCWeight = 90f;
                break;

            case 10:
                SPConsume = 0.9f;
                shotCounter = 1.3f;
                Amount = 3;
                damageToEmeny = 7;
                SCWeight = 70f;
                break;

            case 11:
                SPConsume = 0.9f;
                shotCounter = 1.5f;
                Amount = 3;
                damageToEmeny = 8;
                SCWeight = 80f;
                break;

            case 12:
                SPConsume = 0.9f;
                shotCounter = 1.4f;
                Amount = 3;
                damageToEmeny = 8;
                SCWeight = 80f;
                break;

            case 13:
                SPConsume = 0.9f;
                shotCounter = 1.3f;
                Amount = 3;
                damageToEmeny = 8;
                SCWeight = 80f;
                break;

            case 14:
                SPConsume = 0.9f;
                shotCounter = 1.5f;
                Amount = 3;
                damageToEmeny = 9;
                SCWeight = 90f;
                break;

            case 15:
                SPConsume = 0.9f;
                shotCounter = 1.4f;
                Amount = 3;
                damageToEmeny = 9;
                SCWeight = 90f;
                break;

            default:
                Debug.Log("报错，等级溢出。");
                break;
        }
    }
    // 用于根据等级获取文本
    public override string GetEquipmentSCText()
    {
        switch (this.isEquipmentSCLevel)
        {
            case 0: return "报错了，没有0级，笨~蛋~";
            case 1: return "永续发动。\n可升级。\n每隔1.5秒消耗0.3SP，随机丢出一把6伤害60重量的西瓜刀。";
            case 2: return "永续发动。\n可升级。\n每隔1.5秒消耗0.3SP，随机丢出一把7伤害70重量的西瓜刀。";
            case 3: return "永续发动。\n可升级。\n每隔1.5秒消耗0.3SP，随机丢出一把8伤害80重量的西瓜刀。";
            case 4: return "永续发动。\n可升级。\n每隔1.5秒消耗0.6SP，随机丢出两把6伤害60重量的西瓜刀。";
            case 5: return "永续发动。\n可升级。\n每隔1.5秒消耗0.6SP，随机丢出两把7伤害70重量的西瓜刀。";
            case 6: return "永续发动。\n可升级。\n每隔1.3秒消耗0.6SP，随机丢出两把7伤害70重量的西瓜刀。";
            case 7: return "永续发动。\n可升级。\n每隔1.5秒消耗0.6SP，随机丢出两把8伤害80重量的西瓜刀。";
            case 8: return "永续发动。\n可升级。\n每隔1.3秒消耗0.6SP，随机丢出两把8伤害80重量的西瓜刀。";
            case 9: return "永续发动。\n可升级。\n每隔1.5秒消耗0.6SP，随机丢出两把9伤害90重量的西瓜刀。";
            case 10: return "永续发动。\n可升级。\n每隔1.3秒消耗0.9SP，随机丢出三把7伤害70重量的西瓜刀。";
            case 11: return "永续发动。\n可升级。\n每隔1.5秒消耗0.9SP，随机丢出三把8伤害80重量的西瓜刀。";
            case 12: return "永续发动。\n可升级。\n每隔1.4秒消耗0.9SP，随机丢出三把8伤害80重量的西瓜刀。";
            case 13: return "永续发动。\n可升级。\n每隔1.3秒消耗0.9SP，随机丢出三把8伤害80重量的西瓜刀。";
            case 14: return "永续发动。\n可升级。\n每隔1.5秒消耗0.9SP，随机丢出三把9伤害90重量的西瓜刀。";
            case 15: return "永续发动。\n可升级。\n每隔1.4秒消耗0.9SP，随机丢出三把9伤害90重量的西瓜刀。";
            default: return "报错，等级溢出。";

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

                // 消耗体力，不重置READY时间
                if (PlayerHealthControl.instance.UseSP(SPConsume, false, false))
                {
                    for (int i = 0; i < this.Amount; i++)
                    {
                        damager.damageAmount = damageToEmeny;
                        damager.rb.mass = SCWeight;
                        // 哈哈，写数值的时候忘记这个属性了。
                        // damager.transform.localScale = new Vector3(SCScale, SCScale, 1);
                        Instantiate(damager, MainPlayer.instance.transform.position, damager.transform.rotation).gameObject.SetActive(true);
                    }
                }
            }

        }
    }

}

