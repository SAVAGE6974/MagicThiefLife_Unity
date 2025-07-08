using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using JetBrains.Annotations;

public class Button_OpenSaleDetaile : MonoBehaviour
{
    public GameObject DetailePannel;
    public GameObject SalePannel;

    [InfoBox("해당 위치에는 실제로 보이는 부분입니다")]  
    [Required, FoldoutGroup("Main Item")] public Text MainItemName;
    [Required, FoldoutGroup("Main Item")] public Image MainItemIcon;
    [Required, FoldoutGroup("Main Item")] public Text MainItemDescription;
    [Required, FoldoutGroup("Main Item")] public Text MainItemPrice;
    [Required, FoldoutGroup("Main Item")] public Text PlayerMoney; // 이 Text는 PlayerPrefs에서 읽어온 플레이어의 돈을 표시합니다.

    [InfoBox("해당 아이템들의 실제 가격과 Button의 가격등을 일치해야하므로 Content아래의 ItemSlot의 Text와 이름이 이 스크립트와 함께 작성해야합니다. \n 또한 Content아래의 ItemSlot에 할당되여있는 함수를 직접 변경해야하며, 변경이 되지 않을시 가격이 일정하게 지불이 안되는 버그가 발생합니다.")]    
    [Required, FoldoutGroup("First Item")] public Text firstItemName;
    [Required, FoldoutGroup("First Item")] public Image firstItemIcon;
    [Required, FoldoutGroup("First Item")] public Text firstItemPrice;
    [Required, FoldoutGroup("First Item")] public Text fistItemDescription;

    [InfoBox("해당 아이템들의 실제 가격과 Button의 가격등을 일치해야하므로 Content아래의 ItemSlot의 Text와 이름이 이 스크립트와 함께 작성해야합니다. \n 또한 Content아래의 ItemSlot에 할당되여있는 함수를 직접 변경해야하며, 변경이 되지 않을시 가격이 일정하게 지불이 안되는 버그가 발생합니다.")]
    [Required, FoldoutGroup("Second Item")] public Text secondItemName;
    [Required, FoldoutGroup("Second Item")] public Image secondItemIcon;
    [Required, FoldoutGroup("Second Item")] public Text secondItemPrice;
    [Required, FoldoutGroup("Second Item")] public Text secondItemDescription;

    private void Awake()
    {
        DetailePannel.SetActive(false);
        SalePannel.SetActive(false);
    }

    public void OnclickShowDetaile()
    {
        DetailePannel.SetActive(true);
        SalePannel.SetActive(true);
        
        // PlayerPrefs에서 "PlayerMoney" 키의 최신 값을 가져옵니다.
        // 만약 키가 없으면 기본값 0을 사용합니다.
        int currentPlayerMoney = PlayerPrefs.GetInt("PlayerMoney", 0); 
        PlayerMoney.text = currentPlayerMoney + "$";
        Debug.Log("상세 패널 오픈 시 현재 플레이어 소지금: " + currentPlayerMoney);
    }

    public void OnClickFirstItem()
    {
        MainItemName.text = firstItemName.text;
        MainItemIcon.sprite = firstItemIcon.sprite;
        MainItemPrice.text = firstItemPrice.text + "$";

        fistItemDescription.text = "This is an unknown object created by the king's power. Don't know how to use it, whether it's a weapon, a defense, or just an item, but it seems like it would be useful if bought it.";
        
        // 아이템을 클릭할 때도 플레이어 소지금을 갱신합니다.
        // 상세 패널이 이미 열려 있는 상태에서 다른 아이템을 클릭해도 금액이 최신화됩니다.
        int currentPlayerMoney = PlayerPrefs.GetInt("PlayerMoney", 0);
        PlayerMoney.text = currentPlayerMoney + "$";
    }

    // 예시: OnClickSecondItem도 동일하게 PlayerMoney를 갱신해야 합니다.
    public void OnClickSecondItem()
    {
        MainItemName.text = secondItemName.text;
        MainItemIcon.sprite = secondItemIcon.sprite;
        MainItemPrice.text = secondItemPrice.text + "$";

        secondItemDescription.text = "It seems like a better product than PigStone. However, it seems like it was not made officially, but made on the black market. It seems like it will definitely be more effective than PigStone, but Don't know what the negative effects are.";

        int currentPlayerMoney = PlayerPrefs.GetInt("PlayerMoney", 0);
        PlayerMoney.text = currentPlayerMoney + "$";
    }
}