using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyPoop : MonoBehaviour
{
    private Transform target;
    public float speed = 10f;

    public void Setup(Transform target)
    {
        this.target = target;
    }
    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if (target != null && Vector3.Distance(transform.position, target.position) > 30f)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            // 화면 똥 메서드
            FindAnyObjectByType<DDongInEye>().AddPoop();

            Destroy(gameObject);
        }
    }
}
