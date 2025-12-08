using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.InputSystem;


public class UITopTransparent : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private AddForce _player_OBJ = null; // Player 태그를 가진 오브젝트에서 연결

    [Header("방귀 게이지 UI")]
    public Image _TargetImage;       // 효과를 줄 이미지
    //public float duration = 0.1f;   // 1프레임에 다시 채워지는 값

    [Header("Timer UI")]
    public Text _TimerText;

    [Header("Timer")]
    public int _Sec = 0;
    public int _Min = 0;
    public int _Hour = 0;

    private void Awake()
    {
        CheckStartTime();
        GameManager.Instance._Time = 0f;

        if (_TargetImage == null)
        {
            GameObject.Find("FartGaugeIMG").TryGetComponent(out _TargetImage);
        }

        // 안전장치: 이미지가 연결 되어 있으면 이미지 설정
        if (_TargetImage != null)
        {
            // 코드로 강제 설정 (실수 방지)
            _TargetImage.type = Image.Type.Filled;
            _TargetImage.fillMethod = Image.FillMethod.Vertical;
            _TargetImage.fillOrigin = (int)Image.OriginVertical.Bottom;
            //targetImage.fillAmount = 1f; // 처음엔 다 보여야 함
        }

        if(_player_OBJ == null)
        {
            GameObject.FindGameObjectWithTag("Player").TryGetComponent(out _player_OBJ);
        }

        if(_TimerText == null)
        {
            GameObject.Find("LivingTimer").TryGetComponent(out _TimerText);
        }

        StartCoroutine(UpdateTimer_Co());
        StartCoroutine(UpdateGauge());
    }

    /// <summary>
    /// 전투 씬 진입 시 호출 메서드(전투 시작 시각 저장용)
    /// </summary>
    public void CheckStartTime()
    {
        GameManager.Instance._StartTime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    }

    /// <summary>
    /// 방귀 게이지 UI에 표시
    /// </summary>
    public IEnumerator UpdateGauge()
    {
        while (true)
        {
            _TargetImage.fillAmount = _player_OBJ.CurrentGauge / _player_OBJ.MaxGauge;
            yield return null;
        }

    }

    /// <summary>
    /// 타이머 UI 표시 업데이트 / 게임 플레이 씬에 들어가면 시작
    /// </summary>
    /// <returns></returns>
    public IEnumerator UpdateTimer_Co()
    {
        while (true)
        {
            GameManager.Instance._Time += Time.deltaTime;

            _Sec = (int)GameManager.Instance._Time;
            _Min = _Sec / 60;
            _Sec %= 60;
            _Hour = _Min / 60;
            _Min %= 60;

            SetTimer();

            yield return null;
        }
    }

    /// <summary>
    /// 타이머 업데이트
    /// </summary>
    public void SetTimer()
    {
        string timer = string.Format("{0} : {1:D2} : {2:D2}", _Hour, _Min, _Sec);
        _TimerText.text = timer;
    }


}