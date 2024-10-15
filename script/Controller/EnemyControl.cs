using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.Design;
using System.Runtime.CompilerServices;
using DG.Tweening;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyControl : MonoBehaviour
{
    protected Rigidbody2D rb;

    [Header("属性")]
    [SerializeField] protected float atk;
    [SerializeField] protected float health = 1f;
    [SerializeField] protected float maxHealth;

    [Header("移动速度")]

    // 注意，这个移动速度在受力改版之后，仅代表受力大小，而不代表速度大小
    [SerializeField] protected float moveSpeed;
    // 实际移动速度
    [SerializeField] protected float moveSpeedReal;
    protected int facingDirection = 1;

    protected Transform target;
    [Header("动画")]
    //如果这个真的可以称之为动画的话……
    [SerializeField] protected float changeSpeed;
    [SerializeField] protected float minSize = 0.5f, maxSize = 0.6f;
    [SerializeField] protected Transform sprite;
    [SerializeField] protected SpriteRenderer image;
    [SerializeField] protected float activeSize;

    // [Header("受击后退")]
    // // 废弃，不再使用。
    // [SerializeField] protected float knockBackTime = 0.5f;
    // [SerializeField] protected float knockBackCounter;

    [Header("掉落数值")]
    // [SerializeField] public int expToGive;
    // [SerializeField] public float expDropRate = 0.5f;

    // [SerializeField] public int pointToGive;
    // [SerializeField] public float pointDropRate = 0.5f;
    // [Header("其他物品掉落以及数值")]
    // 物品掉落以及概率 
    // 废弃，改用掉落引用ID
    // [SerializeField] public List<item> dropItemList;
    // [SerializeField] public List<int> dropItemRateList;
    // DropTable物品掉落引用ID
    [SerializeField] public int DropTableId;

    [Header("碰撞无敌时间")]
    [SerializeField] public float InvincibleTime = 0.4f;
    [Header("伤害抗性")]
    [SerializeField] public float bluntResistance;
    [SerializeField] public float sharpResistance;
    [SerializeField] public float SCResistance;
    [Header("环绕模式")]
    [SerializeField] public bool isSurround = false;
    [SerializeField] public bool isSurroundBack = false;
    [SerializeField] public bool isSurroundMove = false;
    // 环绕距离 
    [SerializeField] public float surroundDistanceMax;
    [SerializeField] public float surroundDistanceMin;
    // 环绕模式设定：
    public float smoothTime = 0.2f;
    public float xVelocity = 0;
    public float yVelocity = 0;

    public delegate void EnemyDeadDelegate();
    // 敌人死亡时的事件
    public static event Action OnEnemyDead;

    // Start is called before the first frame update
    virtual protected void Start()
    {
        // 初始化
        maxHealth = health;
        rb = GetComponentInChildren<Rigidbody2D>();
        //target = FindObjectOfType<MainPlayer>().transform;
        target = PlayerHealthControl.instance.transform;
        sprite = GetComponent<Transform>();
        image = GetComponent<SpriteRenderer>();

        activeSize = maxSize;

        changeSpeed *= UnityEngine.Random.Range(0.75f, 1.25f);

        moveSpeedReal = moveSpeed;

    }
    // 设置速度为原始的多少倍
    public void SetSpeed(float rate)
    {
        moveSpeedReal = moveSpeed * rate;
    }
    public float GetMaxHealth()
    {
        return maxHealth;
    }

    virtual protected void FixedUpdate()
    {
        if (!PlayerHealthControl.instance.isDead && Time.timeScale != 0f)
        {
            // 无敌帧
            InvincibleTime -= Time.deltaTime;

            // 非环绕模式，处理简单
            if (!isSurround)
            {
                rb.AddForce(12 * new Vector2(target.position.x - transform.position.x, target.position.y - transform.position.y).normalized * moveSpeedReal, ForceMode2D.Impulse);
            }
            else
            {
                Vector2 targetSmooth = new Vector2(Mathf.SmoothDamp(this.transform.position.x, MainPlayer.instance.gameObject.transform.position.x, ref xVelocity, smoothTime),
                    Mathf.SmoothDamp(this.transform.position.y, MainPlayer.instance.gameObject.transform.position.y, ref yVelocity, smoothTime));

                Vector2 directionToPlayer = ((Vector2)targetSmooth - (Vector2)transform.position).normalized; // 敌人向玩家的方向

                // 环绕模式，优先保持在一定距离。如果距离合适，那么环绕玩家运动
                if (Vector3.Distance(PlayerHealthControl.instance.transform.position, transform.position) > surroundDistanceMax)
                {
                    rb.AddForce(12 * directionToPlayer.normalized * moveSpeedReal, ForceMode2D.Impulse);
                    // rb.velocity = directionToPlayer.normalized * moveSpeed;
                }
                else if (Vector3.Distance(PlayerHealthControl.instance.transform.position, transform.position) < surroundDistanceMin)
                {
                    if (isSurroundBack)
                    {
                        rb.AddForce(12 * directionToPlayer.normalized * moveSpeedReal * -1.1f, ForceMode2D.Impulse);
                    }
                }
                else
                {
                    if (isSurroundMove)
                    {
                        Vector3 orbitDirectionVector = Quaternion.Euler(0, 0, 90) * directionToPlayer;
                        rb.AddForce(12 * orbitDirectionVector.normalized * moveSpeedReal, ForceMode2D.Impulse);
                    }
                }
            }

            // 防止过远机制
            if (Vector3.Distance(PlayerHealthControl.instance.transform.position, transform.position) > 13f)
                rb.velocity = (target.position - transform.position).normalized * moveSpeedReal * 20;


            // 放大缩小的动画环节……
            sprite.localScale = Vector3.MoveTowards(transform.localScale, Vector3.one * activeSize, changeSpeed * Time.deltaTime);

            if (sprite.localScale.x == activeSize)
            {
                if (activeSize == maxSize)
                    activeSize = minSize;
                else
                    activeSize = maxSize;
            }

            if ((rb.velocity.x < 0 && facingDirection == 1) || (rb.velocity.x > 0 && facingDirection == -1))
                if (moveSpeedReal > 0 && Time.time - lastFlipTime >= flipCooldown)
                    flip();
        }
        else
        {
            rb.velocity = Vector2.zero;
        }


    }

    // 防止短时间内多次flip()
    public float lastFlipTime = 0f;
    public float flipCooldown = 0.5f;
    virtual protected void flip()
    {
        if (Time.time - lastFlipTime < flipCooldown)
        {
            // 如果当前时间与上一次改变方向的时间之差小于冷却时间，那么不执行改变方向的操作
            return;
        }

        facingDirection *= -1;
        transform.Rotate(0, 180, 0);

        lastFlipTime = Time.time;
    }

    virtual protected void OnCollisionEnter2D(Collision2D collision)
    {
        // 和玩家碰撞
        // 碰撞有一个最小时间
        // 注意：本时间写死，如果要改需要加新变量
        if (InvincibleTime < 0)
        {
            InvincibleTime = 0.25f;
            if (collision.gameObject.tag == "Player")
            {
                PlayerHealthControl.instance.TakeDamage(atk, 0);

                // 玩家撞击敌人
                TakeDamage(PlayerHealthControl.instance.PlayerAtk, (int)DamageType.collisionDamage);

                // 撞击额外给力 
                rb.velocity = -(PlayerHealthControl.instance.transform.position - this.transform.position).normalized * MainPlayer.instance.rb.mass * atk / this.rb.mass;


                if (PlayerHealthControl.instance.CloseFightAnimation > 0)
                {
                    SFXManger.instance.PlaySFX(22);
                    DestoryAnimeControl.instance.CreateDestoryAnime(7, this.transform.position);
                }
                else
                {
                    SFXManger.instance.PlaySFX(6);
                }

            }
        }
    }

    virtual public void TakeDamage(float damageToTake, int type, int _fontSize = default)
    {
        // 检查敌人是否已经死亡。在部分情况下，该对象没有被销毁，受到多次伤害，导致掉落多次执行。
        if (health <= 0)
        {
            // 杀敌数统计
            GlobalControl.instance.currentTotalKill++;
            return;
        }

        // 停止SpriteRenderer的所有动画
        DOTween.Kill(image);

        getDamage();

        // 检查伤害乘区
        switch (type)
        {
            case 1: damageToTake *= (1 + PlayerHealthControl.instance.BluntDamageRate); break;
            case 2: damageToTake *= (1 + PlayerHealthControl.instance.SharpDamageRate); break;
            case 3: damageToTake *= (1 + PlayerHealthControl.instance.SCDamageRate); break;
            case 4: damageToTake *= (1 + PlayerHealthControl.instance.BluntDamageRate); break;
            default: break;
        }


        // 检查暴击判定
        bool isCritical = false;
        if (UnityEngine.Random.value <= PlayerHealthControl.instance.CriticalRate || (type == 4 && PlayerHealthControl.instance.PlayerCollsionCritical > 0))
        {
            isCritical = true;
            damageToTake *= (1 + PlayerHealthControl.instance.CriticalDamage);
        }

        // 检查伤害抗性
        switch (type)
        {
            case 1: damageToTake *= (1 - bluntResistance / 100); break;
            case 2: damageToTake *= (1 - sharpResistance / 100); break;
            case 3: damageToTake *= (1 - SCResistance / 100); break;
            case 4: damageToTake *= (1 - bluntResistance / 100); break;
            default: break;
        }


        // 如果伤害为0，那么播放音效
        if (damageToTake <= 0)
            SFXManger.instance.PlaySFX(8, default, false);



        // 弹出伤害数字
        if (isCritical)
            DamageNumberControl.instance.ShowDamage(damageToTake, transform.position, false, true, type, _fontSize);
        else
            DamageNumberControl.instance.ShowDamage(damageToTake, transform.position, type, _fontSize);

        // 计算吸血
        if (type == 1 || type == 2 || type == 4)
        {
            if (PlayerHealthControl.instance.CloseFightVampire > 0)
            {
                PlayerHealthControl.instance.changeHP(damageToTake * PlayerHealthControl.instance.CloseFightVampire * 0.01f);
                // 特殊的，使用99表示治愈。这里虽然isplayer给了fasle，实际上是给玩家治愈的数值提示。
                if (damageToTake * PlayerHealthControl.instance.CloseFightVampire * 0.01f > 0.1)
                    DamageNumberControl.instance.ShowDamage(damageToTake * PlayerHealthControl.instance.CloseFightVampire * 0.01f, MainPlayer.instance.transform.position, false, false, 99);

            }
        }

        // 扣除敌人血量
        health -= damageToTake;

        // 敌人死亡
        if (health <= 0)
        {
            DestoryAnimeControl.instance.CreateDestoryAnime(2, transform.position);
            enemyDead();

            // 掉落系统 New!
            if (DropTableId != 0)
            {
                if (EnemyDropTable.Instance.dropTables[DropTableId] != null)
                {
                    // 计算掉落经验
                    int expCount = 0;
                    for (int i = 0; i < EnemyDropTable.Instance.dropTables[DropTableId].expToGiveTimes; i++)
                        // 25%的概率增加1点掉落经验
                        if (UnityEngine.Random.value < 0.25f)
                            expCount++;

                    ExpLevelControl.instance.CreateDrop(this.transform.position, expCount);

                    // 计算掉落金钱
                    int pointCount = 0;
                    for (int i = 0; i < EnemyDropTable.Instance.dropTables[DropTableId].pointToGiveTimes; i++)
                        // 25%的概率增加1点掉落金钱
                        if (UnityEngine.Random.value < 0.25f)
                            pointCount++;

                    AssetControl.instance.DropPoint(transform.position, pointCount);

                    // 计算掉落道具
                    DropTable thisDropTable = EnemyDropTable.Instance.dropTables[DropTableId];
                    for (int i = 0; i < thisDropTable.DropItemTables.Count; i++)
                    {
                        if (UnityEngine.Random.value * 10000 <= thisDropTable.DropItemTables[i].DropRate)
                        {
                            if (thisDropTable.DropItemTables[i].ItemGroupId != 0)
                            {
                                if (EnemyDropTable.GetGroupItem(thisDropTable.DropItemTables[i].ItemGroupId) != -1)
                                    AssetControl.instance.DropItem(transform.position, ItemControl.instance.itemGuide[EnemyDropTable.GetGroupItem(thisDropTable.DropItemTables[i].ItemGroupId)]);
                            }
                            else
                            {
                                AssetControl.instance.DropItem(transform.position, ItemControl.instance.itemGuide[thisDropTable.DropItemTables[i].ItemID]);
                            }
                        }
                    }

                    // 计划以后写。目前先随便定了。掉落的HP和MP恢复道具
                    if (UnityEngine.Random.value < 0.01f)
                    {
                        AssetControl.instance.DropAddSp(transform.position, 1);
                    }
                    if (UnityEngine.Random.value < 0.01f)
                    {
                        AssetControl.instance.DropAddHp(transform.position, 1);
                    }

                }
                else
                {
                    Debug.LogError("DropTableId is not exist");
                }
            }

            // 死亡特效
            // 先将面板颜色渐变到半透明
            image.DOColor(new Color(1f, 1f, 1f, 0.3f), 0.1f).OnComplete(() =>
            {
                // 销毁父对象
                // Destroy(gameObject);
                EnemyCreator.instance.creatorEnemy.Remove(gameObject.transform.parent.gameObject);
                Destroy(gameObject.transform.parent.gameObject);
            });

            // 死亡时的计算（其实应该写在委托里的）
            if (PlayerHealthControl.instance.KillEnemyRecoverHP != 0)
                PlayerHealthControl.instance.changeHP(PlayerHealthControl.instance.KillEnemyRecoverHP);
            if (PlayerHealthControl.instance.KillEnemyRecoverSP != 0)
                PlayerHealthControl.instance.changeSP(PlayerHealthControl.instance.KillEnemyRecoverSP);

            // 杀敌数统计
            GlobalControl.instance.currentTotalKill++;
        }
        else
        {
            // 受伤特效
            // 先将面板颜色渐变到半透明
            if (image != null)
            {
                image.DOKill();

                Color OriBrighterColor = image.color;
                image.DOColor(Color.white, 0.2f).OnComplete(() =>
                {
                    image.DOColor(Color.black, 0.2f);
                });

                // image.DOColor(new Color(1f, 1f, 1f, 0.5f), 0.1f).OnComplete(() =>
                // {
                //     // 然后将面板颜色渐变回不透明
                //     image.DOColor(new Color(1f, 1f, 1f, 1f), 0.1f);
                // });
            }
        }
    }
    virtual protected void getDamage()
    {
    }
    virtual protected void enemyDead()
    {
        OnEnemyDead?.Invoke();
    }

    // knockback参数不再使用
    virtual public void TakeDamage(float damageToTake, bool KnockBack, int type, int _fontSize = default)
    {
        TakeDamage(damageToTake, type, _fontSize);
    }
    // 哪有你这么写DEBUFF的，回炉重写！
    // 是否已经吃减攻BUFF
    public bool isReduce = false;
    // 减攻BUFF
    public void ReduceAtk(float reduce)
    {
        if (!isReduce)
        {
            atk *= (1 - reduce * 0.01f);
            isReduce = true;
        }
    }
    // 是否已经吃减速度BUFF
    public bool isReduce_Speed = false;
    // 减攻BUFF
    public void ReduceSPD(float reduce)
    {
        if (!isReduce_Speed)
        {
            moveSpeedReal *= (1 - reduce * 0.01f);
            isReduce_Speed = true;
        }
    }
}



