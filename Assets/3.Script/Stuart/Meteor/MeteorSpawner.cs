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
        GameObject meteor = poolManager.GetMeteor(); // ������

        Vector3 spawnPos = player.position + (Random.onUnitSphere * 30f); //onUnitSphere = �� ǥ�鿡�� ���� ����

        meteor.transform.position = spawnPos;
        meteor.transform.LookAt(player); //�÷��̾� ����
        meteor.GetComponent<Meteor>().SetPool(poolManager, player);
    }

}
