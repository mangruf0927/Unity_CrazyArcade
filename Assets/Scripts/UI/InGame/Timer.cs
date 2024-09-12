using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [Header("타이머 시간(분으로 입력)")]
    public float totaltime; 

    [Header("타이머를 나타낼 이미지")]
    public Image[] timerImageArray;

    [Header("0-9 숫자 스프라이트")]
    public Sprite[] spritesArray; 

    public delegate void timeHandler();
    public timeHandler OnEndTime;


    private void Start() 
    {
        totaltime *= 60f;
        StartCoroutine(UpdateTimer());        
    }

    public IEnumerator UpdateTimer()
    {
        while(totaltime > 0)
        {
        totaltime -= 1f;
        UpdateTimerImage();

        yield return new WaitForSeconds(1f);
        }

        if(totaltime == 0) OnEndTime?.Invoke();     
    }

        public void UpdateTimerImage()
    {
        int minutes = Mathf.FloorToInt(totaltime / 60f); // 남은 분 계산
        int seconds = Mathf.FloorToInt(totaltime % 60f); // 남은 초 계산

        // 두 자리로 표현하기 위해 문자열 포맷팅
        string minutesString = minutes.ToString("D2");
        string secondsString = seconds.ToString("D2");

        // 분과 초를 결합하여 이미지 업데이트
        UpdateNumberImages(minutesString, secondsString);
    }

    private void UpdateNumberImages(string minutesString, string secondsString)
    {
        // 분 이미지 업데이트
        timerImageArray[0].sprite = spritesArray[int.Parse(minutesString[0].ToString())]; // 십의 자리
        timerImageArray[1].sprite = spritesArray[int.Parse(minutesString[1].ToString())]; // 일의 자리

        // 초 이미지 업데이트
        timerImageArray[2].sprite = spritesArray[int.Parse(secondsString[0].ToString())]; // 십의 자리
        timerImageArray[3].sprite = spritesArray[int.Parse(secondsString[1].ToString())]; // 일의 자리
    }
}
