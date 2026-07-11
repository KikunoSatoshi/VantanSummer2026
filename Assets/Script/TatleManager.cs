using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class TitleManager : MonoBehaviour
{
    public ButtonBlink startButtonBlink;
    public ButtonBlink exitButtonBlink;
    public FadeByCoroutine fade;
    // ゲーム開始
    public void StartGame()
    {
        StartCoroutine(StartRoutine());
    }

    // ゲーム終了
    public void ExitGame()
    {
        StartCoroutine(ExitRoutine());
    }

    IEnumerator StartRoutine()
    {
        Debug.Log("① 点滅開始");

        yield return StartCoroutine(startButtonBlink.Blink());

        Debug.Log("② 点滅終了");

        yield return StartCoroutine(fade.Fade());

        Debug.Log("③ フェード終了");

        SceneManager.LoadScene("GameScene");
    }

    IEnumerator ExitRoutine()
    {
        yield return StartCoroutine(exitButtonBlink.Blink());

        yield return StartCoroutine(fade.Fade());

        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}