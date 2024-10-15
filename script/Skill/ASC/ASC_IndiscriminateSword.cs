using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.Mathematics;
using UnityEngine;

public class ASC_IndiscriminateSword : EnemyDamagerClass
{

    // 属性
    public float damage = 1f;
    // 悬停状态
    [SerializeField] private float ReadyTime;

    // private Collider2D co;
    private TrailRenderer tr;
    // 索敌时的范围 
    public float SCRange;
    // 锁定敌人位置，即使移动也扎这
    public Vector3 targetPosition;
    // 飞刀图层
    public LayerMask whatIsEnemyLayer;

    // 随机方向
    private float randomDirectionX, randomDirectionY;

    //测试用 目标锁定
    //public Transform targettransform;
    // Start is called before the first frame update
    override protected void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponentInChildren<TrailRenderer>();
        // co = GetComponent<Collider2D>();

        Vector2 randomVector2 = UnityEngine.Random.insideUnitCircle.normalized * 1f;
        randomDirectionY = randomVector2.y;
        randomDirectionX = randomVector2.x;
        // 计算并设置初始角度
        // 当一个代码运行没有问题并且看不懂，就不要去动他
        Vector3 targetDir = new Vector3(randomDirectionX + transform.position.x, randomDirectionY + transform.position.y, 0) - transform.position;
        transform.eulerAngles = new Vector3(0, 0, Vector3.Angle(transform.up, targetDir) * (randomDirectionX > 0 ? -1 : 1));
    }

    // 飞刀的动画
    override protected void Update()
    {
        ReadyTime -= Time.deltaTime;
        // 悬停状态
        if (ReadyTime > 0.5f)
        {
            if (tr != null)
                tr.enabled = false;

            //collider2D.enabled = false;
            //transform.eulerAngles = new Vector3(0, 0, angle);

            rb.velocity = new Vector2(randomDirectionX, randomDirectionY) * 4f;
        }
        else if (ReadyTime > 0f)
        {
            rb.velocity = Vector2.zero;

            // 如果范围里有敌人才发射飞刀
            Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, SCRange,
            whatIsEnemyLayer);
            if (enemies.Length > 0 && targetPosition == Vector3.zero)
            //什么？万一真的0点有敌人怎么办？再说吧
            {
                targetPosition = enemies[0].transform.position;
            }

            if (targetPosition != Vector3.zero)
            {
                Vector3 direction = targetPosition - transform.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                angle -= 90;

                var directionRoration = Quaternion.AngleAxis(angle, Vector3.forward);
                transform.rotation = Quaternion.Lerp(transform.rotation, directionRoration, 0.15f);
            }

        }
        else if (ReadyTime > -4)
        {
            if (tr != null)
                tr.enabled = true;

            transform.position += transform.up * 7 * Time.deltaTime;
        }
        else if (ReadyTime < -5)
        {
            Destroy(gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Enemy")
        {
            collider.GetComponent<EnemyControl>().TakeDamage(damage, true, (int)DamageType.SCDamage);
            Destroy(gameObject);
        }
    }

}
