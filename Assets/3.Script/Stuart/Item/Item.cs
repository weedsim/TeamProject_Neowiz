using UnityEngine;
using UnityEngine.Pool;

public enum ItemType
{
    sweetPotato,
    Tissue
}
public class Item : MonoBehaviour
{

    private AddForce addForce;
    public ItemType type;
    private IObjectPool<GameObject> iPool;


    private void Awake()
    {
        addForce = FindAnyObjectByType<AddForce>();
    }
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
        if (other.CompareTag("Player"))
        {
            //먹었을 때 효과발동이랑 반납
           
            EatEffect(other.gameObject);

            ReturnItem();
        }
        
    }
    private void EatEffect(GameObject player)
    {
        if (type == ItemType.sweetPotato)
        {
            addForce.CurrentGauge += 50;
        }

        else if (type == ItemType.Tissue)
        {
            //화면 하나씩 지우기
            DDongInEye clean = FindAnyObjectByType<DDongInEye>();
            if(clean != null)
            {
            Debug.Log("똥 퍼가겠심더~");
            clean.CleanScreen();

            }
            
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
