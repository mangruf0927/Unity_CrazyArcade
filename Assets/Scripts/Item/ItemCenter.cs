using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCenter : MonoBehaviour
{
    [Header("플레이어")]
    public PlayerController player;

    private List<Skate> skateList = new List<Skate>();
    private List<Balloon> balloonList = new List<Balloon>();
    private List<Potion> potionList = new List<Potion>();

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

    public void RegisterSkate(Skate skate)
    {
        if (!skateList.Contains(skate))
        {
            skateList.Add(skate);
            skate.OnSpeedUp += GetSpeed;
        }
    }

    public void RegisterPotion(Potion potion)
    {
        if(!potionList.Contains(potion))
        {
            potionList.Add(potion);
            potion.OnPowerUp += GetPower;    
        }
    }

    public void RegisterBalloon(Balloon balloon)
    {
        if(!balloonList.Contains(balloon))
        {
            balloonList.Add(balloon);
            balloon.OnBalloonUp += GetBalloon;
        }
    }
}
