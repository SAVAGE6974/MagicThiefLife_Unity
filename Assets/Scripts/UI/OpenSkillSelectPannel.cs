using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class OpenSkillSelectPannel : MonoBehaviour
{
    [Required] public GameObject skillUIPanel;
    [Required] public GameObject mainPannel;
    
    [Required] public static bool _isOpen;
    [Required] public static bool _isUse = false;
    private void Awake()
    {
        skillUIPanel.SetActive(false);
        mainPannel.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Q) && !_isOpen && !_isUse)
        {
            skillUIPanel.SetActive(true);
            mainPannel.SetActive(false);
            
            _isOpen = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else if(!Input.GetKey(KeyCode.Q) && _isOpen)
        {
            skillUIPanel.SetActive(false);
            mainPannel.SetActive(true);
            
            _isOpen = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        if (_isUse && Input.GetKey(KeyCode.Q))
        {
            // _isUse 변수를 다시 false로 바꿔서 코루틴이 반복해서 실행되지 않도록 합니다.
            _isUse = false; 
        }
    }
    
}