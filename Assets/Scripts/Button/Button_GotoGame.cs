using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button_GotoGame : MonoBehaviour
{
    public void OnclickGotoScene()
    {
        SceneManager.LoadScene("MainScene");
        PlayerPrefs.DeleteAll();
    }

    public void OnclickGotoLoadScene()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void OnclickQuitGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }

    public void OnclickSettingGame()
    {
        SceneManager.LoadScene("SettingScene");
    }
}
