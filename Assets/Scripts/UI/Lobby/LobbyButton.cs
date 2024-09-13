using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class LobbyButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("다음 씬 번호")]
    public int nextSceneNum;

    [Header("메뉴 버튼")]
    public Button menuButton;
    public GameObject menuPopUp;
    public GameObject menuMessage;

    [Header("Button Image")]
    public Sprite defaultImage;       // 기본 이미지
    public Sprite selectedImage;      // 선택 이미지
    public Sprite highlightedImage;   // 하이라이트 이미지

    [Header("종료 버튼")]
    public Button exitButton;
    public GameObject exitPopUp;
    public GameObject exitMessage;

    [Header("FadeInOut")]
    public FadeInOut fade;

    private bool isMenuActive = false;

    // Start Button
    public void OnClickStart()
    {
        fade.StartFadeOutAndLoadScene(nextSceneNum); 
    }

    // Menu Button
    public void OnClickMenu()
    {
        menuMessage.SetActive(false);
        
        isMenuActive = !isMenuActive; 
        menuPopUp.SetActive(isMenuActive); 

        // 메뉴 버튼 이미지 설정
        menuButton.image.sprite = isMenuActive ? selectedImage : defaultImage;
    }

    // Exit Button
    public void OnClickExit()
    {
        exitMessage.SetActive(false);
        exitPopUp.SetActive(true);
    }

    // Message Active & Deactivate
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerEnter == menuButton.gameObject && !isMenuActive)
        {
            menuButton.image.sprite = highlightedImage;
            menuMessage.SetActive(true);
        }

        if (eventData.pointerEnter == exitButton.gameObject)
        {
            exitMessage.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.pointerEnter == menuButton.gameObject && !isMenuActive)
        {
            menuButton.image.sprite = defaultImage;
            menuMessage.SetActive(false);
        }

        if (eventData.pointerEnter == exitButton.gameObject)
        {
            exitMessage.SetActive(false);
        }
    }
}

