using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Sirenix.OdinInspector;

public class Button_ExcapePannelButtonManager : MonoBehaviour
{
    [InfoBox("현재 InfoMation에 대한 버튼의 로직이 구현되지 않았습니다.")]
    public GameObject EscapePannel;

    public void OnclickBack()
    {
        EscapePannel.SetActive(false);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        OpenEscapePannel._isOpen = false;
    }

    public void OnclickSetting()
    {
        SceneManager.LoadScene("SettingScene");
    }

    public void OnclickGoToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
