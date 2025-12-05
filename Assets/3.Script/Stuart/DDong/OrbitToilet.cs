using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitToilet : MonoBehaviour
{
    public Transform target;
    public GameObject poopPrefab;
    public float rotateSpeed =10f;
    public float shootInterval = 2f;

    private float shootTimer;

    private void Update()
    {
        if (target == null)
            return;

        transform.RotateAround(target.position, Vector3.up, rotateSpeed * Time.deltaTime);

        transform.LookAt(target);

        shootTimer += Time.deltaTime;
        if(shootTimer > shootInterval)
        {
            ShootPoop();
            shootTimer = 0;
        }
    }
    private void ShootPoop()
    {
        GameObject poop = Instantiate(poopPrefab, transform.position, Quaternion.identity);

        poop.transform.LookAt(target);
        poop.GetComponent<FlyPoop>().Setup(target);
    }

}
