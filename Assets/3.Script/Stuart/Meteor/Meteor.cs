using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    private MeteorPool meteorPool;
    private Transform player;
    private PlayerSkill playerSkill;
    public float speed = 10f;


    public void SetPool(MeteorPool pool, Transform playerTransform)
    {
        meteorPool = pool;
        player = playerTransform;
    }
    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if(player != null)
        {
            float distance = Vector3.Distance(transform.position, player.position);
            if (distance > 50f) // �Ÿ��� ���� �̻� �־�����
            {
                ReturnPool();
            }
        }
    }
    private void ReturnPool()
    {
        if(meteorPool != null)
        {
            meteorPool.ReturnMeteor(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<PlayerSkill>().GilrSkill) return;

            if (GameManager.Instance != null)
            {
                GameManager.Instance.GameOver();
            }
            ReturnPool();
        }
    }




}
