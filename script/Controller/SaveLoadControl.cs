using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.Linq;
public class SaveLoadControl : MonoBehaviour
{
    public static SaveLoadControl instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void SaveToFile()
    {

        // 获取当前活动的场景的名字
        string currentSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        // 将场景名添加到保存数据中
        if (currentSceneName != "ScarletDevilMansion")
        {
            UIControl.instance.ShowTips("只能在红魔馆保存游戏", Input.mousePosition);
            return;
        }

        SaveData save = GetSaveInfo();
        string saveJson = JsonMapper.ToJson(save);
        string saveDirectory = Application.dataPath + "/Save";
        string savePath = Application.dataPath + "/Save/save.json";

        // 检查目标文件夹是否存在，如果不存在，创建它
        if (!System.IO.Directory.Exists(saveDirectory))
        {
            System.IO.Directory.CreateDirectory(saveDirectory);
        }


        System.IO.File.WriteAllText(savePath, saveJson);
        UIControl.instance.ShowTips("存档成功(大概)", Input.mousePosition);

    }

    public void LoadByFile()
    {
        string savePath = Application.dataPath + "/Save/save.json";
        if (System.IO.File.Exists(savePath))
        {
            string saveJson = System.IO.File.ReadAllText(savePath);
            SaveData save = JsonMapper.ToObject<SaveData>(saveJson);
            ApplySaveInfo(save);
        }
        else
        {
            Debug.Log("存档文件不存在");
        }

        UIControl.instance.ShowTips("读档成功(大概)", Input.mousePosition);

        UIControl.instance.Restart(false);


    }
    public SaveData GetSaveInfo()
    {
        SaveData save = new SaveData();
        save.globalControl = new GlobalControlState(GlobalControl.instance);
        save.assetControl = new AssetControlState(AssetControl.instance);
        // save.playerHealthControl = new PlayerHealthControlState(PlayerHealthControl.instance);
        // save.itemControl = new ItemControlState(ItemControl.instance);

        return save;
    }

    public void ApplySaveInfo(SaveData save)
    {
        GlobalControl.instance.LoadState(save.globalControl);
        AssetControl.instance.LoadState(save.assetControl);
        // GlobalControl.instance = save.globalControl;
        // PlayerHealthControl.instance = save.playerHealthControl;
    }
}

public class SaveData
{
    public GlobalControlState globalControl;
    public AssetControlState assetControl;
    // public PlayerHealthControlState playerHealthControl;
    // public ItemControlState itemControl;
}


public class GlobalControlState
{
    public GlobalControlState() { }
    // 只包含 GlobalControl 类的状态信息的字段
    public int LoopTimes;
    public int totalKill;
    public int currentTotalKill;
    public float totalTime;
    public float currentTotalTime;
    public int totalExp;
    public int currentTotalExp;
    public int totalPoint;
    public int currentTotalPoint;
    public float deathLoss;
    public float originalHP;
    public float originalSP;
    public float originalSPRecover;
    public float originalATK;
    public float originalDEF;
    public float originalCollisionDEF;
    public int originalCollsionCritical;
    public float originalMoveSpeed;
    public float originalDashSpeed;
    public float originaldashForce;
    public float originalDashSPRate;
    public float originalInvincibleTime;
    public float originalPickupRange;
    public float originalCriticalRate;
    public float originalCriticalDamag;
    public float originalBluntDamageRate;
    public float originalSharpDamageRate;
    public float originalSCDamageRate;
    public bool isBattle;
    public bool isTutorial;
    public bool isGetMarisa;
    public bool isGetCrino;
    public bool isGetRedi;
    public bool isGetYoumu;
    public int enterYukariRoom;
    public int enterNitoriRoom;
    public bool isHasShowLevelUpTips;

