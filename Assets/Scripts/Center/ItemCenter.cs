using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCenter : MonoBehaviour
{
    [Header("플레이어")]
    public PlayerController player;

    [Header("아이템 UI")]
    public GetItem getItem;

    private List<Skate> skateList = new List<Skate>();
    private List<Balloon> balloonList = new List<Balloon>();
    private List<Potion> potionList = new List<Potion>();


    private void Awake() 
    {
        player.stat.AddObserver<IObserver>(player.stat.itemObserverList, getItem);
    }

    public void GetSpeed()
    {
        player.stat.AddSpeed();
    }

    public void GetBalloon()
    {
        player.stat.AddBalloon();
    }

    public void GetPower()
    {
        player.stat.AddPotion();
    }

    public void RegisterSkate(Skate skate)
    {
        if (!skateList.Contains(skate))
        {
            skateList.Add(skate);
            skate.OnSpeedUp += GetSpeed;
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

    public void RegisterPotion(Potion potion)
    {
        if(!potionList.Contains(potion))
        {
            potionList.Add(potion);
            potion.OnPowerUp += GetPower;    
        }
    }
}
