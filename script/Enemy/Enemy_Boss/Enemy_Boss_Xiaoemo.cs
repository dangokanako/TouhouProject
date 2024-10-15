using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Boss_Xiaoemo : Enemy_Boss
{
    public PlayerDamagerClass bulletPrefab;
    protected override void Start()
    {
        base.Start();
        StartCoroutine(ShootBullet());
    }


    // Update is called once per frame
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }
    private IEnumerator ShootBullet()
    {
        while (true)
        {
            // 创建子弹
            var bullet = Instantiate(bulletPrefab, this.transform.position, Quaternion.identity);

            // 计算子弹的方向
            Vector2 direction = MainPlayer.instance.transform.position - this.transform.position;
            direction.Normalize();
            // 施加一个力使子弹朝向玩家的方向移动
            bullet.rb.velocity = direction * 2f;

            yield return new WaitForSeconds(2f);
        }
    }
}
