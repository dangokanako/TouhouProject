using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GlobalControl : MonoBehaviour
{
    public static GlobalControl instance;
    // 多周目数据
    public int LoopTimes = 0;
    // 击杀数量
    public int totalKill = 0;
    public int currentTotalKill = 0;
    // 游戏时间
    public float totalTime = 0;
    public float currentTotalTime = 0;
    // 获得经验
    public int totalExp = 0;
    public int currentTotalExp = 0;
    // 获得点数
    public int totalPoint = 0;
    public int currentTotalPoint = 0;

    // 死亡损失
    public float deathLoss = 0.8f;

    [Header("角色原本的数值")]
    // 角色原本的数值
    public float originalHP = 25;
    public float originalSP = 4;
    public float originalSPRecover = 1f;
    public float originalATK = 3;
    public float originalDEF = 0;
    public float originalCollisionDEF = 0;
    public int originalCollsionCritical = 0;
    public float originalMoveSpeed = 1.0f;
    public float originalDashSpeed = 2f;
    public float originaldashForce = 1f;
    public float originalDashSPRate = 1f;
    public float originalInvincibleTime = 0.7f;
    public float originalPickupRange = 1f;
    public float originalCriticalRate = 0.05f;
    public float originalCriticalDamag = 0.3f;
    public float originalBluntDamageRate = 0;
    public float originalSharpDamageRate = 0;
    public float originalKillEnemyRecoverHP = 0;
    public float originalKillEnemyRecoverSP = 0;
    public float originalSCDamageRate = 0;
    public float originalSPConsumptionReduction = 0;
    public float originalMass = 35;

    // 是否在战斗界面
    // 正在研究isBattle和OnPeace有什么关系
    public bool isBattle = false;
    // 是否经过新手教程
    public bool isTutorial = false;
    /// <summary>
    /// BOSS击败阶段 
    /// </summary>
    // 是否击败琪露诺
    public bool isGetCrino;
    // 是否击败雾雨魔理沙
    public bool isGetMarisa;
    // 是否击败蕾蒂
    public bool isGetRedi;
    // 是否击败妖梦
    public bool isGetYoumu;
    // 累计进入紫的房间的次数
    public int enterYukariRoom;
    // 紫房间 是否正在离开
    public bool isYukariLeaving;
    // 累计进入荷取的房间的次数
    public int enterNitoriRoom;
    // 河城荷取房间 是否正在离开
    public bool isNitoriLeaving;
    // 是否关闭生成敌人
    public bool isCloseCreateEnemy;
    // 是否观看了升级提示
    public bool isHasShowLevelUpTips;

    // 队友编号 0为没有 1为魔理沙
    public int teammate = 0;
    public Dictionary<int, BubbleTalkData> itemTsukkomu = new Dictionary<int, BubbleTalkData>();
    public Dictionary<int, BubbleTalkData> itemTsukkomu_1 = new Dictionary<int, BubbleTalkData>();

    /// <summary>
    /// 文本对话数据。Tuple的第一个int为地图的ID，第二个int为TalkData_SDM类中的_talkPiece的ID，字典的值为阅读次数。 红魔馆地图ID：0（即默认），新手教学地图ID为1，所有战斗对话视为2
    /// </summary>
    public Dictionary<string, int> hasReadDialog = new Dictionary<string, int>();

    // 物品是否已发现
    public HashSet<int> itemDiscovered = new HashSet<int>();
    public void AddItemDiscovered(int id)
    {
        if (!itemDiscovered.Contains(id))
        {
            itemDiscovered.Add(id);
            if (GlobalControl.instance.teammate == 0)
            {
                if (itemTsukkomu.TryGetValue(id, out BubbleTalkData data))
                {
                    BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData(data.smallhead, data.text));
                }
            }
            if (GlobalControl.instance.teammate == 1)
            {
                if (itemTsukkomu_1.TryGetValue(id, out BubbleTalkData data))
                    BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData(data.smallhead, data.text));
            }
        }
    }
    void Start()
    {
        var cursorTexture = Resources.Load<Texture2D>("other/ichijio_1");
        Cursor.SetCursor(cursorTexture, new Vector2(0, 0), CursorMode.Auto);

        DontDestroyOnLoad(gameObject);

        // 设置更新文件
        MoveUpdateFile();

        LoadItemTsukkomu();
    }

    public void LoadItemTsukkomu()
    {
        itemTsukkomu = new Dictionary<int, BubbleTalkData>();
        itemTsukkomu_1 = new Dictionary<int, BubbleTalkData>();
        // itemTsukkomu[37] = new BubbleTalkData("reimu_small_0", "『欸…我拿剑和妖梦打？真的假的？』");
        // itemTsukkomu[17] = new BubbleTalkData("reimu_small_0", "『所以天子帽子上那个桃子能吃吗……』");
        // itemTsukkomu[64] = new BubbleTalkData("reimu_small_0", "『喔喔……厉害……真的是一点额外属性都不给……太抠门了』");
        // itemTsukkomu[70] = new BubbleTalkData("reimu_small_0", "『无限的酒？到底是什么原理呢？能不能拿来赚钱』");
        // itemTsukkomu[59] = new BubbleTalkData("reimu_small_0", "『全能力增强的药，不过我自己都没有吃过，有BUG及时反馈喔』");
        // itemTsukkomu[30] = new BubbleTalkData("reimu_small_0", "『铁盾？弹幕战用这个？』");
        // itemTsukkomu[27] = new BubbleTalkData("reimu_small_0", "『超市里卖的120日元的廉价品』");
        // itemTsukkomu[16] = new BubbleTalkData("reimu_small_0", "『相当有饱腹感的食物呢，趁热尽快吃掉吧』");
        // itemTsukkomu[25] = new BubbleTalkData("reimu_small_0", "『我觉得……美铃不需要这东西的才对……』");
        // itemTsukkomu[26] = new BubbleTalkData("reimu_small_0", "『炒面。』");
        // itemTsukkomu[46] = new BubbleTalkData("reimu_small_0", "『华扇的铁链？这东西要怎么用？抡人吗？』");
        // itemTsukkomu[51] = new BubbleTalkData("reimu_small_0", "『帕琪或许会喜欢的东西呢』");
        // itemTsukkomu[53] = new BubbleTalkData("reimu_small_0", "『不死鸟之盾这个名字就帅气多了』");
        // itemTsukkomu[71] = new BubbleTalkData("reimu_small_0", "『一根羽毛加这么多属性，那文文一身羽毛不得飞起来啊？』");
        // itemTsukkomu[33] = new BubbleTalkData("reimu_small_0", "『第一步。』");
        // itemTsukkomu[21] = new BubbleTalkData("reimu_small_0", "『这张符卡手感不错。但伤害有点太⑨了』");
        // itemTsukkomu[65] = new BubbleTalkData("reimu_small_0", "『咲夜这张符卡看着还不错，但伤害也太低了，慢慢升级吧』");
        // itemTsukkomu[60] = new BubbleTalkData("reimu_small_0", "『逐梦者，这把武器的名字是在说梅莉喔。』");
    }
    public bool GetItemDiscovered(int id)
    {
        return itemDiscovered.Contains(id);
    }


    public void AddHasReadDialog(int mapId, int talkPieceId)
    {
        var key = $"{mapId},{talkPieceId}";
        if (!hasReadDialog.ContainsKey(key))
            hasReadDialog.Add(key, 1);
        else
            hasReadDialog[key]++;
    }

    public bool GetHasReadDialog(int mapId, int talkPieceId)
    {
        var key = $"{mapId},{talkPieceId}";
        if (hasReadDialog.ContainsKey(key) && hasReadDialog[key] > 0)
            return true;
        else
            return false;
    }
    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
        }
    }



    public void MoveUpdateFile()
    {
        // 源文件路径（在StreamingAssets文件夹中）
        string sourcePath = Path.Combine(Application.streamingAssetsPath, "更新日志.txt");
        // 目标文件路径（在exe文件所在的目录）
        string targetPath = Path.Combine(Application.dataPath, "../更新日志.txt");

        if (File.Exists(sourcePath))
        {
            // 如果目标文件已经存在，先删除它
            if (File.Exists(targetPath))
            {
                File.Delete(targetPath);
            }

            // 复制文件
            File.Copy(sourcePath, targetPath);
        }
        else
        {
            Debug.LogError("Source file not found: " + sourcePath);
        }
    }

    // 设置角色属性为初始数值
    public void SetStatusToOriginal()
    {
        PlayerHealthControl.instance.maxHealth = GlobalControl.instance.originalHP;
        PlayerHealthControl.instance.currentHealth = GlobalControl.instance.originalHP;
        // 刷新HP
        PlayerHealthControl.instance.maxSP = GlobalControl.instance.originalSP;
        PlayerHealthControl.instance.currentSP = GlobalControl.instance.originalSP;
        // 刷新SP
        PlayerHealthControl.instance.PlayerAtk = GlobalControl.instance.originalATK;
        PlayerHealthControl.instance.PlayerDef = GlobalControl.instance.originalDEF;
        PlayerHealthControl.instance.PlayerCollsionDef = GlobalControl.instance.originalCollisionDEF;
        PlayerHealthControl.instance.PlayerCollsionCritical = GlobalControl.instance.originalCollsionCritical;

        MainPlayer.instance.moveSpeed = GlobalControl.instance.originalMoveSpeed;
        MainPlayer.instance.dashSpeed = GlobalControl.instance.originalDashSpeed;
        MainPlayer.instance.dashSPRate = GlobalControl.instance.originalDashSPRate;
        MainPlayer.instance.dashForce = GlobalControl.instance.originaldashForce;
        MainPlayer.instance.isFreeDash = false;

        PlayerHealthControl.instance.pickupRange = GlobalControl.instance.originalPickupRange;
        PlayerHealthControl.instance.CriticalRate = GlobalControl.instance.originalCriticalRate;
        PlayerHealthControl.instance.CriticalDamage = GlobalControl.instance.originalCriticalDamag;
        PlayerHealthControl.instance.playerSPRecover = GlobalControl.instance.originalSPRecover;

        PlayerHealthControl.instance.BluntDamageRate = GlobalControl.instance.originalBluntDamageRate;
        PlayerHealthControl.instance.SharpDamageRate = GlobalControl.instance.originalSharpDamageRate;
        PlayerHealthControl.instance.SCDamageRate = GlobalControl.instance.originalSCDamageRate;

        PlayerHealthControl.instance.KillEnemyRecoverHP = GlobalControl.instance.originalKillEnemyRecoverHP;
        PlayerHealthControl.instance.KillEnemyRecoverSP = GlobalControl.instance.originalKillEnemyRecoverSP;

        PlayerHealthControl.instance.SPConsumptionReduction = GlobalControl.instance.originalSPConsumptionReduction;

        PlayerHealthControl.instance.InvincibleTime = GlobalControl.instance.originalInvincibleTime;
        MainPlayer.instance.rb.mass = GlobalControl.instance.originalMass;

        // 刷新HP和SP条
        PlayerHealthControl.instance.FreshHP();
        PlayerHealthControl.instance.FreshSP();

        // 刷新面板属性
        UIControl.instance.SetPlayerInfoShow();
    }

    public void LoadState(GlobalControlState loadstate)
    {
        LoopTimes = loadstate.LoopTimes;
        totalKill = loadstate.totalKill;
        currentTotalKill = loadstate.currentTotalKill;
        totalTime = loadstate.totalTime;
        currentTotalTime = loadstate.currentTotalTime;
        totalExp = loadstate.totalExp;
        currentTotalExp = loadstate.currentTotalExp;
        totalPoint = loadstate.totalPoint;
        currentTotalPoint = loadstate.currentTotalPoint;
        deathLoss = loadstate.deathLoss;
        originalHP = loadstate.originalHP;
        originalSP = loadstate.originalSP;
        originalSPRecover = loadstate.originalSPRecover;
        originalATK = loadstate.originalATK;
        originalDEF = loadstate.originalDEF;
        originalCollisionDEF = loadstate.originalCollisionDEF;
        originalCollsionCritical = loadstate.originalCollsionCritical;
        originalMoveSpeed = loadstate.originalMoveSpeed;
        originalDashSpeed = loadstate.originalDashSpeed;
        originaldashForce = loadstate.originaldashForce;
        originalDashSPRate = loadstate.originalDashSPRate;
        originalInvincibleTime = loadstate.originalInvincibleTime;
        originalPickupRange = loadstate.originalPickupRange;
        originalCriticalRate = loadstate.originalCriticalRate;
        originalCriticalDamag = loadstate.originalCriticalDamag;
        originalBluntDamageRate = loadstate.originalBluntDamageRate;
        originalSharpDamageRate = loadstate.originalSharpDamageRate;
        originalSCDamageRate = loadstate.originalSCDamageRate;
        isBattle = loadstate.isBattle;
        isTutorial = loadstate.isTutorial;
        isGetMarisa = loadstate.isGetMarisa;
        isGetCrino = loadstate.isGetCrino;
        isGetRedi = loadstate.isGetRedi;
        isGetYoumu = loadstate.isGetYoumu;
        enterYukariRoom = loadstate.enterYukariRoom;
        enterNitoriRoom = loadstate.enterNitoriRoom;
        isHasShowLevelUpTips = loadstate.isHasShowLevelUpTips;

        teammate = loadstate.teammate;
        instance.LoadItemTsukkomu();
        hasReadDialog = loadstate.hasReadDialog;
        itemDiscovered = new HashSet<int>(loadstate.itemDiscovered);

    }
}
