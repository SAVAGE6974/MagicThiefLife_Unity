using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerCrouch : MonoBehaviour
{
    // CharacterController 컴포넌트
    private CharacterController controller;
    
    // 플레이어의 카메라 트랜스폼
    [SerializeField] private Transform playerCamera;

    private float originalHeight;
    private Vector3 originalCenter;
    
    // 원래 카메라의 월드 Y 위치
    private float originalCameraWorldY;

    // 앉았을 때의 높이와 카메라 Y 위치
    [SerializeField] private float crouchHeight = 1.0f;
    [SerializeField] private float crouchCameraYOffset = -1.0f; // 앉았을 때 플레이어 위치로부터의 오프셋
    
    [SerializeField] private float transitionSpeed = 5.0f;

    private bool isCrouching = false;
    
    private float targetHeight;
    private Vector3 targetCenter;
    private float targetCameraWorldY; // 월드 Y 위치를 목표값으로 사용

    void Start()
    {
        controller = GetComponent<CharacterController>();

        originalHeight = controller.height;
        originalCenter = controller.center;

        if (playerCamera != null)
        {
            // 원래 카메라의 월드 Y 위치 저장
            originalCameraWorldY = playerCamera.position.y;
        }

        // 초기 목표값 설정
        targetHeight = originalHeight;
        targetCenter = originalCenter;
        targetCameraWorldY = originalCameraWorldY;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            ToggleCrouch();
        }

        SmoothTransition();

        // ... (이동 코드) ...
    }

    void ToggleCrouch()
    {
        if (isCrouching)
        {
            if (CanStandUp())
            {
                isCrouching = false;
                targetHeight = originalHeight;
                targetCenter = originalCenter;
                // 일어설 때 카메라 목표 위치
                targetCameraWorldY = originalCameraWorldY;
            }
        }
        else
        {
            isCrouching = true;
            targetHeight = crouchHeight;
            targetCenter = new Vector3(originalCenter.x, crouchHeight / 2f, originalCenter.z);
            // 앉을 때 카메라 목표 위치: 플레이어 Y 위치 + 오프셋
            targetCameraWorldY = transform.position.y + crouchCameraYOffset;
        }
    }

    void SmoothTransition()
    {
        controller.height = Mathf.Lerp(controller.height, targetHeight, Time.deltaTime * transitionSpeed);
        controller.center = Vector3.Lerp(controller.center, targetCenter, Time.deltaTime * transitionSpeed);

        if (playerCamera != null)
        {
            // 카메라의 월드 Y 위치를 부드럽게 변경
            Vector3 newCameraPos = playerCamera.position;
            newCameraPos.y = Mathf.Lerp(newCameraPos.y, targetCameraWorldY, Time.deltaTime * transitionSpeed);
            playerCamera.position = newCameraPos;
        }
    }

    // ... (CanStandUp 함수는 그대로) ...
    private bool CanStandUp()
    {
        Vector3 capsuleStart = transform.position + originalCenter - Vector3.up * (originalHeight / 2f - controller.radius);
        Vector3 capsuleEnd = transform.position + originalCenter + Vector3.up * (originalHeight / 2f - controller.radius);
        
        float capsuleRadius = controller.radius;

        int playerLayer = gameObject.layer;
        int layerMask = ~LayerMask.GetMask(LayerMask.LayerToName(playerLayer));

        Collider[] colliders = Physics.OverlapCapsule(capsuleStart, capsuleEnd, capsuleRadius, layerMask);

        if (colliders.Length > 0)
        {
            return false;
        }
        return true;
    }
}