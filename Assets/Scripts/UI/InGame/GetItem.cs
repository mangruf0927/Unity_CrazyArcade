using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetItem : MonoBehaviour, IObserver
{
    [Header("물풍선 UI")]
    public GameObject balloonUI;
    public Slider balloonSlider;

    [Header("물약 UI")]
    public GameObject potionUI;
    public Slider potionSlider;

    [Header("스피드 UI")]
    public GameObject speedUI;
    public Slider speedSlider;

    // IObserver 인터페이스의 Notify 메서드 구현
    public void Notify(ISubject subject)
    {
        if (subject is PlayerStat playerStat)
        {
            // 어떤 변화가 일어났는지 판단
            if (playerStat.itmeType == ItemTypeEnums.BALLOON)
            {
                UpdateBalloonUI(playerStat);
            }
            else if (playerStat.itmeType == ItemTypeEnums.SKATE)
            {
                UpdateSpeedUI(playerStat);
            }
            else if (playerStat.itmeType == ItemTypeEnums.POTION)
            {
                UpdatePotionUI(playerStat);
            }
        }
    }


    // 풍선 UI 업데이트 및 활성화/비활성화 처리
    private void UpdateBalloonUI(PlayerStat playerStat)
    {
        DisableOtherUI();

        balloonUI.SetActive(true);
        balloonSlider.maxValue = playerStat.maxBalloonNum;
        balloonSlider.value = playerStat.balloonNum;

        // Debug.Log("물풍선 : " + playerStat.balloonNum + "/" + playerStat.maxBalloonNum );

        StartCoroutine(HideUI(balloonUI, 2f));
    }

    // 속도 UI 업데이트 및 활성화/비활성화 처리
    private void UpdateSpeedUI(PlayerStat playerStat)
    {        
        DisableOtherUI();

        speedUI.SetActive(true);
        speedSlider.maxValue = playerStat.maxSpeed;
        speedSlider.value = playerStat.moveSpeed;

        // Debug.Log("스케이드 : " + playerStat.moveSpeed + "/" + playerStat.maxSpeed );

        StartCoroutine(HideUI(speedUI, 2f));
    }

    // 포션 UI 업데이트 및 활성화/비활성화 처리
    private void UpdatePotionUI(PlayerStat playerStat)
    {
        DisableOtherUI();

        potionUI.SetActive(true);
        potionSlider.maxValue = playerStat.maxPopLength;
        potionSlider.value = playerStat.popLength;

        // Debug.Log("물약 : " + playerStat.popLength + "/" + playerStat.maxPopLength );

        StartCoroutine(HideUI(potionUI, 2f));
    }

    private void DisableOtherUI()
    {
        balloonUI.SetActive(false);
        speedUI.SetActive(false);
        potionUI.SetActive(false);
    }

    // 일정 시간 후 UI를 비활성화하는 Coroutine
    private IEnumerator HideUI(GameObject uiElement, float delay)
    {
        yield return new WaitForSeconds(delay);
        uiElement.SetActive(false);
    }
}
