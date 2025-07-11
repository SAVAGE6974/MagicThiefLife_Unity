using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class OpenEscapePannel : MonoBehaviour
{
    [FoldoutGroup("Pannels")] public GameObject InventoryPannel;
    [FoldoutGroup("Pannels")] public GameObject BuyPannel;
    [FoldoutGroup("Pannels")] public GameObject SkillSelectPannel;
    [FoldoutGroup("Pannels")] public GameObject MenuBarPannel;

    public GameObject AreUSure;

    public GameObject EscapePannel;
    public static bool _isOpen;

    private void Awake()
    {
        EscapePannel.SetActive(false);
        AreUSure.SetActive(false);
    }

    private bool HasAnyActiveChild(GameObject parent)
    {
        foreach (Transform child in parent.transform)
        {
            if (child.gameObject.activeInHierarchy) return true;
            if (HasAnyActiveChild(child.gameObject)) return true;
        }
        return false;
    }


    private void Update()
    {
        // --- 새로 추가: 인벤토리가 Escape 키로 방금 닫혔는지 확인 ---
        if (OpenInvnetory._justClosedByEscape)
        {
            // 확인 후 바로 플래그를 false로 초기화
            OpenInvnetory._justClosedByEscape = false;
            return; // 이 프레임에서는 Escape 메뉴를 열지 않고 Update 메서드를 종료합니다.
        }

        // 인벤토리가 열려 있으면 Escape 메뉴는 열지 않음
        if (OpenInvnetory._isOpen) return;

        if (Input.GetKeyDown(KeyCode.Escape) && !_isOpen &&
            !InventoryPannel.activeInHierarchy &&
            !BuyPannel.activeInHierarchy &&
            !SkillSelectPannel.activeInHierarchy &&
            !MenuBarPannel.activeInHierarchy)
        {
            EscapePannel.SetActive(true);

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            _isOpen = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && _isOpen)
        {
            EscapePannel.SetActive(false);

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            _isOpen = false;
        }
    }
}