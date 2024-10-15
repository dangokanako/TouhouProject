using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Item_SakuyanoSword", menuName = "Inventory/Item_SakuyanoSword")]
public class Item_SakuyanoSword : item
{

    public EnemyDamagerClass bullet;
    public override bool Use()
    {
        SFXManger.instance.PlaySFX(4);
        // 生成一个子弹
        var bulletObj = Instantiate(bullet, MainPlayer.instance.transform.position, Quaternion.identity);

        // // 将子弹的方向设置为鼠标的方向
        // 获取鼠标的位置
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 direction = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)MainPlayer.instance.transform.position;

        direction.Normalize();

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // 计算旋转角度
        bulletObj.transform.rotation = Quaternion.Euler(0f, 0f, angle - 90);

        // 设置子弹的速度
        bulletObj.rb.velocity = bulletObj.transform.up * 12f;
        bulletObj.lifeTime = 0.8f;
        bulletObj.damageAmount = 18;
        bulletObj.damageType = 2;


        return true;
    }
}
