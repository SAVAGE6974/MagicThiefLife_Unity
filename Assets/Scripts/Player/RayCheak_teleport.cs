using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RayCheak_teleport : MonoBehaviour
{
    public Image CrossHire;
    public float maxDistance = 20f;

    // CrossHire의 기본 색상을 저장할 변수
    private Color defaultCrossHireColor;

    private void Start()
    {
        // 시작 시 CrossHire의 기본 색상을 저장합니다.
        if (CrossHire != null)
        {
            defaultCrossHireColor = CrossHire.color;
        }
    }

    private void Update()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (SkillTimerUI.IsSkill2Active && Physics.Raycast(ray, out hit, maxDistance))
        {
            if (hit.collider.CompareTag("entity"))
            {
                if (CrossHire != null)
                {
                    CrossHire.color = Color.yellow;
                }
            }
            else
            {
                if (CrossHire != null)
                {
                    CrossHire.color = defaultCrossHireColor;
                }
            }
        }
        else
        {
            if (CrossHire != null)
            {
                CrossHire.color = defaultCrossHireColor;
            }
        }
    }
}