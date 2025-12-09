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
    [SerializeField] private bool farting = false;
    [SerializeField] private Vector3 targetDirection;
    [SerializeField] private LayerMask _layerToDie;

    [SerializeField] private bool isBoosting = false;
    [SerializeField] private bool isFilling = false;

    public SkillCooldownUI girlSkillUI;//스킬쿨타임

    [Header("소리")]
    public AudioSource audioSource;
    public AudioClip FartClip;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = FartClip;
        //OnGirlSkill();
    }

    private void OnEnable()
    {
        PlayCam = Camera.main.gameObject;
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

            if(fartEffect != null)
            {
                fartEffect.Play();
                audioSource.PlayOneShot(FartClip);
            }

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
        //if(transform.position.x > 1600 || transform.position.x < -1600)
        //{
        //    transform.position = new Vector3(transform.position.x * -1, transform.position.y, transform.position.z);
        //}

        //if (transform.position.y > 1600 || transform.position.y < -1600)
        //{
        //    transform.position = new Vector3(transform.position.x, transform.position.y * -1, transform.position.z);
        //}

        //if (transform.position.z > 1600 || transform.position.z < -1600)
        //{
        //    transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z * -1);
        //}

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
        Vector3 DDong = Camera.main.transform.forward;
        rigidbody.linearVelocity = Camera.main.transform.forward * Boostfarting_Speed;

        yield return FartingCooltime; 

        while (rigidbody.linearVelocity.magnitude >= Force)
        {
            rigidbody.linearVelocity -= DDong;
            Debug.Log(rigidbody.linearVelocity);
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
    void OnGirlSkill() 
    {
        girlSkillUI.StartCooldown(60f); // 60초 쿨타임
    }


    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.layer);
        if(collision.gameObject.layer == 7)
        {
            Debug.Log("충돌로 인한 게임 오버");
            GameManager.Instance.GameOver();
        }
    }

}