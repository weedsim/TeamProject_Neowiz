using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{

    #region Singleton
    public static GameManager Instance = null;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    public UIManager _TargetEffect;

    public WaitForSeconds _WFS = new WaitForSeconds(0.1f);

    public float _Time = 0f;
    public string _StartTime;

    [Header("혐인가 아닌가")]
    public bool _IsCute = false;

    [Header("Choosen Character Name")]
    public string _ChooseCharacter;

    [Header("죽었니 살았니")]
    public bool _IsGameOver = false;



    //private void Start()
    //{
    //    GameObject.Find("UIManager").GetComponent<UIManager>().CheckStartTime();
    //    GameObject.Find("UIManager").GetComponent<UIManager>().UpdateTimer();
    //}



    /// <summary>
    /// 플레이어 사망 후 랭킹에 등록 시 이름 지정
    /// </summary>
    /// <param name="playerName"></param>
    public void InitName(string playerName)
    {
        RankDataManager.Instance._PlayerName = playerName;
    }


    public void ToRankingScene()
    {
        SceneManager.LoadScene("RankingScene");
    }
    

    public void GameOver()
    {
        if (_IsGameOver)
        {
            return;
        }

        _IsGameOver = true;
        Time.timeScale = 0f;

        // UIManager에서 게임오버 시의 UI가 뜨게끔 하기

        _TargetEffect = GameObject.Find("UIManager").GetComponent<UIManager>();
        _TargetEffect.OnGameOverUI();

    }

    /// <summary>
    /// 랭킹에 쓰일 이름 등록
    /// </summary>
    /// <param name="playerName"></param>
    public void EnterPlayerName(string playerName)
    {
        RankDataManager.Instance._PlayerName = playerName;
        RankDataManager.Instance.AddRank();
    }


    public void ResetGame()
    {
        _Time = 0;
        _IsGameOver = false;
        Time.timeScale = 1f;
    }

}
