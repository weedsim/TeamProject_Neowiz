using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RankingUI : MonoBehaviour
{
    public TextMeshProUGUI rankText;
    public Text nameText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;

    public void SetData(int rank, string name, float score, string time)
    {
        rankText.text = rank + "DDONG";
        nameText.text = name;
        scoreText.text = score + "time";
        timeText.text = time;
    }
}
