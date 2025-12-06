using UnityEngine;
using UnityEditor;

public class FillSlots : EditorWindow
{
    Transform slotParent;
    Transform sourceSlot;

    [MenuItem("Tools/Fill All Slots")]
    public static void ShowWindow()
    {
        GetWindow<FillSlots>("Fill All Slots");
    }

    private void OnGUI()
    {
        GUILayout.Label("Slot 자동 채우기", EditorStyles.boldLabel);

        slotParent = EditorGUILayout.ObjectField("Slot Parent (SpaceObjects)", slotParent, typeof(Transform), true) as Transform;
        sourceSlot = EditorGUILayout.ObjectField("Source Slot (slot 0)", sourceSlot, typeof(Transform), true) as Transform;

        if (GUILayout.Button("27개 슬롯 자동 구성"))
        {
            if (slotParent == null || sourceSlot == null)
            {
                Debug.LogError("slotParent 또는 sourceSlot이 설정되지 않음!");
                return;
            }

            for (int i = 0; i < slotParent.childCount; i++)
            {
                Transform slot = slotParent.GetChild(i);

                if (slot == sourceSlot)
                    continue; // slot0은 건너뜀

                // slot 안 기존 내용 삭제
                foreach (Transform child in slot)
                    DestroyImmediate(child.gameObject);

                // slot0의 자식들을 복제해서 넣기
                foreach (Transform child in sourceSlot)
                {
                    Transform clone = Instantiate(child, slot);
                    clone.localPosition = child.localPosition;
                    clone.localRotation = child.localRotation;
                    clone.localScale = child.localScale;
                }

                Debug.Log($"{slot.name} → 채움 완료!");
            }

            Debug.Log("모든 슬롯 채우기 완료!");
        }
    }
}
