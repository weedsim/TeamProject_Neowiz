using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionPopup : MonoBehaviour
{
    [Header("BGM Option")]
    public GameObject popupPanel;
    public Slider bgmSlider;
    public Toggle cuteMode;

    [Header("Resource")]
    public AudioSource bgmPlayer;
    public DDongInEye ddong;

    private void Start()
    {
        if (bgmPlayer != null)
        {
            bgmSlider.value = bgmPlayer.volume;
        }
        if(ddong != null)
        {
            cuteMode.isOn = ddong.isCute;
        }
         
    }
    public void OpenOption()
    {
        popupPanel.SetActive(true);
    }
    public void CloseOption()
    {
        popupPanel.SetActive(false);
    }
    public void BGMChanger()
    {
        if(bgmPlayer != null)
        {
            bgmPlayer.volume = bgmSlider.value;
        }
    }
    public void CuteModeSetting()
    {
        GameManager.Instance._IsCute = !transform.GetChild(1).GetComponent<Toggle>().isOn;
    }
}
