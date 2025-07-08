using UnityEngine;
using Sirenix.OdinInspector;

public class ItemObject : MonoBehaviour
{
    [InfoBox("SaleManager의 price값을 직접 변경해야지, \nplayerPrefs에 정상적으로 저장됩니다.")]

    public string itemName;
    public float weight;
    public float price;

    [TextArea(1, 5)]
    public string description;
    public Sprite itemIcon;

    public Item GetItemData()
    {
        return new Item(itemName, weight, price, description);
    }
}