using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class BubbleTalkControl : MonoBehaviour
{
    public static BubbleTalkControl instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private Queue<GameObject> bubbleTalkQueue = new Queue<GameObject>(); // 存储所有气泡对话的队列
    private Queue<BubbleTalkData> textQueue = new Queue<BubbleTalkData>(); // 存储待处理的文本

    private bool isProcessing = false; // 是否正在处理文本

    public GameObject BubbleTalkUI;
    public CanvasGroup canvasGroup;
    private int AddCounter;

    public void ShowBubbleTalk(BubbleTalkData text)
    {
        // 将文本添加到队列中
        textQueue.Enqueue(text);

        // 如果当前没有正在处理文本，那么就开始处理队列中的文本
        if (!isProcessing)
        {
            StartCoroutine(ProcessTextQueue());
        }
    }

    public void ShowBubbleTalkButWaitingFormouse(BubbleTalkData text)
    {
        // 等待0.1s添加，否则按键会直接显示对话。不太想用这个方案，少一个协程是一个。
        // StartCoroutine(WaitForMouse(text));

        // // 将文本添加到队列中
        AddCounter = 0;
        textQueue.Enqueue(text);
    }


    void Update()
    {
        // if (TalkControl.sentence > 0)
        // {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown((int)MouseButton.Left))
        {
            if (!isProcessing)
            {
                AddCounter++;
                if (AddCounter == 2)
                    StartCoroutine(ProcessTextQueue());
            }
        }
        // }
    }


    private IEnumerator ProcessTextQueue()
    {
        isProcessing = true;

        while (textQueue.Count > 0)
        {
            // 从队列中取出一个文本并处理它
            BubbleTalkData text = textQueue.Dequeue();
            ShowBubbleTalkInternal(text);

            // 等待一秒
            yield return new WaitForSeconds(2);
        }

        isProcessing = false;
    }

    public void ShowBubbleTalkInternal(BubbleTalkData text)
    {
        // 初始化一个BubbleTalkUI对象
        var temp = Instantiate(BubbleTalkUI, this.transform.position, Quaternion.identity);
        temp.transform.SetParent(this.transform);
        temp.SetActive(true);

        var canvasGroup = temp.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        canvasGroup.DOFade(1, 0.5f);

        temp.GetComponentInChildren<TMPro.TMP_Text>().text = text.text;

        var smallhead = temp.GetComponentsInChildren<UnityEngine.UI.Image>();
        smallhead[0].sprite = Resources.Load<Sprite>("smallHead/" + text.smallhead);
        smallhead[1].sprite = Resources.Load<Sprite>("UI/" + GetDialog(text.smallhead));

        // temp.GetComponentInChildren<UnityEngine.UI.Image>().sprite = Resources.Load<Sprite>("smallHead/" + text.smallhead);

        // 将新的气泡对话添加到队列
        bubbleTalkQueue.Enqueue(temp);

        // // 移动队列中的所有气泡对话
        // foreach (var bubbleTalk in bubbleTalkQueue)
        // {
        //     bubbleTalk.transform.DOLocalMoveY(bubbleTalk.transform.localPosition.y + 150, 1);
        // }

        // 渐隐新的气泡对话，并在完成后销毁它
        canvasGroup.DOFade(0, 1.3f).SetDelay(2.3f + text.text.Length * 0.05f).OnComplete(() =>
        {
            // 从队列中移除并销毁气泡对话
            var removedBubbleTalk = bubbleTalkQueue.Dequeue();
            DOTween.Kill(removedBubbleTalk); // 停止所有与 removedBubbleTalk 相关的 DOTween 动画
            Destroy(removedBubbleTalk);
        });
    }

    public string GetDialog(string name)
    {
        if (name.Contains("reimu"))
        {
            return "对话框_灵梦";
        }
        else if (name.Contains("marisa"))
        {
            return "对话框_魔理沙";
        }
        return "对话框_灵梦";
    }
}

public struct BubbleTalkData
{
    public string smallhead;
    public string text;

    public BubbleTalkData(string text1, string text2)
    {
        smallhead = text1;
        text = text2;
    }
}