    public int teammate;
    public Dictionary<string, int> hasReadDialog;
    public List<int> itemDiscovered;
    public GlobalControlState(GlobalControl globalControl)
    {
        LoopTimes = globalControl.LoopTimes;
        totalKill = globalControl.totalKill;
        currentTotalKill = globalControl.currentTotalKill;
        totalTime = globalControl.totalTime;
        currentTotalTime = globalControl.currentTotalTime;
        totalExp = globalControl.totalExp;
        currentTotalExp = globalControl.currentTotalExp;
        totalPoint = globalControl.totalPoint;
        currentTotalPoint = globalControl.currentTotalPoint;
        deathLoss = globalControl.deathLoss;
        originalHP = globalControl.originalHP;
        originalSP = globalControl.originalSP;
        originalSPRecover = globalControl.originalSPRecover;
        originalATK = globalControl.originalATK;
        originalDEF = globalControl.originalDEF;
        originalCollisionDEF = globalControl.originalCollisionDEF;
        originalCollsionCritical = globalControl.originalCollsionCritical;
        originalMoveSpeed = globalControl.originalMoveSpeed;
        originalDashSpeed = globalControl.originalDashSpeed;
        originaldashForce = globalControl.originaldashForce;
        originalDashSPRate = globalControl.originalDashSPRate;
        originalInvincibleTime = globalControl.originalInvincibleTime;
        originalPickupRange = globalControl.originalPickupRange;
        originalCriticalRate = globalControl.originalCriticalRate;
        originalCriticalDamag = globalControl.originalCriticalDamag;
        originalBluntDamageRate = globalControl.originalBluntDamageRate;
        originalSharpDamageRate = globalControl.originalSharpDamageRate;
        originalSCDamageRate = globalControl.originalSCDamageRate;
        isBattle = globalControl.isBattle;
        isTutorial = globalControl.isTutorial;
        isGetMarisa = globalControl.isGetMarisa;
        isGetCrino = globalControl.isGetCrino;
        isGetRedi = globalControl.isGetRedi;
        isGetYoumu = globalControl.isGetYoumu;
        enterYukariRoom = globalControl.enterYukariRoom;
        enterNitoriRoom = globalControl.enterNitoriRoom;
        isHasShowLevelUpTips = globalControl.isHasShowLevelUpTips;


        teammate = globalControl.teammate;
        hasReadDialog = globalControl.hasReadDialog;
        itemDiscovered = new List<int>(globalControl.itemDiscovered);

    }

}


public class ItemControlState
{
    // 太复杂了，暂时不写
    public ItemControlState() { }
    // 只包含 ItemControl 类的状态信息的字段
    // public List<ItemData> itembagList;

    // public ItemControlState(ItemControl globalControl)
    // {
    //     itembagList = globalControl.itembagList;
    // }

}



public class PlayerHealthControlState
{
    // 只包含 PlayerHealthControl 类的状态信息的字段
}

public class AssetControlState
{
    // 普通P点
    public int currentPoint;
    // 超级P点
    public int currentSuperPoint;
    // 快捷栏数量
    public int MaxOfBags;
    // 背包数量
    public int MaxOfWarehouse;
    // 帕秋莉商店购买情况
    public bool[][] PatchouliShopData = new bool[4][]{
        new bool[7],
        new bool[7],
        new bool[7],
        new bool[7]
    };

    public AssetControlState() { }
    public AssetControlState(AssetControl assetControl)
    {
        currentPoint = assetControl.currentPoint;
        currentSuperPoint = assetControl.currentSuperPoint;

        MaxOfBags = ItemControl.instance.MaxOfBags;
        MaxOfWarehouse = ItemControl.instance.MaxOfWarehouse;

        // DEBUGPatchouliShopData
        for (int i = 0; i < PatchouliShopData.Count(); i++)
        {
            for (int j = 0; j < PatchouliShopData[i].Count(); j++)
            {
                Debug.Log("PatchouliShopData[" + i + "][" + j + "]:" + PatchouliShopData[i][j]);
            }
        }

        // DEBUGPatchouliShopData
        for (int i = 0; i < ShopControl.instance.PatchouliShopData.Count(); i++)
        {
            for (int j = 0; j < ShopControl.instance.PatchouliShopData[i].Count(); j++)
            {
                Debug.Log("ShopControl.instance.PatchouliShopData[" + i + "][" + j + "]:" + ShopControl.instance.PatchouliShopData[i][j]);
            }
        }


        PatchouliShopData = ShopControl.instance.PatchouliShopData;
    }
}

