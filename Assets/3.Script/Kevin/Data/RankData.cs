using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class RankData : System.IComparable<RankData>
{
    public string UserName;
    public double SurvivalTime;
    public string PlayDate;

    public RankData(string userName, double survivalTime, string playDate)
    {
        UserName = userName;
        SurvivalTime = survivalTime;
        PlayDate = playDate;
    }

    public RankData() { }

    public int CompareTo(RankData other)
    {
        if(other.SurvivalTime != SurvivalTime)
        {
            return other.SurvivalTime.CompareTo(SurvivalTime);
        }

        return other.PlayDate.CompareTo(PlayDate);
    }
}

[System.Serializable]
public class FullRankData
{
    public List<RankData> RankDatas;
}
