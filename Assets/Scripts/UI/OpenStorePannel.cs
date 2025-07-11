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
    public GameObject menuBar;
    public GameObject inventoryMeunuBar;
    public GameObject BuyPannelDetail; // 구매 패널 상세 정보

    private void Awake()
    {
        menuBar.SetActive(false);
    }

    public void OpenStore() // 이 메서드는 GetKeyDown(KeyCode.E)에 의해 호출됩니다.
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!isStoreOpen) 
            {
                // 상점 열기
                openInvnetory.open(); // 인벤토리를 닫거나 열어서 UI 상태를 맞춤
                openStoreObject.SetActive(true); 
                isStoreOpen = true; 
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                menuBar.SetActive(true);
                inventoryMeunuBar.SetActive(false);
                // 상점이 열릴 때는 Escape 닫힘 플래그를 false로 초기화
                OpenInvnetory._justClosedByEscape = false; 
            }
            else 
            {
                // 상점 닫기
                openInvnetory.open(); // 인벤토리를 닫거나 열어서 UI 상태를 맞춤
                openStoreObject.SetActive(false); 
                isStoreOpen = false; 
                uiOpenDetail.CloseDetailPanel(); 
                menuBar.SetActive(false); 
                BuyPannelDetail.SetActive(false); 
                // 상점이 닫힐 때는 Escape 닫힘 플래그를 false로 초기화 (E키로 닫았으므로)
                OpenInvnetory._justClosedByEscape = false;
            }
        }

        // Escape 키를 눌러 상점을 닫는 로직
        if (Input.GetKeyDown(KeyCode.Escape) && isStoreOpen)
        {
            openInvnetory.close(); // 인벤토리를 닫는 로직을 활용 (커서 등)
            openStoreObject.SetActive(false); 
            isStoreOpen = false; 
            uiOpenDetail.CloseDetailPanel(); 
            menuBar.SetActive(false); 
            BuyPannelDetail.SetActive(false); 

            Cursor.visible = false; 
            Cursor.lockState = CursorLockMode.Locked; 
            
            // --- 중요: 상점이 Escape 키로 닫혔음을 알리는 플래그 설정 ---
            OpenInvnetory._justClosedByEscape = true;
        }
    }
}