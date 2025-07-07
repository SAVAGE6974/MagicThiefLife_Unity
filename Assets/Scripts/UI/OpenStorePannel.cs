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
    public GameObject BuyPannelDetail; // 구매 패널 상세 정보

    private void Awake()
    {
        menuBar.SetActive(false);
    }

    public void OpenStore() // 이 메서드는 GetKeyDown(KeyCode.E)에 의해 호출됩니다.
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!isStoreOpen) // 현재 isStoreOpen을 기준으로 상점 열기/닫기를 결정합니다.
            {
                openInvnetory.open(); // 인벤토리를 열고 닫는 로직을 활용
                openStoreObject.SetActive(true); // 상점 패널 활성화
                isStoreOpen = true; // 상점이 열렸음을 표시
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                menuBar.SetActive(true); // 메뉴 바 비활성화
            }
            else // 상점이 이미 열려있는 경우 (닫기)
            {
                openInvnetory.open(); // 인벤토리를 닫는 로직을 활용 (커서 상태 등)
                openStoreObject.SetActive(false); // 상점 패널 비활성화
                isStoreOpen = false; // 상점이 닫혔음을 표시
                uiOpenDetail.CloseDetailPanel(); // 상세 패널도 닫기
                menuBar.SetActive(false); // 메뉴 바 비활성화
                BuyPannelDetail.SetActive(false); // 구매 패널 상세 정보 비활성화
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape) && isStoreOpen)
        {
            openInvnetory.close(); // 인벤토리를 닫는 로직을 활용
            openStoreObject.SetActive(false); // 상점 패널 비활성화
            isStoreOpen = false; // 상점이 닫혔음을 표시
            uiOpenDetail.CloseDetailPanel(); // 상세 패널도 닫기
            menuBar.SetActive(false); // 메뉴 바 비활성화
            BuyPannelDetail.SetActive(false); // 구매 패널 상세 정보 비활성화

            Cursor.visible = false; // 커서 비활성화
            Cursor.lockState = CursorLockMode.Locked; // 커서 잠금 상태로 변경
        }
    }
}