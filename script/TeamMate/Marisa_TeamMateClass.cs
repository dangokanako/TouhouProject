using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marisa_TeamMateClass : TeamBaseClass
{

    // 攻击间隔组件
    private float attackTime = 5f;
    private float attackTimedelta = 5f;

    // 重写Attack方法   
    protected override void Attack()
    {
        if (GlobalControl.instance.isBattle)
        {
            // 每五秒进行一次攻击   
            if (attackTimedelta > 0)
            {
                attackTimedelta -= Time.deltaTime;
            }
            else
            {
                // 重置攻击时间
                attackTimedelta = attackTime;
                // 攻击
                StartCoroutine(AttackCoroutine());
            }
        }
        else
        {
            return;
        }
    }

    // 攻击协程
    private IEnumerator AttackCoroutine()
    {
        for (int i = 0; i < 720; i += 30)
        {
            int random = Random.Range(1, 21);

            // 这不是我想要的效果………………
            var bullet = Instantiate(SCAactiveControl.instance.ASC_MarisaBullet, this.transform.position, Quaternion.AngleAxis(25 * i + random, Vector3.forward));
            bullet.rb.velocity = bullet.transform.right * 4f;
            bullet.moveSpeedAdd = 0.025f;



            yield return new WaitForSeconds(0.1f);
        }
    }
}
