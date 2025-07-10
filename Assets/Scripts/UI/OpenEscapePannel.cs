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
    public static bool _isOpen = false;

    private void Awake()
    {
        EscapePannel.SetActive(false);
        AreUSure.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !_isOpen &&
        !InventoryPannel.activeSelf &&
        !BuyPannel.activeSelf && !SkillSelectPannel.activeSelf && !MenuBarPannel.activeSelf)
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
