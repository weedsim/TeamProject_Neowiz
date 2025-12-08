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
    public void BGMChanger(float sound)
    {
        if(bgmPlayer != null)
        {
            bgmPlayer.volume = sound;
        }
    }
    public void CuteModeSetting(bool isOn)
    {
        if(ddong != null)
        {
            ddong.CheckCute(isOn);
        }
    }
}
