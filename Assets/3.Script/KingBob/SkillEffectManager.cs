using UnityEngine;
using System.Collections;

public class SkillEffectManager : MonoBehaviour
{
    [Header("Common Visual Effects")]
    public CanvasGroup overlay;
    public GameObject aura;
    public Transform cam;

    private bool isRunning = false;

    // =============================
    //  스킬 시작
    // =============================
    public void OnSkillStart(string character)
    {
        isRunning = true;

        switch (character)
        {
            case "Girl":
                StartCoroutine(GirlEffect());
                break;

            case "Boy":
                StartCoroutine(BoyEffect());
                break;

            case "Go":
                StartCoroutine(GoEffect());
                break;
        }
    }

    // =============================
    //  스킬 종료
    // =============================
    public void OnSkillEnd(string character)
    {
        isRunning = false;

        overlay.alpha = 0;
        aura.SetActive(false);
        cam.localPosition = Vector3.zero;
    }

    // =============================
    //  캐릭터별 스킬 이펙트
    // =============================

    private IEnumerator GirlEffect()
    {
        aura.SetActive(true);

        while (isRunning)
        {
            overlay.alpha = Mathf.PingPong(Time.time * 0.8f, 0.35f);
            yield return null;
        }
    }

    private IEnumerator BoyEffect()
    {
        aura.SetActive(true);

        // 한 번의 카메라 흔들림
        StartCoroutine(CameraShake());

        while (isRunning)
        {
            overlay.alpha = Mathf.PingPong(Time.time * 0.5f, 0.4f);
            yield return null;
        }
    }

    private IEnumerator GoEffect()
    {
        aura.SetActive(true);

        while (isRunning)
        {
            overlay.alpha = Mathf.PingPong(Time.time * 1.2f, 0.25f);
            yield return null;
        }
    }

    // =============================
    //  카메라 흔들림
    // =============================
    private IEnumerator CameraShake()
    {
        Vector3 origin = cam.localPosition;
        float t = 0f;

        while (t < 0.2f)
        {
            t += Time.deltaTime;
            cam.localPosition = origin + (Vector3)Random.insideUnitCircle * 0.04f;
            yield return null;
        }

        cam.localPosition = origin;
    }
}
