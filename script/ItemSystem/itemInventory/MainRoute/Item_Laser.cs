using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_Laser", menuName = "MainRoute/Item_Laser")]
public class Item_Laser : item
{
    [Header("SP消耗")]
    public float SPConsume;
    // public float chargePower;
    // public float NextTimeChargePower;
    // private float chargePowerCount;
    public ASC_Laser LaserPrefab;
    public ASC_Laser Laser;
    public override bool Use()
    {
        return true;
    }

    public override bool UseChargePower()
    {
        MainPlayer.instance.PlayWeaponAnime(this.itemImage, 0.6f);



        if (!PlayerHealthControl.instance.UseSP(Time.deltaTime * SPConsume))
            return false;


        if (Laser == null)
            Laser = Instantiate(LaserPrefab, MainPlayer.instance.transform.position, Quaternion.identity);

        // 计算鼠标的世界坐标和玩家的位置之间的方向
        Vector2 direction = ((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)MainPlayer.instance.transform.position).normalized;
        //Vector2 mousePosition = Input.mousePosition - new Vector3(Screen.width / 2, Screen.height / 2, 0);

        // 计算鼠标的位置和玩家的位置之间的方向
        // Vector2 direction = mousePosition.normalized;


        // 设置激光的起始位置和结束位置
        // 使用新的激光方向来设置激光的起始位置和结束位置
        Vector2 startPosition = (Vector2)MainPlayer.instance.transform.position + direction * 0.3f;
        Vector2 endPosition = direction * Laser.maxLength + (Vector2)MainPlayer.instance.transform.position;
        Laser.gameObject.SetActive(true);
        Laser.SetLaser(startPosition, endPosition);

        // Vector2 startPosition = (Vector2)MainPlayer.instance.transform.position + direction * 0.3f;
        // Vector2 endPosition = direction * Laser.maxLength + (Vector2)MainPlayer.instance.transform.position;
        // Laser.gameObject.SetActive(true);
        // Laser.SetLaser(startPosition, endPosition);

        return true;
    }
}