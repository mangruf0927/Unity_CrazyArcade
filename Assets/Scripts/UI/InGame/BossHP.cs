using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHP : MonoBehaviour, IObserver
{
    public Slider hpSlider;
    public Image fillImage;
    public Sprite hpSprite;

    // IObserver 인터페이스의 Notify 메서드 구현
    public void Notify(ISubject subject)
    {
        if(subject is BossStat bossStat)
        {
            hpSlider.maxValue = bossStat.maxHP;
            hpSlider.value = bossStat.currentHP;

            if(bossStat.currentHP <= 30)
            {
                fillImage.sprite = hpSprite;
            }

            if(bossStat.currentHP <= 0)
            {
                gameObject.SetActive(false);
            } 
        }
    }
}
