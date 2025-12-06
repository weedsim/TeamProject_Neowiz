using UnityEngine;
using UnityEditor;

public class PrintChildPositions : EditorWindow
{
    [MenuItem("Tools/Print Child Positions")]
    public static void ShowWindow()
    {
        GameObject obj = Selection.activeGameObject;

        if (obj == null)
        {
            Debug.LogWarning(" 아무 오브젝트도 선택되지 않았습니다. 좌표를 출력할 부모 오브젝트를 선택하세요.");
            return;
        }

        Debug.Log($"선택된 부모 오브젝트: {obj.name}");
        Debug.Log("자식 좌표 출력 시작 ---------------------------");

        PrintChildren(obj.transform);

        Debug.Log("자식 좌표 출력 완료 ---------------------------");
    }

    private static void PrintChildren(Transform parent)
    {
        foreach (Transform child in parent)
        {
            Vector3 pos = child.position;
            Debug.Log($"{child.name} → Position: ({pos.x:F2}, {pos.y:F2}, {pos.z:F2})");

            // 자식의 자식도 계속 출력하고 싶다면 아래 코드를 활성화:
            PrintChildren(child);
        }
    }
}
