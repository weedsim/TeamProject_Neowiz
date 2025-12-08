using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PlayerSkill : MonoBehaviour
{
    private bool isSkill = false; // ÄðÅ¸ÀÓ
    private WaitForSeconds Coooltime = new WaitForSeconds(60f); // Ä³½Ì ¤»¤»

    private IEnumerator CoolTime_co()
    {
        isSkill = true;
        yield return Coooltime;
        isSkill = false;
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
