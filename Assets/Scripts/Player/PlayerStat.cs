using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour, ISubject
{
    // : 플레이어 스탯
    [HideInInspector]
    public float moveSpeed;

    [HideInInspector]
    public float maxSpeed;
    
    [HideInInspector]
    public int maxBalloonNum;

    [HideInInspector]
    public int balloonNum;
    private int curBalloonNum;

    [HideInInspector]
    public int maxPopLength;

    [HideInInspector]
    public int popLength;
    
    public ItemTypeEnums itmeType;
    public List<IObserver> itemObserverList = new List<IObserver>();

    private void Start() 
    {
        curBalloonNum = 0;    
    }

    // >> :
    public int GetCurBalloon()
    {
        return curBalloonNum;
    }

    public void ChargeBalloon()
    {
        if (curBalloonNum > 0)
        {
            curBalloonNum--;
        }
    }

    public void UseBalloon()
    {
        curBalloonNum++;
    }

    public void AddBalloon()
    {
        if(balloonNum < maxBalloonNum)
        {
            balloonNum ++;
            itmeType = ItemTypeEnums.BALLOON;
            NotifyObservers(itemObserverList);  // 풍선 개수 변화 알림
        }
    }

    public void AddSpeed()
    {
        if(moveSpeed < maxSpeed)
        {
            moveSpeed ++;
            itmeType = ItemTypeEnums.SKATE;
            NotifyObservers(itemObserverList);  // 스피드 개수 변화 알림
        }
    }

    public void AddPotion()
    {
        if(popLength < maxPopLength)
        {
            popLength++;
            itmeType = ItemTypeEnums.POTION;
            NotifyObservers(itemObserverList); // 물약 개수 변화 알림
        }
    }

    public void SetBalloonNum(int init, int max)
    {
        balloonNum = init;
        maxBalloonNum = max;
    }

    public void SetSpeed(float init, float max)
    {
        moveSpeed = init;
        maxSpeed = max;
    }

    public void SetPopLength(int init, int max)
    {
        popLength = init;
        maxPopLength = max;
    }

    // >> 
    public void AddObserver<T>(List<T> observerList, T observer) where T : IObserver
    {
        observerList.Add(observer);
    }

    public void RemoveObserver<T>(List<T> observerList, T observer) where T : IObserver
    {
        observerList.Remove(observer);
    }

    public void NotifyObservers<T>(List<T> observerList) where T : IObserver
    {
        foreach (T observer in observerList)
        {
            observer.Notify(this);
        }
    }
}
