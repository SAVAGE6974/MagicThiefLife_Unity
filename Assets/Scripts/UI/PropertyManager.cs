using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class PropertyManager : MonoBehaviour
{
    [Required] public Text PlayerMoneyTextUI; // UI Text와 변수 이름 충돌을 피하기 위해 이름 변경

    private int currentMoney; // money 변수 이름 변경

    private void Awake()
    {
        // "PlayerProperty" 대신 "PlayerMoney" 키 사용
        currentMoney = PlayerPrefs.GetInt("PlayerMoney", 0);
        Debug.Log("Awake에서 불러온 초기 돈: " + currentMoney);
    }

    private void Update()
    {
        // if (Input.GetKeyDown(KeyCode.R))
        // {
        //     PlayerPrefs.DeleteAll();
        //     Debug.Log("All PlayerPrefs keys deleted");
        //     currentMoney = 0; // 초기화
        // }

        // // 매 프레임 최신 돈을 가져와 UI에 표시
        // // PropertyManager가 돈을 변경하는 유일한 스크립트라면 이 줄은 필요 없을 수도 있지만,
        // // 다른 스크립트가 돈을 변경할 경우 즉시 반영하기 위해 유지하는 것이 좋습니다.
        currentMoney = PlayerPrefs.GetInt("PlayerMoney", 0);
        PlayerMoneyTextUI.text = currentMoney + "$";
    }

    public void AddMoney(int amount)
    {
        currentMoney += amount;
        // "PlayerProperty" 대신 "PlayerMoney" 키 사용
        PlayerPrefs.SetInt("PlayerMoney", currentMoney);
        PlayerPrefs.Save();
        Debug.Log("돈 추가됨: " + amount + ", 현재 돈: " + currentMoney);
    }

    public void SubtractMoney(int amount)
    {
        if (currentMoney >= amount)
        {
            currentMoney -= amount;
            // "PlayerProperty" 대신 "PlayerMoney" 키 사용
            PlayerPrefs.SetInt("PlayerMoney", currentMoney);
            PlayerPrefs.Save();
            Debug.Log("돈 사용됨: " + amount + ", 현재 돈: " + currentMoney);
        }
        else
        {
            Debug.Log("돈이 부족합니다! 현재 금액: " + currentMoney);
        }
    }
}