using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class StartSceneButton : MonoBehaviour, IPointerEnterHandler
{
    public GameObject rulePanel;
    public int nextSceneNum;

    public void PressStartButton()
    {
        SoundManager.Instance.PlaySFX("Click");
        SceneManager.LoadScene(nextSceneNum);
    }

    public void ActiveGameRule()
    {
        SoundManager.Instance.PlaySFX("Click");
        rulePanel.SetActive(true);
    }

    public void DeactiveGameRule()
    {
        SoundManager.Instance.PlaySFX("Cancle");
        rulePanel.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        SoundManager.Instance.PlaySFX("Hover");
    }
}

