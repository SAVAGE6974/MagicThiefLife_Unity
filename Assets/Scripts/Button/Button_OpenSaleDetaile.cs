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
    [FoldoutGroup("Main Item")]
    public Text MainItemName;
    [FoldoutGroup("Main Item")]
    public Image MainItemIcon;
    [FoldoutGroup("Main Item")]
    public Text MainItemDescription;
    [FoldoutGroup("Main Item")]
    public Text MainItemPrice;

    [InfoBox("해당 아이템들의 실제 가격과 Button의 가격등을 일치해야하므로 Content아래의 ItemSlot의 Text와 이름이 이 스크립트와 함께 작성해야합니다.")]    
    [FoldoutGroup("First Item")]
    public Text firstItemName;
    [FoldoutGroup("First Item")]
    public Image firstItemIcon;
    [FoldoutGroup("First Item")]
    public Text firstItemPrice;

    [InfoBox("해당 아이템들의 실제 가격과 Button의 가격등을 일치해야하므로 Content아래의 ItemSlot의 Text와 이름이 이 스크립트와 함께 작성해야합니다.")]
    [FoldoutGroup("Second Item")]
    public Text secondItemName;
    [FoldoutGroup("Second Item")]
    public Image secondItemIcon;
    [FoldoutGroup("Second Item")]
    public Text secondItemPrice;

    private void Awake()
    {
        DetailePannel.SetActive(false);
        SalePannel.SetActive(false);
    }

    public void OnclickShowDetaile()
    {
        DetailePannel.SetActive(true);
        SalePannel.SetActive(true);
    }

    public void OnClickFirstItem()
    {
        MainItemName.text = firstItemName.text;
        MainItemIcon.sprite = firstItemIcon.sprite;
        MainItemPrice.text = firstItemPrice.text + "$";
    }
}