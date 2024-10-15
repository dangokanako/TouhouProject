using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.Mathematics;
using UnityEngine;

public class SC_Yinyangyu : MonoBehaviour
{
    public Transform holder;
    public Transform tobeCreated;
    [SerializeField] private float timeBetweenCreate;
    [SerializeField] private float createCounter;
    // Start is called before the first frame update
    public SC_YinyangyuDamage damager;
    // 产生数量
    public int amount;
    // 消耗SP
    public float SPConsume;
    // 阴阳玉质量
    public float mass;
    // 旋转速度
    public float rotateSpeed;
    // 攻击伤害
    public float damageToEmeny;


    void Update()
    {
        if (GlobalControl.instance.isBattle)
        {
            createCounter -= Time.deltaTime;
            if (createCounter <= 0)
            {
                createCounter = timeBetweenCreate;
                //Instantiate(tobeCreated, tobeCreated.position, tobeCreated.rotation, holder).gameObject.SetActive(true);

                if (PlayerHealthControl.instance.UseSP(SPConsume, false, false))
                {
                    for (int i = 0; i < amount; i++)
                    {
                        float rot = (360f / amount) * i;
                        var temp =
                            Instantiate(tobeCreated, tobeCreated.position,
                           Quaternion.Euler(0f, 0f, rot)
                           ,
                            holder);
                        temp.gameObject.SetActive(true);
                        var yanyangyu = temp.GetComponentInChildren<SC_YinyangyuDamage>();

                        yanyangyu.rotateAngle = rot;
                        yanyangyu.damage = damageToEmeny;
                        yanyangyu.mass = mass;
                        // yanyangyu.rotateSpeed = rotateSpeed;
                        yanyangyu.rotateRadius = rotateSpeed;
                    }
                }
            }
        }

    }

    // 注意，伤害组件是另写在有碰撞器检测的组件上的。
}
