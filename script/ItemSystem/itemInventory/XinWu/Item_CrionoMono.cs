using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Item_CrionoMono", menuName = "Xinwu/Item_CrionoMono")]
public class Item_CrionoMono : item
{
    public EnemyDamagerClass bullet;
    private float Rate;
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
                Rate = 2.3f;
                EnemyControl.OnEnemyDead += HandleEnemyDead;
                break;
            case ItemQuality.Normal:
                Rate = 2.8f;
                EnemyControl.OnEnemyDead += HandleEnemyDead;
                break;
            case ItemQuality.Good:
                Rate = 3.3f;
                EnemyControl.OnEnemyDead += HandleEnemyDead;
                break;
            case ItemQuality.Excellent:
                Rate = 3.8f;
                EnemyControl.OnEnemyDead += HandleEnemyDead;
                break;
            case ItemQuality.Legendary:
                Rate = 4.3f;
                EnemyControl.OnEnemyDead += HandleEnemyDead;
                break;
        }
        if (GlobalControl.instance.teammate == 1)
            Rate *= 2;

        return true;
    }

    public override bool discardPassive()
    {
        // 根据品质调整数值
        switch (this.itemCiti)
        {
            case ItemQuality.Inferior:
                EnemyControl.OnEnemyDead -= HandleEnemyDead;
                break;
            case ItemQuality.Normal:
                EnemyControl.OnEnemyDead -= HandleEnemyDead;
                break;
            case ItemQuality.Good:
                EnemyControl.OnEnemyDead -= HandleEnemyDead;
                break;
            case ItemQuality.Excellent:
                EnemyControl.OnEnemyDead -= HandleEnemyDead;
                break;
            case ItemQuality.Legendary:
                EnemyControl.OnEnemyDead -= HandleEnemyDead;
                break;
        }
        return true;
    }

    private void HandleEnemyDead()
    {
        float random = Random.Range(0f, 100f);
        if (random < Rate)
        {
            SCAactiveControl.instance.SCActive_CrinoMono();
        }
    }

    // 获取品质文本
    public override string GetQualityText()
    {
        switch (this.itemCiti)
        {
            case ItemQuality.Inferior:
                return "持有时，2.3%的概率敌人死亡时会追加一次冰冻弹幕攻击。琪露诺作为队友时，触发概率和弹幕量提升至两倍。";
            case ItemQuality.Normal:
                return "持有时，2.8%的概率敌人死亡时会追加一次冰冻弹幕攻击。琪露诺作为队友时，触发概率和弹幕量提升至两倍。";
            case ItemQuality.Good:
                return "持有时，3.3%的概率敌人死亡时会追加一次冰冻弹幕攻击。琪露诺作为队友时，触发概率和弹幕量提升至两倍。";
            case ItemQuality.Excellent:
                return "持有时，3.8%的概率敌人死亡时会追加一次冰冻弹幕攻击。琪露诺作为队友时，触发概率和弹幕量提升至两倍。";
            case ItemQuality.Legendary:
                return "持有时，4.3%的概率敌人死亡时会追加一次冰冻弹幕攻击。琪露诺作为队友时，触发概率和弹幕量提升至两倍。";
            case ItemQuality.Null:
                return "持有时，2.8%的概率敌人死亡时会追加一次冰冻弹幕攻击。琪露诺作为队友时，触发概率和弹幕量提升至两倍。";
        }
        return "itemGetQualityText()报错了";
    }
}
