using UnityEngine;
using UnityEngine.EventSystems;

public class DebugCursorHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Texture2D hoverCursor;   // 호버 시 사용할 커서 이미지
    public Texture2D defaultCursor; // 기본 커서 이미지
    public Vector2 hotspot = Vector2.zero;

    private void Start()
    {
        // 시작할 때 기본 커서로 설정
        Cursor.SetCursor(defaultCursor, hotspot, CursorMode.Auto);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Cursor.SetCursor(hoverCursor, hotspot, CursorMode.Auto);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Cursor.SetCursor(defaultCursor, hotspot, CursorMode.Auto);
    }
}
