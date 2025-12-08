using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private AddForce _player_OBJ = null; // Player 태그를 가진 오브젝트에서 연결

    [Header("방귀 게이지 UI")]
    public Image _TargetImage;       // 효과를 줄 이미지
    //public float duration = 0.1f;   // 1프레임에 다시 채워지는 값
    public Coroutine _Gauge_Co;

    [Header("Timer UI")]
    public Text _TimerText;

    [Header("Timer")]
    public int _Sec = 0;
    public int _Min = 0;
    public int _Hour = 0;
    public Coroutine _Timer_Co;

    [Header("Video Player")]
    public GameObject _VideoPlayer;

    [Header("GameOver_Canvas")]
    public Canvas _GameOver_Canvas;

    //private void Awake()
    //{
    //    CheckStartTime();
    //    GameManager.Instance._Time = 0f;

    //    if (_TargetImage == null)
    //    {
    //        GameObject.Find("FartGaugeIMG").TryGetComponent(out _TargetImage);
    //    }

    //    // 안전장치: 이미지가 연결 되어 있으면 이미지 설정
    //    if (_TargetImage != null)
    //    {
    //        // 코드로 강제 설정 (실수 방지)
    //        _TargetImage.type = Image.Type.Filled;
    //        _TargetImage.fillMethod = Image.FillMethod.Vertical;
    //        _TargetImage.fillOrigin = (int)Image.OriginVertical.Bottom;
    //        //targetImage.fillAmount = 1f; // 처음엔 다 보여야 함
    //    }

    //    if (_player_OBJ == null)
    //    {
    //        GameObject.FindGameObjectWithTag("Player").TryGetComponent(out _player_OBJ);
    //    }

    //    if (_TimerText == null)
    //    {
    //        GameObject.Find("LivingTimer").TryGetComponent(out _TimerText);
    //    }

    //    StartCoroutine(UpdateTimer_Co());
    //    StartCoroutine(UpdateGauge());
    //}

    /// <summary>
    /// 전투 씬 진입 시 호출 메서드(전투 시작 시각 저장용)
    /// </summary>
    public void CheckStartTime()
    {
        GameManager.Instance._StartTime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    }

    /// <summary>
    /// 방귀게이지 상시 충전 시작
    /// </summary>
    public void UpdateGauge()
    {
        _Gauge_Co = StartCoroutine(UpdateGauge_Co());
    }

    /// <summary>
    /// 방귀 게이지 UI에 표시
    /// </summary>
    public IEnumerator UpdateGauge_Co()
    {
        while (true)
        {
            _TargetImage.fillAmount = _player_OBJ.CurrentGauge / _player_OBJ.MaxGauge;
            yield return null;
        }
    }

    /// <summary>
    /// 타이머 실행
    /// </summary>
    public void UpdateTimer()
    {
        _Timer_Co = StartCoroutine(UpdateTimer_Co());
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

    /// <summary>
    /// 캐릭터 선택 시 호출 메서드
    /// </summary>
    /// <param name="characterName"></param>
    public void ChoosenCharacter(string characterName)
    {
        GameManager.Instance._ChooseCharacter = characterName;

        TurnonVideo();
    }

    /// <summary>
    /// 스킬 쿨타임 업데이트
    /// </summary>
    /// <returns></returns>
    public IEnumerator UpdateSkillColl_Co()
    {
        while (_player_OBJ)
        {


            yield return null;
        }
    }


    public void TurnonVideo()
    {
        _VideoPlayer.GetComponent<UnityEngine.Video.VideoPlayer>().Play();

        StartCoroutine(LoadingAsync("MainGame"));
    }

    /// <summary>
    /// 동영상 종료 후 
    /// </summary>
    /// <param name="sceneName"></param>
    /// <returns></returns>
    public IEnumerator LoadingAsync(string sceneName)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        asyncOperation.allowSceneActivation = false; // 씬 로딩이 완료되는대로 씬을 활성화

        while (!asyncOperation.isDone)
        {
            UnityEngine.Video.VideoPlayer videoPlayer;
            if (_VideoPlayer != null && _VideoPlayer.TryGetComponent(out videoPlayer))
            {
                // 동영상 플레이가 끝나면 다음 씬으로 이동
                if (videoPlayer.isPaused)
                {
                    CheckStartTime();

                    asyncOperation.allowSceneActivation = true;
                }
            }

            yield return null;
        }
    }

    /// <summary>
    /// 게임 오버 시 이름 입력하는 부분 나오게 함
    /// </summary>
    public void OnGameOverUI()
    {
        _GameOver_Canvas.gameObject.SetActive(true);

    }

    /// <summary>
    /// 게임 오버 후 플레이어가 이름 입력하고 버튼 클릭
    /// </summary>
    public void EnterPlayerName()
    {
        GameManager.Instance.EnterPlayerName(_GameOver_Canvas.GetComponentInChildren<InputField>().text);

    }

    /// <summary>
    /// 게임 오버 팝업에서 캐릭터 선택 씬으로 이동 버튼을 누른 경우
    /// </summary>
    public void GoToCharacterScene()
    {
        SceneManager.LoadScene("CharacterSelection");
    }

    /// <summary>
    /// 게임 오버 팝업에서 랭킹 버튼을 누른 경우
    /// </summary>
    public void OnRankingUI()
    {

    }


}
