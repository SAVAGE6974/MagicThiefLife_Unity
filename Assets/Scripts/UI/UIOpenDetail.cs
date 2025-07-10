using UnityEngine;
using UnityEngine.UI;
using System.Globalization;
using Sirenix.OdinInspector;

public class UIOpenDetail : MonoBehaviour
{
    [Required, SerializeField] private OpenStorePannel openStorePannel;

    [Required, FoldoutGroup("inv")] public GameObject inv_DeatilePannel;
    [Required, FoldoutGroup("inv")] public GameObject SalePannel;
    [Required, FoldoutGroup("inv")] public Image inv_Icon;
    [Required, FoldoutGroup("inv")] public Text inv_ItemName;
    [Required, FoldoutGroup("inv")] public Text inv_Description;
    [Required, FoldoutGroup("inv")] public Text Price;
    [Required, FoldoutGroup("inv")] public GameObject cheakPrefab;

    [Required, FoldoutGroup("equ")] public GameObject equ_DeatilePannel;
    [Required, FoldoutGroup("equ")] public Image equ_Icon;
    [Required, FoldoutGroup("equ")] public Text equ_ItemName;
    [Required, FoldoutGroup("equ")] public Text equ_Description;
    [Required, FoldoutGroup("equ")] public GameObject equ_cheakPrefab;

    private Item currentItem;
    private Sprite currentIcon;

    private void Awake()
    {
        if (inv_DeatilePannel != null)
        {
            inv_DeatilePannel.SetActive(false);
            SalePannel.SetActive(false);
        }

        if (equ_DeatilePannel != null)
        {
            equ_DeatilePannel.SetActive(false);
        }
    }

    // 외부에서 클릭된 슬롯의 데이터 전달
    public void SetSlotData(Item item, Sprite icon)
    {
        currentItem = item;
        currentIcon = icon;
    }

    // 버튼에 연결할 함수
    public void OnSlotClicked()
    {
        if (currentItem != null && currentIcon != null)
        {
            UpdateDetailPanel(currentItem, currentIcon);
        }
        else
        {
            Debug.LogWarning("UIOpenDetail: 슬롯 데이터가 설정되지 않았습니다.");
        }
    }

    // 상세 패널 갱신
    public void UpdateDetailPanel(Item clickedItem, Sprite itemIcon)
    {
        if (!OpenInvnetory._isOpenEquipment)
        {
            if (inv_DeatilePannel == null || inv_ItemName == null || inv_Description == null || inv_Icon == null)
            {
                Debug.LogError("UIOpenDetail: 인벤토리 UI 요소가 할당되지 않았습니다.");
                return;
            }

            inv_DeatilePannel.SetActive(true);
            if (openStorePannel.isStoreOpen)
                SalePannel.SetActive(true);

            inv_ItemName.text = clickedItem.name;
            inv_Description.text = clickedItem.description;
            inv_Icon.sprite = itemIcon;
            Price.text = clickedItem.price.ToString("N0", CultureInfo.InvariantCulture) + "$";

            Debug.Log($"UIOpenDetail: {clickedItem.name}이 클릭되었습니다.");
        }
        else
        {
            if (equ_DeatilePannel == null || equ_ItemName == null || equ_Description == null || equ_Icon == null)
            {
                Debug.LogError("UIOpenDetail: 장비 UI 요소가 할당되지 않았습니다.");
                return;
            }

            equ_DeatilePannel.SetActive(true);
            if (openStorePannel.isStoreOpen)
                SalePannel.SetActive(true);

            equ_ItemName.text = clickedItem.name;
            equ_Description.text = clickedItem.description;
            equ_Icon.sprite = itemIcon;
            Price.text = clickedItem.price.ToString("N0", CultureInfo.InvariantCulture) + "$";

            Debug.Log($"UIOpenDetail: {clickedItem.name}이 클릭되었습니다.");
        }
    }

    public void CloseDetailPanel()
    {
        if (inv_DeatilePannel != null)
        {
            inv_DeatilePannel.SetActive(false);
            SalePannel.SetActive(false);
        }

        if (equ_DeatilePannel != null)
        {
            equ_DeatilePannel.SetActive(false);
            SalePannel.SetActive(false);
        }
    }
}
