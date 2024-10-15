using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    void Awake()
    {
        instance = this;
    }
    private bool gameActive;
    public float timer;
    // Start is called before the first frame update
    void Start()
    {
        gameActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameActive)
        {
            // timer += Time.deltaTime;
            // UIControl.instance.UpdateTimer(timer);

            UIControl.instance.SetSkipButton();
        }
    }

    // 游戏结束
    public void EndLevel()
    {
        gameActive = false;
        AssetControl.instance.DropPoint(PlayerHealthControl.instance.transform.position, 20);
        StartCoroutine(EndLevelCo());
    }

    public IEnumerator EndLevelCo()
    {
        yield return new WaitForSeconds(1f);

        gameActive = false;

        float minute = Mathf.FloorToInt(timer / 60f);
        float seconds = Mathf.FloorToInt(timer % 60);

        //结算文字
        UIControl.instance.gameoverPanel.SetActive(true);
        float superpoint = 0;
        string text = "满身疮痍\n" + "结算：（假装有动画）\n";
        text += "时长：" + minute + "分" + seconds.ToString("00") + "秒——获得" + minute * 0.5 + "超级P点" + "\n";
        superpoint += minute * 0.5f;
        text += "获得" + GlobalControl.instance.currentTotalPoint + "P点——获得" + GlobalControl.instance.currentTotalPoint * 0.1 + "超级P点" + "\n";
        superpoint += GlobalControl.instance.currentTotalPoint * 0.1f;
        text += "获得" + GlobalControl.instance.currentTotalExp + "经验——获得" + GlobalControl.instance.currentTotalExp * 0.1 + "超级P点" + "\n";
        superpoint += GlobalControl.instance.currentTotalExp * 0.1f;
        text += "总计" + GlobalControl.instance.currentTotalKill + "击杀——获得" + GlobalControl.instance.currentTotalKill * 0.05 + "超级P点" + "\n";
        superpoint += GlobalControl.instance.currentTotalKill * 0.05f;
        UIControl.instance.gameoverText.text = text;
        // 增加超级P点
        AssetControl.instance.AddSuperPoint(Mathf.FloorToInt(superpoint));
        // 减少普通P点
        AssetControl.instance.ReducePoint(Mathf.FloorToInt(AssetControl.instance.currentPoint * GlobalControl.instance.deathLoss));

        // 重置GlobalControl
        GlobalControl.instance.totalExp += GlobalControl.instance.currentTotalExp;
        GlobalControl.instance.totalKill += GlobalControl.instance.currentTotalKill;
        GlobalControl.instance.totalPoint += GlobalControl.instance.currentTotalPoint;

        GlobalControl.instance.currentTotalExp = 0;
        GlobalControl.instance.currentTotalKill = 0;
        GlobalControl.instance.currentTotalPoint = 0;
    }
}
