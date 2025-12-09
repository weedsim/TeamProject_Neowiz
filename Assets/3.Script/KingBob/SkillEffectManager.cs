using UnityEngine;
using System.Collections;

public class SkillEffectManager : MonoBehaviour
{
    [Header("Overlay Skill Effect")]
    public CanvasGroup overlay;   // �׵θ� �������� �̹��� (CanvasGroup)

    private bool isRunning = false;

    // =============================
    //  ��ų ����
    // =============================
    public void OnSkillStart()
    {
        if (isRunning) return;

        isRunning = true;

        StartCoroutine(OverlayEffect());
    }

    // =============================
    //  ��ų ����
    // =============================
    public void OnSkillEnd()
    {
        isRunning = false;

        if (overlay != null)
            overlay.alpha = 0;
    }

    // =============================
    //  Overlay �׵θ� �����
    // =============================
    private IEnumerator OverlayEffect()
    {
        while (isRunning)
        {
            // �ε巯�� ����/����� ȿ��
            if (overlay != null)
                overlay.alpha = Mathf.PingPong(Time.time * 1.2f, 0.4f);

            yield return null;
        }

        if (overlay != null)
            overlay.alpha = 0;
    }

    private void Start()
    {
        //OnSkillStart();
    }
}
