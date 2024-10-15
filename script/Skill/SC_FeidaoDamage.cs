using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_FeidaoDamage : EnemyDamagerClass
{
    public float moveSpeed = 1f;

    override protected void Update()
    {
        base.Update();
        transform.position += transform.up * moveSpeed * Time.deltaTime;
    }
}
