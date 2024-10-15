using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
// using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using DG.Tweening;
using System.Security.Cryptography;
using System;
public class TalkControl : MonoBehaviour
{
    public static TalkControl instance;
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
        OriginLeftImagePostion = left_image.transform.position;
        OriginRightImagePostion = right_image.transform.position;
    }
    public GameObject TalkUI;
    public UnityEngine.UI.Image left_image;
    public UnityEngine.UI.Image right_image;
    public TMP_Text left_person_name;
    public UnityEngine.UI.Image left_person_name_content;
    public TMP_Text right_person_name;
    public UnityEngine.UI.Image right_person_name_content;
    public TMP_Text main_text;
    public Color emptyTempColor;
    public Color LeftTempColor;
    public Color RightTempColor;
    // 数据来源
    public TalkData talkdata;
    // 记录第几句
    [SerializeField] public int paragraph;
    [SerializeField] public static int sentence;
    // 记录是否正在播放文字
    [SerializeField] public bool isPlayingText = false;
    // 文本动画系统
    private Tween textTween;
    // 文本强制等待时间
    private float waitTime;
    // 左右立绘的原始位置
    private Vector2 OriginLeftImagePostion;
    private Vector2 OriginRightImagePostion;

    void Update()
    {
        // 强制等待时间
        if (waitTime > 0)
        {
            waitTime -= Time.deltaTime;
            return;
        }

        // 只有已开始对话，才继续显示
        if (sentence > 0)
        {
            // 翻看历史记录时不推进对话
            if (BacklogControl.instance.isOpenBacklog)
                return;

            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown((int)MouseButton.Left) || Input.GetKey(KeyCode.LeftControl))
            {
                if (sentence >= talkdata.TalkPiece[paragraph].Length)
                {
                    sentence = -1;
                    TalkUI.SetActive(false);
                    StartCoroutine(SetOntalkDelay());
                    return;
                }

                // 如果正在播放，那么应该显示上一句文本
                if (instance.isPlayingText)
                    sentence--;

                waitTime = 0.08f;

                ShowTalkPiece(talkdata.TalkPiece[paragraph][sentence]);

                sentence++;

            }
        }
    }


    // 显示文本的入口
    public void ShowText(int _paragraph)
    {
        if (waitTime > 0)
            return;

        if (UIControl.instance.levelupPanel.activeSelf)
            return;

        paragraph = _paragraph;
        // 第一次显示，sentence设为0
        if (!TalkUI.activeSelf)
        {
            PlayerHealthControl.instance.OnTalk = true;
            // 笑死，速度居然可以不是0的
            MainPlayer.instance.rb.velocity = Vector2.zero;
            TalkUI.SetActive(true);
            sentence = 0;
        }
        ShowTalkPiece(talkdata.TalkPiece[paragraph][sentence]);
        waitTime = 0.2f;

        StartCoroutine(waitsentence());
    }
    IEnumerator waitsentence()
    {
        // 直接增加sentence会导致update内检测到输入，直接跳到第二句。所以等一下
        yield return new WaitForSeconds(0.1f);
        sentence++;
    }
    // 主要文本处理
    void ShowTalkPiece(talkPiece data)
    {
        // 停止旧的动画
        if (textTween != null)
        {
            textTween.Kill();
        }

        main_text.text = "";
        // 主文本内容
        // 如果正在播放文本，停止播放，之后立即显示全部文本
        if (instance.isPlayingText)
        {
            isPlayingText = false;
            main_text.text = data.main_text_content;
            return;
        }
        else
        {
            instance.isPlayingText = true;
            textTween = main_text.DOText(data.main_text_content, data.main_text_content.Length * 0.03f).SetUpdate(true);
            // 在播放时间后设置为false
            StartCoroutine(SetIsplayingText(data.main_text_content.Length * 0.02f, false));
        }



        // 载入临时颜色
        if (data.left_person_color_string != emptyTempColor)
            LeftTempColor = data.left_person_color_string;
        if (data.right_person_color_string != emptyTempColor)
            RightTempColor = data.right_person_color_string;

        // 显示立绘
        // 关于立绘，我想加入说话的立绘会靠前，不说话的立绘会缩小，并且黯淡处理
        if (!string.IsNullOrEmpty(data.left_image_string))
        {
            left_image.sprite = Resources.Load<Sprite>("lihui/" + data.left_image_string);
        }
        if (!string.IsNullOrEmpty(data.right_image_string))
            right_image.sprite = Resources.Load<Sprite>("lihui/" + data.right_image_string);




        if (data.main_tenxt_content_color == emptyTempColor)
        {
            if (data.IsLeft == (int)ShowName.Left)
                main_text.color = LeftTempColor;
            if (data.IsLeft == (int)ShowName.Right)
                main_text.color = RightTempColor;
        }

        // 名字框的颜色文本设置
        if (!string.IsNullOrEmpty(data.left_person_name_string))
            left_person_name.text = data.left_person_name_string;
        if (data.left_person_color_string == emptyTempColor)
            data.left_person_color_string = LeftTempColor;
        else
        {
            left_person_name_content.color = data.left_person_color_string;
            left_person_name.color = Color.white;
        }

        // 根据名字长度调整content的长度
        if (data.left_person_name_string == null)
        {

        }
        else if (data.left_person_name_string.Length < 4)
            left_person_name_content.rectTransform.sizeDelta = new Vector2(212, 76);
        else
            left_person_name_content.rectTransform.sizeDelta = new Vector2(212 + (data.left_person_name_string.Length - 4) * 35, 76);

        if (data.right_person_name_string == null)
        {
        }
        else if (data.right_person_name_string.Length < 4)
            right_person_name_content.rectTransform.sizeDelta = new Vector2(212, 76);
        else
            right_person_name_content.rectTransform.sizeDelta = new Vector2(212 + (data.right_person_name_string.Length - 4) * 35, 76);

        // 根据是否显示角色调整left_person_name_content是否显示
        if (data.left_image_string == "不显示")
            left_person_name_content.gameObject.SetActive(false);
        else
            left_person_name_content.gameObject.SetActive(true);


        if (data.right_image_string == "不显示")
            right_person_name_content.gameObject.SetActive(false);
        else
            right_person_name_content.gameObject.SetActive(true);


        if (!string.IsNullOrEmpty(data.right_person_name_string))
            right_person_name.text = data.right_person_name_string;

        if (data.right_person_color_string == emptyTempColor)
            data.right_person_color_string = RightTempColor;
        else
        {
            right_person_name_content.color = data.right_person_color_string;
            right_person_name.color = Color.white;
        }

        // 文本方向和颜色调整
        if (data.IsLeft == (int)ShowName.Left)
        {
            main_text.alignment = TextAlignmentOptions.Left;
            main_text.color = left_person_name_content.color;
        }
        else if (data.IsLeft == (int)ShowName.Right)
        {
            // main_text.alignment = TextAlignmentOptions.Right;
            main_text.alignment = TextAlignmentOptions.Left;
            main_text.color = right_person_name_content.color;
        }

        // 数据里的颜色优先级最高
        if (data.main_tenxt_content_color != emptyTempColor)
            main_text.color = data.main_tenxt_content_color;


        DOTween.Kill(right_image.transform);
        DOTween.Kill(left_image.transform);
        // 图片对比度设置，没有说话的角色对比度降低
        if (data.IsLeft == (int)ShowName.Left)
        {
            right_image.color = CharecterColor.Unactive;
            right_image.transform.DOMove(new Vector3(OriginRightImagePostion.x + 60f, OriginRightImagePostion.y - 15f), 0.2f);
            right_image.transform.DOScale(new Vector2(0.98f, 0.98f), 0.2f);


            left_image.color = CharecterColor.Active;
            left_image.transform.DOMove(new Vector3(OriginLeftImagePostion.x, OriginLeftImagePostion.y), 0.2f);
            left_image.transform.DOScale(new Vector2(1.03f, 1.03f), 0.2f);
            StartCoroutine(SetImageDelay(left_image));
        }
        else if (data.IsLeft == (int)ShowName.Right)
        {
            left_image.color = CharecterColor.Unactive;
            left_image.transform.DOMove(new Vector3(OriginLeftImagePostion.x - 60f, OriginLeftImagePostion.y - 15f), 0.2f);
            left_image.transform.DOScale(new Vector2(0.98f, 0.98f), 0.2f);


            right_image.color = CharecterColor.Active;
            right_image.transform.DOMove(new Vector3(OriginRightImagePostion.x, OriginRightImagePostion.y), 0.2f);
            right_image.transform.DOScale(new Vector2(1.03f, 1.03f), 0.2f);
            StartCoroutine(SetImageDelay(right_image));
        }

        // 不显示的话，颜色直接设置为透明。由于是一个变量，优先度要高于对比度设置。
        if (data.left_image_string == "不显示")
        {
            left_image.color = emptyTempColor;
        }

        if (data.right_image_string == "不显示")
        {
            right_image.color = emptyTempColor;
        }


        if (data.PlayAudio > 0)
        {
            // 播放音效
        }

        // 向回放里添加数据
        BacklogControl.instance.AddBacklogData(new BacklogData(data.IsLeft == (int)ShowName.Left ? left_person_name.text : right_person_name.text, data.main_text_content));


        // 执行函数
        if (data != null)
        {
            data.Execute();

            // 当你删除一行意义不明的代码时功能就不能运行了，就是这句：
            waitTime = 0f;
        }
    }

    public IEnumerator SetIsplayingText(float waittime, bool _isPlayingText)
    {
        yield return new WaitForSeconds(waittime);
        instance.isPlayingText = _isPlayingText;
    }
    public IEnumerator SetOntalkDelay()
    {
        // 防止对话结束之后再次进入对话
        yield return new WaitForSeconds(0.05f);
        PlayerHealthControl.instance.OnTalk = false;
    }

    private IEnumerator SetImageDelay(UnityEngine.UI.Image image)
    {
        yield return new WaitForSeconds(0.1f);
        image.transform.DOScale(new Vector2(1f, 1f), 0.1f);
        yield break;
    }

}
