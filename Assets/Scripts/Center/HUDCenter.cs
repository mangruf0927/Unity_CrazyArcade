using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HUDCenter : MonoBehaviour
{
    [Header("게임 메세지 UI")]
    [SerializeField]        private ShowMessage gameMessageUI;

    [Header("Help UI")]
    [SerializeField]        private GameObject HelpUI;

    [Header("나가기 버튼")]
    [SerializeField]        private Button exitButton;

    [Header("플레이어 프로필 애니메이터")]
    [SerializeField]        public Animator profileUIAnimator;

    [Header("FadeInOut")]
    [SerializeField]        private FadeInOut fade;

    public IEnumerator ShowStartMessage()
    {
        return gameMessageUI.ShowStartMessage();
    }

    public IEnumerator ShowClearMessage()
    {
        return gameMessageUI.ShowClearMessage();
    }
    
    public IEnumerator ShowLoseMessage()
    {
        return gameMessageUI.ShowLoseMessage();
    }

    public void PlayerTrapUI(bool isActive)
    {
        HelpUI.SetActive(isActive);
        exitButton.interactable = !isActive;
    }

    public void PlayProfileAnimation()
    {
        profileUIAnimator.Play("Lose");
    }

    public void FadeOutAndLoadScene(int num)
    {
        fade.StartFadeOutAndLoadScene(num);
    }

    public void FadeIn()
    {
        fade.StartFadeIn();
    }
}
