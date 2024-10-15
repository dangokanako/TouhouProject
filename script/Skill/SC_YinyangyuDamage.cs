using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_YinyangyuDamage : MonoBehaviour
{
    public float damage;
    public float lifetime;
    public float growSpeed = 1f;
    public Vector3 targetSize;
    public float rotateAngle;
    public Rigidbody2D rb;
    public float mass = 70f;
    // 旋转半径
    public float rotateRadius;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.mass = mass;
        targetSize = transform.localScale;
        transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = Vector3.MoveTowards(transform.localScale, targetSize, growSpeed * Time.deltaTime);

        lifetime -= Time.deltaTime;
        if (lifetime <= 0)
        {
            targetSize = Vector3.zero;
            if (transform.localScale.x == 0f)
            {
                // 消除父对象
                if (transform.parent != null)
                {
                    Destroy(transform.parent.gameObject);
                }
            }
        }

        // 计算环绕运动的半径
        float radius = rotateRadius;
        // 计算物体在环绕运动中的角度
        float angle = Time.time * 2.5f * rotateRadius + Mathf.PI * rotateAngle / 180;
        // 计算物体相对于环绕中心的位置偏移
        Vector3 offset = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;

        // 设置圆形刚体的位置
        this.transform.position = MainPlayer.instance.transform.position + offset;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Enemy")
        {
            SFXManger.instance.PlaySFX(21);
            collider.GetComponent<EnemyControl>().TakeDamage(this.damage, (int)DamageType.bluntDamage);
        }
    }

    virtual protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            SFXManger.instance.PlaySFX(21);
            var enemy = collision.gameObject.GetComponent<EnemyControl>();
            enemy.TakeDamage(this.damage, (int)DamageType.bluntDamage);

            this.damage -= 1f;
            if (this.damage <= 0)
            {
                Destroy(gameObject.transform.parent.gameObject);
            }


            // 碰撞伤害系统
            var Rigidbody = this.GetComponent<Rigidbody2D>();
            if (Rigidbody != null)
            {

                // (攻击的质量/4*相对速度)/敌人质量 先测试
                float relativeVelocity = collision.relativeVelocity.magnitude;
                float damage = (Rigidbody.mass / 3 * relativeVelocity) / enemy.GetComponent<Rigidbody2D>().mass;
                if (damage > 1)
                {
                    Debug.Log("相对速度:" + relativeVelocity + "攻击质量:" + Rigidbody.mass + "敌人质量:" + enemy.GetComponent<Rigidbody2D>().mass);
                    Debug.Log("出伤:" + damage);
                    enemy.TakeDamage(damage, true, 1);
                }

            }
        }
    }
}
