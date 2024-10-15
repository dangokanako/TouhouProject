using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingControl : MonoBehaviour
{
    public Toggle toggle;
    // 设置分辨率
    public void OnResolutionChange1()
    {
        Screen.SetResolution(1280, 720, toggle.isOn);
    }
    public void OnResolutionChange2()
    {
        Screen.SetResolution(1600, 900, toggle.isOn);
    }
    public void OnResolutionChange3()
    {
        Screen.SetResolution(1920, 1080, toggle.isOn);
    }
    public void OnResolutionChange4()
    {
        Screen.SetResolution(2560, 1440, toggle.isOn);
    }
    // 设置是否全屏
    public void OnFullScreenChange()
    {
        Screen.fullScreen = toggle.isOn;
    }
}
