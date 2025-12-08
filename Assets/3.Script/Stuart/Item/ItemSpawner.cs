using UnityEngine;
using UnityEngine.Pool;

public class ItemSpawner : MonoBehaviour
{
    public GameObject itemPrefab;
    public Transform player;

    private IObjectPool<GameObject> iPool;
    private float timer;
    //딕셔너리 쓰면 1개만 만들어도됨
    private void Awake()
    {
        //풀초기화

        iPool = new ObjectPool<GameObject>(createFunc: () => { GameObject item = Instantiate(itemPrefab);
            item.GetComponent<Item>().Setup(iPool);
            return item;
        },
        actionOnGet: (Item) => Item.SetActive(true), 
        actionOnRelease : (item) => item.SetActive(false), 
        actionOnDestroy: (item) => Destroy(item), maxSize: 20) ;  
    }//일단 유니티 풀 이용해보려고 썻는데 핵 어렵긴함
    private void Update()
    {
        timer += Time.deltaTime;
        if(timer > 5f) //스폰 초시계
        {
            timer = 0;
            SpawnItem();
        }
    }
    private void SpawnItem()
    {
        GameObject item = iPool.Get();

        Vector3 randomPos = player.position + (Random.insideUnitSphere * 20f);//주변에서 생성
        item.transform.position = randomPos;
    }
}
