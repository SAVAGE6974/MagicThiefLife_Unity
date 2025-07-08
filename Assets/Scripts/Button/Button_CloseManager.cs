using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button_CloseManager : MonoBehaviour
{
    public void OnclickGotoMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
