using UnityEngine;
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

    public UITopTransparent _TargetEffect;

    public WaitForSeconds _WFS = new WaitForSeconds(0.1f);

    public float _Time = 0f;
    public string _StartTime;


    private void Start()
    {
        StartCoroutine(FazingTransparent());
    }

    /// <summary>
    /// 게임 플레이 씬으로 넘어가면 실행
    /// </summary>
    /// <returns></returns>
    public IEnumerator FazingTransparent()
    {
        while (true)
        {
            UpdateTrasparency(0.2f);

            yield return _WFS;
        }
    }

    public void UpdateTrasparency(float inputValue)
    {
        if(_TargetEffect != null)
        {
            //_TargetEffect.SetTransparencyRange(inputValue);
        }
    }


}
