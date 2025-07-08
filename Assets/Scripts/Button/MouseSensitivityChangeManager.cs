using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class MouseSensitivityChangeManager : MonoBehaviour
{
    private float mouseSensitivity;


    [InfoBox("해당 스크립트는 +와 -의 버튼을 눌러 조절하거나, -와 +키를 눌러 마우스의 감도를 조정하기위한 스크립트입니다. 컴포넌트는 꼭 넣으셔야 합니다.")]
    [Required] public Text mouseSensitivityText;
    // [Required] public Text SceneMouseSensitivityText; 
    // 추가예정

    private void Awake()
    {
        mouseSensitivity = PlayerPrefs.GetFloat("MouseSensitivity", 1000f);
        PlayermoveMent.mouseSensitivity = mouseSensitivity;
    }

    private void Start()
    {
        mouseSensitivityText.text = PlayermoveMent.mouseSensitivity.ToString();
    }

    public void OnclickPlus()
    {
        PlayermoveMent.mouseSensitivity += 100f;
        PlayerPrefs.SetFloat("MouseSensitivity", PlayermoveMent.mouseSensitivity);
        PlayerPrefs.Save();
        mouseSensitivityText.text = PlayermoveMent.mouseSensitivity.ToString();
    }

    public void OnclickMinrse()
    {
        PlayermoveMent.mouseSensitivity -= 100f;
        PlayerPrefs.SetFloat("MouseSensitivity", PlayermoveMent.mouseSensitivity);
        PlayerPrefs.Save();
        mouseSensitivityText.text = PlayermoveMent.mouseSensitivity.ToString();
    }
}
