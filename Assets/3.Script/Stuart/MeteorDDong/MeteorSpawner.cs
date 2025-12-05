using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    public MeteorPool poolManager;
    public Transform player;

    private float timeSpawn;

    private void Update()
    {
        timeSpawn += Time.deltaTime;

        if(timeSpawn >= 0.1f)
        {
            timeSpawn = 0f;
            SpawnMeteor();
        }
    }
    private void SpawnMeteor()
    {
        GameObject meteor = poolManager.GetMeteor(); // 꺼내잇

        Vector3 spawnPos = player.position + (Random.onUnitSphere * 30f); //onUnitSphere = 구 표면에서 랜덤 생성

        meteor.transform.position = spawnPos;
        meteor.transform.LookAt(player); //플레이어 보기
        meteor.GetComponent<Meteor>().SetPool(poolManager, player);
    }

}
