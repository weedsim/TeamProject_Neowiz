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
        transform.Rotate(Vector3.up * 90 * Time.deltaTime); //ȸ��ȸ����~
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //�Ծ��� �� ȿ���ߵ��̶� �ݳ�
           
            EatEffect(other.gameObject);

            ReturnItem();
        }
        
    }
    private void EatEffect(GameObject player)
    {
        if (type == ItemType.sweetPotato)
        {
            player.GetComponent<AddForce>().CurrentGauge += 50;
        }

        else if (type == ItemType.Tissue)
        {
            //ȭ�� �ϳ��� �����
            DDongInEye clean = FindAnyObjectByType<DDongInEye>();
            if (clean != null)
            {
                Debug.Log("�� �۰��ڽɴ�~");
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
