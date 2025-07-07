// getItem.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class getItem : MonoBehaviour
{
    public float maxDistance = 20f;
    public List<Item> itemList = new List<Item>();
    private StolenItemUI stolenItemUI;

    [InfoBox("현재 R구현은 안되여 있음. 현재 R버튼은 플레이어컴포넌트 데이터 삭제")]
    public GameObject LeftCrossHair;
    public GameObject RightCrossHair;

    private void Awake()
    {
        LeftCrossHair.SetActive(false);
        RightCrossHair.SetActive(false);
    }

    private void Start()
    {
        stolenItemUI = FindObjectOfType<StolenItemUI>();
        if (stolenItemUI == null)
        {
            Debug.LogError("StolenItemUI를 찾을 수 없습니다!");
        }
    }

    private void Update()
    {
        rayGetItem();
        checkMyItems();
    }

    private void checkMyItems()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) // itemList != null 체크는 불필요
        {
            foreach (var item in itemList)
            {
                Debug.Log(item.ToString());
            }
        }
    }

    private void rayGetItem()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            if (hit.collider.CompareTag("item") || hit.collider.tag.StartsWith("item"))
            {
                LeftCrossHair.SetActive(true);
                RightCrossHair.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E)
                    && !OpenInvnetory._isOpen && !OpenSkillSelectPannel._isOpen)
                {
                    var itemObj = hit.collider.GetComponent<ItemObject>();
                    if (itemObj != null)
                    {
                        Item pickedItem = itemObj.GetItemData(); // ItemObject의 GetItemData()는 이미 description을 포함한 Item 객체를 반환합니다.
                        itemList.Add(pickedItem);

                        Debug.Log($"획득: {pickedItem.name}, {pickedItem.weight}");

                        // UI에 추가
                        if (stolenItemUI != null)
                        {
                            // *** 이 부분이 중요합니다. stolenItemUI.AddItem() 호출 시 description도 함께 넘겨야 합니다. ***
                            // stolenItemUI의 AddItem 메서드가 description 파라미터를 받도록 수정되었다는 전제하에
                            stolenItemUI.AddItem(itemObj.itemName, itemObj.itemIcon, itemObj.weight, itemObj.description, itemObj.price);
                        }

                        hit.collider.gameObject.SetActive(false);
                    }
                }
            }
        }
        else
        {
            LeftCrossHair.SetActive(false);
            RightCrossHair.SetActive(false);
        }
    }
}