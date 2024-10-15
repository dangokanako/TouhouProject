using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using TMPro;
using UnityEngine.Playables;
using Unity.VisualScripting;

public class PlayerHealthControl : MonoBehaviour
{
    [Header("受伤无敌帧")]
    public float InvincibleTime = 0f;
    public float HowManyInvincibleTime = 1f;
    public bool isInvincible = false;
    public static PlayerHealthControl instance;
    [Header("血条以及显示")]
    [SerializeField] public float currentHealth;
    public float maxHealth;
    public float MaxHealth
    {
        get { return maxHealth; }
        set
        {
            maxHealth = value;
            UIControl.instance.SetPlayerInfoShow();
        }
    }
    [SerializeField] public Slider slider;
    [SerializeField] public TMP_Text HealthText;
    [Header("耐力条以及显示")]
    public float currentSP;
    public float maxSP;
    public float MaxSP
    {
        get { return maxSP; }
        set
        {
            maxSP = value;
            UIControl.instance.SetPlayerInfoShow();
        }
    }
    [SerializeField] public Slider SPSlider;
    [SerializeField] public TMP_Text SPText;
    [SerializeField] public GameObject SPcolor;
    // 耐力颜色条变化
    [SerializeField] public Image SPColorObject;
    [SerializeField] public Color SPChangeToColorObject;
    [SerializeField] public Color SPOriginColorObject;
    private bool moveback;
    private float ColorRate;
    [SerializeField] public float SPReadyTime = 0.6f;
    [SerializeField] public float SPReadyTimeCounter;
    // SP消耗减少
    public float SPConsumptionReduction = 0;


    [Header("拾取范围")]
    public float pickupRange = 2;
    public float _pickupRange
    {
        get { return pickupRange; }
        set
        {
            pickupRange = value;
            UIControl.instance.SetPlayerInfoShow();
        }
    }
    [Header("玩家属性")]
    public float PlayerAtk = 1;
    public float _PlayerAtk
    {
        get { return PlayerAtk; }
        set
        {
            PlayerAtk = value;
            UIControl.instance.SetPlayerInfoShow();
        }
    }
    // 碰撞防御，仅碰撞时会加算这个防御
    public float PlayerCollsionDef = 0;
    public float _PlayerCollsionDef
    {
        get { return PlayerCollsionDef; }
        set
        {
            PlayerCollsionDef = value;
            UIControl.instance.SetPlayerInfoShow();
        }
    }
    // 碰撞是否必定暴击 大于1就必定暴击
    public int PlayerCollsionCritical = 0;
    public int _PlayerCollsionCritical
    {
        get { return PlayerCollsionCritical; }
        set
        {
            PlayerCollsionCritical = value;
            // UIControl.instance.SetPlayerInfoShow();
        }
    }
    // 回避率
    public float PlayerDodge = 0;
    public float _PlayerDodge
    {
        get { return PlayerDodge; }
        set
        {
            PlayerDodge = value;
            // UIControl.instance.SetPlayerInfoShow();
        }
    }
    public float PlayerDef = 0;
    public float _PlayerDef
    {
        get { return PlayerDef; }
        set
        {
            PlayerDef = value;
            UIControl.instance.SetPlayerInfoShow();
        }
    }
    // 耐力恢复
    public float playerSPRecover = 1;
    public float _playerSPRecover
    {
        get { return playerSPRecover; }
        set
        {
            playerSPRecover = value;
            UIControl.instance.SetPlayerInfoShow();
        }
    }
    [Header("暴击组件")]
    public float CriticalRate = 0.05f;
    public float _CriticalRate
    {
        get { return CriticalRate; }
        set
        {
            CriticalRate = value;
            UIControl.instance.SetPlayerInfoShow();
        }
    }
    /// <summary>
    /// 暴击造成的伤害倍率。例如0.5的话，就是额外造成50%的伤害
    /// </summary>
    public float CriticalDamage;
    public float _CriticalDamage
    {
        get { return CriticalDamage; }
        set
        {
            CriticalDamage = value;
            UIControl.instance.SetPlayerInfoShow();
        }
    }
    [Header("伤害增加组件")]
    /// <summary>
    /// 增加的伤害倍率。例如0.5的话，就是额外造成50%的伤害
    /// </summary>
    public float SCDamageRate = 0f;
    public float _SCDamageRate
    {
        get { return SCDamageRate; }
        set
        {
            SCDamageRate = value;
            UIControl.instance.SetPlayerInfoShow();
        }
    }
    public float BluntDamageRate = 0f;
    public float _BluntDamageRate
    {
        get { return BluntDamageRate; }
        set
        {
            BluntDamageRate = value;
            UIControl.instance.SetPlayerInfoShow();
        }
    }
    public float SharpDamageRate = 0f;
    public float _SharpDamageRate
    {
        get { return SharpDamageRate; }
        set
        {
            SharpDamageRate = value;
            UIControl.instance.SetPlayerInfoShow();
        }
    }
    [Header("近战吸血")]
    // 1表示吸血1%，0.5表示吸血0.5%。这个值只给锐器和钝器和碰撞使用。
    public float CloseFightVampire = 0;
    // 击杀敌人的回复生命值
    public float KillEnemyRecoverHP = 0;
    public float KillEnemyRecoverSP = 0;

