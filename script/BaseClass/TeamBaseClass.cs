using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamBaseClass : MonoBehaviour
{
    [Header("移动速度")]
    [SerializeField] protected float moveSpeed;
    protected Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // 像素移动系统
        StartCoroutine(MovePixel());
    }

    void Update()
    {
        if (MainPlayer.instance.gameObject.activeSelf)
        {
            // 反转组件
            Filp();

            // 移动组件
            Move();

            // 攻击组件
            Attack();
        }
    }
    // 攻击组件 虚函数，由子类重写
    protected virtual void Attack()
    {
        // 由子类重写
    }

    // 丝滑移动组件：    
    public float smoothTime = 0.3f;
    private float xVelocity = 0;
    private float yVelocity = 0;

    private void Move()
    {
        // 如果和玩家的距离超出范围，那么移动。否则不移动
        if (Vector2.Distance(MainPlayer.instance.gameObject.transform.position, transform.position) > 0.7f)
        {
            Vector2 target = new Vector2(Mathf.SmoothDamp(transform.position.x, MainPlayer.instance.gameObject.transform.position.x, ref xVelocity, smoothTime),
                Mathf.SmoothDamp(transform.position.y, MainPlayer.instance.gameObject.transform.position.y, ref yVelocity, smoothTime));

            // 朝向玩家移动
            rb.velocity = (target - new Vector2(transform.position.x, transform.position.y)).normalized * moveSpeed;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }

        // 如果过远，那么直接位移
        if (Vector2.Distance(MainPlayer.instance.gameObject.transform.position, transform.position) > 10f)
        {
            // 随意确定一个MainPlayer.instance.gameObject.transform.position距离0.7f的位置
            transform.position = MainPlayer.instance.gameObject.transform.position + new Vector3(UnityEngine.Random.Range(-0.5f, 0.5f), UnityEngine.Random.Range(-0.5f, 0.5f), 0);
            DestoryAnimeControl.instance.CreateDestoryAnime(2, transform.position);
        }


    }

    // 反转组件
    protected int facingDirection = 1;

    private void Filp()
    {
        if ((rb.velocity.x < 0 && facingDirection == 1) || (rb.velocity.x > 0 && facingDirection == -1))
            if (moveSpeed > 0)
            {
                facingDirection *= -1;
                TeamMatePicture.transform.Rotate(0, 180, 0);
            }
    }

    [Header("像素画位移控制")]
    private int timecount = 0;
    public GameObject TeamMatePicture;
    private IEnumerator MovePixel()
    {
        // 每0.5秒按照以下顺序循环移动
        // 图像左移2像素
        // 图像左移1像素，上移1像素
        // 图像上移2像素
        // 图像右移1像素，上移1像素
        // 图像右移2像素
        // 图像右移1像素，上移1像素
        // 图像上移2像素
        // 图像左移1像素，上移1像素
        while (true)
        {
            timecount++;
            switch (timecount % 8)
            {
                case 0:
                    TeamMatePicture.transform.position = new Vector3(this.transform.position.x - 0.02f, this.transform.position.y, 0);
                    break;
                case 1:
                    TeamMatePicture.transform.position = new Vector3(this.transform.position.x - 0.01f, this.transform.position.y + 0.01f, 0);
                    break;
                case 2:
                    TeamMatePicture.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.02f, 0);
                    break;
                case 3:
                    TeamMatePicture.transform.position = new Vector3(this.transform.position.x + 0.01f, this.transform.position.y + 0.01f, 0);
                    break;
                case 4:
                    TeamMatePicture.transform.position = new Vector3(this.transform.position.x + 0.02f, this.transform.position.y, 0);
                    break;
                case 5:
                    TeamMatePicture.transform.position = new Vector3(this.transform.position.x + 0.01f, this.transform.position.y + 0.01f, 0);
                    break;
                case 6:
                    TeamMatePicture.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.02f, 0);
                    break;
                case 7:
                    TeamMatePicture.transform.position = new Vector3(this.transform.position.x - 0.01f, this.transform.position.y + 0.01f, 0);
                    break;
            }
            //等待0.5秒
            yield return new WaitForSeconds(0.25f);
        }

    }

}
