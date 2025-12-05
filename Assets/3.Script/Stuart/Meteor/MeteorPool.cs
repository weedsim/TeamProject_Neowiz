using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorPool : MonoBehaviour
{
    public GameObject meteorPrefabs;

    private List<GameObject> pool = new List<GameObject>(100);

    public GameObject GetMeteor()
    {
        if (pool.Count > 0)
        {
            int lastIndex = pool.Count - 1; //마지막에서만 해서 킹적화가 잘됨 리스트로 중간부터 꺼내오면 다 밀리니까

            GameObject obj = pool[lastIndex];
            pool.RemoveAt(lastIndex);

            obj.SetActive(true);
            return obj;
        }
        else
        {
            GameObject newObj = Instantiate(meteorPrefabs);
            return newObj;
        }
    }
    public void ReturnMeteor(GameObject meteor)
    {
        meteor.SetActive(false); 
        pool.Add(meteor);
    }
}
