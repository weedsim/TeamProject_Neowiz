using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
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

    public UITopTransparent _TargetEffect;

    public WaitForSeconds _WFS = new WaitForSeconds(0.1f);

    private void Start()
    {
        StartCoroutine(FazingTransparent());
    }

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
