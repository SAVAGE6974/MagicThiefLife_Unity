using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;

public class SkillTimerUI : MonoBehaviour
{
    public static SkillTimerUI Instance;

    private void Awake()
    {
        Instance = this;
    }

    [System.Serializable]
    public class SkillBarEntry
    {
        public Image fillBar;
        public GameObject uiPanel;
        public Text skillText;
        public float duration;
        [ReadOnly] public int skillNumber;
        [ReadOnly] public float currentRemainingTime;
        [ReadOnly] public bool isActive;
    }

    [Header("UI References")]
    [Tooltip("순서대로 SkillBarPannel(1) ~ (5)의 Image, GameObject, Text를 할당하세요.")]
    public List<SkillBarEntry> skillBars = new List<SkillBarEntry>(5); 

    public static bool IsSkill2Active = false; // 기존 스킬 2 상태
    public static bool IsSkill5Active = false; // 새로 추가된 스킬 5 상태 변수

    void Start()
    {
        foreach (var skillBar in skillBars)
        {
            if (skillBar.fillBar != null)
                skillBar.fillBar.fillAmount = 0f;
            if (skillBar.uiPanel != null)
                skillBar.uiPanel.SetActive(false);
            if (skillBar.skillText != null)
                skillBar.skillText.text = "";
            skillBar.isActive = false;
            skillBar.currentRemainingTime = 0f;
        }
        IsSkill2Active = false;
        IsSkill5Active = false; // 스킬 5 상태도 초기화
    }

    void Update()
    {
        // ... (이 부분은 이전과 동일)
        if (UseUISkill.finalSelectNum >= 1 && UseUISkill.finalSelectNum <= 5) 
        {
            bool skillAlreadyActive = skillBars.Any(sb => sb.isActive && sb.skillNumber == UseUISkill.finalSelectNum);

            if (!skillAlreadyActive)
            {
                SkillBarEntry targetSkillBar = GetAvailableSkillBar();

                if (targetSkillBar != null)
                {
                    targetSkillBar.skillNumber = UseUISkill.finalSelectNum;
                    StartSkillTimer(targetSkillBar);
                    OpenSkillSelectPannel._isUse = true;
                    UseUISkill.finalSelectNum = 0;
                }
                else
                {
                    Debug.LogWarning("모든 스킬 바가 사용 중입니다. 새로운 스킬을 시작할 수 없습니다.");
                    UseUISkill.finalSelectNum = 0;
                }
            }
            else
            {
                UseUISkill.finalSelectNum = 0;
            }
        }

        foreach (var skillBar in skillBars)
        {
            if (skillBar.isActive)
            {
                skillBar.currentRemainingTime -= Time.deltaTime;

                if (skillBar.fillBar != null)
                    skillBar.fillBar.fillAmount = skillBar.currentRemainingTime / skillBar.duration;

                if (skillBar.currentRemainingTime <= 0f)
                {
                    skillBar.currentRemainingTime = 0f;
                    skillBar.isActive = false;

                    if (skillBar.fillBar != null)
                        skillBar.fillBar.fillAmount = 0f;

                    if (skillBar.uiPanel != null)
                        skillBar.uiPanel.SetActive(false);
                    
                    if (skillBar.skillText != null)
                        skillBar.skillText.text = "";

                    Debug.Log($"스킬 {skillBar.skillNumber} 타이머 종료!");

                    if (skillBar.skillNumber == 2)
                    {
                        IsSkill2Active = false;
                    }
                    // 스킬 5가 종료되면 IsSkill5Active를 false로 설정
                    if (skillBar.skillNumber == 5) 
                    {
                        IsSkill5Active = false;
                    }
                }
            }
        }

        if (!skillBars.Any(sb => sb.isActive))
        {
            OpenSkillSelectPannel._isUse = false;
        }
    }

    private SkillBarEntry GetAvailableSkillBar()
    {
        foreach (var skillBar in skillBars)
        {
            if (!skillBar.isActive)
            {
                return skillBar;
            }
        }
        return null;
    }

    private void StartSkillTimer(SkillBarEntry skillBar)
    {
        // ... (이 부분도 동일)
        if (skillBar.fillBar == null)
        {
            Debug.LogError($"Skill Fill Bar UI Image가 할당되지 않았습니다! (Skill {skillBar.skillNumber})");
            return;
        }

        switch (skillBar.skillNumber)
        {
            case 1:
                skillBar.duration = 3f;
                skillBar.skillText.text = "USING FIRST SKILL";
                break;
            case 2:
                skillBar.duration = 10f;
                skillBar.skillText.text = "SECOUND SKILL ACTIVE";
                break;
            case 3:
                skillBar.duration = 6f;
                skillBar.skillText.text = "THIRD SKILL ACTIVE";
                break;
            case 4:
                skillBar.duration = 8f;
                skillBar.skillText.text = "FOURTH SKILL IN PROGRESS";
                break;
            case 5: 
                skillBar.duration = 10f; 
                skillBar.skillText.text = "Activate skill by targeting."; 
                break;
            default:
                skillBar.duration = 5f;
                skillBar.skillText.text = ""; 
                break;
        }

        skillBar.currentRemainingTime = skillBar.duration;
        skillBar.isActive = true;

        skillBar.fillBar.fillAmount = 1f;

        if (skillBar.uiPanel != null)
            skillBar.uiPanel.SetActive(true);

        if (skillBar.skillNumber == 2)
        {
            IsSkill2Active = true;
        }
        // 스킬 5가 시작되면 IsSkill5Active를 true로 설정
        if (skillBar.skillNumber == 5) 
        {
            IsSkill5Active = true;
        }
    }

    public void ForceEndSkill(int skillNumToEnd)
    {
        foreach (var skillBar in skillBars)
        {
            if (skillBar.skillNumber == skillNumToEnd && skillBar.isActive)
            {
                skillBar.currentRemainingTime = 0f;
            }
        }
    }
}