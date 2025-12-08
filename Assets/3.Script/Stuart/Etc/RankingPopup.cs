using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankingPopup : MonoBehaviour
{
    [Header("연결쓰")]
    public GameObject rankUIPrefab;
    public Transform content;
    public GameObject popupPanel;


    
    public void OpenPopup()
    {

        popupPanel.SetActive(true);
        List<RankData> data = new List<RankData>();
        ShowRank(data);
    }
    public void ClosePopup()
    {
        popupPanel.SetActive(false);
    }

    private void ShowRank(List<RankData> dataList)
    {
        
        for(int i = 0; i<dataList.Count; i++)
        {
            GameObject row = Instantiate(rankUIPrefab, content);
            RankingUI ui = row.GetComponent<RankingUI>();

            ui.SetData(i + 1,                          // 등수
                dataList[i].UserName,           // 이름
                (float)dataList[i].SurvivalTime, // 시간 (형변환 필수!)
                dataList[i].PlayDate            // 날짜
            );
        }
    }
}
