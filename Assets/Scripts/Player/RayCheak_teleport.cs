using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RayCheak_teleport : MonoBehaviour
{
    public Image CrossHire;
    public float maxDistance = 20f;
    public GameObject player;

    private Color defaultCrossHireColor;

    private void Start()
    {
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

                if (Input.GetMouseButtonDown(0)) // 좌클릭
                {
                    Vector3 targetPosition = hit.collider.transform.position;
                    Vector3 teleportPosition = targetPosition + new Vector3(-1f, 0f, 0f);

                    if (player != null)
                    {
                        CharacterController cc = player.GetComponent<CharacterController>();
                        if (cc != null)
                        {
                            cc.enabled = false;
                            player.transform.position = teleportPosition;
                            cc.enabled = true;
                        }
                        else
                        {
                            player.transform.position = teleportPosition;
                        }

                        SkillTimerUI.Instance.ForceEndSkill(2);
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
        else
        {
            if (CrossHire != null)
            {
                CrossHire.color = defaultCrossHireColor;
            }
        }
    }
}
