using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class StolenItemUI : MonoBehaviour
{
    [InfoBox("아이템 추가시 조건문도 함께 수동적으로 추가해야지 됩니다.")]

    [Required] public GameObject itemSlotPrefab; // 프리팹 연결
    [Required] public Transform contentPanel_nomal; // Content 오브젝트 연결

    private List<GameObject> itemSlots = new List<GameObject>();

    // AddItem 메소드의 매개변수를 정확히 사용하도록 수정
    public void AddItem(string name, Sprite icon, float weight, string description, float price)
    {
        // ItemObject에서 받은 정보를 Item 클래스 인스턴스로 변환
        // 이제 price 값도 제대로 전달받아 Item 객체를 생성할 수 있습니다.
        Item newItem = new Item(name, weight, price, description); // <-- 매개변수 'name', 'weight', 'description', 'price'를 사용

        GameObject slot = Instantiate(itemSlotPrefab, contentPanel_nomal);

        // 슬롯 이름에 고유 번호 붙이기 "조건문 추가필요"
        if (name == "GreatSword") slot.name = $"ItemSlot_{name}_Equipment_Hand";
        else if (name == "LongSword") slot.name = $"ItemSlot_{name}_Equipment_Hand";
        else if (name == "Magic Pearl") slot.name = $"ItemSlot_{name}_Item";
        else if (name == "Magic Stick") slot.name = $"ItemSlot_{name}_Item";

        // 리스트에 추가해서 관리
        itemSlots.Add(slot);

        // ItemSlotUIHandler 컴포넌트를 찾아서 아이템 정보를 설정
        ItemSlotUIHandler slotHandler = slot.GetComponent<ItemSlotUIHandler>();
        if (slotHandler != null)
        {
            slotHandler.SetItemData(newItem, icon); // <-- 아이콘도 매개변수 'icon'을 사용
        }
        else
        {
            Debug.LogError($"생성된 ItemSlot 프리팹에 ItemSlotUIHandler 컴포넌트가 없습니다! 프리팹을 확인해주세요: {slot.name}");
        }
        Debug.Log($"새 슬롯 생성: {slot.name} (부모: {slot.transform.parent.name})");
    }
}