using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class Button_Sale : MonoBehaviour
{
    [Required, SerializeField] private UIOpenDetail uiOpenDetail;

    [InfoBox("판매/구매 아이템 이름을 여기에 표시합니다.")]
    [Required]public Text itemName;
    [Required]public Text buyItemName;

    // 이 PlayerMoney Text는 UI에 현재 플레이어의 돈을 표시하는 데 사용됩니다.
    // PropertyManager의 PlayerMoneyTextUI와 동일한 Text 컴포넌트에 연결되어야 합니다.
    [Required]public Text PlayerMoney;

    /// <summary>
    /// 판매 버튼에 연결
    /// </summary>
    
    private IEnumerator ChangeColorTemporarily(Color fromColor, Color toColor, float duration)
    {
        PlayerMoney.color = fromColor;
        yield return new WaitForSeconds(duration);
        PlayerMoney.color = toColor;
    }

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

        // --- 여기를 수정했습니다: PlayerPrefs 키를 "PlayerMoney"로 통일 ---
        int currentMoney = PlayerPrefs.GetInt("PlayerMoney", 0);
        currentMoney += itemPrice;
        PlayerPrefs.SetInt("PlayerMoney", currentMoney); // "TotalPrice" 대신 "PlayerMoney" 사용

        Debug.Log("아이템 판매: " + itemName.text + " / 가격: " + itemPrice + " / 누적 금액: " + currentMoney);
        PlayerMoney.text = currentMoney + "$"; // UI 업데이트도 currentMoney로 변경

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
        else if (buyItemName.text == "PigRock")
        {
            itemPrice = 1500;
        }
        else
        {
            Debug.LogWarning("구매할 수 없는 아이템입니다: " + buyItemName.text);
            return;
        }

        // --- 여기를 수정했습니다: PlayerPrefs 키를 "PlayerMoney"로 통일 ---
        int currentMoney = PlayerPrefs.GetInt("PlayerMoney", 0);

        if (currentMoney < itemPrice)
        {
            Debug.LogWarning("소지 금액이 부족합니다! 현재 금액: " + currentMoney);
            StartCoroutine(ChangeColorTemporarily(new Color(0.8f, 0f, 0f), Color.white, 1.5f));
            return;
        }

        currentMoney -= itemPrice;
        PlayerPrefs.SetInt("PlayerMoney", currentMoney); // "TotalPrice" 대신 "PlayerMoney" 사용

        Debug.Log("아이템 구매: " + buyItemName.text + " / 가격: " + itemPrice + " / 남은 금액: " + currentMoney);
        PlayerMoney.text = currentMoney + "$"; // UI 업데이트도 currentMoney로 변경

        // 구매 시 슬롯은 파괴하지 않음
        uiOpenDetail.CloseDetailPanel();
    }
}