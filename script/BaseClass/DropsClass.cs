using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropsClass : MonoBehaviour
{

    [SerializeField] public int expValue;
    [SerializeField] public int pointValue;


    protected bool moveingToPlayer = false;
    protected float moveSpeed = 0.03f;
    // 隔一段时间进行检查
    protected float timeBetweenCheck = 1.0f;
    protected float checkCount;
    // 掉落动画
    public float dropAnim = 2f;
    // 吸收动画，后退秒
    public float backAnim;
    public Vector3 ScaleToBig;
    // 初始重力设置
    public float GraSet;
    public Rigidbody2D rb;
    // 丢弃时的速度方向调整
    public bool dropFlag = false;
    // 物品存在时间
    public float ExistTime = 95f;
    public bool dontDisappear = false;


    public void ChangecheckCount(float time)
    {
        checkCount = time;
    }
    virtual protected void DestorySelf()
    {
        Destroy(gameObject);
    }
    virtual protected void PlaySE()
    {

    }
    virtual protected void OnTriggerEnter2D(Collider2D collision)
    {
        // 实现请写到子类里面！
    }

    virtual protected void Start()
    {
        ScaleToBig = transform.localScale * 1.15f;
        rb.gravityScale = GraSet;
        if (!dropFlag)
            rb.velocity = Vector2.up + new Vector2(Random.Range(-2f, 2f), 0);
        else
        {
            // 从背包丢弃动画
            rb.velocity = Vector2.up + new Vector2(Random.Range(0.5f, 1f) * MainPlayer.instance.facingDirection, 0);
            rb.gravityScale += 0.1f;
        }

    }

    // Update is called once per frame
    virtual protected void Update()
    {
        dropAnim -= Time.deltaTime;
        if (dropAnim <= 0)
        {
            rb.gravityScale = 0;
            rb.velocity = Vector2.zero;
        }

        if (moveingToPlayer && !PlayerHealthControl.instance.isDead)
        {
            backAnim -= Time.deltaTime;
            // 接近模式
            if (backAnim <= 0)
            {
                if (moveSpeed < 0)
                {
                    moveSpeed *= -1f;
                    moveSpeed += 0.2f;
                }
                moveSpeed += (Time.deltaTime * 0.01f);

                transform.localScale = Vector3.MoveTowards(transform.localScale, ScaleToBig * 0.75f, 0.01f);


                transform.position = Vector3.MoveTowards(transform.position, PlayerHealthControl.instance.transform.position, moveSpeed);
            }
            // 后退模式
            else
            {
                if (moveSpeed > 0)
                    moveSpeed *= -1f;

                transform.localScale = Vector3.MoveTowards(transform.localScale, ScaleToBig, 0.01f);

                transform.position = Vector3.MoveTowards(transform.position, PlayerHealthControl.instance.transform.position, moveSpeed * Time.deltaTime * 30);
            }
        }
        // 检测是否要移动
        CheckMove();

        // 消失时间
        if (!dontDisappear)
        {
            ExistTime -= Time.deltaTime;
            if (ExistTime <= 0)
            {
                playDisappearAnim();
                // 消失动画
                Invoke("DestorySelf", 2.5f);
            }
        }

    }

    virtual protected void playDisappearAnim()
    {
        // 注意，不是所有的掉落物都有消失动画
        if (this.GetComponentInChildren<Animator>() != null)
            this.GetComponentInChildren<Animator>().SetBool("isDisappear", true);

        return;
    }


    virtual protected void CheckMove()
    {
        // 一段时间检测一次，降低消耗
        checkCount -= Time.deltaTime;
        if (checkCount <= 0)
        {
            checkCount = timeBetweenCheck;
            // 如果距离计算合适，就移动
            if (Vector3.Distance(transform.position, PlayerHealthControl.instance.transform.position) < PlayerHealthControl.instance.pickupRange)
            {
                moveingToPlayer = true;
            }
        }
    }
    public void OnMouseEnter()
    {
        //  Debug.Log("Mouse Enter");
        moveingToPlayer = true;
    }
    // virtual public void SetMovingToplayer()
    // {
    //     if (checkCount <= 0)
    //     {
    //         moveingToPlayer = true;
    //     }
    // }
}
