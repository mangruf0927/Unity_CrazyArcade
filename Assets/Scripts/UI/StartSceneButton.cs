using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneButton : MonoBehaviour
{
    public GameObject rulePanel;

    public void PressStartButton()
    {
        SceneManager.LoadScene("00.WaitingRoom");
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

