using UnityEngine;
using System.Collections;

public class PlayerSkill : MonoBehaviour
{
    private bool isSkill = false; // 쿨타임
    private WaitForSeconds Coooltime = new WaitForSeconds(60f); // 캐싱 ㅋㅋ

    public SkillEffectManager effectManager; // 스킬 이펙트 연결

    private IEnumerator CoolTime_co()
    {
        isSkill = true;

        // 이펙트 종료 신호 보내기
        if (effectManager != null && GameManager.Instance != null)
        {
            effectManager.OnSkillStart(GameManager.Instance._ChooseCharacter);
        }           

        yield return Coooltime;
        isSkill = false;

        // 이펙트 종료 신호 보내기
        if (effectManager != null && GameManager.Instance != null)
        {
            effectManager.OnSkillEnd(GameManager.Instance._ChooseCharacter);
        }
            
    }

    private void OnClick()
    {
        if (isSkill) return;

        switch (GameManager.Instance._ChooseCharacter)
        {
            case "Girl":
                break;
            case "Boy":
                break;
            case "Go":
                break;

            default: return;
        }
        StartCoroutine(CoolTime_co());
    }

}
