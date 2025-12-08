using UnityEngine;
using UnityEngine.Pool;

public class ItemSpawner : MonoBehaviour
{
    public GameObject itemPrefab;
    public Transform player;

    private IObjectPool<GameObject> iPool;
    private float timer;
    //��ųʸ� ���� 1���� ������
    private void Awake()
    {
        //Ǯ�ʱ�ȭ

        iPool = new ObjectPool<GameObject>(createFunc: () => { GameObject item = Instantiate(itemPrefab);
            item.GetComponent<Item>().Setup(iPool);
            return item;
        },
        actionOnGet: (Item) => Item.SetActive(true), 
        actionOnRelease : (item) => item.SetActive(false), 
        actionOnDestroy: (item) => Destroy(item), maxSize: 20) ;  
    }//�ϴ� ����Ƽ Ǯ �̿��غ����� ���µ� �� ��Ʊ���
    private void Update()
    {
        timer += Time.deltaTime;
        if(timer > 5f) //���� �ʽð�
        {
            timer = 0;
            SpawnItem();
        }
    }
    private void SpawnItem()
    {
        GameObject item = iPool.Get();

        Vector3 randomPos = player.position + (Random.insideUnitSphere * 20f);//�ֺ����� ����
        item.transform.position = randomPos;
    }
}
