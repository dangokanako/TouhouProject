using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CameraControl : MonoBehaviour
{

    public static CameraControl instance;
    void Awake()
    {
        if (instance == null)
        {
            // 如果没有实例，那么就将这个对象设置为实例
            instance = this;
        }
        else if (instance != this)
        {
            // 如果已经有一个实例，并且这个实例不是这个对象，那么就销毁这个对象
            Destroy(gameObject);
        }
    }


    /// <summary>
    /// 摇晃屏幕
    /// </summary>
    /// <param name="time">摇晃时间</param>
    /// <param name="strength">摇晃强度，0.2左右就差不多</param>
    public void DoShake(float time, float strength)
    {
        CameraControl.instance.transform.DOShakePosition(time, strength, fadeOut: true);
        CameraFather.instance.LockTargetAgain();
    }
}
