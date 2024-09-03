using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitPopUp : MonoBehaviour
{
    // Quit Button
    public void OnClickQuit()
    {
        // 게임이 에디터에서 실행 중일 때는 플레이 모드를 종료
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    // Cancle Button
    public void OnClickCancle()
    {
        this.gameObject.SetActive(false);
    }
}