    // 近战动画
    public int CloseFightAnimation = 0;
    [Header("弹幕飞行速度加成")]
    public float BulletSpeedRate = 0;

    [Header("交互作用")]
    [SerializeField] public bool OnPeace;
    // 可交互对象
    [SerializeField] public InteractiveClass InteractiveCollider;
    // 在说话呢
    [SerializeField] public bool OnTalk;

    public bool isDead = false;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject); return;
        }
        else
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // 从playerstatcontrol读取初始数据
        // 这么写没问题吗
        // 20230925 这么写有问题！
        // PlayerStatControl独立于playerhealthcontrol组件，当切换场景，playerstatcontrol就不在了，就获取不到值了，初始化就寄了。
        // pickupRange = PlayerStatControl.instance.pickupRange[0].value;
        // maxHealth = PlayerStatControl.instance.health[0].value;
        // MainPlayer.instance.moveSpeed = PlayerStatControl.instance.moveSpeed[0].value;

        DontDestroyOnLoad(gameObject);

        currentHealth = maxHealth;
        currentSP = maxSP;
        slider.maxValue = maxHealth;
        slider.value = currentHealth;
        SPSlider.maxValue = maxSP;
        SPSlider.value = currentSP;
        changeSP(0);

        // 获取SP条的颜色
        // Image SPColorObject = SPcolor.GetComponent<Image>();
        SPChangeToColorObject = new Color(0.4325f, 0.05f, 0.120f);
        SPOriginColorObject = SPColorObject.color;
    }

    // Update is called once per frame
    void Update()
    {

        if (!Input.GetKey(KeyCode.LeftShift) || MainPlayer.instance.DashTried)
        {
            SPReadyTimeCounter -= Time.deltaTime;
            if (currentSP < maxSP && SPReadyTimeCounter < 0)
            {
                if (MainPlayer.instance.rb.velocity == Vector2.zero)
                    changeSP(Time.deltaTime * 2F * playerSPRecover);
                else
                    changeSP(Time.deltaTime * playerSPRecover);
            }
            else if (currentSP >= maxSP)
            {
                currentSP = maxSP;
                MainPlayer.instance.DashTried = false;
                SPColorObject.color = SPOriginColorObject;
                changeSP(0);
            }

            //颜色渐变
            // 不是，这效果不好看啊，做的啥玩意儿 
            if (MainPlayer.instance.DashTried)
            {
                if (ColorRate > 1 || moveback)
                {
                    moveback = true;
                    ColorRate -= Time.deltaTime / 2;
                }
                if (!moveback)
                {
                    ColorRate += Time.deltaTime / 2;
                }
                if (ColorRate < 0)
                {
                    moveback = false;
                }
                SPColorObject.color = Color.Lerp(
                    SPOriginColorObject, SPChangeToColorObject,
                    ColorRate);
            }
        }

        //无敌帧处理
        if (InvincibleTime > 0)
        {
            isInvincible = true;
            InvincibleTime -= Time.deltaTime;
        }
        else
        {
            isInvincible = false;
        }
        MainPlayer.instance.am.SetBool("isInvincible", isInvincible);
    }

    // 受到伤害
    public void TakeDamage(float damageToPlayer, int type = 0)
    {
        if (isInvincible)
            return;


        // 类型防御
        if (type == (int)ToPlayerDamageType.Blunt)
        {
            // 减去碰撞防御
            damageToPlayer -= PlayerHealthControl.instance.PlayerCollsionDef;
            if (damageToPlayer < 0)
                damageToPlayer = 0;
        }


        // 回避计算
        float dodge = PlayerDodge > 50 ? 50 : PlayerDodge;
        if (Random.Range(0, 100) < dodge)
        {
            // 播放音效
            SFXManger.instance.PlaySFX(15);
            return;
        }

        // 减去防御，如果是0仍然显示，但是不进入无敌帧。
        if (PlayerDef > 0)
            damageToPlayer -= PlayerDef;
        if (damageToPlayer < 0)
            damageToPlayer = 0;


        currentHealth -= damageToPlayer;

        DamageNumberControl.instance.ShowDamage(damageToPlayer, transform.position, true, 0);

        // 在死亡前播放受伤音效
        // 播放音效
        SFXManger.instance.PlaySFX(6);
        // 屏幕抖动
        // StartCoroutine(CameraControl.instance.Shake(.3f, 0.03f));
        CameraControl.instance.DoShake(0.2f, 0.2f);




        if (currentHealth <= 0 && !isDead)
        {

            if (this.OnTalk)
            {
                currentHealth = 0.1f;
            }
            else
            {
                Debug.Log("game over");

                // 隐藏角色
                MainPlayer.instance.HidePlayer();
                this.isDead = true;

                // 关闭BOSS血条
                UIControl.instance.BossHealth.SetActive(false);

                LevelManager.instance.EndLevel();
                return;
            }
        }

        slider.value = currentHealth;
        HealthText.text = currentHealth.ToString("F1") + "/" + maxHealth;


        //受到伤害会进入无敌帧
        if (damageToPlayer > 0)
        {
            InvincibleTime = HowManyInvincibleTime;
            isInvincible = true;
        }

    }

    public float OnDash()
    {
        changeSP(-Time.deltaTime * (1 + MainPlayer.instance.DashSPRate) * (GlobalControl.instance.isBattle ? 1 : 0));
        return currentSP;
    }

    // 改变 HP
    public bool changeHP(float input)
    {
        if (currentHealth >= maxHealth && input > 0)
        {
            slider.value = currentHealth;
            HealthText.text = currentHealth.ToString("F1") + "/" + maxHealth;
            return false;
        }

        currentHealth += input;
        if (currentHealth >= maxHealth)
            currentHealth = maxHealth;

        slider.value = currentHealth;
        HealthText.text = currentHealth.ToString("F1") + "/" + maxHealth;
        return true;
    }

    public bool ReduceHP(float input)
    {
        currentHealth -= input;

        slider.value = currentHealth;
        HealthText.text = currentHealth.ToString("F1") + "/" + maxHealth;

        if (currentHealth < 0)
            TakeDamage(input);
        return true;
    }

    public void FreshHP()
    {
        slider.value = currentHealth;
        slider.maxValue = maxHealth;
        HealthText.text = currentHealth.ToString("F1") + "/" + maxHealth;
    }

    // 改变SP
    public void changeSP(float input)
    {
        currentSP += input;
        SPSlider.value = currentSP;
        // 显示的SP数值保留小数点后一位小数
        SPText.text = System.Math.Round(currentSP, 1).ToString() + "/" + maxSP;
    }
    public void FreshSP()
    {
        SPSlider.value = currentSP;
        SPSlider.maxValue = maxSP;
        SPText.text = System.Math.Round(currentSP, 1).ToString() + "/" + maxSP;
    }

    /// <summary>
    /// 使用技能时，消耗SP
    /// 算我谢谢你了，这里input直接填正数就行了，不用填负数
    /// </summary>
    /// <param name="input"></param>
    /// <param name="isTouzhiFlag"></param>
    /// <param name="isResetReadyTime"></param>
    /// <returns></returns>
    public bool UseSP(float input, bool isTouzhiFlag = false, bool isResetReadyTime = true)
    {
        input = input * (1 - SPConsumptionReduction);


        // 如果已经疲劳，那么直接返回不能使用
        if (MainPlayer.instance.DashTried)
            return false;

        if (currentSP < 0)
            return false;

        if (currentSP >= input)
        {
            changeSP(-input);
            // 重置歇口气的时间
            if (isResetReadyTime)
            {
                SPReadyTimeCounter = SPReadyTime;
            }
            return true;
        }
        else if (isTouzhiFlag)
        {
            changeSP(-input);
            // 重置歇口气的时间
            if (isResetReadyTime)
                SPReadyTimeCounter = SPReadyTime;
            return true;
        }
        else
        {
            return false;
        }
    }
    // flag参数:是否可以透支
    public bool UseSP(float input, bool flag)
    {
        if (!flag)
            return UseSP(input);

        // 如果已经疲劳，那么直接返回不能使用
        if (MainPlayer.instance.DashTried)
            return false;

        if (currentSP < 0)
            return false;

        changeSP(-input);
        // 重置歇口气的时间
        SPReadyTimeCounter = SPReadyTime;

        if (currentSP < 0)
            MainPlayer.instance.DashTried = true;

        return true;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        // 在室内才发动 //2024 09 06 在室内才发动    吗？
        // if (OnPeace)
        // {
        if (collider.gameObject.tag == "Interactive")
        {
            InteractiveCollider = collider.GetComponent<InteractiveClass>();
            collider.GetComponent<InteractiveClass>().isShowTips = true;
        }
        // }

        // 如果是触发性，直接触发
        if (collider.gameObject.tag == "TriggerInteractive")
        {
            collider.GetComponent<TriggerClass>().Active();
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        // 在室内才发动
        // if (OnPeace)
        // {
        if (collider.gameObject.tag == "Interactive")
        {
            InteractiveCollider = null;
            if (collider.GetComponent<InteractiveClass>() != null)
            {
                collider.GetComponent<InteractiveClass>().isShowTips = false;
            }
        }
        // }
    }

    // 增加最大SP
    public void AddMaxSP(float input)
    {
        MaxSP += input;
        SPSlider.maxValue = MaxSP;
        SPSlider.value = currentSP;
        SPText.text = Mathf.RoundToInt(currentSP) + "/" + MaxSP;
    }
    // 增加最大HP
    public void AddMaxHP(float input)
    {
        MaxHealth += input;
        currentHealth += input;

        slider.maxValue = MaxHealth;
        slider.value = currentHealth;
        HealthText.text = Mathf.RoundToInt(currentHealth) + "/" + MaxHealth;
    }
}


enum ToPlayerDamageType
{
    Blunt = 0,
    Sharp = 1,
    SC = 2
}