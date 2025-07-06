using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenStorePannel : MonoBehaviour
{
    [SerializeField] private OpenInvnetory openInvnetory;
    [SerializeField] private UIOpenDetail uiOpenDetail;

    // 혼란을 줄 수 있으므로 제거하거나 용도를 명확히 하는 것이 좋습니다.
    public bool isStoreOpen = false; // 이 변수가 상점 상태를 나타내는 주 변수입니다.
    
    public GameObject openStoreObject;

    private void Awake()
    {
        openStoreObject.SetActive(false);
    }
    
    public void OpenStore() // 이 메서드는 GetKeyDown(KeyCode.E)에 의해 호출됩니다.
    {
        // GetKeyDown은 Update에서만 제대로 작동합니다.
        // cheakReyCast에서 OpenStore()를 호출하는 방식은 GetKeyDown을 한 번 놓칠 수 있습니다.
        // 하지만 현재 로직에서는 E 키가 눌렸을 때만 내부 조건문이 실행되므로 큰 문제는 아닐 수 있습니다.
        // 다만, 더 견고하게 만들려면 cheakReyCast에서 E 키 입력을 처리하는 것이 좋습니다.
        if (Input.GetKeyDown(KeyCode.E)) 
        {
            if (!isStoreOpen) // 현재 isStoreOpen을 기준으로 상점 열기/닫기를 결정합니다.
            {
                openInvnetory.open(); // 인벤토리를 열고 닫는 로직을 활용
                openStoreObject.SetActive(true); // 상점 패널 활성화
                isStoreOpen = true; // 상점이 열렸음을 표시
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else // 상점이 이미 열려있는 경우 (닫기)
            {
                openInvnetory.open(); // 인벤토리를 닫는 로직을 활용 (커서 상태 등)
                openStoreObject.SetActive(false); // 상점 패널 비활성화
                isStoreOpen = false; // 상점이 닫혔음을 표시
                uiOpenDetail.CloseDetailPanel(); // 상세 패널도 닫기
            }
        }
    }
}