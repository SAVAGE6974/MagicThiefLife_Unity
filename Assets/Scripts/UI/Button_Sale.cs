using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class Button_Sale : MonoBehaviour
{
    [SerializeField] private UIOpenDetail uiOpenDetail;
    [InfoBox("판매 아이템 정보. price의 값은 직접 변경해야지, \nplayerPrefs에 정상적으로 저장됩니다.")]

    public Text itemName;

    public void pressButton()
    {
        int itemPrice = 0; // 현재 판매할 아이템의 가격
        string itemSlotName = ""; // 파괴할 아이템 슬롯의 이름

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
            return; // 판매할 수 없는 아이템이면 함수 종료
        }

        // --- 이 부분이 중요합니다 ---
        // 1. PlayerPrefs에서 현재 저장된 총 가격을 불러옵니다. (없으면 0)
        int currentTotalPrice = PlayerPrefs.GetInt("TotalPrice", 0);

        // 2. 현재 아이템의 가격을 총 가격에 더합니다.
        currentTotalPrice += itemPrice;

        // 3. 더해진 총 가격을 PlayerPrefs에 다시 저장합니다.
        PlayerPrefs.SetInt("TotalPrice", currentTotalPrice);

        Debug.Log("Sale Item: " + itemName.text + " / Price: " + itemPrice + " / Current Total Sale Price: " + currentTotalPrice);

        // 아이템 슬롯 파괴 및 UI 패널 닫기
        GameObject itemSlotToDestroy = GameObject.Find(itemSlotName);
        if (itemSlotToDestroy != null)
        {
            GameObject.Destroy(itemSlotToDestroy);
        }
        uiOpenDetail.CloseDetailPanel();
    }
}