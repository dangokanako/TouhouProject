using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Drops_Point : DropsClass
{

    override protected void Start()
    {
        base.Start();
    }

    override protected void Update()
    {
        base.Update();
    }

    override protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            AssetControl.instance.AddPoint(this.pointValue);
            PlaySE();
            Invoke("DestorySelf", 0.1f);

            // 统计数据
            GlobalControl.instance.totalPoint++;
        }

    }

    override protected void PlaySE()
    {
        SFXManger.instance.PlaySFX(0);
    }


}
