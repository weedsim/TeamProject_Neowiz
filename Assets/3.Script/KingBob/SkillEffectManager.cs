using UnityEngine;
using System.Collections;

public class SkillEffectManager : MonoBehaviour
{
    [Header("Overlay Skill Effect")]
    public CanvasGroup overlay;   

    private bool isRunning = false;

    // =============================
    //  skill start
    // =============================
    public void OnSkillStart()
    {
        if (isRunning) return;

        isRunning = true;

        StartCoroutine(OverlayEffect());
    }

    // =============================
    //  skill end
    // =============================
    public void OnSkillEnd()
    {
        isRunning = false;

        if (overlay != null)
            overlay.alpha = 0;
    }

    // =============================
    //  Overlay frame ggambackggamback
    // =============================
    private IEnumerator OverlayEffect()
    {
        while (isRunning)
        {            
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
