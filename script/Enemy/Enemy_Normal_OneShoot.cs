using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Normal_OneShoot : Enemy_Normal
{
    // 射击概率
    [Header("射击概率")]
    public float shootProbability;
    [Header("射击子弹")]
    public PlayerDamagerClass bulletPrefab;
    // 图片的子弹方向
    // 0:上 1:右 2:下 3:左
    public int bulletFacingDirection;

    [Header("射击速度")]
    public float shootSpeed = 2f;
    // Start is called before the first frame update
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

            // 根据射击概率创建子弹
            if (Random.Range(0f, 1f) > shootProbability)
            {
                yield return new WaitForSeconds(3f);
                continue;
            }

            // 随机等待1-2秒
            yield return new WaitForSeconds(Random.Range(1f, 2f));

            // 创建子弹
            var bullet = Instantiate(bulletPrefab, this.transform.position, Quaternion.identity);

            // 把子弹旋转朝向玩家
            Vector3 vectorToTarget = MainPlayer.instance.transform.position - this.transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
            bullet.transform.rotation = Quaternion.Euler(0f, 0f, angle + 90 * bulletFacingDirection);


            // 计算子弹的方向
            Vector2 direction = MainPlayer.instance.transform.position - this.transform.position;
            direction.Normalize();
            // 施加一个力使子弹朝向玩家的方向移动
            bullet.rb.velocity = direction * shootSpeed;

            yield return new WaitForSeconds(3f);
        }
    }
}
