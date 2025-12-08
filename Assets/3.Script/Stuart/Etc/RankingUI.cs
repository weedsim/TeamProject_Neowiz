using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RankingUI : MonoBehaviour
{
    public TextMeshProUGUI rankText;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;

    public void SetData(int rank, string name, float score, string time)
    {
        rankText.text = rank + "��";
        nameText.text = name;
        scoreText.text = score + "��";
        timeText.text = time;
    }
}
