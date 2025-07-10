using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenInvnetory : MonoBehaviour
{
    [SerializeField] private UIOpenDetail uiOpenDetail;
    [SerializeField] private OpenStorePannel openStorePannel; // 상점 패널 참조

    [InfoBox("_isOpenEquipment를 이용하여 디테일패널 변경할 예정")]

    public GameObject _gameObject;          // 인벤토리 패널
    public static bool _isOpen;
    public static bool _isOpenEquipment = false;
    public GameObject invnetDetailPannel;
    public GameObject equipmentDetailPannel;
    public GameObject equipmentmenuBarPannel;

    public GameObject MenuBar;              // 메뉴 바 (게임 전체 UI 바 등)
    public GameObject inventoryMenuBar;     // 인벤토리 상단 바 UI
    public GameObject inventoryUnderBar;

    // --- 새로 추가: 인벤토리가 Escape 키로 방금 닫혔는지 여부를 나타내는 플래그 ---
    public static bool _justClosedByEscape = false; 

    private void Awake()
    {
        _gameObject.SetActive(false);
        _isOpen = false;
        inventoryMenuBar.SetActive(false);
        inventoryUnderBar.SetActive(false);
        equipmentDetailPannel.SetActive(false);
        equipmentmenuBarPannel.SetActive(false);
    }

    private void Update()
    {
        if (!MenuBar.activeSelf && Input.GetKeyDown(KeyCode.Tab))
        {
            open();
        }

        if (!MenuBar.activeSelf && Input.GetKeyDown(KeyCode.Escape) && _isOpen)
        {
            close();
            // --- 새로 추가: Escape로 닫았을 때 플래그를 true로 설정 ---
            _justClosedByEscape = true; 
            // 이 플래그는 OpenEscapePannel에서 확인 후 바로 초기화될 것입니다.
        }
    }

    // 인벤토리 열기
    public void open()
    {
        // 인벤토리 열 때 Equipment 관련 오브젝트는 숨김
        GameObject[] rootObjects = SceneManager.GetActiveScene().GetRootGameObjects();
        foreach (GameObject obj in rootObjects)
        {
            DeactivateEquipmentObjects(obj);
        }

        if (!_isOpen)
        {
            _gameObject.SetActive(true);
            _isOpen = true;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            uiOpenDetail.CloseDetailPanel();

            inventoryMenuBar.SetActive(true);

            // 상점 끄기
            if (openStorePannel != null)
            {
                openStorePannel.isStoreOpen = false;
                openStorePannel.openStoreObject.SetActive(false);
            }
            // --- 새로 추가: 인벤토리가 열릴 때는 이 플래그를 false로 초기화 (바로 Escape를 누를 상황이 아니므로) ---
            _justClosedByEscape = false; 
        }
        else
        {
            _gameObject.SetActive(false);
            _isOpen = false;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            uiOpenDetail.CloseDetailPanel();

            inventoryMenuBar.SetActive(false);

            equipmentDetailPannel.SetActive(false);
            equipmentmenuBarPannel.SetActive(false);
        }
    }

    // 인벤토리 닫기
    public void close()
    {
        _gameObject.SetActive(false);
        _isOpen = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        uiOpenDetail.CloseDetailPanel();

        inventoryMenuBar.SetActive(false);
        equipmentDetailPannel.SetActive(false);
        equipmentmenuBarPannel.SetActive(false);
    }

    // "Equipment"라는 이름이 포함된 오브젝트만 비활성화
    private void DeactivateEquipmentObjects(GameObject obj)
    {
        if (obj.name.Contains("Equipment"))
        {
            obj.SetActive(false);
        }

        foreach (Transform child in obj.transform)
        {
            DeactivateEquipmentObjects(child.gameObject);
        }
    }

    // ▶ 버튼에 연결할 함수: Equipment 보기 (Item 숨기고, Equipment 보이기)
    public void OnclickEquipmentBtn()
    {
        GameObject[] rootObjects = SceneManager.GetActiveScene().GetRootGameObjects();
        foreach (GameObject obj in rootObjects)
        {
            ToggleItemAndEquipment(obj);
        }
        inventoryUnderBar.SetActive(true);

        _isOpenEquipment = true;
        invnetDetailPannel.SetActive(false);
    }

    // Equipment는 활성화, Item은 비활성화
    private void ToggleItemAndEquipment(GameObject obj)
    {
        if (obj.name.Contains("Equipment"))
        {
            obj.SetActive(true); // Equipment 켜기
        }
        else if (obj.name.Contains("Item"))
        {
            obj.SetActive(false); // Item 끄기
        }

        foreach (Transform child in obj.transform)
        {
            ToggleItemAndEquipment(child.gameObject);
        }
    }

    // ▶ 버튼에 연결할 함수: Item 보기 (Equipment 숨기고, Item 보이기)
    public void OnclickItemBtn()
    {
        GameObject[] rootObjects = SceneManager.GetActiveScene().GetRootGameObjects();
        foreach (GameObject obj in rootObjects)
        {
            ToggleEquipmentAndItem(obj);
        }
        inventoryUnderBar.SetActive(false);

        _isOpenEquipment = false;

        equipmentDetailPannel.SetActive(false);
    }

    // Item은 활성화, Equipment는 비활성화
    private void ToggleEquipmentAndItem(GameObject obj)
    {
        if (obj.name.Contains("Item"))
        {
            obj.SetActive(true); // Item 켜기
        }
        if (obj.name.Contains("Equipment"))
        {
            obj.SetActive(false); // Equipment 끄기
        }

        foreach (Transform child in obj.transform)
        {
            ToggleEquipmentAndItem(child.gameObject);
        }
    }
}