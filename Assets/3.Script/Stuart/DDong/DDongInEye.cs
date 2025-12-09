using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DDongInEye : MonoBehaviour
{
    [Header("19 Mode")]
    public GameObject poopUIPrefab;

    [Header("Cute Mode")]
    public GameObject _PoopCuteUI_Prefab;

    public Transform canvasTransform;

    [Header("Canvas에 묻는 이미지")]
    public bool isCute = false;
    public Sprite realPoop;
    public Sprite cutePoop;

    private List<GameObject> stain_List = new List<GameObject>();

    public void AddPoop()
    {
        GameObject stain;
        if (GameManager.Instance._IsCute)
        {
            stain = Instantiate(_PoopCuteUI_Prefab, canvasTransform);
        }
        else
        {
            stain = Instantiate(poopUIPrefab, canvasTransform);
        }

        Image imgChange = stain.GetComponent<Image>();
        if (GameManager.Instance._IsCute)
        {
            imgChange.sprite = cutePoop;
        }
        else
        {
            imgChange.sprite = realPoop;
        }
        
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
    
    public void CleanScreen()
    {
        if (stain_List.Count > 0)
        {
            int lastIndex = stain_List.Count - 1;
            GameObject targetPoop = stain_List[lastIndex];

            Destroy(targetPoop);
            stain_List.RemoveAt(lastIndex);
        }
        else
        {
            Debug.Log("지울 것이 없습니다");
        }
    }
}
