using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Enemy_Boss : EnemyControl
{


    // 增强模式简介
    // 其本意是为了防止玩家一个符卡爆发攻击直接把BOSS秒了
    // 所以在增强模式下，BOSS处于无敌状态，玩家的攻击无法对其造成伤害
    // 但为了防止此时BOSS再对玩家造成巨额伤害
    // 会大大降低BOSS的速度，使其无法追击玩家

    // 是否进入第一阶段
    protected bool firstStage = false;
    protected bool secondStage = false;

    public string ShowName;

    protected override void Start()
    {
        base.Start();

        // 在屏幕上显示BOSS血条
        UIControl.instance.BossHealth.SetActive(true);
        UIControl.instance.maxHealth_Boss = this.health;
        UIControl.instance.currentHealth_Boss = this.health;
        UIControl.instance.slider_Boss.value = 1;
        UIControl.instance.NameText_Boxx.text = ShowName;
        UIControl.instance.HealthText_Boss.text = UIControl.instance.maxHealth_Boss.ToString() + "/" + UIControl.instance.maxHealth_Boss.ToString();

        Map_Battle_Wuzhihu_1.instance.GetBubbleByBoss(ShowName);
    }

    // Update is called once per frame
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    override protected void enemyDead()
    {
        // 在屏幕上隐藏BOSS血条
        UIControl.instance.BossHealth.SetActive(false);
        base.enemyDead();
    }

    override protected void getDamage()
    {
        // 更新BOSS血条
        UIControl.instance.BossHealth.SetActive(true);
        UIControl.instance.currentHealth_Boss = this.health;
        UIControl.instance.maxHealth_Boss = this.maxHealth;
        UIControl.instance.NameText_Boxx.text = ShowName;
        UIControl.instance.HealthText_Boss.text = UIControl.instance.currentHealth_Boss.ToString("F1") + "/" + UIControl.instance.maxHealth_Boss.ToString();


        // 在0.1秒内将滑动条的值渐变到目标值
        float targetValue = this.health / this.maxHealth;
        UIControl.instance.slider_Boss.DOValue(targetValue, 0.2f);
        // UIControl.instance.slider_Boss.value = UIControl.instance.currentHealth_Boss / UIControl.instance.maxHealth_Boss;
    }
}
