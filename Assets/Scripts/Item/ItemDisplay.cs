using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDisplay : MonoBehaviour
{
    public ItemData itemData;

    private void Start()
    {
        Debug.Log("아이템 이름: " + itemData.itemName);
    }
}
