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
        if(subject is BossHitScan hitScan)
        {
            hpSlider.maxValue = hitScan.maxHP;
            hpSlider.value = hitScan.currentHP;

            if(hitScan.currentHP <= 30)
            {
                fillImage.sprite = hpSprite;
            }

            if(hitScan.currentHP <= 0)
            {
                gameObject.SetActive(false);
            } 
        }
    }
}
