using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSkillManager : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // '1'번 키를 눌렀을 때 5번 스킬 발동
        if (Input.GetKeyDown(KeyCode.Alpha1)) // Alpha1은 키보드 상단의 숫자 1을 의미합니다.
        {
            // SkillTimerUI에 스킬 번호를 전달하여 5번 스킬을 시작합니다.
            // SkillTimerUI가 싱글톤이므로 이렇게 접근할 수 있습니다.
            if (SkillTimerUI.Instance != null)
            {
                UseUISkill.finalSelectNum = 5; // UseUISkill 스크립트에서 이 값을 읽어 5번 스킬을 시작합니다.
                Debug.Log("5번 스킬 발동 요청됨.");
            }
            else
            {
                Debug.LogError("SkillTimerUI 인스턴스를 찾을 수 없습니다. SkillTimerUI 스크립트가 활성화되어 있는지 확인하세요.");
            }
        }
    }
}