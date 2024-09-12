using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HUDCenter : MonoBehaviour
{
    [Header("게임 메세지 UI")]
    [SerializeField]        private ShowMessage gameMessageUI;

    [Header("Help UI")]
    [SerializeField]        private GameObject HelpUI;

    [Header("나가기 버튼")]
    [SerializeField]        private GameObject exitButton;

    [Header("플레이어 프로필 애니메이터")]
    [SerializeField]        public Animator profileUIAnimator;

    public void ShowStartMessage()
    {
        StartCoroutine(gameMessageUI.ShowStartMessage());
    }

    public void ShowClearMessage()
    {
        StartCoroutine(gameMessageUI.ShowClearMessage());
    }
    
    public void ShowLoseMessage()
    {
        StartCoroutine(gameMessageUI.ShowLoseMessage());
    }

    public void PlayerTrapUI(bool isActive)
    {
        HelpUI.SetActive(isActive);
        exitButton.SetActive(!isActive);
    }

    public void PlayProfileAnimation()
    {
        profileUIAnimator.Play("Lose");
    }
}
