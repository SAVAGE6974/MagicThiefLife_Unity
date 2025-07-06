using System.ComponentModel;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem; // New Input System
using UnityEngine.UI;

public class UseUISkill : MonoBehaviour
{
    public GameObject target; // Unity 에디터에서 여기에 움직임을 따라갈 UI 패널을 드래그 앤 드롭하세요.

    [Header("중앙 영역 설정")] public float centralAreaSize = 300f; // 중앙 영역의 가로/세로 길이 (픽셀)

    [Header("오브젝트")] public GameObject top;
    public GameObject bottom;
    public GameObject left;
    public GameObject right;
    
    [Header("텍스트")] public Text text;

    // 현재 선택된 스킬을 추적하는 변수
    private int selectedSkill = 0; // 0: 선택되지 않음, 1: 상단, 2: 우측, 3: 하단, 4: 좌측
    
    public static int finalSelectNum = 0;
    

    private void Awake()
    {
        // 시작 시 UI 패널과 모든 방향 표시를 비활성화합니다.
        target.SetActive(false); 
        top.SetActive(false);
        bottom.SetActive(false);
        left.SetActive(false);
        right.SetActive(false);
        text.text = ""; // 시작 시 텍스트도 비워둡니다.
    }

    void Update()
    {
        if (!OpenSkillSelectPannel._isUse)
        {
            // 1. Q 키를 누르고 있을 때 (Hold)
            if (Keyboard.current.qKey.isPressed)
            {
                // UI 패널이 비활성화 상태였다면 활성화합니다.
                if (!target.activeSelf)
                {
                    target.SetActive(true);
                }

                // 현재 마우스 포인터의 스크린 좌표를 읽어옵니다.
                Vector2 mousePosition = Mouse.current.position.ReadValue();

                // 화면의 중앙점과 중앙 영역의 크기를 계산합니다.
                float screenWidth = Screen.width;
                float screenHeight = Screen.height;
                Vector2 screenCenter = new Vector2(screenWidth / 2f, screenHeight / 2f);
                float halfCentralArea = centralAreaSize / 2f;
                float centerXMin = screenCenter.x - halfCentralArea;
                float centerXMax = screenCenter.x + halfCentralArea;
                float centerYMin = screenCenter.y - halfCentralArea;
                float centerYMax = screenCenter.y + halfCentralArea;

                // --- 마우스 위치에 따라 스킬 선택 상태 및 UI 업데이트 ---
                if (mousePosition.x >= centerXMin && mousePosition.x <= centerXMax &&
                    mousePosition.y >= centerYMin && mousePosition.y <= centerYMax)
                {
                    // 중앙 영역 (스킬 선택 없음)
                    top.SetActive(false);
                    bottom.SetActive(false);
                    left.SetActive(false);
                    right.SetActive(false);
                    text.text = "마우스를 이동하여 스킬 선택 \n키를 해제하여 취소하기";
                    selectedSkill = 0;
                }
                else // 중앙 영역 밖이라면 상하좌우 판단
                {
                    float horizontalDistance = Mathf.Abs(mousePosition.x - screenCenter.x);
                    float verticalDistance = Mathf.Abs(mousePosition.y - screenCenter.y);

                    if (horizontalDistance > verticalDistance)
                    {
                        // X축 거리가 더 클 경우 (좌/우)
                        if (mousePosition.x < screenCenter.x) // 왼쪽
                        {
                            top.SetActive(false);
                            bottom.SetActive(false);
                            left.SetActive(true);
                            right.SetActive(false);
                            text.text = "4번 스킬 선택됨 \n키를 해제하여 활성화";
                            selectedSkill = 4;
                        }
                        else // 오른쪽
                        {
                            top.SetActive(false);
                            bottom.SetActive(false);
                            left.SetActive(false);
                            right.SetActive(true);
                            text.text = "2번 스킬 선택됨 \n키를 해제하여 활성화";
                            selectedSkill = 2;
                        }
                    }
                    else
                    {
                        // Y축 거리가 더 크거나 같을 경우 (상/하)
                        if (mousePosition.y > screenCenter.y) // 위쪽
                        {
                            top.SetActive(true);
                            bottom.SetActive(false);
                            left.SetActive(false);
                            right.SetActive(false);
                            text.text = "1번 스킬 선택됨 \n키를 해제하여 활성화";
                            selectedSkill = 1;
                        }
                        else // 아래쪽
                        {
                            top.SetActive(false);
                            bottom.SetActive(true);
                            left.SetActive(false);
                            right.SetActive(false);
                            text.text = "3번 스킬 선택됨 \n키를 해제하여 활성화";
                            selectedSkill = 3;
                        }
                    }
                }
            }
            // 2. Q 키에서 손을 뗄 때 (Release)
            else if (Keyboard.current.qKey.wasReleasedThisFrame)
            {
                // UI 패널을 즉시 비활성화합니다.
                target.SetActive(false);
                top.SetActive(false);
                bottom.SetActive(false);
                left.SetActive(false);
                right.SetActive(false);
                text.text = "";

                // Q 키를 떼는 순간 마지막으로 선택된 스킬을 발동합니다.
                if (selectedSkill != 0)
                {
                    // 스킬 활성화 메시지를 디버그 콘솔에 출력합니다.
                    Debug.Log($"Q 키를 떼서 {selectedSkill}번 스킬이 활성화되었습니다!");
                    if (selectedSkill == 1)
                        finalSelectNum = selectedSkill;
                    else if (selectedSkill == 2)
                        finalSelectNum = selectedSkill;
                    else if (selectedSkill == 3)
                        finalSelectNum = selectedSkill;
                    else if (selectedSkill == 4)
                        finalSelectNum = selectedSkill;
                }
                else
                {
                    // 스킬을 선택하지 않은 채로 Q 키를 뗐을 때의 처리
                    Debug.Log("스킬이 선택되지 않았으므로 취소되었습니다.");
                }

                // 상태를 초기화합니다.
                selectedSkill = 0;
            }
        }
    }
    
    IEnumerator WaitAndDoSomething(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
    }
}