using UnityEngine;
using UnityEngine.InputSystem;
using System;
using System.Collections;
using System.Collections.Generic;

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

    private new Rigidbody rigidbody;
    private bool farting = false;
    private Vector3 targetDirection;
    private bool isfarting = false;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        CurrentGauge = MaxGauge;
    }

    private void OnMove(InputValue value)
    {
        if (isfarting) return;
        Mathf.Clamp(CurrentGauge, 0, MaxGauge);
        CurrentGauge -= FartCost;
        if (CurrentGauge <= 0f) // 안전장치
        {
            CurrentGauge = 0f;
        }
        isfarting = true;
        rigidbody.linearVelocity = Vector3.zero;
        StartCoroutine(Boostfarting_co());
        farting = value.isPressed;

        if (farting)
        {
            targetDirection = PlayCam.transform.forward;
        }
    }

    private void FixedUpdate()
    {
        if (farting)
        {
            rigidbody.AddForce(targetDirection * Force, ForceMode.Force);
        }
        if (CurrentGauge < MaxGauge)
        {
            StartCoroutine(FillFartGauge_co());
        }
        else if (CurrentGauge >= MaxGauge)
        {
            StopCoroutine(FillFartGauge_co());
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
        isfarting = false;
    }

    private IEnumerator FillFartGauge_co()
    {
        while (true)
        {
            if (CurrentGauge >= MaxGauge) break;
            CurrentGauge += FartRegenGauge;
            yield return new WaitForSeconds(FartRegenSpeed);
        }
    }
}
