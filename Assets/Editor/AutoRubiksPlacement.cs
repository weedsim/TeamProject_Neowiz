using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

/// <summary>
/// SpaceObjects 내 오브젝트를 3x3x3 루빅스 큐브 위치로 자동 배치하는 스크립트
/// </summary>
public class AutoRubiksPlacement : EditorWindow
{
    private static Dictionary<string, Vector3> placementMap;

    [MenuItem("Tools/Auto Arrange/Rubiks Cube Placement")]
    public static void ApplyRubiksPlacement()
    {
        GameObject root = Selection.activeGameObject;

        if (root == null)
        {
            Debug.LogError("SpaceObjects 를 선택하세요.");
            return;
        }

        BuildPlacementMap();  // 좌표 매핑 생성

        foreach (Transform child in root.GetComponentsInChildren<Transform>())
        {
            if (placementMap.ContainsKey(child.name))
            {
                Undo.RecordObject(child, "Rubiks Placement");
                child.position = placementMap[child.name];
                Debug.Log($"{child.name} → {child.position}");
            }
        }

        Debug.Log("루빅스 큐브 자동 배치 완료!");
    }

    /// <summary>
    /// 이름 → 좌표 매칭 테이블 생성
    /// </summary>
    private static void BuildPlacementMap()
    {
        float D = 800f; // 간격

        placementMap = new Dictionary<string, Vector3>()
        {
            // Z = +800 Layer
            ["Scatter_4"] = new Vector3(-D, -D, D),
            ["Scatter_1"] = new Vector3(0, -D, D),
            ["Scatter_2"] = new Vector3(D, -D, D),

            ["Scatter_5"] = new Vector3(-D, 0, D),
            ["Scatter_3"] = new Vector3(0, 0, D),
            ["SetPiece_1"] = new Vector3(D, 0, D),

            ["SetPiece_3"] = new Vector3(-D, D, D),
            ["Scatter_8"] = new Vector3(0, D, D),
            ["SetPiece_5"] = new Vector3(D, D, D),

            // Z = 0 Layer
            ["Scatter_6"] = new Vector3(-D, -D, 0),
            ["Scatter_7"] = new Vector3(0, -D, 0),
            ["SetPiece_2"] = new Vector3(D, -D, 0),

            ["Clusters1"] = new Vector3(-D, 0, 0),
            // (0,0,0) intentionally empty
            ["Clusters2"] = new Vector3(D, 0, 0),

            ["SetPiece_4"] = new Vector3(-D, D, 0),
            ["Scatter_12"] = new Vector3(0, D, 0),
            ["SetPiece_6"] = new Vector3(D, D, 0),

            // Z = -800 Layer
            ["Scatter_9"] = new Vector3(-D, -D, -D),
            ["Scatter_10"] = new Vector3(0, -D, -D),
            ["SetPieces3_1"] = new Vector3(D, -D, -D),

            ["Scatter_11"] = new Vector3(-D, 0, -D),
            ["SetPieces3_2"] = new Vector3(0, 0, -D),
            ["SetPieces3_3"] = new Vector3(D, 0, -D),

            ["SetPieces4_3"] = new Vector3(-D, D, -D),
            ["SetPieces4_6"] = new Vector3(0, D, -D),
            ["SetPieces4_5"] = new Vector3(D, D, -D)
        };
    }
}
