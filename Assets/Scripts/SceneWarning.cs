using UnityEngine;
using UnityEngine.SceneManagement;
using Sirenix.OdinInspector;

public class SceneWarning : MonoBehaviour
{
    [ReadOnly, ShowInInspector]
    [InfoBox("@GetWarningMessage()", InfoMessageType.Warning)]
    private string dummy;

    private string GetWarningMessage()
    {
        return "해당 씬에서 마우스감도 실시간변경에 대한 이벤트가 없습니다. 해당씬을 만들어주세요";
    }
}
