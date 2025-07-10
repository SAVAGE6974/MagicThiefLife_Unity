using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Sirenix.OdinInspector;

public class OpenInventory_equipMent : MonoBehaviour
{
    [InfoBox("현재, 전체와 무기만 작동합니다. 그 외는 직접 다시 추가해야하며. 그래야지 작동합니다.")]
    public GameObject DoNotDefindThere;

    public void OnclickAllBtn()
    {
        GameObject[] rootObjects = SceneManager.GetActiveScene().GetRootGameObjects();
        foreach (GameObject obj in rootObjects)
        {
            ShowAllInventoryContents(obj);
        }
    }

    public void OnclickHandBtn()
    {
        GameObject[] rootObjects = SceneManager.GetActiveScene().GetRootGameObjects();
        foreach (GameObject obj in rootObjects)
        {
            ShowHandInventoryContents(obj);
        }
    }



    // ===================================== //

    private void ShowHandInventoryContents(GameObject obj)
    {
        if (obj.name.Contains("Hand"))
        {
            obj.SetActive(true);
        }
    }

    private void ShowAllInventoryContents(GameObject obj)
    {
        if (obj.name.Contains("Equipment"))
        {
            obj.SetActive(true);
        }
    }
}
