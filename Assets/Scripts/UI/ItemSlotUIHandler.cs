// ItemSlotUIHandler.cs
using UnityEngine;
using UnityEngine.UI; // Button과 Text를 사용하기 위함

public class ItemSlotUIHandler : MonoBehaviour
{
    // 이 슬롯이 표시할 아이템 데이터
    private Item _itemData;

    // UI 요소들을 직접 연결 (Inspector에서 할당)
    public Text itemNameText;   // ItemSlot 프리팹 내부의 Text 컴포넌트
    public Image itemIconImage; // ItemSlot 프리팹 내부의 Image 컴포넌트
    public Text itemWeightText; // ItemSlot 프리팹 내부의 Weight Text 컴포넌트
    public Button slotButton;   // ItemSlot 프리팹 내부의 Button 컴포넌트 (클릭 이벤트를 위해)

    // UIOpenDetail 스크립트 인스턴스 (씬에 하나만 있다고 가정하고 FindObjectOfType 사용)
    private UIOpenDetail uiOpenDetail;

    private void Awake()
    {
        // 씬에서 UIOpenDetail 인스턴스를 찾습니다. (싱글톤 패턴 사용 시 더 견고함)
        uiOpenDetail = FindObjectOfType<UIOpenDetail>();
        if (uiOpenDetail == null)
        {
            Debug.LogError("ItemSlotUIHandler: 씬에서 UIOpenDetail을 찾을 수 없습니다!");
        }

        // 버튼 클릭 시 OnSlotClicked 메서드 호출
        if (slotButton != null)
        {
            slotButton.onClick.AddListener(OnSlotClicked);
        }
    }

    // 이 메서드는 StolenItemUI에서 아이템 슬롯 생성 시 호출되어 정보를 설정합니다.
    public void SetItemData(Item item, Sprite icon)
    {
        _itemData = item;

        if (itemNameText != null)
        {
            itemNameText.text = _itemData.name;
        }
        if (itemIconImage != null)
        {
            itemIconImage.sprite = icon;
        }
        if (itemWeightText != null)
        {
            itemWeightText.text = _itemData.weight.ToString();
        }
    }

    // 버튼이 클릭되었을 때 호출되는 메서드
    private void OnSlotClicked()
    {
        if (_itemData != null && uiOpenDetail != null)
        {
            // UIOpenDetail에 클릭된 아이템의 모든 정보를 전달
            uiOpenDetail.UpdateDetailPanel(_itemData, itemIconImage.sprite);
        }
        else
        {
            Debug.LogWarning("ItemSlotUIHandler: 아이템 데이터 또는 UIOpenDetail이 설정되지 않았습니다.");
        }
    }
}