using UnityEngine;
using UnityEngine.Pool;

public class ItemSpawner : MonoBehaviour
{
    public GameObject itemPrefab;
    public GameObject _SweetPhotato_Prefab;
    public GameObject _Tissue_Prefab;
    public Transform player;

    private IObjectPool<GameObject> iPool;
    private IObjectPool<GameObject> _iPool_Sweet;
    private IObjectPool<GameObject> _iPool_Tissue;
    private float timer;
    
    private void Awake()
    {

        iPool = new ObjectPool<GameObject>(createFunc: () => { GameObject item = Instantiate(itemPrefab);
            item.GetComponent<Item>().Setup(iPool);
            return item;
        },
        actionOnGet: (Item) => Item.SetActive(true), 
        actionOnRelease : (item) => item.SetActive(false), 
        actionOnDestroy: (item) => Destroy(item), maxSize: 20) ;

        _iPool_Sweet = new ObjectPool<GameObject>(createFunc: () => {
            GameObject item = Instantiate(_SweetPhotato_Prefab);
            item.GetComponent<Item>().Setup(_iPool_Sweet);
            return item;
        },
        actionOnGet: (Item) => Item.SetActive(true),
        actionOnRelease: (item) => item.SetActive(false),
        actionOnDestroy: (item) => Destroy(item), maxSize: 20);

        _iPool_Tissue = new ObjectPool<GameObject>(createFunc: () => {
            GameObject item = Instantiate(_Tissue_Prefab);
            item.GetComponent<Item>().Setup(_iPool_Tissue);
            return item;
        },
        actionOnGet: (Item) => Item.SetActive(true),
        actionOnRelease: (item) => item.SetActive(false),
        actionOnDestroy: (item) => Destroy(item), maxSize: 20);


    }

    private void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if(timer > 5f)
        {
            timer = 0;
            SpawnItem();
        }
    }
    private void SpawnItem()
    {

        int random_int = Random.Range(0, 2);
        Vector3 randomPos = player.position + (Random.insideUnitSphere * 20f);

        switch (random_int)
        {
            case 0:
                GameObject sweetPhtoato = _iPool_Sweet.Get();

                sweetPhtoato.transform.position = randomPos;

                break;

            case 1:
                GameObject tissue = _iPool_Tissue.Get();

                tissue.transform.position = randomPos;

                break;

            default:
                GameObject item = iPool.Get();

                item.transform.position = randomPos;
                break;
        }
    }
}
