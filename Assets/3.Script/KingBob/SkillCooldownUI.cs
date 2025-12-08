using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SkillCooldownUI : MonoBehaviour
{
    public Image cooldownMask;
    public Text cooldownText;

    private bool isCooling = false;

    public void StartCooldown(float cooldownTime)
    {
        if (isCooling) return;
        StartCoroutine(CooldownRoutine(cooldownTime));
    }

    private IEnumerator CooldownRoutine(float cooldownTime)
    {
        isCooling = true;
        float timeLeft = cooldownTime;

        cooldownMask.fillAmount = 1;
        cooldownText.gameObject.SetActive(true);

        while (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            cooldownMask.fillAmount = timeLeft / cooldownTime;
            cooldownText.text = Mathf.Ceil(timeLeft).ToString();
            yield return null;
        }

        cooldownMask.fillAmount = 0;
        cooldownText.gameObject.SetActive(false);

        isCooling = false;
    }
}
