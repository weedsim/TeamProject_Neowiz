using UnityEngine;
using UnityEngine.Pool;

public enum ItemType
{
    sweetPotato,
    Tissue
}
public class Item : MonoBehaviour
{
    public ItemType type;
    private IObjectPool<GameObject> iPool;

    public void Setup(IObjectPool<GameObject> pool)
    {
        iPool = pool;
    }
    private void Update()
    {
        transform.Rotate(Vector3.up * 90 * Time.deltaTime); //회전회오리~
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            //먹었을 때 효과발동이랑 반납
        }
    }
    private void EatEffect(GameObject player)
    {
        if (type == ItemType.sweetPotato)
        {
            //플레이어에서 이속부스터 가져오기 (대한님)
        }
        else if (type == ItemType.Tissue)
        {
            //화면 하나씩 지우기
        }
    }
    public void ReturnItem()
    {
        if(iPool != null)
        {
            iPool.Release(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
