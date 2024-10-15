using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Item_Yinyangyu", menuName = "Inventory/Item_Yinyangyu")]
public class Item_Yinyangyu : item
{

    public EnemyDamagerClass bullet;
    public override bool Use()
    {
        CameraControl.instance.DoShake(0.1f, 0.1f);
        SFXManger.instance.PlaySFX(20);
        // 生成一个子弹
        var bulletObj = Instantiate(bullet, MainPlayer.instance.transform.position, Quaternion.identity);

        // // 将子弹的方向设置为鼠标的方向
        // 获取鼠标的位置
        Vector2 direction = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)MainPlayer.instance.transform.position;

        direction.Normalize();

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // 计算旋转角度
        bulletObj.transform.rotation = Quaternion.Euler(0f, 0f, angle - 90);

        // 设置子弹的速度
        bulletObj.rb.velocity = bulletObj.transform.up * 3.5f;
        bulletObj.lifeTime = 10f;
        bulletObj.damageAmount = 45;
        bulletObj.damageType = 1;


        return true;
    }
}
