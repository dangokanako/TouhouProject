using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// 子弹基类，包括敌人的子弹和自己的子弹
public class BulletBaseClass : MonoBehaviour
{
    [Header("伤害值")]

    public float damageAmount;

    [Header("生成、消失时间")]

    [SerializeField] public float lifeTime = 5f;
    [SerializeField] public float growSpeed = 1f;
    // 是否消灭父类（敌人的弹幕有时候好像需要消灭父类，我自己也忘了）
    public bool destroyParent;

    [Header("是否自旋转")]
    public bool shouldSpin;

    /// <summary>
    /// 弹幕在生成的时候会有一个从0到目标大小的过程
    /// </summary>
    [Header("关闭产生动画")]
    public bool closeGrowAnime;
    // 目标大小
    public Vector3 targetSize;


    /// <summary>
    /// 初始方向。在自旋转的时候可能丢失初始方向。
    /// </summary>
    public Vector3 initialDirection;

    // 速度会随时间增加
    [Header("递增速度")]
    public float moveSpeedAdd = 0f;

    [Header("刚体组件")]
    public Rigidbody2D rb;



    [Header("是否自己有初始速度")]
    public bool hasInitialVelocity;
    public float initialVelocity;
    private SpriteRenderer sr;
    private Collider2D c2d;

