// UIOpenDetail.cs
using UnityEngine;
using UnityEngine.UI;
using System.Globalization;

public class UIOpenDetail : MonoBehaviour
{
    [SerializeField] private OpenStorePannel openStorePannel;
    
    public GameObject DeatilePannel; // 상세 정보 패널 GameObject
    public GameObject SalePannel;
    public Image Icon;              // 상세 패널의 아이콘 Image 컴포넌트
    public Text ItemName;           // 상세 패널의 아이템 이름 Text 컴포넌트
    public Text Description;        // 상세 패널의 아이템 설명 Text 컴포넌트
    public Text Price;
    public GameObject cheakPrefab;  // 이 변수의 용도는 아직 불분명합니다.

    private void Awake()
    {
        if (DeatilePannel != null)
        {
            DeatilePannel.SetActive(false); // 시작 시 상세 패널 비활성화
            SalePannel.SetActive(false);
        }
    }

    // ItemSlotUIHandler에서 호출될 새로운 메서드
    // 클릭된 아이템의 정보와 아이콘을 직접 받아옵니다.
    public void UpdateDetailPanel(Item clickedItem, Sprite itemIcon)
    {
        if (DeatilePannel == null || ItemName == null || Description == null || Icon == null)
        {
            Debug.LogError("UIOpenDetail: 필요한 UI 요소가 할당되지 않았습니다. Inspector를 확인해주세요.");
            return;
        }

        // 패널 활성화
        DeatilePannel.SetActive(true);
        if (openStorePannel.isStoreOpen)
            SalePannel.SetActive(true);

        // 아이템 정보에 따라 UI 업데이트
        ItemName.text = clickedItem.name;
        Description.text = clickedItem.description; // Item 클래스에 description이 추가되어야 함
        Icon.sprite = itemIcon; // 클릭된 슬롯의 아이콘을 그대로 사용
        Price.text = clickedItem.price.ToString("N0", CultureInfo.InvariantCulture) + "$";

        // 특정 아이템에 따른 추가 로직 (요구사항에 따라)
        if (clickedItem.name == "Magic Pearl")
        {
            // Magic Pearl에 대한 특별 처리 (예: 특정 이펙트, 추가 정보 등)
            Debug.Log("UIOpenDetail: Magic Pearl이 클릭되었습니다.");
        }
        else if (clickedItem.name == "Magic JiPangii")
        {
            // Magic JiPangii를 클릭하면 패널을 닫는다면 (이전 요청 기준)
            // 하지만 보통은 JiPangii의 상세 정보를 보여주는 게 일반적입니다.
            // 요구사항에 따라 이 부분의 동작을 정의하세요.
            // 현재는 다른 아이템 클릭 시와 동일하게 상세 정보를 보여주도록 합니다.
            Debug.Log("UIOpenDetail: Magic JiPangii가 클릭되었습니다.");
            // DeatilePannel.SetActive(false); // 만약 JiPangii 클릭 시 닫히게 하려면 이 주석을 해제
        }
        else
        {
            // 그 외의 아이템 처리
            Debug.Log($"UIOpenDetail: {clickedItem.name}이 클릭되었습니다.");
        }
    }

    // 상세 패널을 닫는 공용 메서드 (예: "닫기" 버튼에 연결)
    public void CloseDetailPanel()
    {
        if (DeatilePannel != null)
        {
            DeatilePannel.SetActive(false);
            SalePannel.SetActive(false);
        }
    }
}