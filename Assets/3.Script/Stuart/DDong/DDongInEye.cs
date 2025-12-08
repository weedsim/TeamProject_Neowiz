using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DDongInEye : MonoBehaviour
{
    public GameObject poopUIPrefab;
    public Transform canvasTransform;

    [Header("���ΰ� �ƴѰ�")]
    public bool isCute = false;
    public Sprite realPoop;
    public Sprite cutePoop;

    private List<GameObject> stain_List = new List<GameObject>();

    public void AddPoop()
    {
        GameObject stain = Instantiate(poopUIPrefab, canvasTransform);

        Image imgChange = stain.GetComponent<Image>(); // ���ΰ� �ƴѰ�!
        if (isCute == true)
        {
            imgChange.sprite = cutePoop;
        }
        else
        {
            imgChange.sprite = realPoop;
        }
        //ȭ�鿡�� �����ϰ� ����
        float x = Random.Range(-900f, 900f);
        float y = Random.Range(-500f, 500f);
        stain.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);

        stain.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0f, 360f));

        stain_List.Add(stain);
    }
    public void CheckCute(bool isOn)
    {
        isCute = isOn;
    }
    //���������� ����� �� �߰��ؾߵ�
    public void CleanScreen()
    {
        if (stain_List.Count > 0)
        {
            //�� ����Ʈ ���������� �ϴ� �� ����ȭ �����̶��ϴ�
            int lastIndex = stain_List.Count - 1;
            GameObject targetPoop = stain_List[lastIndex];

            Destroy(targetPoop);
            stain_List.RemoveAt(lastIndex);
        }
        else
        {
            Debug.Log("���� ���� ���ٳ� (*)");
        }
    }
}
