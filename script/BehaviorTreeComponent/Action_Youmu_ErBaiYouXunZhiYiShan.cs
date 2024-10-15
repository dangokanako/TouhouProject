using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;


public class Action_Youmu_ErBaiYouXunZhiYiShan : Action
{
    private Rigidbody2D rb;
    public PlayerDamagerClass bulletPrefab;
    public int power;
    public override void OnStart()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }
    public override TaskStatus OnUpdate()
    {
        BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("youmu_small_1_0", "狱界剑「二百由旬之一闪」！"));

        StartCoroutine(DoAction());
        return TaskStatus.Success;
    }

    private IEnumerator DoAction()
    {
        yield return new WaitForSeconds(0.2f);


        var target = new Vector3(MainPlayer.instance.transform.position.x + Random.Range(-1f, 1f), MainPlayer.instance.transform.position.y + Random.Range(-1f, 1f));
        var direction = target - this.transform.position;
        direction.Normalize();

        List<PlayerDamagerClass> list = new List<PlayerDamagerClass>();
        for (int i = 0; i < 130; i++)
        {
            var bulletPosition = this.transform.position + (1f + 0.1f * i) * direction;
            bulletPosition = new Vector2(bulletPosition.x + Random.Range(-0.3f, 0.3f), bulletPosition.y + Random.Range(-0.3f, 0.3f));
            var bullet = Object.Instantiate(bulletPrefab, bulletPosition, Quaternion.Euler(0, 0, Random.Range(0, 360))); //生成子弹
            bullet.growSpeed = 0.4f;
            bullet.lifeTime = 9f;
            bullet.initialVelocity = 0f;
            list.Add(bullet);
            yield return new WaitForSeconds(0.008f);
        }

        rb.AddForce(power * direction, ForceMode2D.Impulse);

        yield return new WaitForSeconds(0.7f);

        for (int i = 0; i < 130; i++)
        {
            list[i].moveSpeedAdd = 0.1f;
            list[i].growSpeed = 5f;
            yield return new WaitForSeconds(0.02f);
        }

        yield return new WaitForSeconds(0.5f);
    }

}
