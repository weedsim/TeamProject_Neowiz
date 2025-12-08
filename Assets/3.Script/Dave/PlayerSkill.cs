using UnityEngine;
using System.Collections;

public class PlayerSkill : MonoBehaviour
{
    private bool isSkill = false; 
    public bool GilrSkill = false; 
    private WaitForSeconds Coooltime = new WaitForSeconds(60f); 
    private WaitForSeconds GilrSkilltime = new WaitForSeconds(10f); 
    private WaitForSeconds BoySkilltime = new WaitForSeconds(15f);
    private AddForce addForce;
    private GameManager gameManager = GameManager.Instance;
    

    private void Awake()
    {
        TryGetComponent(out addForce);
    }

    private void GoSkill()
    {
        int GoSkillNum = Random.Range(1, 4);

        switch (GoSkillNum)
        {
            case 1:
                gameManager._Time = 0;
                break;
            case 2:
                gameManager._Time = gameManager._Time / 2;
                break;
            case 3:
                gameManager._Time = gameManager._Time * 2;
                break;
            default: return;
        }
    }

    public SkillEffectManager effectManager; 

    private IEnumerator CoolTime_co()
    {
        isSkill = true;

         if (effectManager != null && GameManager.Instance != null)
        {
            effectManager.OnSkillStart();
        }           

        yield return Coooltime;
        isSkill = false;

        if (effectManager != null && GameManager.Instance != null)
        {
            effectManager.OnSkillEnd();
        }
            
    }

    private IEnumerator BoySkill_co()
    {
        addForce.FartCost = 0f;
        yield return BoySkilltime;
        addForce.FartCost = 20f;
    }

    private IEnumerator GirlSkill_co()
    {
        GilrSkill = true;
        yield return GilrSkilltime;
        GilrSkill = false;
    }

    private void OnClick()
    {
        if (isSkill) return;

        switch (GameManager.Instance._ChooseCharacter)
        {
            case "Girl":
                StartCoroutine(GirlSkill_co());
                break;
            case "Boy":
                StartCoroutine(BoySkill_co());
                break;
            case "Go":GoSkill();
                break;

            default: return;
        }
        StartCoroutine(CoolTime_co());
    }

}
