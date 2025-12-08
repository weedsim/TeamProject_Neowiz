using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroStart : MonoBehaviour
{
    public void GoNextScene()
    {
        SceneManager.LoadScene("CharacterSelection");   // 캐릭터 선택 창으로 이동
    }
}
