using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCenter : MonoBehaviour
{
    [Header("플레이어")]
    public PlayerController player;

    [Header("아이템")]
    public Skate skate;
    public Potion potion;
    public Balloon balloon;

    private void Start() 
    {
        skate.OnSpeedUp += GetSpeed;
        potion.OnPowerUp += GetPower;    
        balloon.OnBalloonUp += GetBalloon;
    }

    public void GetSpeed()
    {
        Debug.Log("스피드 업");
        player.GetSpeed();
    }

    public void GetPower()
    {
        Debug.Log("물줄기 세짐");
        player.GetPotionPower();
    }

    public void GetBalloon()
    {
        Debug.Log("물풍선 겟");
        player.GetBalloon();
    }
}
