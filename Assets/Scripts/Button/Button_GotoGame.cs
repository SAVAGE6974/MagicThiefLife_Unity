using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button_GotoGame : MonoBehaviour
{
    public void OnclickGotoScene()
    {
        SceneManager.LoadScene("MainScene");
        PlayerPrefs.DeleteKey("PlayerMoney");
    }

    public void OnclickGotoLoadScene()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void OnclickQuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void OnclickSettingGame()
    {
        SceneManager.LoadScene("SettingScene");
    }
}
