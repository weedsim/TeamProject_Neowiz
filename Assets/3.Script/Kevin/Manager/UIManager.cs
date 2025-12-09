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

    [Header("Prefabs")]
    [SerializeField] private GameObject _boy_OBJ;
    [SerializeField] private GameObject _girl_OBJ;
    [SerializeField] private GameObject _go_OBJ;
    [SerializeField] private Transform _characterPosition;

    [Header("Skill CoolTime Image")]
    public Image _SkillCool_IMG;
    public Image _SkillCool_Background_IMG;

    public Sprite _BoySkillIcon;
    public Sprite _GirlSkillIcon;
    public Sprite _GoSkillIcon;

    private void Awake()
    {
        GameManager.Instance._TargetEffect = this;

        // 안전장치: 이미지가 연결 되어 있으면 이미지 설정
        if (_TargetImage != null)
        {
            // 코드로 강제 설정 (실수 방지)
            _TargetImage.type = Image.Type.Filled;
            _TargetImage.fillMethod = Image.FillMethod.Vertical;
            _TargetImage.fillOrigin = (int)Image.OriginVertical.Bottom;
            //targetImage.fillAmount = 1f; // 처음엔 다 보여야 함
        }

        if (SceneManager.GetActiveScene().name.Equals("MainGame"))
        {
            // 초기화 
            CheckStartTime();
            GameManager.Instance.ResetGame();
            GameObject playerOBJ;
            switch (GameManager.Instance._ChooseCharacter)
            {
                case "Boy":
                    playerOBJ = Instantiate(_boy_OBJ, _characterPosition.position, _characterPosition.rotation);
                    _SkillCool_IMG.sprite = _BoySkillIcon;
                    _SkillCool_Background_IMG.sprite = _BoySkillIcon;
                    break;

                case "Girl":
                    playerOBJ = Instantiate(_girl_OBJ, _characterPosition.position, _characterPosition.rotation);
                    _SkillCool_IMG.sprite = _GirlSkillIcon;
                    _SkillCool_Background_IMG.sprite = _GirlSkillIcon;
                    break;

                case "Go":
                    playerOBJ = Instantiate(_go_OBJ, _characterPosition.position, _characterPosition.rotation);
                    _SkillCool_IMG.sprite = _GoSkillIcon;
                    _SkillCool_Background_IMG.sprite = _GoSkillIcon;
                    break;

                default:
                    Debug.Log("없는 캐릭터를 요청하였습니다.");
                    playerOBJ = null;
                    break;
            }

            playerOBJ.TryGetComponent(out _player_OBJ);
            PlayerSkill playerSkill;
            playerOBJ.TryGetComponent(out playerSkill);
            playerSkill.effectManager = GameObject.Find("skilleffect_tester").GetComponent<SkillEffectManager>();

            Camera.main.transform.SetParent(playerOBJ.transform);
            UpdateGauge();
            UpdateTimer();
        }

    }

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
        if (_TimerText != null)
        {
            _TimerText.text = timer;
        }
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
        _SkillCool_IMG.fillAmount = 0;
        while (_SkillCool_IMG.fillAmount < 1f)
        {
            _SkillCool_IMG.fillAmount += Time.deltaTime / 60f;

            yield return null;
        }
    }

    /// <summary>
    /// 비디오 실행 메서드
    /// </summary>
    public void TurnonVideo()
    {
        Camera.main.gameObject.GetComponent<AudioSource>().volume = 0;

        _VideoPlayer.GetComponent<UnityEngine.Video.VideoPlayer>().Play();

        GameManager.Instance.ResetGame();

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

                    GameManager.Instance.ResetGame();

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

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Camera.main.transform.parent = transform.parent;
        player.SetActive(false);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

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


    public void OnSkip()
    {
        _VideoPlayer.GetComponent<UnityEngine.Video.VideoPlayer>().Pause();
    }


}
