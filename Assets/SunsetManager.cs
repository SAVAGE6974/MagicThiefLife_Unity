using UnityEngine;

[ExecuteAlways] // 에디터에서도 동작
public class SunsetManager : MonoBehaviour
{
    [Header("Lighting References")]
    public Light directionalLight; // 태양
    public Material skyboxMaterial; // Skybox Material (Skybox/Procedural 추천)

    [Header("Time Settings")]
    [Range(0f, 1f)] public float timeOfDay = 0.5f; // 0 = 자정, 0.5 = 정오
    public float daySpeed = 0.01f; // 자동 시간 흐름 속도

    [Header("Gradients")]
    public Gradient directionalLightColor;
    public Gradient ambientLightColor;
    public Gradient fogColor;
    public Gradient skyboxTintColor;

    void Update()
    {
        // 시간 흐름
        timeOfDay += Time.deltaTime * daySpeed;
        if (timeOfDay > 1f) timeOfDay -= 1f;

        UpdateLighting(timeOfDay);
    }

    void UpdateLighting(float timePercent)
    {
        // 태양 회전
        if (directionalLight != null)
        {
            directionalLight.transform.rotation = Quaternion.Euler((timePercent * 360f) - 90f, 170f, 0f);
            directionalLight.color = directionalLightColor.Evaluate(timePercent);
        }

        // 주변광 색
        RenderSettings.ambientLight = ambientLightColor.Evaluate(timePercent);

        // 안개 색
        RenderSettings.fogColor = fogColor.Evaluate(timePercent);

        // Skybox Tint
        if (skyboxMaterial != null)
        {
            skyboxMaterial.SetColor("_Tint", skyboxTintColor.Evaluate(timePercent));
        }
    }

    void OnValidate()
    {
        if (RenderSettings.sun != null)
            directionalLight = RenderSettings.sun;
    }
}
