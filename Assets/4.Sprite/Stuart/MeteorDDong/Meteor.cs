using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    private MeteorPool meteorPool;
    private Transform player;
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
            if (distance > 50f) // 거리가 일정 이상 멀어지면
            {
                //반환 메소드
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

}
