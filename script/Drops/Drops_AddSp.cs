using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Drops_AddSp : DropsClass
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
            PlayerHealthControl.instance.changeSP(1);
            DamageNumberControl.instance.ShowDamage(1, PlayerHealthControl.instance.transform.position, 99);
            PlaySE();
            Invoke("DestorySelf", 0.1f);
        }

    }

    override protected void PlaySE()
    {
        SFXManger.instance.PlaySFX(28);
    }


}
