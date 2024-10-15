using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_Qumozhen_1", menuName = "MainRoute/Item_Qumozhen_1")]
public class Item_Qumozhen_1 : item
{
    public float SPConsume;
    public float chargePower;
    private float chargePowerCount;
    public EnemyDamagerClass bullet;
    public float NextTimeChargePower;

    public override bool Use()
    {
        return true;
    }

    public override bool UseChargePower()
    {
        if (PlayerHealthControl.instance.UseSP(Time.deltaTime * SPConsume))
            chargePowerCount += Time.deltaTime;

        if (chargePowerCount > chargePower)
        {
            chargePowerCount = 0;
            SFXManger.instance.PlaySFX(4, default, false);


            // // 将子弹的方向设置为鼠标的方向
            // 获取鼠标的位置的方向
            Vector2 direction = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)MainPlayer.instance.transform.position;
            direction.Normalize();
            direction = new Vector2(direction.x + UnityEngine.Random.Range(0.1f, 0.1f), direction.y + UnityEngine.Random.Range(-0.1f, 0.1f));

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // 计算旋转角度



            // 生成一个子弹
            var bulletObj = Instantiate(bullet, (Vector2)MainPlayer.instance.transform.position + direction * 0.01f, Quaternion.Euler(0f, 0f, angle));
            bulletObj.initialVelocity *= Random.Range(0.7f, 1.3f);

            return true;
        }

        return true;
    }
}