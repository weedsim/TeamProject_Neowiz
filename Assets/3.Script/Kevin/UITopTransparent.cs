using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.InputSystem;


public class UITopTransparent : MonoBehaviour
{

    [Header("설정")]
    public Image targetImage;       // 효과를 줄 이미지
    //public KeyCode triggerKey = KeyCode.Space; // 누를 키
    public float duration = 10f;   // 투명해지는 데 걸리는 시간

    private Coroutine currentCoroutine;

    void Start()
    {
        // 안전장치: 이미지가 연결 안 되어 있으면 자기 자신의 컴포넌트를 가져옴
        if (targetImage == null)
            targetImage = GetComponent<Image>();

        // 코드로 강제 설정 (실수 방지)
        targetImage.type = Image.Type.Filled;
        targetImage.fillMethod = Image.FillMethod.Vertical;
        targetImage.fillOrigin = (int)Image.OriginVertical.Bottom;
        targetImage.fillAmount = 1f; // 처음엔 다 보여야 함

        currentCoroutine = StartCoroutine(RestoreRoutine());
    }

    //void Update()
    //{
    //    // 키를 눌렀을 때 실행
    //    if (Input.GetKeyDown(triggerKey))
    //    {
    //        // 이미 실행 중인 코루틴이 있다면 멈추고 새로 시작
    //        if (currentCoroutine != null) StopCoroutine(currentCoroutine);

    //        currentCoroutine = StartCoroutine(FadeOutRoutine());
    //    }
    //    else
    //    {
    //        // 이미 실행 중인 코루틴이 있다면 멈추고 새로 시작
    //        if (currentCoroutine != null) StopCoroutine(currentCoroutine);

    //        currentCoroutine = StartCoroutine(RestoreRoutine());
    //    }
    //}

    void OnMove(InputValue inputValue)
    {
        Debug.Log("aa");
        if (inputValue.isPressed)
        {

            FadeOut();

            //// 이미 실행 중인 코루틴이 있다면 멈추고 새로 시작
            //if (currentCoroutine != null) StopCoroutine(currentCoroutine);

            //currentCoroutine = StartCoroutine(RestoreRoutine());
        }
    }

    IEnumerator FadeOutRoutine()
    {
        float fill = targetImage.fillAmount;
        // 이미지가 완전히 사라질 때까지 반복
        while (targetImage.fillAmount >= fill - 0.2f)
        {
            // 현재 값에서 점점 뺌 (부드럽게 이어지도록)
            targetImage.fillAmount -= 0.01f;
            yield return null;
        }
    }

    private void FadeOut()
    {
        targetImage.fillAmount -= 0.2f;
    }

    // --- 코루틴 2: 원래대로 복구됨 (FillAmount 0 -> 1) ---
    IEnumerator RestoreRoutine()
    {
        // 이미지가 완전히 찰 때까지 반복
        while (targetImage.fillAmount < 1f)
        {
            Debug.Log(targetImage.fillAmount);
            // 현재 값에서 점점 더함
            targetImage.fillAmount += Time.deltaTime / duration;
            yield return null;
        }

        targetImage.fillAmount = 1f; // 확실하게 1로 고정
    }
}