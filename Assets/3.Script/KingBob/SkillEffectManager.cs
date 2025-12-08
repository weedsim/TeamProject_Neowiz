using UnityEngine;
using System.Collections;

public class SkillEffectManager : MonoBehaviour
{
    [Header("Common Skill Effect")]
    public CanvasGroup overlay;   // 테두리 오버레이 (캔버스 내 Image)
    public GameObject aura;       // 캐릭터 Glow 효과
    public Transform cam;         // 카메라
    public bool useCameraShake = true;

    private bool isRunning = false;

    // =============================
    //  스킬 시작 (공통 이펙트)
    // =============================
    public void OnSkillStart()
    {
        if (isRunning) return;

        isRunning = true;

        if (aura != null)
            aura.SetActive(true);

        StartCoroutine(OverlayEffect());

        /*if (useCameraShake)
        {
            StartCoroutine(CameraShake());
        }*/
            
    }

    // =============================
    //  스킬 종료
    // =============================
    public void OnSkillEnd()
    {
        isRunning = false;

        if (aura != null)
            aura.SetActive(false);

        if (overlay != null)
            overlay.alpha = 0;

        if (cam != null)
            cam.localPosition = Vector3.zero;
    }

    // =============================
    //  Overlay 테두리 깜빡임
    // =============================
    private IEnumerator OverlayEffect()
    {
        while (isRunning)
        {
            // 부드러운 깜빡임
            overlay.alpha = Mathf.PingPong(Time.time * 1.2f, 0.4f);
            yield return null;
        }

        overlay.alpha = 0;
    }

    // =============================
    //  카메라 흔들림 (옵션)
    // =============================
    private IEnumerator CameraShake()
    {
        Vector3 origin = cam.localPosition;
        float duration = 0.25f;
        float t = 0f;

        while (t < duration)
        {
            t += Time.deltaTime;
            cam.localPosition = origin +
                (Vector3)Random.insideUnitCircle * 0.03f;
            yield return null;
        }

        cam.localPosition = origin;
    }
}
