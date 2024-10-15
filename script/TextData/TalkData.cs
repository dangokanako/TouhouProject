using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TalkData : MonoBehaviour
{
    // 请在子类中重写
    public virtual talkPiece[][] TalkPiece { get; set; }
}



[Serializable]
public class talkPiece
{
    // 对话中调用函数的委托系统
    // 一个委托类型，描述需要执行的函数的签名
    public delegate void ExecuteFunction();

    // 委托字段，用于存储需要执行的函数
    public ExecuteFunction OnExecute;
    // 在适当的时机调用委托执行函数
    public void Execute()
    {
        // 检查委托是否存在，并调用委托执行函数
        OnExecute?.Invoke();

        // 清空委托
        // OnExecute = null;
    }

    public talkPiece()
    {

    }
    public talkPiece(string _left_image_string, string _right_image_string, string _left_person_name_string, string _right_person_name_string, Color _left_person_color_string, Color _right_person_color_string, int _IsLeft, string _main_text_content, Color _main_tenxt_content_color
    )
    {
        left_image_string = _left_image_string;
        right_image_string = _right_image_string;
        left_person_name_string = _left_person_name_string;
        right_person_name_string = _right_person_name_string;
        left_person_color_string = _left_person_color_string;
        right_person_color_string = _right_person_color_string;
        IsLeft = _IsLeft;
        main_text_content = _main_text_content;
        main_tenxt_content_color = _main_tenxt_content_color;
        PlayAudio = -1;
    }
    public talkPiece(string _left_image_string, string _right_image_string, string _left_person_name_string, string _right_person_name_string, Color _left_person_color_string, Color _right_person_color_string, int _IsLeft, string _main_text_content, Color _main_tenxt_content_color, ExecuteFunction func
    )
    {
        left_image_string = _left_image_string;
        right_image_string = _right_image_string;
        left_person_name_string = _left_person_name_string;
        right_person_name_string = _right_person_name_string;
        left_person_color_string = _left_person_color_string;
        right_person_color_string = _right_person_color_string;
        IsLeft = _IsLeft;
        main_text_content = _main_text_content;
        main_tenxt_content_color = _main_tenxt_content_color;
        PlayAudio = -1;
        OnExecute += func;
    }

    // 一种简化构造，只需要输入左还是右，以及中央文字，其他在处理时会自动继承上文习惯
    public talkPiece(int _IsLeft, string _main_text_content
    )
    {
        IsLeft = _IsLeft;
        main_text_content = _main_text_content;
        PlayAudio = -1;
    }
    public talkPiece(int _IsLeft, string _main_text_content, ExecuteFunction func)
    {
        IsLeft = _IsLeft;
        main_text_content = _main_text_content;
        PlayAudio = -1;
        OnExecute += func;
    }
    // 一种简化构造，只需要输入左还是右，以及中央文字，以及左右立绘，其他在处理时会自动继承上文习惯
    public talkPiece(string _left_image_string, string _right_image_string, int _IsLeft, string _main_text_content
    )
    {
        IsLeft = _IsLeft;
        main_text_content = _main_text_content;
        left_image_string = _left_image_string;
        right_image_string = _right_image_string;
        PlayAudio = -1;
    }
    public talkPiece(string _left_image_string, string _right_image_string, int _IsLeft, string _main_text_content, ExecuteFunction func
)
    {
        IsLeft = _IsLeft;
        main_text_content = _main_text_content;
        left_image_string = _left_image_string;
        right_image_string = _right_image_string;
        PlayAudio = -1;
        OnExecute += func;
    }
    // 左立绘图片
    public string left_image_string;
    // 右立绘图片
    public string right_image_string;
    // 左角色名字
    public string left_person_name_string;
    // 右角色名字
    public string right_person_name_string;
    // 左角色颜色
    public Color left_person_color_string;
    // 右角色颜色
    public Color right_person_color_string;
    // 文本左对齐还是右对齐。1为左对齐,0为右对齐。其他保留
    public int IsLeft;
    // 文本内容
    public string main_text_content;
    // 文本颜色
    public Color main_tenxt_content_color;
    // 播放音效
    public int PlayAudio;
    // 音效在文本前还是文本后播放。true为文本前
    public bool IsAudioBehind;



}



namespace UnityEngine
{
    // 不好用，别用了
    public enum PatchouliText
    {
        FirstTalk = 0,
        SecondTalk = 3,
    }
    public enum MeirinText
    {
        FirstTalk = 1,
        SecondTalk = 2,
    }
    public enum ReimiliaText
    {
        FirstTalk = 4,
        SecondTalk = 5,
    }
    public enum SakuyaText
    {
        FirstTalk = 6,
        SecondTalk = 7,
        ThirdTalk = 8,
    }
    public enum FlandreText
    {
        FirstTalk = 10,
        SecondTalk = 11,
    }
    public enum OtherText
    {
        EnterText = 9,
        ChufaText = 12,
    }

    // 好用，用
    public enum ShowName
    {
        Left = 1,
        Right = 0,
        All = -1
    }
    public class CharecterColor
    {
        public static readonly Color Unactive = new Color(100f / 255f, 100f / 255f, 100f / 255f, 255f / 255f);
        public static readonly Color Active = new Color(1, 1, 1, 1);
        public static readonly Color Patchouli = new Color(151f / 255f, 0, 100f / 255f, 1);
        public static readonly Color Meirin = new Color(182f / 255f, 76f / 255f, 37f / 255f, 255f / 255f);
        public static readonly Color Reimu = new Color(200f / 255f, 42f / 255f, 51f / 255f, 255f / 255f);
        public static readonly Color Reimilia = new Color(1, 0, 0, 1);
        public static readonly Color Sakuya = new Color(14f / 255f, 142f / 255f, 255f / 255f, 1);
        public static readonly Color Flandre = new Color(255f / 255f, 179f / 255f, 0, 1);
        public static readonly Color Marisa = new Color(242f / 255f, 193f / 83f, 0, 1);

        public static readonly Color Keine = new Color(151f / 255f, 221f / 255f, 1, 1);
        public static readonly Color Yukari = new Color(169f / 255f, 146f / 255f, 218 / 255f, 1);
        public static readonly Color Nitori = new Color(136f / 255f, 207f / 255f, 127 / 255f, 1);
        public static readonly Color Crino = new Color(160f / 255f, 224f / 255f, 245f / 255f, 1);
        public static readonly Color Youmu = new Color(64f / 255f, 154f / 255f, 136f / 255f, 1);
    }
}