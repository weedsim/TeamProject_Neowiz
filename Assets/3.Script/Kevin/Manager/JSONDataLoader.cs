using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using LitJson;

[System.Serializable]
public class JSONDataLoader
{
    /// <summary>
    /// 파일 이름과 경로 지정
    /// </summary>
    [SerializeField] private const string _rankDataJsonFilePath = "/RankData.json";

    /// <summary>
    /// 랭킹 시스템 데이터 불러오는 메서드
    /// </summary>
    /// <returns></returns>
    public static FullRankData RankDataLoader()
    {
        // 파일이 없는 경우 생성
        if (!File.Exists(Application.persistentDataPath + _rankDataJsonFilePath))
        {
            Debug.Log("랭킹 파일이 없습니다. 새로운 데이터를 생성합니다.");
            FullRankData tempDatas = new FullRankData();
            tempDatas.RankDatas = new List<RankData>();
            RankData tempData = new RankData();
            tempData.UserName = "Origin";
            tempData.SurvivalTime = 0;
            tempData.PlayDate = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            tempDatas.RankDatas.Add(tempData);
            SaveRankData(tempDatas);
            return tempDatas;
        }

        // 에러나면 빈 데이터로 전송되게끔 진행
        try
        {
            string jsonString = File.ReadAllText(Application.persistentDataPath + _rankDataJsonFilePath);

            // json 파일 내용을 랭크 시스템 데이터로 변환
            FullRankData rankDatas = JsonMapper.ToObject<FullRankData>(jsonString);

            Debug.Log($"랭킹 데이터 로드: {rankDatas}");

            return rankDatas;
        }
        catch (System.Exception e)
        {
            FullRankData temp = null;
            Debug.LogError($"랭킹 로드 중 에러 발생: {e.Message}");
            return temp;
        }
    }

    /// <summary>
    /// 입력받은 rankData로 Json 파일에 저장
    /// </summary>
    /// <param name="rankData"></param>
    public static void SaveRankData(FullRankData rankData)
    {
        try
        {
            // Json 형식으로 변환
            string jsonString = JsonMapper.ToJson(rankData);

            File.WriteAllText(Application.persistentDataPath + _rankDataJsonFilePath, jsonString);

            Debug.Log($"랭킹 저장 완료! {jsonString}");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"랭킹 저장 실패: {e.Message}");
        }
    }

}
