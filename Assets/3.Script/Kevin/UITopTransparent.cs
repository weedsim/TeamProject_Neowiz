using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.InputSystem;


public class UITopTransparent : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private AddForce _player_OBJ = null; // 직접 플레이어 오브젝트 연결

    [Header("설정")]
    public Image targetImage;       // 효과를 줄 이미지
    //public float duration = 0.1f;   // 1프레임에 다시 채워지는 값

    [Header("Timer")]
    public float _Sec = 0f;
    public float _Min = 0f;
    public float _Hour = 0f;
    public string _StartTime;

    private void Awake()
    {
        _StartTime = System.DateTime.Now.ToString();

        // 안전장치: 이미지가 연결 안 되어 있으면 자기 자신의 컴포넌트를 가져옴
        if (targetImage == null)
            targetImage = GetComponent<Image>();

        // 코드로 강제 설정 (실수 방지)
        targetImage.type = Image.Type.Filled;
        targetImage.fillMethod = Image.FillMethod.Vertical;
        targetImage.fillOrigin = (int)Image.OriginVertical.Bottom;
        //targetImage.fillAmount = 1f; // 처음엔 다 보여야 함

        StartCoroutine(UpdateTimer_Co());
    }

    private void Update()
    {
        UpdateGauge();
    }

    public void UpdateGauge()
    {
        targetImage.fillAmount = _player_OBJ.CurrentGauge / _player_OBJ.MaxGauge;
    }

    public IEnumerator UpdateTimer_Co()
    {
        while (true)
        {
            _Sec += Time.deltaTime;

            if(_Sec > 59)
            {
                _Sec -= 60;
                _Min++;
                if(_Min > 59)
                {
                    _Min -= 60;
                    _Hour++;
                }
            }





            yield return null;
        }
    }




}