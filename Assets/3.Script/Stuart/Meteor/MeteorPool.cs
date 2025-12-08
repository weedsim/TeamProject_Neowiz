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
            int lastIndex = pool.Count - 1; //������������ �ؼ� ŷ��ȭ�� �ߵ� ����Ʈ�� �߰����� �������� �� �и��ϱ�

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
