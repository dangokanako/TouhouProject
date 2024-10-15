using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_LongKnives", menuName = "Weapon_Blade/Item_LongKnives")]
public class Item_LongKnives : item
{
    [Header("剑的预制体")]
    public SwordEnemyBulletBase swordObject;

    /// <summary>
    /// 攻击速度。单位为10秒钟内可以攻击的次数
    /// 例如15的话，就是10秒钟内可以攻击15次，每次攻击的时间间隔为0.67秒
    /// </summary>
    /// 10为慢，20为中
    [SerializeField] private float attackSpeed;
    /// <summary>
    /// 每次攻击的时间
    /// </summary>
    [SerializeField] private float attackTime { get { return 10 / attackSpeed; } }

    /// <summary>
    ///  攻击距离 0.5为近
    /// </summary>
    [SerializeField] private float attackDistance;
    // 武器CD 可不填
    [SerializeField] private float weaponCD;
    // SP消耗
    [SerializeField] private float spCost;
    // 额外攻击角度 
    // 多20度就会多20度的攻击范围。
    [SerializeField] private float attackAngle;
    // 是否能自动使用
    [SerializeField] private bool autoUse;
    // 伤害
    [SerializeField] private float damage;

    public override bool Use()
    {
        // 计算武器通用CD
        if (MainPlayer.instance.WeaponCDCount > 0)
            return false;

        // 扣取SP费用
        if (!PlayerHealthControl.instance.UseSP(spCost))
            return false;

        // 武器CD
        if (weaponCD == 0)
        {
            MainPlayer.instance.WeaponCDCount = attackTime;
        }
        else
        {
            MainPlayer.instance.WeaponCDCount = weaponCD;
        }

        // 播放音效
        SFXManger.instance.PlaySFX(10);

        // 在鼠标位置播放动画
        // 鼠标位置只能在自身的范围内，如果在范围外，那么距离将会被限制在范围内
        Vector2 direction = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)MainPlayer.instance.transform.position;

        direction.Normalize();
        if (direction.magnitude > attackDistance)
        {
            direction *= attackDistance;
        }

        var temp2 = Instantiate(swordObject, (Vector2)MainPlayer.instance.transform.position + direction, Quaternion.identity, MainPlayer.instance.transform);

        temp2.lifeTime = attackTime;
        if (damage != 0)
        {
            temp2.damageAmount = damage;
        }


        // 根据方向旋转图片
        // 荣获我最不想看的代码奖
        float angle = Vector2.Angle(Vector2.right, direction);
        if (direction.x < 0 && direction.y < 0)
        {
            temp2.transform.rotation = Quaternion.Euler(0, 180, angle - 180);
            temp2.transform.DORotate(new Vector3(0, 180, angle - 270 - attackAngle), attackTime);
        }
        else if (direction.x > 0 && direction.y < 0)
        {
            temp2.transform.rotation = Quaternion.Euler(0, 0, -angle);
            temp2.transform.DORotate(new Vector3(0, 0, -angle - 90 - attackAngle), attackTime);
        }
        else if (direction.x < 0 && direction.y > 0)
        {
            temp2.transform.rotation = Quaternion.Euler(0, 180, -angle + 180);
            temp2.transform.DORotate(new Vector3(0, 180, -angle + 90 - attackAngle), attackTime);
        }
        else if (direction.x > 0 && direction.y > 0)
        {
            temp2.transform.rotation = Quaternion.Euler(0, 0, angle);
            temp2.transform.DORotate(new Vector3(0, 0, angle - 90 - attackAngle), attackTime);
        }



        return true;
    }
}
