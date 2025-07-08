using UnityEngine;
using UnityEngine.UI; // UI Image를 사용하기 위해 필요
using TMPro; // TextMeshProUGUI를 사용하기 위해 필요 (UI > Text - TextMeshPro를 사용했다면)
using Sirenix.OdinInspector;

public class SkillTimerUI : MonoBehaviour
{
    [Header("UI References")]
    public Image skillFillBar; // Fill Amount를 조절할 UI Image
    public GameObject skillUIPanel; // 스킬 UI 전체를 묶은 패널 (선택 사항)
    public TextMeshProUGUI skillUsageText; // "키를 다시 눌러 사용" 텍스트 (선택 사항)

    [Header("Skill Settings")]
    [Required, MinValue(1), MaxValue(30)] public float skillDuration = 5f; // 스킬이 지속될 시간 (초)

    private float _currentSkillTime; // 현재 남은 스킬 시간
    private bool _isSkillActive = false; // 스킬이 현재 활성화 중인지 여부

    void Start()
    {
        // 게임 시작 시 초기 상태 설정
        if (skillFillBar != null)
        {
            skillFillBar.fillAmount = 0f; // 처음에는 바를 비워둠
        }
        if (skillUIPanel != null)
        {
            skillUIPanel.SetActive(false); // 처음에는 스킬 UI 전체를 숨김
        }
    }

    void Update()
    {
        // 스킬 발동 키 입력 감지 (스킬이 비활성화 상태일 때만)
        if (!_isSkillActive && UseUISkill.finalSelectNum == 1)
        {
            StartSkillTimer(); // 스킬 타이머 시작
            OpenSkillSelectPannel._isUse = true;
        }

        // 스킬이 활성화 중일 때 시간 업데이트
        if (_isSkillActive)
        {
            _currentSkillTime -= Time.deltaTime; // 경과 시간만큼 남은 시간 감소

            // Fill Amount 업데이트: (남은 시간 / 전체 지속 시간)
            // 이 값이 1.0 (시작)에서 0.0 (종료)으로 줄어들게 됨
            skillFillBar.fillAmount = _currentSkillTime / skillDuration;

            // 스킬 종료 조건
            if (_currentSkillTime <= 0)
            {
                _currentSkillTime = 0; // 음수가 되지 않도록 방지
                _isSkillActive = false; // 스킬 비활성화

                skillFillBar.fillAmount = 0f; // 바를 완전히 비움
                Debug.Log("스킬 타이머 종료!");

                // 스킬 종료 시 UI 숨김
                if (skillUIPanel != null)
                {
                    skillUIPanel.SetActive(false);
                    UseUISkill.finalSelectNum = 0;
                    OpenSkillSelectPannel._isUse = false;
                }
            }
        }
    }

    // 외부에서 스킬을 발동할 때 호출할 함수 (예: 애니메이션 이벤트, 다른 스킬 시스템 등)
    public void StartSkillTimer()
    {
        if (skillFillBar == null)
        {
            Debug.LogError("Skill Fill Bar UI Image가 할당되지 않았습니다!");
            return;
        }

        _currentSkillTime = skillDuration; // 남은 시간을 스킬 지속 시간으로 초기화
        _isSkillActive = true; // 스킬 활성화 상태로 변경

        skillFillBar.fillAmount = 1f; // 바를 꽉 채운 상태로 시작

        // 스킬 시작 시 UI 패널을 보이게 함
        if (skillUIPanel != null)
        {
            skillUIPanel.SetActive(true);
        }

        // 스킬 사용 안내 텍스트 표시
        if (skillUsageText != null)
        {
            skillUsageText.gameObject.SetActive(true);
            skillUsageText.text = "Using Skill..."; // 스킬 발동 시 임시 텍스트
        }
    }
}