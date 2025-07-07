using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class Button_Sale : MonoBehaviour
{
    [SerializeField] private UIOpenDetail uiOpenDetail;

    [InfoBox("판매/구매 아이템 이름을 여기에 표시합니다.")]
    public Text itemName;
    public Text buyItemName;

    /// <summary>
    /// 판매 버튼에 연결
    /// </summary>
    public void PressSellButton()
    {
        int itemPrice = 0;
        string itemSlotName = "";

        if (itemName.text == "Magic Pearl")
        {
            itemPrice = 1000;
            itemSlotName = "ItemSlot_Magic Pearl";
        }
        else if (itemName.text == "Magic Stick")
        {
            itemPrice = 500;
            itemSlotName = "ItemSlot_Magic Stick";
        }
        else
        {
            Debug.LogWarning("판매할 수 없는 아이템입니다: " + itemName.text);
            return;
        }

        int totalSalePrice = PlayerPrefs.GetInt("TotalPrice", 0);
        totalSalePrice += itemPrice;
        PlayerPrefs.SetInt("TotalPrice", totalSalePrice);

        Debug.Log("아이템 판매: " + itemName.text + " / 가격: " + itemPrice + " / 누적 금액: " + totalSalePrice);

        GameObject itemSlotToDestroy = GameObject.Find(itemSlotName);
        if (itemSlotToDestroy != null)
        {
            Destroy(itemSlotToDestroy);
        }

        uiOpenDetail.CloseDetailPanel();
    }

    /// <summary>
    /// 구매 버튼에 연결
    /// </summary>
    public void PressBuyButton()
    {
        int itemPrice = 0;

        if (buyItemName.text == "PigStone")
        {
            itemPrice = 1000;
        }
        else
        {
            Debug.LogWarning("구매할 수 없는 아이템입니다: " + buyItemName.text);
            return;
        }

        int currentTotalPrice = PlayerPrefs.GetInt("TotalPrice", 0);

        if (currentTotalPrice < itemPrice)
        {
            Debug.LogWarning("소지 금액이 부족합니다! 현재 금액: " + currentTotalPrice);
            return;
        }

        currentTotalPrice -= itemPrice;
        PlayerPrefs.SetInt("TotalPrice", currentTotalPrice);

        Debug.Log("아이템 구매: " + buyItemName.text + " / 가격: " + itemPrice + " / 남은 금액: " + currentTotalPrice);

        // 구매 시 슬롯은 파괴하지 않음
        uiOpenDetail.CloseDetailPanel();
    }
}
