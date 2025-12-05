using UnityEngine;
using UnityEngine.InputSystem;
using System;
using System.Collections;
using System.Collections.Generic;

public class AddForce : MonoBehaviour
{
    [Header("설정")]
    public GameObject PlayCam;
    public float Force = 20f;
    public float Boostfarting_Speed = 1000f;

    private Rigidbody rigidbody;
    private bool farting = false;
    private Vector3 targetDirection;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void OnMove(InputValue value)
    {
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
    }

    private IEnumerator Boostfarting_co()
    {
        Vector3 DDong = transform.forward;
        rigidbody.linearVelocity = transform.forward * Boostfarting_Speed;
        while (rigidbody.linearVelocity.magnitude >= Force)
        {
            rigidbody.linearVelocity -= DDong;
            yield return null;
            
        }
    }




}
