using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class Button_StorMenuBarButton : MonoBehaviour
{
    public GameObject SalePannel;
    public GameObject BuyPannel;
    private void Awake()
    {
        SalePannel.SetActive(false);
        BuyPannel.SetActive(false);
    }

    public void OnClickSale()
    {
        SalePannel.SetActive(true);
        BuyPannel.SetActive(false);
    }
    public void OnClickBuy()
    {
        SalePannel.SetActive(false);
        BuyPannel.SetActive(true);
    }
}