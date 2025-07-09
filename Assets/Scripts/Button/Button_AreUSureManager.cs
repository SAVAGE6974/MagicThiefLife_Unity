using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button_AreUSureManager : MonoBehaviour
{
    public GameObject AreUSurePannel;

    public void OnclickYes()
    {
        if (Button_ExcapePannelButtonManager.whatIsPannel == "Setting")
        {
            SceneManager.LoadScene("SettingScene");
            Button_ExcapePannelButtonManager.whatIsPannel = "";
            PlayerPrefs.Save();
        }
        else if (Button_ExcapePannelButtonManager.whatIsPannel == "GoToMenu")
        {
            SceneManager.LoadScene("MainMenu");
            Button_ExcapePannelButtonManager.whatIsPannel = "";
            PlayerPrefs.Save();
        }
    }

    public void OnclickCansel()
    {
        AreUSurePannel.SetActive(false);
        Button_ExcapePannelButtonManager.whatIsPannel = "";
    }
}
