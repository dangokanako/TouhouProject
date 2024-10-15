using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_Bagualu_1", menuName = "MainRoute/Item_Bagualu_1")]
public class Item_Bagualu_1 : item
{
    // SP消耗
    public float SPConsume;
    public float chargePower;
    public float NextTimeChargePower;
    private float chargePowerCount;
    public EnemyDamagerClass bullet;
    public override bool Use()
    {
        return true;
    }

    public override bool UseChargePower()
    {
        MainPlayer.instance.PlayWeaponAnime(this.itemImage, 0.6f);


        if (PlayerHealthControl.instance.UseSP(Time.deltaTime * SPConsume))
            chargePowerCount += Time.deltaTime;

        if (chargePowerCount > chargePower)
        {
            chargePowerCount = 0;
            NextTimeChargePower = chargePower * Random.Range(0.8f, 1.2f);

            // // 将子弹的方向设置为鼠标的方向
            // 获取鼠标的位置的方向
            Vector2 direction = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)MainPlayer.instance.transform.position;
            direction.Normalize();
            direction = new Vector2(direction.x + UnityEngine.Random.Range(-0.2f, 0.2f), direction.y + UnityEngine.Random.Range(-0.2f, 0.2f));

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // 计算旋转角度


            SFXManger.instance.PlaySFX(4, default, false);

            // 生成一个子弹
            var bulletObj = Instantiate(bullet, (Vector2)MainPlayer.instance.transform.position + direction * 0.05f, Quaternion.Euler(0f, 0f, angle));
            bulletObj.initialVelocity *= Random.Range(0.7f, 1.3f);

            // 随机颜色
            string[] bulletPath = new string[] { "bullet/marisa/blue", "bullet/marisa/blue2", "bullet/marisa/green", "bullet/marisa/greenblue", "bullet/marisa/yellow", "bullet/marisa/yellow2", "bullet/marisa/red", "bullet/marisa/red2" };

            bulletObj.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(bulletPath[Random.Range(0, bulletPath.Length)]);

            // 随机大小
            float random = Random.Range(0.5f, 2f);
            bulletObj.transform.localScale = new Vector3(random, random, 0);

            return true;
        }

        return true;
    }
}