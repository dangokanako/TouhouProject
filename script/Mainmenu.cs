using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Mainmenu : MonoBehaviour
{
    public Button startButton;
    public Button quitButton;
    public Button tutorialButton;

    public string firstLevelName;
    public void Start()
    {
        SFXManger.instance.playBGM(3);

        startButton.interactable = true;
        quitButton.interactable = true;
        tutorialButton.interactable = true;
    }

    public void StartGame()
    {
        SetButtonFalse();
        // 场景载入
        FadeInOut.instance.GoToScene(firstLevelName);
    }

    public void StartGameWithTutorial()
    {
        SetButtonFalse();
        // 场景载入
        FadeInOut.instance.GoToScene("Tutorial");

    }


    public void QuitGame()
    {
        SetButtonFalse();
        Application.Quit();
        Debug.Log("游戏已关闭（迫真）");
    }

    private void SetButtonFalse()
    {
        startButton.interactable = false;
        quitButton.interactable = false;
        tutorialButton.interactable = false;
    }
}
