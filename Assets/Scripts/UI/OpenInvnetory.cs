using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenInvnetory : MonoBehaviour
{
    [SerializeField] private UIOpenDetail uiOpenDetail;
    [SerializeField] private OpenStorePannel openStorePannel; // OpenStorePannel 참조 추가

    public GameObject _gameObject;
    public static bool _isOpen;

    public GameObject MenuBar; // 메뉴 바 오브젝트


    private void Awake()
    {
        _gameObject.SetActive(false);
        _isOpen = false;
    }

    private void Update()
    {
        if (!MenuBar.activeSelf && Input.GetKeyDown(KeyCode.Tab))
        {
            open();
        }
        if (!MenuBar.activeSelf && Input.GetKeyDown(KeyCode.Escape)
        && _isOpen)
        {
            close();
        }
    }

    public void open()
    {
        if (!_isOpen)
        {
            _gameObject.SetActive(true);
            _isOpen = true;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            uiOpenDetail.CloseDetailPanel();

            // 인벤토리를 열 때 상점 상태를 false로 강제 설정
            if (openStorePannel != null)
            {
                openStorePannel.isStoreOpen = false;
                openStorePannel.openStoreObject.SetActive(false); // 상점 오브젝트도 비활성화
            }
        }
        else
        {
            _gameObject.SetActive(false);
            _isOpen = false;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            uiOpenDetail.CloseDetailPanel();

        }
    }

    public void close()
    {
        _gameObject.SetActive(false);
        _isOpen = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        uiOpenDetail.CloseDetailPanel();

        Cursor.visible = false; // 커서 비활성화z
    }
}