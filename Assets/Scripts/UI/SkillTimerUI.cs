using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using System.Collections.Generic; // List 사용을 위해 필요
using System.Linq; // OrderBy를 사용하기 위해 필요

public class SkillTimerUI : MonoBehaviour
{
    [System.Serializable]
    public class SkillBarEntry
    {
        public Image fillBar;
        public GameObject uiPanel;
        public Text skillText; // 스킬 텍스트 UI 추가
        public float duration; // 각 스킬 바의 지속 시간
        [ReadOnly] public int skillNumber; // 이 스킬 바가 몇 번째 스킬인지 (1, 2, 3, 4)
        [ReadOnly] public float currentRemainingTime; // 현재 남은 시간
        [ReadOnly] public bool isActive; // 이 스킬 바가 현재 활성화되어 있는지
    }

    [Header("UI References")]
    [Tooltip("순서대로 SkillBarPannel(1) ~ (4)의 Image, GameObject, Text를 할당하세요.")]
    public List<SkillBarEntry> skillBars = new List<SkillBarEntry>(4);

    // 스킬 2의 활성화 상태를 외부에 노출하기 위한 변수 추가
    public static bool IsSkill2Active = false;

    void Start()
    {
        // 초기화: 모든 스킬 바 UI 비활성화 및 바 비우기, 텍스트 초기화
        foreach (var skillBar in skillBars)
        {
            if (skillBar.fillBar != null)
                skillBar.fillBar.fillAmount = 0f;
            if (skillBar.uiPanel != null)
                skillBar.uiPanel.SetActive(false);
            if (skillBar.skillText != null) // 텍스트 초기화
                skillBar.skillText.text = "";
            skillBar.isActive = false;
            skillBar.currentRemainingTime = 0f;
        }
        // 스킬2 상태도 초기화
        IsSkill2Active = false;
    }

    void Update()
    {
        // 새로운 스킬 발동 조건: 아직 실행 중이 아니고, 선택 번호가 1~4일 때
        if (UseUISkill.finalSelectNum >= 1 && UseUISkill.finalSelectNum <= 4)
        {
            // 이미 해당 스킬 번호가 활성화된 스킬 바가 있는지 확인
            bool skillAlreadyActive = skillBars.Any(sb => sb.isActive && sb.skillNumber == UseUISkill.finalSelectNum);

            if (!skillAlreadyActive)
            {
                // 사용 가능한 스킬 바를 찾아서 할당
                SkillBarEntry targetSkillBar = GetAvailableSkillBar();

                if (targetSkillBar != null)
                {
                    targetSkillBar.skillNumber = UseUISkill.finalSelectNum;
                    StartSkillTimer(targetSkillBar);
                    OpenSkillSelectPannel._isUse = true; // 외부 사용 상태 갱신
                    UseUISkill.finalSelectNum = 0; // 스킬 발동 후 선택 번호 초기화
                }
                else
                {
                    Debug.LogWarning("모든 스킬 바가 사용 중입니다. 새로운 스킬을 시작할 수 없습니다.");
                    UseUISkill.finalSelectNum = 0; // 선택 초기화
                }
            }
            else
            {
                // 이미 활성화된 스킬이 다시 선택되었을 경우, 선택 번호 초기화만
                UseUISkill.finalSelectNum = 0;
            }
        }

        // 스킬이 실행 중인 모든 스킬 바에 대해 타이머 갱신
        foreach (var skillBar in skillBars)
        {
            if (skillBar.isActive)
            {
                skillBar.currentRemainingTime -= Time.deltaTime;

                // 채워진 이미지 줄이기
                if (skillBar.fillBar != null)
                    skillBar.fillBar.fillAmount = skillBar.currentRemainingTime / skillBar.duration;

                // 타이머 종료 시
                if (skillBar.currentRemainingTime <= 0f)
                {
                    skillBar.currentRemainingTime = 0f;
                    skillBar.isActive = false;

                    if (skillBar.fillBar != null)
                        skillBar.fillBar.fillAmount = 0f;

                    if (skillBar.uiPanel != null)
                        skillBar.uiPanel.SetActive(false);
                    
                    if (skillBar.skillText != null) // 텍스트 초기화
                        skillBar.skillText.text = "";

                    Debug.Log($"스킬 {skillBar.skillNumber} 타이머 종료!");

                    // 스킬 2가 종료되면 IsSkill2Active를 false로 설정
                    if (skillBar.skillNumber == 2)
                    {
                        IsSkill2Active = false;
                    }
                }
            }
        }

        // 모든 스킬이 비활성화되었을 때 OpenSkillSelectPannel._isUse 초기화
        if (!skillBars.Any(sb => sb.isActive))
        {
            OpenSkillSelectPannel._isUse = false;
        }
    }

