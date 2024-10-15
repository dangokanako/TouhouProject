using System;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using Unity.Mathematics;

//using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.UIElements;

public class MainPlayer : MonoBehaviour
{
    static public MainPlayer instance;
    public Animator am;
    private float xInput;
    private float yInput;

    [Header("移动/冲刺速度")]
    [SerializeField] public float moveSpeed;
    public float MoveSpeed
    {
        get { return moveSpeed; }
        set
        {
            moveSpeed = value;
            UIControl.instance.SetPlayerInfoShow();
        }
    }
    [SerializeField] public float dashSpeed;
    public float DashSpeed
    {
        get { return dashSpeed; }
        set
        {
            dashSpeed = value;
            UIControl.instance.SetPlayerInfoShow();
        }
    }
    // 移动速度体力消耗倍率
    [SerializeField] public float dashSPRate;
    public float DashSPRate
    {
        get { return dashSPRate; }
        set
        {
            dashSPRate = value;
            // UIControl.instance.SetPlayerInfoShow();
        }
    }
    // 突进力修正
    [SerializeField] public float dashForce;
    public bool isFreeDash;
    public float FreeDashCount;


    [SerializeField] public int facingDirection = 1;
    [SerializeField] public bool DashTried;
    [Header("物理引擎")]
    [SerializeField] public Rigidbody2D rb;

    [Header("翻转组件")]
    [SerializeField] public bool isFlip;
    [SerializeField] private Transform filePic;
    [Header("使用道具动画组件")]
    private bool isItemAnime;
    private bool isWeaponAnime;
    private float isWeaponAnimeTime;
    public SpriteRenderer ItemImage;

    public int MaxSC = 3;
    [Header("像素移动组件")]
    public GameObject pixelMove;
    private int timecount = 0;


