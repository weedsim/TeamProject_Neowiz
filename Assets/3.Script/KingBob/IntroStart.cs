using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroStart : MonoBehaviour
{
    public void GoNextScene()
    {
        SceneManager.LoadScene("KingBob");   // 이동할 씬 이름
    }
}