    [Header("是否自动跟踪玩家方向旋转(仅限敌人的子弹)")]
    [SerializeField] public bool shouldRotateToPlayer;
    // 旋转速度
    [SerializeField] public float rotateSpeed;
    // 跟踪开始时间
    [SerializeField] public float rotatestartTime;
    // 跟踪持续时间
    [SerializeField] public float rotateTime;
    private float rotateTimeCounter;


    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        c2d = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }
    virtual protected void Start()
    {
        if (!closeGrowAnime)
        {
            targetSize = transform.localScale;
            transform.localScale = Vector3.zero;
        }

        // 在开始时保存初始的前进方向
        initialDirection = transform.right;

        // 如果自己有初始速度，那么给自己一个向前的初速度
        if (hasInitialVelocity)
        {
            rb.velocity = initialDirection * initialVelocity;
        }
    }

    virtual protected void Update()
    {
        // 自旋转部分。如果是自旋转，那么调整自身旋转角度
        if (shouldSpin)
        {
            transform.Rotate(Vector3.forward * 360 * Time.deltaTime);
        }


        // 放大缩小的动画部分
        if (!closeGrowAnime)
            transform.localScale = Vector3.MoveTowards(transform.localScale, targetSize, growSpeed * Time.deltaTime);


        // 生命时间到了自动销毁的部分
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            if (!closeGrowAnime)
            {
                // 开始改变为0
                targetSize = Vector3.zero;

                if (transform.localScale.x == 0f)
                {
                    Destroy(gameObject);
                    // EnemyBulletControl.instance.ReturnBullet(this);
                    if (destroyParent)
                    {
                        DestoryAnimeControl.instance.CreateDestoryAnime(1, this.transform.position);
                        Destroy(transform.parent.gameObject);
                    }
                }
            }
            else
            {
                // 重新打开动画，播放消失动画。
                closeGrowAnime = false;
            }
        }


        // 速度自递增部分
        if (moveSpeedAdd != default)
            rb.velocity += new Vector2(transform.right.x, transform.right.y) * moveSpeedAdd;

        // 如果需要自动跟踪玩家方向旋转，那么调整自身旋转角度
        if (shouldRotateToPlayer)
        {
            rotateTimeCounter += Time.deltaTime;
            if (rotateTimeCounter < rotateTime && rotateTimeCounter > rotatestartTime)
            {
                Vector3 direction = PlayerHealthControl.instance.transform.position - this.transform.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // Subtract 90 to align with the right direction
                Quaternion rotation = Quaternion.Euler(0, 0, angle);
                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, rotation, rotateSpeed * Time.deltaTime);

                // 改变RB的速度方向
                rb.velocity = transform.right * rb.velocity.magnitude;
            }

        }


    }

    // 深拷贝   
    public void CopyFrom(BulletBaseClass other)
    {
        // 复制other的所有属性到当前对象
        this.moveSpeedAdd = other.moveSpeedAdd;
        this.initialDirection = other.initialDirection;
        this.initialVelocity = other.initialVelocity;
        this.hasInitialVelocity = other.hasInitialVelocity;
        this.shouldSpin = other.shouldSpin;
        this.closeGrowAnime = other.closeGrowAnime;
        this.targetSize = other.targetSize;
        this.lifeTime = other.lifeTime;
        this.growSpeed = other.growSpeed;
        this.damageAmount = other.damageAmount;
        this.destroyParent = other.destroyParent;

        this.transform.localScale = other.transform.localScale;
        this.transform.rotation = other.transform.rotation;
        this.transform.position = other.transform.position;


        CopyRigidbody2D(other.GetComponent<Rigidbody2D>());
        CopyCollider2D(other.GetComponent<Collider2D>());
        CopySpriteRenderer(other.GetComponent<SpriteRenderer>());


        if (!closeGrowAnime)
        {
            targetSize = transform.localScale;
            transform.localScale = Vector3.zero;
        }

        // 在开始时保存初始的前进方向
        initialDirection = transform.right;

        // 如果自己有初始速度，那么给自己一个向前的初速度
        if (hasInitialVelocity)
        {
            rb.velocity = initialDirection * initialVelocity;
        }
    }
    // 深拷贝spriterenderer的全部参数
    private void CopySpriteRenderer(SpriteRenderer other)
    {
        sr.sprite = other.sprite;
        sr.color = other.color;
        sr.flipX = other.flipX;
        sr.flipY = other.flipY;
        sr.drawMode = other.drawMode;
        sr.size = other.size;
        sr.adaptiveModeThreshold = other.adaptiveModeThreshold;
        sr.tileMode = other.tileMode;
        sr.maskInteraction = other.maskInteraction;
        sr.hideFlags = other.hideFlags;

        // 创建一个新的Material实例，然后复制其值
        // sr.material = new Material(other.material);
        sr.sharedMaterial = new Material(other.sharedMaterial);

        // 对于materials和sharedMaterials属性，你需要创建一个新的数组，然后复制每个元素的值
        sr.materials = other.materials.Select(m => new Material(m)).ToArray();
        sr.sharedMaterials = other.sharedMaterials.Select(m => new Material(m)).ToArray();

        sr.sortingLayerID = other.sortingLayerID;
        sr.sortingLayerName = other.sortingLayerName;
        sr.sortingOrder = other.sortingOrder;
        sr.hideFlags = other.hideFlags;
    }

    private void CopyRigidbody2D(Rigidbody2D other)
    {
        rb.drag = other.drag;
        rb.angularDrag = other.angularDrag;
        rb.gravityScale = other.gravityScale;
        rb.mass = other.mass;
        rb.isKinematic = other.isKinematic;
        rb.freezeRotation = other.freezeRotation;
        rb.constraints = other.constraints;
        rb.sharedMaterial = other.sharedMaterial;
        rb.collisionDetectionMode = other.collisionDetectionMode;
        rb.sleepMode = other.sleepMode;
        rb.interpolation = other.interpolation;
        rb.simulated = other.simulated;
    }

    private void CopyCollider2D(Collider2D other)
    {
        if (other is BoxCollider2D)
        {
            BoxCollider2D otherBox = other as BoxCollider2D;
            BoxCollider2D thisBox = this.GetComponent<BoxCollider2D>();
            if (thisBox != null)
            {
                thisBox.size = otherBox.size;
                thisBox.offset = otherBox.offset;
                thisBox.isTrigger = otherBox.isTrigger;
                thisBox.usedByEffector = otherBox.usedByEffector;
                thisBox.edgeRadius = otherBox.edgeRadius;
                thisBox.autoTiling = otherBox.autoTiling;
                thisBox.hideFlags = otherBox.hideFlags;
            }
        }
        else if (other is CircleCollider2D)
        {
            CircleCollider2D otherCircle = other as CircleCollider2D;
            CircleCollider2D thisCircle = this.GetComponent<CircleCollider2D>();
            if (thisCircle != null)
            {
                thisCircle.radius = otherCircle.radius;
                thisCircle.offset = otherCircle.offset;
                thisCircle.isTrigger = otherCircle.isTrigger;
                thisCircle.usedByEffector = otherCircle.usedByEffector;
                // thisCircle.edgeRadius = otherCircle.edgeRadius;
                thisCircle.hideFlags = otherCircle.hideFlags;
            }
        }
        // 其他类型的碰撞器...
    }

}
