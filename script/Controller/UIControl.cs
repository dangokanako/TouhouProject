using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Linq;
using Unity.VisualScripting;
using DG.Tweening;
using System.Numerics;
using Vector2 = UnityEngine.Vector2;
using Quaternion = UnityEngine.Quaternion;

public class UIControl : MonoBehaviour
{
    public static UIControl instance;
    // 经验条，普通P点，超级P点
    public Slider expSlider;
    public TMP_Text expText;
    public TMP_Text pointText;
    public TMP_Text superpointText;
    [Header("菜单")]
    public GameObject menuPanel;
    public GameObject menuPanel_Player;
    // 注意：合成面板实际使用的是itemcontrol里的对象。这里仅对称好看
    public GameObject menuPanel_Warehouse;
    public GameObject menuPanel_Setting;
    public GameObject menuPanel_Upgrade;

    [Header("菜单里的那四个按钮")]
    public GameObject PausePanelButtonPic1;
    public GameObject PausePanelButtonPic2;
    public GameObject PausePanelButtonPic3;
    public GameObject PausePanelButtonPic4;
    [Header("菜单里的那三个条")]
    public Slider sliderUI1;
    public Slider sliderUI2;
    public Slider sliderUI3;

    [Header("大地图组件")]
    public GameObject bigMapPanel;

    [Header("跳过按钮")]
    public Button SkipButton;

    public TMP_Text timeText;
    public GameObject levelupPanel;
    [Header("结算面板")]

    // 游戏结束结算画面
    public GameObject gameoverPanel;
    public TMP_Text gameoverText;
    public string mainMenuName;
    // 暂停组件
    public GameObject pauseScreen;
    [Header("发动符卡时的立绘和文字")]
    public Image Lihui;
    public TMP_Text scName;
    [Header("BOSS血条组件")]
    [SerializeField] public GameObject BossHealth;
    [SerializeField] public float currentHealth_Boss, maxHealth_Boss;
    [SerializeField] public Slider slider_Boss;
    [SerializeField] public TMP_Text HealthText_Boss;
    [SerializeField] public TMP_Text NameText_Boxx;
    [Header("显示角色信息组件")]
    [SerializeField] public TMP_Text showPlayerInfo_1;
    [SerializeField] public TMP_Text showPlayerInfo_2;
    [SerializeField] public TMP_Text showPlayerInfo_3;

    [Header("商店面板")]
    public GameObject shopPanel;
    [Header("小提示组件")]

