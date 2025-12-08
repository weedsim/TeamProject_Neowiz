using UnityEngine;
using UnityEditor;

public class SetMeshColliderTriggerTool
{
    [MenuItem("Tools/Set All MeshColliders to Trigger")]
    private static void SetAllMeshCollidersToTrigger()
    {
        // SpaceObjects 루트를 탐색
        GameObject spaceObjects = GameObject.Find("SpaceObjects");

        if (spaceObjects == null)
        {
            Debug.LogError("'SpaceObjects' 오브젝트를 찾을 수 없습니다. 정확한 이름인지 확인해주세요.");
            return;
        }

        int count = 0;
        Transform root = spaceObjects.transform;

        // 모든 자식을 순회
        MeshCollider[] colliders = root.GetComponentsInChildren<MeshCollider>(true);

        foreach (MeshCollider mc in colliders)
        {
            if (mc != null)
            {
                mc.convex = true;
                mc.isTrigger = true;
                count++;
            }
        }

        Debug.Log($"MeshCollider {count}개를 Convex + isTrigger = true 로 변경했습니다!");
    }
}
