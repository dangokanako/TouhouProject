using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BigMapComponentControl : MonoBehaviour
{
    // 位置ID
    public int LoctionID;
    // 位置名称 其实不写也无所谓
    public string LoctionName;

    // 按钮组件
    public Button button;

    void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(HandleClick);
    }

    void Start()
    {
        BigMapControl.instance.mapControlDict.Add(LoctionID, this);
    }

    public void HandleClick()
    {
        // 根据ID传送地图，目前写死
        if (!GoToSceneByID(LoctionID))
            return;

        // 删除地面上的DropsClass类型的物品
        DropsClass[] drops = FindObjectsOfType<DropsClass>();
        foreach (DropsClass drop in drops)
        {
            Destroy(drop.gameObject);
        }


        GlobalControl.instance.isBattle = true;
        if (GlobalControl.instance.isBattle)
        {
            PlayerHealthControl.instance.OnPeace = false;
        }


        // 设置地点，写死的
        BigMapControl.instance.explored.Add(LoctionID);
        BigMapControl.instance.BigMapLoctionID = LoctionID;

        Time.timeScale = 1f;
        UIControl.instance.CloseBigMap();
    }
    // 地图ID 记得手动更新（笑）
    // 雾之湖 不包含BOSS战
    public static List<int> WuZhiHu = new List<int> { 2, 3, 4 };
    // 魔法之森 不包含BOSS战
    public static List<int> MoFaSenLin = new List<int> { 5, 8, 10, 11, 23 };
    private bool GoToSceneByID(int id)
    {
        switch (id)
        {
            case 1:
                // FadeInOut.instance.GoToScene("ScarletDevilMansion");
                GlobalControl.instance.isBattle = false;
                UIControl.instance.Restart();
                break;
            case 2:
                FadeInOut.instance.GoToScene("WuZhiHu_1");
                break;
            case 3:
                FadeInOut.instance.GoToSceneFake(() =>
                        {
                            // 移动玩家位置
                            MainPlayer.instance.transform.position = new Vector3(0.11f, -20.97f, 0);
                            GlobalControl.instance.isBattle = true;
                            EnemyCreator.instance.LoadEnemyCretor("WuZhiHu_2");
                            Map_Battle_Wuzhihu_1.instance.GetBubble(LoctionID);
                        }
                );
                break;
            case 4:
                FadeInOut.instance.GoToSceneFake(() =>
                    {
                        // 移动玩家位置
                        MainPlayer.instance.transform.position = new Vector3(22f, -1f, 0);
                        EnemyCreator.instance.LoadEnemyCretor("WuZhiHu_3");
                        GlobalControl.instance.isBattle = true;
                        Map_Battle_Wuzhihu_1.instance.GetBubble(LoctionID);
                    }
                    );
                break;
            case 5:
                FadeInOut.instance.GoToSceneFake(() =>
                    {
                        // 移动玩家位置
                        MainPlayer.instance.transform.position = new Vector3(22f, -26f, 0);
                        EnemyCreator.instance.LoadEnemyCretor("MoFaSenLin_1");
                        GlobalControl.instance.isBattle = true;
                        Map_Battle_Wuzhihu_1.instance.GetBubble(LoctionID);
                    }
                    );
                break;
            case 8:
                FadeInOut.instance.GoToSceneFake(() =>
                    {
                        // 移动玩家位置
                        MainPlayer.instance.transform.position = new Vector3(42f, -1f, 0);
                        GlobalControl.instance.isBattle = true;
                        EnemyCreator.instance.LoadEnemyCretor("MoFaSenLin_2");
                        Map_Battle_Wuzhihu_1.instance.GetBubble(LoctionID);
                    }
                    );
                break;
            // 雾雨魔法店
            case 9:
                FadeInOut.instance.GoToSceneFake(() =>
                {
                    // 移动玩家位置
                    MainPlayer.instance.transform.position = new Vector3(42f, -23f, 0);
                    GlobalControl.instance.isBattle = true;
                    TalkDialogMarisa.GetText();
                    // 注：魔理沙在队伍时规避魔理沙战斗，但为了不破坏结构这里还是正常加载
                    EnemyCreator.instance.LoadEnemyCretor("MoFaSenLin_6");
                }
                );
                break;
            // 魔法之森南
            case 10:
                FadeInOut.instance.GoToSceneFake(() =>
                    {
                        // 移动玩家位置
                        MainPlayer.instance.transform.position = new Vector3(43f, -12f, 0);
                        EnemyCreator.instance.LoadEnemyCretor("MoFaSenLin_3");
                        GlobalControl.instance.isBattle = true;
                        Map_Battle_Wuzhihu_1.instance.GetBubble(LoctionID);
                    }
                    );
                break;
            case 11:
                FadeInOut.instance.GoToSceneFake(() =>
                    {
                        // 移动玩家位置
                        MainPlayer.instance.transform.position = new Vector3(42f, -12f, 0);
                        EnemyCreator.instance.LoadEnemyCretor("MoFaSenLin_5");
                        GlobalControl.instance.isBattle = true;
                        Map_Battle_Wuzhihu_1.instance.GetBubble(LoctionID);
                    }
                    );
                break;
            case 12:
                {
                    //UIControl.instance.ShowTips("前面没做了", Input.mousePosition);
                    FadeInOut.instance.GoToScene("BaiyuLou_1");
                    break;
                }
            case 13:
                FadeInOut.instance.GoToSceneFake(() =>
                    {
                        // 移动玩家位置
                        MainPlayer.instance.transform.position = new Vector3(1.8f, -2.1f, 0);
                        GlobalControl.instance.isBattle = true;
                        EnemyCreator.instance.LoadEnemyCretor("BaiyuLou_2");
                    }
                    );
                break;
            case 14:
                FadeInOut.instance.GoToSceneFake(() =>
                    {
                        // 移动玩家位置
                        MainPlayer.instance.transform.position = new Vector3(1.8f, -14f, 0);
                        GlobalControl.instance.isBattle = true;
                        TalkDialogLeidi.GetText();
                        EnemyCreator.instance.LoadEnemyCretor("BaiyuLou_3");
                    }
                    );
                break;
            case 15:
                FadeInOut.instance.GoToSceneFake(() =>
                    {
                        // 移动玩家位置
                        MainPlayer.instance.transform.position = new Vector3(1.5f, -25.6f, 0);
                        GlobalControl.instance.isBattle = true;
                        EnemyCreator.instance.LoadEnemyCretor("BaiyuLou_4");
                    }
                    );
                break;
            case 16:
                FadeInOut.instance.GoToSceneFake(() =>
                    {
                        // 移动玩家位置
                        MainPlayer.instance.transform.position = new Vector3(20.4f, 7.9f, 0);
                        GlobalControl.instance.isBattle = true;
                        EnemyCreator.instance.LoadEnemyCretor("BaiyuLou_5");
                    }
                    );
                break;
            case 17:
                FadeInOut.instance.GoToSceneFake(() =>
                    {
                        // 这里放妖梦
                        TalkDialogYoumu.GetText();
                        // 移动玩家位置
                        MainPlayer.instance.transform.position = new Vector3(20.3f, -4.6f, 0);
                        GlobalControl.instance.isBattle = true;
                        EnemyCreator.instance.LoadEnemyCretor("BaiyuLou_6");
                    }
                    );
                break;
            case 18:
                FadeInOut.instance.GoToSceneFake(() =>
                    {
                        // 移动玩家位置
                        MainPlayer.instance.transform.position = new Vector3(21f, -15f, 0);
                        GlobalControl.instance.isBattle = true;
                        EnemyCreator.instance.LoadEnemyCretor("BaiyuLou_7");
                    }
                    );
                break;
            case 19:
                FadeInOut.instance.GoToSceneFake(() =>
                    {
                        // 移动玩家位置
                        MainPlayer.instance.transform.position = new Vector3(20f, -27.8f, 0);
                        GlobalControl.instance.isBattle = true;
                        EnemyCreator.instance.LoadEnemyCretor("BaiyuLou_8");
                    }
                    );
                break;
            case 20:
                FadeInOut.instance.GoToSceneFake(() =>
                    {
                        // 移动玩家位置
                        MainPlayer.instance.transform.position = new Vector3(40f, 8f, 0);
                        GlobalControl.instance.isBattle = true;
                        EnemyCreator.instance.LoadEnemyCretor("BaiyuLou_9");
                    }
                    );
                break;
            case 21:
                FadeInOut.instance.GoToSceneFake(() =>
                    {
                        // 移动玩家位置
                        MainPlayer.instance.transform.position = new Vector3(40f, -4f, 0);
                        GlobalControl.instance.isBattle = true;
                        EnemyCreator.instance.LoadEnemyCretor("BaiyuLou_10");
                    }
                    );
                break;
            case 22:
                FadeInOut.instance.GoToSceneFake(() =>
                {

                    // 移动玩家位置
                    MainPlayer.instance.transform.position = new Vector3(22f, -1f, 0);
                    EnemyCreator.instance.LoadEnemyCretor("WuZhiHu_4");
                    TalkDialogCrino.GetText();
                    GlobalControl.instance.isBattle = true;
                    // Map_Battle_Wuzhihu_1.instance.GetBubble(LoctionID);

                }
                );
                break;
            case 23:
                FadeInOut.instance.GoToSceneFake(() =>
                    {
                        // 移动玩家位置
                        MainPlayer.instance.transform.position = new Vector3(42f, -12f, 0);
                        EnemyCreator.instance.LoadEnemyCretor("MoFaSenLin_4");
                        GlobalControl.instance.isBattle = true;
                        //Map_Battle_Wuzhihu_1.instance.GetBubble(LoctionID);
                    }
                    );
                break;
            default:
                Debug.Log("没做了喵");
                return false;
        }
        return true;
    }



}
