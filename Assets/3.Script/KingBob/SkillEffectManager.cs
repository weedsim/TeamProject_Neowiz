using UnityEngine;
using System.Collections;

public class SkillEffectManager : MonoBehaviour
{
    [Header("Overlay Skill Effect")]
    public CanvasGroup overlay;   // 테두리 오버레이 이미지 (CanvasGroup)

    private bool isRunning = false;

    // =============================
    //  스킬 시작
    // =============================
    public void OnSkillStart()
    {
        if (isRunning) return;

        isRunning = true;

        StartCoroutine(OverlayEffect());
    }

    // =============================
    //  스킬 종료
    // =============================
    public void OnSkillEnd()
    {
        isRunning = false;

        if (overlay != null)
            overlay.alpha = 0;
    }

    // =============================
    //  Overlay 테두리 깜빡임
    // =============================
    private IEnumerator OverlayEffect()
    {
        while (isRunning)
        {
            // 부드러운 진동/깜빡임 효과
            if (overlay != null)
                overlay.alpha = Mathf.PingPong(Time.time * 1.2f, 0.4f);

            yield return null;
        }

        if (overlay != null)
            overlay.alpha = 0;
    }

    private void Start()
    {
        OnSkillStart();
    }
}
