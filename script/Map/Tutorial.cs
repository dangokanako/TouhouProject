using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MapDataControl
{
    // Start is called before the first frame update
    void Start()
    {
        // 不是，哥们，你怎么两个变量相冲，谁想的代码
        // TODO
        PlayerHealthControl.instance.OnPeace = true;
        GlobalControl.instance.isBattle = true;

        // 设置对话文本
        // TalkControl.instance.talkdata = TalkData_Tutorial.instance;

        // 不知道为什么要等一帧
        StartCoroutine(Idontknowwhy());

    }
    public IEnumerator Idontknowwhy()
    {
        yield return new WaitForSeconds(0.1f);
        TalkControl.instance.ShowText(0);
    }
}