    /// <summary>
    /// 사용 가능한 스킬 바를 찾습니다. (비활성화된 스킬 바 우선)
    /// </summary>
    private SkillBarEntry GetAvailableSkillBar()
    {
        // 비활성화된 스킬 바를 찾습니다.
        foreach (var skillBar in skillBars)
        {
            if (!skillBar.isActive)
            {
                return skillBar;
            }
        }
        // 모든 스킬 바가 사용 중이라면 null 반환 (현재 요구사항에 따르면 대체하지 않음)
        // 만약 '가장 오래된 것을 대체'하는 로직을 원한다면 여기에 추가해야 합니다.
        return null;
    }


    /// <summary>
    /// 특정 스킬 바를 활성화하고 타이머를 시작합니다.
    /// </summary>
    private void StartSkillTimer(SkillBarEntry skillBar)
    {
        if (skillBar.fillBar == null)
        {
            Debug.LogError($"Skill Fill Bar UI Image가 할당되지 않았습니다! (Skill {skillBar.skillNumber})");
            return;
        }

        switch (skillBar.skillNumber)
        {
            case 1:
                skillBar.duration = 3f; // 첫 번째 스킬은 3초
                break;
            case 2:
                skillBar.duration = 10f; // 두 번째 스킬은 10초
                break;
            case 3:
                skillBar.duration = 6f; // 예시: 세 번째 스킬은 6초
                break;
            case 4:
                skillBar.duration = 8f; // 예시: 네 번째 스킬은 8초
                break;
            default:
                skillBar.duration = 5f; // 기본값
                break;
        }

        skillBar.currentRemainingTime = skillBar.duration;
        skillBar.isActive = true;

        skillBar.fillBar.fillAmount = 1f;

        if (skillBar.uiPanel != null)
            skillBar.uiPanel.SetActive(true);

        if (skillBar.skillText != null)
        {
            if (skillBar.skillNumber == 1)
                skillBar.skillText.text = "USING FIRST SKILL";
            else if (skillBar.skillNumber == 2)
                skillBar.skillText.text = "Activate skill by targeting.";
            else if (skillBar.skillNumber == 3)
                skillBar.skillText.text = "THIRD SKILL ACTIVE";
            else if (skillBar.skillNumber == 4)
                skillBar.skillText.text = "FOURTH SKILL IN PROGRESS";
        }

        // 스킬 2가 시작되면 IsSkill2Active를 true로 설정
        if (skillBar.skillNumber == 2)
        {
            IsSkill2Active = true;
        }
    }

    /// <summary>
    /// 특정 스킬 번호의 타이머를 강제로 종료합니다.
    /// </summary>
    /// <param name="skillNumToEnd">종료할 스킬 번호</param>
    public void ForceEndSkill(int skillNumToEnd)
    {
        foreach (var skillBar in skillBars)
        {
            if (skillBar.skillNumber == skillNumToEnd && skillBar.isActive)
            {
                skillBar.currentRemainingTime = 0f; // 즉시 0으로 설정하여 Update에서 종료되도록 유도
                // 또는 직접 종료 로직을 여기에 복사해도 됩니다:
                // skillBar.isActive = false;
                // if (skillBar.fillBar != null) skillBar.fillBar.fillAmount = 0f;
                // if (skillBar.uiPanel != null) skillBar.uiPanel.SetActive(false);
                // if (skillBar.skillText != null) skillBar.skillText.text = "";
                // if (skillBar.skillNumber == 2) IsSkill2Active = false; // 스킬 2인 경우 static 변수도 업데이트
                // Debug.Log($"스킬 {skillBar.skillNumber} 타이머 강제 종료!");
                // break; // 해당 스킬을 찾았으면 루프 종료
            }
        }
    }
}