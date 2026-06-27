using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    // ゲーム開始
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    // ゲーム終了
    public void ExitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}