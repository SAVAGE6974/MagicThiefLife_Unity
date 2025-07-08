using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;

public class SaleButtonBG_Changer : MonoBehaviour
{
    [InfoBox("구매 가능한 물건을 제외한 나머지는 반투명색으로 변경하기 위함으로, 아이템이 추가될때마다 임포넌트를 추가해야합니다.")]
    [FoldoutGroup("UI")] public Button button1;
    [FoldoutGroup("UI")] public Text buttonText1;
    [FoldoutGroup("UI")] public Button button2;
    [FoldoutGroup("UI")] public Text buttonText2;

    private int button1Price;
    private int button2Price;

    private int userMoney;

    private void Awake()
    {
        button1Price = int.Parse(buttonText1.text);
        button2Price = int.Parse(buttonText2.text);
    }

    private void Start()
    {
        userMoney = PlayerPrefs.GetInt("PlayerMoney");
    }

    public void OnClick()
    {
        userMoney = PlayerPrefs.GetInt("PlayerMoney");

        if (button1Price > userMoney) button1.image.color = new Color(237f / 255f, 231f / 255f, 208f / 255f, 0.5f);
        else button1.image.color = new Color(237f / 255f, 231f / 255f, 208f / 255f, 1f);
        if (button2Price > userMoney) button2.image.color = new Color(237f / 255f, 231f / 255f, 208f / 255f, 0.5f);
        else button2.image.color = new Color(237f / 255f, 231f / 255f, 208f / 255f, 1f);
    }
}
