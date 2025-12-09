using UnityEngine;
using System.Collections;

public class PlayerSkill : MonoBehaviour
{
    [SerializeField] private bool isSkill = false; // 스킬 사용상태인지
    public bool GilrSkill = false; // Girl 스킬 사용 상태인지
    private WaitForSeconds Coooltime = new WaitForSeconds(60f); // 스킬 쿨타임
    private WaitForSeconds GilrSkilltime = new WaitForSeconds(10f); // Girl 스킬 지속 시간
    private WaitForSeconds BoySkilltime = new WaitForSeconds(15f); // Boy 스킬 지속 시간
    [SerializeField] private AddForce addForce;
    private GameManager gameManager = GameManager.Instance;
    private UIManager _uiManager = null;
    

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

    public SkillEffectManager effectManager; // 

    private IEnumerator CoolTime_co()
    {
        isSkill = true;
        
        if(_uiManager == null)
        {
            GameObject.Find("UIManager").TryGetComponent(out _uiManager);
        }
        _uiManager.StartCoroutine(_uiManager.UpdateSkillColl_Co());

        // ����Ʈ ���� ��ȣ ������
        if (effectManager != null && GameManager.Instance != null)
        {
            effectManager.OnSkillStart();
        }           

        yield return Coooltime;
        isSkill = false;

        // ����Ʈ ���� ��ȣ ������
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

        Debug.Log("스킬 사용");
        if(effectManager == null)
        {
            GameObject.Find("skilleffect_tester").TryGetComponent(out effectManager);
        }

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
