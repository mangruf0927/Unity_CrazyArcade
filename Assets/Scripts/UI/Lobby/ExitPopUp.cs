using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitPopUp : MonoBehaviour
{
    public FadeInOut fade;

    // Quit Button
    public void OnClickQuit()
    {
        StartCoroutine(ExitGame());
    }

    // Cancle Button
    public void OnClickCancle()
    {
        SoundManager.Instance.PlaySFX("Cancle");
        this.gameObject.SetActive(false);
    }

    private IEnumerator ExitGame()
    {
        SoundManager.Instance.StopBGM();
        SoundManager.Instance.PlaySFX("GameExit");

        yield return StartCoroutine(fade.FadeOut());
        yield return new WaitForSeconds(5f);

        // 게임이 에디터에서 실행 중일 때는 플레이 모드를 종료
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
