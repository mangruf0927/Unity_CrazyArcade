using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeInOut : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public float timeToFade;
    private bool isFading = false;

    public void StartFadeOutAndLoadScene(int sceneNum)
    {
        if (!isFading)
        {
            StartCoroutine(FadeOutAndLoadScene(sceneNum));
        }
    }

    public void StartFadeIn()
    {
        if (!isFading)
        {
            StartCoroutine(FadeIn());
        }
    }

    private IEnumerator FadeOutAndLoadScene(int sceneNum)
    {
        isFading = true;
        while (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += timeToFade * Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 1;

        // 씬을 전환하는 부분
        SceneManager.LoadScene(sceneNum);
    }

    private IEnumerator FadeIn()
    {
        isFading = true;
        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= timeToFade * Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 0;
        isFading = false;
    }

    public IEnumerator FadeOut()
    {
        isFading = true;
        while (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += timeToFade * Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 1;
    }
}