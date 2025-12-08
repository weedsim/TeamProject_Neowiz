using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class AddForce : MonoBehaviour
{
    [Header("설정")]
    public GameObject PlayCam;
    public float Force = 30f;
    public float Boostfarting_Speed = 100f;
    public WaitForSeconds FartingCooltime = new WaitForSeconds(2f);
    public float MaxGauge = 100f;
    public float CurrentGauge = 0f;
    public float FartCost = 20f;
    public float FartRegenSpeed = 1f;
    public float FartRegenGauge = 2f;
    public ParticleSystem fartEffect; 

    private new Rigidbody rigidbody;
    private bool farting = false;
    private Vector3 targetDirection;

    private bool isBoosting = false;
    private bool isFilling = false;

    public SkillCooldownUI girlSkillUI;//스킬쿨타임

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        OnGirlSkill();
    }

    private void OnEnable()
    {
        CurrentGauge = MaxGauge;
    }

    private void OnMove(InputValue value)
    {
        if (isBoosting) return;

        if (value.isPressed)
        {
            if (CurrentGauge < FartCost) return;

            CurrentGauge -= FartCost;
            CurrentGauge = Mathf.Clamp(CurrentGauge, 0, MaxGauge);

            isBoosting = true;
            rigidbody.linearVelocity = Vector3.zero;

            fartEffect.Play(); 

            StartCoroutine(Boostfarting_co());

            targetDirection = PlayCam.transform.forward;
            farting = true;
        }
        else
        {
            farting = false;
        }
    }

    private void FixedUpdate()
    {
        if (farting)
        {
            rigidbody.AddForce(targetDirection * Force, ForceMode.Force);
        }

        if (CurrentGauge < MaxGauge && !isFilling)
        {
            StartCoroutine(FillFartGauge_co());
        }
        else if (CurrentGauge >= MaxGauge)
        {
            StopCoroutine(FillFartGauge_co());
            isFilling = false; 
        }
    }

    private IEnumerator Boostfarting_co()
    {
        Vector3 DDong = transform.forward;
        rigidbody.linearVelocity = transform.forward * Boostfarting_Speed;

        yield return FartingCooltime; 

        while (rigidbody.linearVelocity.magnitude >= Force)
        {
            rigidbody.linearVelocity -= DDong;
            yield return null;
        }

        isBoosting = false;
    }

    private IEnumerator FillFartGauge_co()
    {
        isFilling = true; 

        while (CurrentGauge < MaxGauge)
        {
            yield return new WaitForSeconds(FartRegenSpeed);
            CurrentGauge += FartRegenGauge;
            CurrentGauge = Mathf.Clamp(CurrentGauge, 0, MaxGauge);
        }

        isFilling = false; 
    }

    //스킬 쿨타임
    private void OnGirlSkill() 
    {
        girlSkillUI.StartCooldown(60f); // 60초 쿨타임
    }

}