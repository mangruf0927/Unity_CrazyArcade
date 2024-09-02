using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneButton : MonoBehaviour
{
    public GameObject rulePanel;
    public int nextSceneNum;

    public void PressStartButton()
    {
        SceneManager.LoadScene(nextSceneNum);
    }

    public void ActiveGameRule()
    {
        rulePanel.SetActive(true);
    }

    public void DeactiveGameRule()
    {
        rulePanel.SetActive(false);
    }
}