    [Header("武器通用CD计时")]
    public float WeaponCD = 0f;
    public float WeaponCDCount = 0f;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject); return;
        }
        else
        {
            instance = this;
        }

        rb = GetComponentInChildren<Rigidbody2D>();
        am = GetComponentInChildren<Animator>();
    }


    void Start()
    {
        DontDestroyOnLoad(gameObject);
        Debug.Log("游戏初始化成功，没保存场景记得保存。");
        AssetControl.instance.AddPoint(20);
        AssetControl.instance.AddSuperPoint(20);

        StartCoroutine(this.MovePixel());

    }
    // 测试用按键
    void ForTest()
    {

#if !UNITY_EDITOR
                if (!Input.GetKey(KeyCode.RightControl))
                {
                    return;
                }

#endif


        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("T测试按钮发动！增加经验值！");
            ExpLevelControl.instance.GetExp(10);
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            Debug.Log("y测试按钮发动！即死状态");
            PlayerHealthControl.instance.TakeDamage(25);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            Debug.Log("H测试按钮发动！加血");
            PlayerHealthControl.instance.changeHP(20);
            PlayerHealthControl.instance.changeSP(20);
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log("I测试按钮发动！打开商店");
            UIControl.instance.OpenShop();
            ShopControl.instance.LoadShop(ShopControl.instance.SakuyaShop);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("R测试按钮发动！加P点");
            AssetControl.instance.AddPoint(50);
            AssetControl.instance.AddSuperPoint(100);
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("Z测试按钮发动！");
            // 打印所有道具和ID
            string str = "";
            for (int i = 55; i < ItemControl.instance.itemGuide.Count; i++)
            {
                var item = ItemControl.instance.itemGuide[i];

                if (item.hasQuality)
                {
                    str += item.itemId + " " + item.itemName + "\n";
                    item.itemCiti = ItemQuality.Inferior;
                    str += "劣质  " + item.GetQualityText() + "\n";
                    item.itemCiti = ItemQuality.Normal;
                    str += "普通  " + item.GetQualityText() + "\n";
                    item.itemCiti = ItemQuality.Good;
                    str += "优秀  " + item.GetQualityText() + "\n";
                    item.itemCiti = ItemQuality.Excellent;
                    str += "精良  " + item.GetQualityText() + "\n";
                    item.itemCiti = ItemQuality.Legendary;
                    str += "传说  " + item.GetQualityText() + "\n";

                    str += item.itemRemarkText + "\n";
                }
                else
                {
                    str += item.itemId + " " + item.itemName + "\n" + item.itemInfoText + "\n" + item.itemRemarkText + "\n";
                }

                if (item.itemSlot == ItemSlotEnum.All)
                {
                    str += "槽位：全部" + "\n";
                }
                else if (item.itemSlot == ItemSlotEnum.AllExceptWarehouse)
                {
                    str += "槽位：除了仓库" + "\n";
                }
                else if (item.itemSlot == ItemSlotEnum.AllEquipment)
                {
                    str += "槽位：全部装备栏" + "\n";
                }
                else if (item.itemSlot == ItemSlotEnum.OnlyBlue)
                {
                    str += "槽位：仅限蓝色" + "\n";
                }
                else if (item.itemSlot == ItemSlotEnum.OnlyGreen)
                {
                    str += "槽位：仅限绿色" + "\n";
                }
                else if (item.itemSlot == ItemSlotEnum.OnlyRed)
                {
                    str += "槽位：仅限红色" + "\n";
                }


                str += "\n";
            }
            Debug.Log(str);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("X测试按钮发动！");
            AssetControl.instance.DropItem(PlayerHealthControl.instance.transform.position, ItemControl.instance.itemGuide[120]);
            AssetControl.instance.DropItem(PlayerHealthControl.instance.transform.position, ItemControl.instance.itemGuide[123]);
            AssetControl.instance.DropItem(PlayerHealthControl.instance.transform.position, ItemControl.instance.itemGuide[124]);
            AssetControl.instance.DropItem(PlayerHealthControl.instance.transform.position, ItemControl.instance.itemGuide[125]);
            AssetControl.instance.DropItem(PlayerHealthControl.instance.transform.position, ItemControl.instance.itemGuide[126]);
            AssetControl.instance.DropItem(PlayerHealthControl.instance.transform.position, ItemControl.instance.itemGuide[127]);
            AssetControl.instance.DropItem(PlayerHealthControl.instance.transform.position, ItemControl.instance.itemGuide[128]);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("C测试按钮发动！");
            TeamMateControl.instance.ActiveTeamMate(0);
            BigMapControl.instance.SetMarisaRoute();
            GlobalControl.instance.teammate = 1;
            GlobalControl.instance.isGetMarisa = true;
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            Debug.Log("U测试按钮发动！");
            ItemControl.instance.itembagList[0].itemInfo.UpgradeEquipmentSC();
        }


        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("F测试按钮发动！");
            ItemControl.instance.AddOneBag();
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            Debug.Log("B测试按钮发动！");
            BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("reimu_small_0", "reimu_small_0"));
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            Debug.Log("O测试按钮发动！");
            TalkControl.instance.ShowText(14);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("L测试按钮发动！");
            EnemyCreator.instance.test();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("k测试按钮发动！");
            UIControl.instance.OpenBigMap();
            BigMapControl.instance.InitMap_Test();
        }
    }
    void Update()
    {
        // 测试用
        ForTest();
        if (isFreeDash)
            FreeDashCount -= Time.deltaTime;

        // 空格互动
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // if (PlayerHealthControl.instance.OnPeace)
            // {
            // 范围内有互动项目，并且不能是正在阅读
            if (PlayerHealthControl.instance.InteractiveCollider != null)
            {
                if (!PlayerHealthControl.instance.OnTalk)
                {
                    PlayerHealthControl.instance.InteractiveCollider.Interactive();
                }
                // }

            }


            xInput = Input.GetAxisRaw("Horizontal");
            yInput = Input.GetAxisRaw("Vertical");
            Vector2 direction = new Vector2(xInput, yInput).normalized;

            // 战斗中空格冲刺

            if (GlobalControl.instance.isBattle)
            {
                // 如果速度很低那么不冲刺
                if (rb.velocity.magnitude > 0.1f)
                {
                    if ((isFreeDash && FreeDashCount < 0) || PlayerHealthControl.instance.UseSP(0.3f))
                    {
                        if (isFreeDash && FreeDashCount < 0)
                            FreeDashCount = 4f;

                        // 给无敌时间
                        PlayerHealthControl.instance.isInvincible = true;
                        PlayerHealthControl.instance.InvincibleTime = 0.1f;

                        SFXManger.instance.PlaySFX(19);
                        // 获取当前移动方向，并且向当前移动方向给一个200的力
                        MainPlayer.instance.rb.AddForce(direction * 260 * dashForce, ForceMode2D.Impulse);
                        // 窗口抖动
                        CameraControl.instance.DoShake(0.1f, 0.1f);
                    }
                }
                else
                {
                    Debug.Log("速度太低，无法冲刺");
                }
            }
        }


        if (Time.timeScale != 0f)
        {
            if (!PlayerHealthControl.instance.OnTalk && !UIControl.instance.isShopOpen())
            {
                ItemAnime();
            }
        }


    }
    void FixedUpdate()
    {
        if (Time.timeScale != 0f)
        {
            if (!PlayerHealthControl.instance.OnTalk && !UIControl.instance.isShopOpen())
            {
                if (!PlayerHealthControl.instance.isDead)
                {
                    Move();
                    // 武器CD计时
                    CDcount();
                }
            }
        }
    }

    // 武器CD计时
    private void CDcount()
    {
        if (WeaponCDCount > 0)
        {
            WeaponCDCount -= Time.deltaTime;
            // if (WeaponCDCount <= 0)
            // {
            //     SFXManger.instance.PlaySFX(10);
            // }
        }
    }
    private static readonly float TimeSecond_1 = 0.01f;
    private static readonly float TimeSecond_2 = 0.02f;
    private static bool isOnDash;
    // 像素画位移控制
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
                    pixelMove.transform.position = new Vector3(this.transform.position.x - (isOnDash ? TimeSecond_2 * 3f : TimeSecond_2), this.transform.position.y, 0);
                    break;
                case 1:
                    pixelMove.transform.position = new Vector3(this.transform.position.x - (isOnDash ? TimeSecond_1 * 3f : TimeSecond_1), this.transform.position.y + (isOnDash ? TimeSecond_1 * 3f : TimeSecond_1), 0);
                    break;
                case 2:
                    pixelMove.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + (isOnDash ? TimeSecond_2 * 3f : TimeSecond_2), 0);
                    break;
                case 3:
                    pixelMove.transform.position = new Vector3(this.transform.position.x + (isOnDash ? TimeSecond_1 * 3f : TimeSecond_1), this.transform.position.y + (isOnDash ? TimeSecond_1 * 3f : TimeSecond_1), 0);
                    break;
                case 4:
                    pixelMove.transform.position = new Vector3(this.transform.position.x + (isOnDash ? TimeSecond_2 * 3f : TimeSecond_2), this.transform.position.y, 0);
                    break;
                case 5:
                    pixelMove.transform.position = new Vector3(this.transform.position.x + (isOnDash ? TimeSecond_1 * 3f : TimeSecond_1), this.transform.position.y + (isOnDash ? TimeSecond_1 * 3f : TimeSecond_1), 0);
                    break;
                case 6:
                    pixelMove.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + (isOnDash ? TimeSecond_2 * 3f : TimeSecond_2), 0);
                    break;
                case 7:
                    pixelMove.transform.position = new Vector3(this.transform.position.x - (isOnDash ? TimeSecond_1 * 3f : TimeSecond_1), this.transform.position.y + (isOnDash ? TimeSecond_1 * 3f : TimeSecond_1), 0);
                    break;
            }
            //等待0.5秒
            yield return new WaitForSeconds(0.25f);
        }

    }

    // 道具、武器动画控制
    private void ItemAnime()
    {
        if (isItemAnime)
        {
            ItemImage.transform.eulerAngles = Vector3.MoveTowards(ItemImage.transform.eulerAngles, new Vector3(0, ItemImage.transform.eulerAngles.y, 90), 2);
            if (ItemImage.transform.eulerAngles.z >= 90)
            {
                isItemAnime = false;
                ItemImage.transform.eulerAngles = new Vector3(0, 0, 0);
                ItemImage.gameObject.SetActive(false);
            }
        }
        if (isWeaponAnime)
        {
            // 鼠标方向
            Vector2 direction = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)MainPlayer.instance.transform.position;
            direction.Normalize();

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // 计算旋转角度
            ItemImage.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        }
    }
    IEnumerator DelaySetActive()
    {
        yield return new WaitForSeconds(isWeaponAnimeTime); // 延迟

        // 恢复默认设置
        isWeaponAnime = false;
        ItemImage.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        ItemImage.gameObject.SetActive(false);
    }
    // 移动控制
    private void Move()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
        Vector2 direction = new Vector2(xInput, yInput).normalized;

        if (Input.GetKey(KeyCode.LeftShift) && !DashTried)
        {
            isOnDash = true;
            PlayerHealthControl.instance.SPReadyTimeCounter = PlayerHealthControl.instance.SPReadyTime;
            if (PlayerHealthControl.instance.OnDash() > 0)
            {
                rb.AddForce(direction * 12 * moveSpeed * dashSpeed, ForceMode2D.Impulse);
            }
            else
            {
                DashTried = true;
            }
        }
        else
        {
            isOnDash = false;
            rb.AddForce(direction * 12 * moveSpeed, ForceMode2D.Impulse);
        }


        if ((xInput < 0 && facingDirection == 1) || (xInput > 0 && facingDirection == -1))
        {
            // 方向不对，需要旋转
            flip();
        }
        if (isFlip)
        {
            // 翻转
            filePic.transform.eulerAngles = Vector3.MoveTowards(filePic.transform.eulerAngles,
            new Vector3(0, facingDirection == 1 ? 0 : 180, 0),
            5);

            //我怀疑能节省性能
            // if ((facingDirection == 1 && transform.eulerAngles.y == 0) || (facingDirection == -1 && transform.eulerAngles.y == 180))
            //     isFlip = false;
        }


    }

    // 通过反转90度隐藏角色
    public void HidePlayer()
    {
        filePic.transform.eulerAngles = new Vector3(0, 90, 0);
    }
    public void UnHidePlayer()
    {
        filePic.transform.eulerAngles = new Vector3(0, 0, 0);
    }

    private void flip()
    {
        facingDirection *= -1;
        //transform.Rotate(0, 180, 0);
        isFlip = true;
    }
    // 播放道具动画
    public void PlayItemAnime(Sprite image)
    {
        if (!ItemImage.gameObject.activeSelf)
        {
            ItemImage.gameObject.SetActive(true);
        }

        ItemImage.sprite = image;

        float desiredSize = 1f; // 目标大小
        float originalSize = image.bounds.size.x; // 获取原始图片的宽度
        float scaleFactor = desiredSize / originalSize; // 计算缩放比例

        // 应用缩放
        ItemImage.transform.localScale = new Vector3(scaleFactor, scaleFactor, 1f);

        isItemAnime = true;
    }

    // 播放武器动画
    public void PlayWeaponAnime(Sprite image, float time = 0.3f)
    {

        if (isWeaponAnime)
        {
            return;
        }

        if (!ItemImage.gameObject.activeSelf)
        {
            ItemImage.gameObject.SetActive(true);
        }

        isWeaponAnimeTime = time;

        ItemImage.sprite = image;

        float desiredSize = 1f; // 目标大小
        float originalSize = image.bounds.size.x; // 获取原始图片的宽度
        float scaleFactor = desiredSize / originalSize; // 计算缩放比例

        // 应用缩放
        ItemImage.transform.localScale = new Vector3(scaleFactor, scaleFactor, 1f);

        isWeaponAnime = true;

        StartCoroutine(DelaySetActive());

    }
}
