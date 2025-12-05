using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DDongInEye : MonoBehaviour
{
    public GameObject poopUIPrefab;
    public Transform canvasTransform;

    [Header("혐인가 아닌가")]
    public bool isCute = false;
    public Sprite realPoop;
    public Sprite cutePoop;

    private List<GameObject> stain_List = new List<GameObject>();

    public void AddPoop()
    {
        GameObject stain = Instantiate(poopUIPrefab, canvasTransform);

        Image imgChange = stain.GetComponent<Image>(); // 혐인가 아닌가!
        if (isCute == true)
        {
            imgChange.sprite = cutePoop;
        }
        else
        {
            imgChange.sprite = realPoop;
        }
        //화면에서 랜덤하게 묻기
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
    //아이템으로 지우는 거 추가해야됨
}