    public TMP_Text TipsPlane;
    [Header("大提示组件")]
    public GameObject BigTipsPlane;

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
    }

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        this.SetPlayerInfoShow();
        bouns = 50;
    }

    void Update()
    {
        SettingPlane();

    }
    public void SettingPlane()
    {
        if (Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {

            if (UIControl.instance.bigMapPanel.activeSelf)
                return;

            if (!menuPanel.activeSelf)
            {
                Time.timeScale = 0f;
            }
            else
            {

                if (!UIControl.instance.levelupPanel.activeSelf)
                    Time.timeScale = 1f;
            }

            menuPanel.SetActive(!menuPanel.activeSelf);
            // 改变状态
            ItemControl.instance.isCombinePlane = menuPanel.activeSelf;

            // 基础面板显示
            if (menuPanel.activeSelf)
            {
                sliderUI1.value = PlayerHealthControl.instance.currentHealth / PlayerHealthControl.instance.maxHealth;
                sliderUI2.value = PlayerHealthControl.instance.currentSP / PlayerHealthControl.instance.maxSP;


                sliderUI3.value = (float)ExpLevelControl.instance.currentExp / (float)ExpLevelControl.instance.expLevelNeed[ExpLevelControl.instance.currentLevel];
            }



            if (UIControl.instance.menuPanel.activeSelf)
                this.PausePanelButtonPush2();

            if (!UIControl.instance.menuPanel.activeSelf)
            {
                // 如果关闭菜单面板的业务
                ItemControl.instance.ClearCombineItem();
            }
        }
    }

    public bool isShopOpen()
    {
        if (shopPanel == null)
            return false;
        return shopPanel.activeSelf;
    }
    // 显示商店面板
    public void OpenShop()
    {
        // 关闭BOSS血条
        UIControl.instance.BossHealth.SetActive(false);

        shopPanel.SetActive(true);
    }
    public void OpenShopForInitialize()
    {
        shopPanel.SetActive(true);
        StartCoroutine(CloseShopForInitialize());
    }
    IEnumerator CloseShopForInitialize()
    {
        yield return new WaitForSeconds(0.01f);
        shopPanel.SetActive(false);
    }

    // 关闭商店面板
    // 注意：有个小问题，按空格会直接关闭商店，原因是空格直接触发了UNITY的UI的按钮事件，需要在button的navi里面选择none即可。
    public void CloseShop()
    {
        shopPanel.SetActive(false);
    }
    public void UpdateExp(int _currentExp, int _levelUpExp, int _currentLevel)
    {
        expSlider.maxValue = _levelUpExp;
        // expSlider.value = _currentExp;
        expSlider.DOValue(_currentExp, 0.2f).SetUpdate(true);

        // expText.text = "Level:" + _currentLevel;
    }


    public void SkipLevelUP()
    {
        UIControl.instance.levelupPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void updatePoint()
    {
        pointText.text = ":" + AssetControl.instance.currentPoint;
    }

    public void updateSuperPoint()
    {
        superpointText.text = ":" + AssetControl.instance.currentSuperPoint;
    }

    public item GetRandomItem()
    {
        return ItemControl.instance.itemGuide[GetRandomWithExceptions(0, ItemControl.instance.itemGuide.Count - 1, EnemyDropTable.shouldnotFreshItem)];
    }

    List<int> GenerateRangeWithExceptions(int min, int max, List<int> exceptions)
    {
        return Enumerable.Range(min, max - min + 1).Except(exceptions).ToList();
    }

    int GetRandomWithExceptions(int min, int max, List<int> exceptions)
    {
        List<int> numbers = GenerateRangeWithExceptions(min, max, exceptions);
        return numbers[Random.Range(0, numbers.Count)];
    }
    // 点击跳过波次的按钮
    public void SkipButtonEvent()
    {
        if (EnemyCreator.instance.currentWave > EnemyCreator.instance.waves.Count - 1)
        {

            if (EnemyCreator.instance.creatorEnemy.Count == 0)
            {
                UIControl.instance.OpenBigMap();
                EnemyCreator.instance.CloseAllProtal();

            }
            return;
        }


        // AssetControl.instance.AddPoint((int)(EnemyCreator.instance.waveCounter * bouns * 0.005f));


        var num = (int)(EnemyCreator.instance.waveCounter * bouns * 0.005f);
        for (int i = 0; i < num; i++)
        {
            // if (Random.Range(0, 100) < 50)
            //     ExpLevelControl.instance.CreateDrop(MainPlayer.instance.transform.position, 1);
            // else
            AssetControl.instance.DropPoint(MainPlayer.instance.transform.position, 1);
        }
        SFXManger.instance.PlaySFX(0);
        EnemyCreator.instance.waves[EnemyCreator.instance.currentWave].immediatelyFlag = true;
        bouns -= 10;

    }
    public void UpdateTimer(float _time)
    {
        float minute = Mathf.FloorToInt(_time / 60f);
        float seconds = Mathf.FloorToInt(_time % 60);
        float hour = 0f;
        if (minute > 60f)
        {
            hour = Mathf.FloorToInt(minute / 60f);
            minute %= 60;
        }

        if (hour != 0)
        {
            timeText.text = "时长：" + hour + ":" + minute + ":" + seconds;
        }
        else
        {
            timeText.text = "时长：" + minute + ":" + seconds.ToString("00");
        }


    }
    public int bouns;
    public float BounsCounter;
    public void SetSkipButton()
    {
        if (EnemyCreator.instance)
        {
            if (EnemyCreator.instance.currentWave > EnemyCreator.instance.waves.Count - 1)
            {
                SkipButton.GetComponentInChildren<TMP_Text>().text = "已经是最后一波次了";

                if (EnemyCreator.instance.creatorEnemy.Count == 0)
                {
                    SkipButton.GetComponentInChildren<TMP_Text>().text = "向前进发";
                    SkipButton.interactable = true;
                }
                return;
            }

            if (bouns < 70)
            {
                BounsCounter -= Time.deltaTime;
                if (BounsCounter <= 0)
                {
                    bouns += 1;
                    BounsCounter = 1f;
                }
            }


            if (EnemyCreator.instance.waves[EnemyCreator.instance.currentWave].restFlag)
            {
                SkipButton.GetComponentInChildren<TMP_Text>().text = "休整时间：" + EnemyCreator.instance.waveCounter.ToString("F0") + "秒\n" +
                "点击本按钮跳过获取" + (EnemyCreator.instance.waveCounter * (bouns * 0.5f) * 0.01).ToString("F0") + "P点";
            }
            else
            {
                SkipButton.GetComponentInChildren<TMP_Text>().text = "本波次剩余：" + EnemyCreator.instance.waveCounter.ToString("F0") + "秒\n" +
                "点击本按钮跳过获取" + (EnemyCreator.instance.waveCounter * (bouns * 0.5f + 10) * 0.01).ToString("F0") + "P点";
            }

            if (bouns < 20 || !GlobalControl.instance.isBattle)
                SkipButton.interactable = false;
            else
                SkipButton.interactable = true;
        }
    }
    public void GoToMainMenu()
    {
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
        }
        SceneManager.LoadScene(mainMenuName);
    }

    public void Restart(bool isAddLoopTimes = true)
    {
        try
        {
            if (isAddLoopTimes)
                GlobalControl.instance.LoopTimes++;

            // 删除地面上的DropsClass类型的物品
            DropsClass[] drops = FindObjectsOfType<DropsClass>();
            foreach (DropsClass drop in drops)
            {
                Destroy(drop.gameObject);
            }

            FadeInOut.instance.GoToScene("ScarletDevilMansion");

            // TODO 需要重构
            GlobalControl.instance.isBattle = false; ;
            PlayerHealthControl.instance.OnPeace = true;

            // 重置玩家位置
            MainPlayer.instance.transform.position = new Vector2(-1.022f, 0.37f);
            MainPlayer.instance.transform.localRotation = Quaternion.Euler(0, 0, 0);

            // 清空所有格子
            ItemControl.instance.ClearAllBag();

            // 重置按钮
            UIControl.instance.SkipButton.GetComponentInChildren<TMP_Text>().text = "跳过按钮";
            MainPlayer.instance.UnHidePlayer();


            // 重置时间
            // LevelManager.instance.timer = 0f;

            // 重置BOSS血条
            UIControl.instance.BossHealth.SetActive(false);

            // 取消游戏结束面板
            UIControl.instance.gameoverPanel.SetActive(false);

            // 重置地图探索
            if (BigMapControl.instance != null)
                BigMapControl.instance.Reset();

            // 重置玩家信息
            PlayerHealthControl.instance.isDead = false;

            // 重置技能树
            if (SkillPointControl.instance != null)
                SkillPointControl.instance.ResetSkillPoint();


            // MainPlayer.instance.gameObject.SetActive(true);
            // PlayerHealthControl.instance.gameObject.SetActive(true);

            // 重置玩家状态 
            // 重置玩家状态必须在重置背包之后，不然初始状态后才删除被动装备，会导致倒扣数值
            GlobalControl.instance.SetStatusToOriginal();

            if (Time.timeScale == 0f)
            {
                Time.timeScale = 1f;
            }


            // 自动保存游戏
            SaveLoadControl.instance.SaveToFile();

            UIControl.instance.ShowTips("已自动保存游戏", Input.mousePosition);
        }
        catch (System.Exception e)
        {
            Debug.LogError(e);
        }
    }
    // 初始化信息
    // 重置游戏
    IEnumerator ClearInfo()
    {
        yield return new WaitForSeconds(2.0f);


    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("游戏已关闭（迫真）");
    }

    public void PauseUnpause()
    {
        // 按下暂停按钮，如果有暂停面板，那么关闭暂停面板，并回复时间
        if (pauseScreen.activeSelf)
        {
            pauseScreen.SetActive(false);
            Time.timeScale = 1f;

            // if (levelupPanel.activeSelf)
            // {
            //     Time.timeScale = 1f;
            // }
        }
        // 按下暂停按钮，如果没有暂停面板，那么调出暂停面板并暂停
        else
        {

            pauseScreen.SetActive(true);
            Time.timeScale = 0f;
        }

    }

    // 参数 duration 持续时间（减速）
    public void UI_SCActive(float duration)
    {
        Time.timeScale = 0.2f;
        StartCoroutine(ReturnNormalTime(duration * 0.2f)); // 时间变成0.2倍速的话，计算时间也会变成0.2倍速        

        // 图像来源设置 TODO

        // 图像渐变 移动设置 TODO
        Instantiate(Lihui, Lihui.transform.position, Quaternion.identity, instance.transform).gameObject.SetActive(true);

    }

    public IEnumerator ReturnNormalTime(float _duration)
    {
        yield return new WaitForSeconds(_duration);
        Time.timeScale = 1f;
    }

    // 设置角色信息显示
    public void SetPlayerInfoShow()
    {
        showPlayerInfo_1.text =
        "最大生命值：" + PlayerHealthControl.instance.maxHealth + "\n" +
        "防御力：" + PlayerHealthControl.instance.PlayerDef + "\n" +
        "近战攻击：" + PlayerHealthControl.instance.PlayerAtk + "\n" +
        "移动速度：" + MainPlayer.instance.moveSpeed + "\n" +
        "奔跑速度：" + MainPlayer.instance.DashSpeed + "\n";

        showPlayerInfo_2.text =
        "最大耐力：" + PlayerHealthControl.instance.maxSP + "\n" +
        "耐力恢复：" + PlayerHealthControl.instance.playerSPRecover * 100 + "%" + "\n" +
        "暴击率：" + PlayerHealthControl.instance.CriticalRate * 100 + "%" + "\n" +
        "暴击倍率：" + PlayerHealthControl.instance.CriticalDamage * 100 + "%" + "\n" +
        "碰撞防御：" + PlayerHealthControl.instance.PlayerCollsionDef + "\n";

        showPlayerInfo_3.text =
        "打击伤增：" + PlayerHealthControl.instance.BluntDamageRate * 100 + "%" + "\n" +
        "锐器伤增：" + PlayerHealthControl.instance.SharpDamageRate * 100 + "%" + "\n" +
        "符卡伤增：" + PlayerHealthControl.instance.SCDamageRate * 100 + "%" + "\n";
    }

    public void PausePanelButtonHover1()
    {
        PausePanelButtonPic1.SetActive(true);
    }
    public void PausePanelButtonHoverEnd1()
    {
        PausePanelButtonPic1.SetActive(false);
    }
    public void PausePanelButtonPush1()
    {
        menuPanel_Player.SetActive(true);
        menuPanel_Warehouse.SetActive(false);
        menuPanel_Setting.SetActive(false);
        menuPanel_Upgrade.SetActive(false);
    }

    public void PausePanelButtonHover2()
    {
        PausePanelButtonPic2.SetActive(true);
    }
    public void PausePanelButtonHoverEnd2()
    {
        PausePanelButtonPic2.SetActive(false);
    }
    public void PausePanelButtonPush2()
    {
        menuPanel_Player.SetActive(false);
        menuPanel_Warehouse.SetActive(true);
        menuPanel_Setting.SetActive(false);
        menuPanel_Upgrade.SetActive(false);

    }

    public void PausePanelButtonHover3()
    {
        PausePanelButtonPic3.SetActive(true);
    }
    public void PausePanelButtonHoverEnd3()
    {
        PausePanelButtonPic3.SetActive(false);
    }
    public void PausePanelButtonPush3()
    {
        menuPanel_Player.SetActive(false);
        menuPanel_Warehouse.SetActive(false);
        menuPanel_Setting.SetActive(false);
        menuPanel_Upgrade.SetActive(true);
    }

    public void PausePanelButtonHover4()
    {
        PausePanelButtonPic4.SetActive(true);
    }
    public void PausePanelButtonHoverEnd4()
    {
        PausePanelButtonPic4.SetActive(false);
    }
    public void PausePanelButtonPush4()
    {
        menuPanel_Player.SetActive(false);
        menuPanel_Warehouse.SetActive(false);
        menuPanel_Upgrade.SetActive(false);
        menuPanel_Setting.SetActive(true);
    }

    // 大地图组件
    public void CloseBigMap()
    {
        Time.timeScale = 1f;
        bigMapPanel.SetActive(false);
    }
    public void OpenBigMap()
    {
        Time.timeScale = 0f;
        bigMapPanel.SetActive(true);
    }


    // 设置TIPS
    public void ShowTips(string _tips, Vector2 vector2)
    {
        CanvasGroup canvasGroup = TipsPlane.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = TipsPlane.gameObject.AddComponent<CanvasGroup>();
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
        // 重置透明度
        canvasGroup.alpha = 1;

        TipsPlane.gameObject.SetActive(true);
        TipsPlane.text = _tips;

        // 设置初始位置
        TipsPlane.transform.position = vector2;

        // 在1秒内，位置上升5单位
        TipsPlane.transform.DOKill(); // 停止之前的动画
        TipsPlane.transform.DOLocalMoveY(0.3f, 1f);
        // 获取Text组件的CanvasGroup，用于改变透明度
        canvasGroup.DOKill();
        canvasGroup.DOFade(0, 1f).SetUpdate(true).OnComplete(() =>
        {
            // 动画完成后，隐藏TipsPlane
            TipsPlane.gameObject.SetActive(false);
        });

    }

    // 设置大tips
    public void ShowBigTips(string tipsPath)
    {
        Time.timeScale = 0f;
        BigTipsPlane.SetActive(true);
        var Image = BigTipsPlane.GetComponentInChildren<Image>();
        Image.sprite = Resources.Load<Sprite>("BigTips/" + tipsPath);
    }
    public void CloseBigTips()
    {
        Time.timeScale = 1f;
        BigTipsPlane.SetActive(false);
    }
}
