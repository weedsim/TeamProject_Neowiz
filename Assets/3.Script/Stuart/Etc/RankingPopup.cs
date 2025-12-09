using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankingPopup : MonoBehaviour
{
    [Header("Ranking Panel")]
    public GameObject rankUIPrefab;
    public Transform content;
    public GameObject popupPanel;


    
    public void OpenPopup()
    {

        popupPanel.SetActive(true);
        List<RankData> data = RankDataManager.Instance.GetRankList();
        ShowRank(data);
    }
    public void ClosePopup()
    {
        popupPanel.SetActive(false);
    }

    public void GoToCharacterScene()
    {
        Debug.Log("ddd");
        GameObject.Find("UIManager").GetComponent<UIManager>().GoToCharacterScene();
    }

    private void ShowRank(List<RankData> dataList)
    {
        foreach(Transform chid in content)
        {
            Destroy(chid.gameObject);
        }

        for(int i = 0; i<dataList.Count; i++)
        {
            GameObject row = Instantiate(rankUIPrefab, content);
            RankingUI ui = row.GetComponent<RankingUI>();

            ui.SetData(i + 1,
                dataList[i].UserName,
                (float)dataList[i].SurvivalTime,
                dataList[i].PlayDate
            );
        }
    }
}
