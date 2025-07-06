using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "ItemData/Item")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public float weight;
    public float price;
}
