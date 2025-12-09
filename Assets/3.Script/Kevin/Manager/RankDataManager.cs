using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RankDataManager : MonoBehaviour
{
    #region Singleton
    public static RankDataManager Instance = null;

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

    [Header("Rank System Datas")]
    [SerializeField] private FullRankData _rankDatas;
    [SerializeField] private string _currentDate;
    [SerializeField] private float _surviveTime;
    public string _PlayerName;

    [Header("상위 100등까지만 저장")]
    [SerializeField] private const int _topRankCount = 100;

    /// <summary>
    /// 게임 시작시 호출해두기
    /// </summary>
    private void Start()
    {
        _rankDatas = JSONDataLoader.RankDataLoader();

    }

    /// <summary>
    /// 플레이어 사망 시 호출 메서드(랭킹 시스템에 최종 등록)
    /// </summary>
    /// <param name="playerName"></param>
    /// <param name="survivalTime"></param>
    public void AddRank()
    {
        _surviveTime = GameManager.Instance._Time;
        _currentDate = GameManager.Instance._StartTime;

        // 랭킹 시스템에 추가할 데이터 생성
        RankData newRankData = new RankData(_PlayerName, (double)_surviveTime, _currentDate);

        // 랭킹 시스템 리스트에 추가
        _rankDatas.RankDatas.Add(newRankData);

        // 랭킹 시스템 정렬
        _rankDatas.RankDatas.Sort();

        // 혹시라도 최대 등수 보다 많이 등록이 된 경우 최대 등수까지만 저장되게끔
        if(_rankDatas.RankDatas.Count > _topRankCount)
        {
            _rankDatas.RankDatas.RemoveRange(_topRankCount, _rankDatas.RankDatas.Count - _topRankCount);
        }

        Debug.Log("등록할 데이터는 " + newRankData);

        // 랭킹 데이터 등록했으니 json 파일에 저장
        JSONDataLoader.SaveRankData(_rankDatas);


    }

    /// <summary>
    /// UI 표시를 위해 데이터를 가져오는 메서드
    /// </summary>
    /// <returns></returns>
    public List<RankData> GetRankList()
    {
        return _rankDatas.RankDatas;
    }



}
