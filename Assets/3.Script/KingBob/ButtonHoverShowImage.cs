using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHoverShowImage : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject hoverImage;  // 노출할 이미지 오브젝트

    private void Start()
    {
        hoverImage.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hoverImage.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hoverImage.SetActive(false);
    }
}
