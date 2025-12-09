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

        if (target != null && Vector3.Distance(transform.position, target.position) > 100f)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            // ȭ�� �� �޼���
            FindAnyObjectByType<DDongInEye>().AddPoop();

            Destroy(gameObject);
        }
    }
}
