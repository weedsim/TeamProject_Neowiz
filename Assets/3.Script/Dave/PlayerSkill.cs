using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerSkill : MonoBehaviour
{
    private bool isSkill = false; // 籐顫歜
    public bool GilrSkill = false; // 籐顫歜
    private WaitForSeconds Coooltime = new WaitForSeconds(60f); // 議諒 六六
    private WaitForSeconds GilrSkilltime = new WaitForSeconds(10f); // 議諒 六六
    private WaitForSeconds BoySkilltime = new WaitForSeconds(15f); // 議諒 六六
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

    private IEnumerator CoolTime_co()
    {
        isSkill = true;
        yield return Coooltime;
        isSkill = false;
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
