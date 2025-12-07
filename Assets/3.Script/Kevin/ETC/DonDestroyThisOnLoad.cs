using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DonDestroyThisOnLoad : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
