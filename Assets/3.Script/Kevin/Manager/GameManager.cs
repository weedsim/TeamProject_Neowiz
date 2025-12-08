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

    public UIManager _TargetEffect;

    public WaitForSeconds _WFS = new WaitForSeconds(0.1f);

    public float _Time = 0f;
    public string _StartTime;

    [Header("Choosen Character Name")]
    public string _ChooseCharacter;

    private void Start()
    {
        StartCoroutine(FazingTransparent());
    }

    /// <summary>
    /// 
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
            
        }
    }

    /// <summary>
    /// 플레이어 사망 시 호출
    /// 랭킹에 추가
    /// </summary>
    public void PlayerDie()
    {
        RankDataManager.Instance.AddRank();
    }


    


}
