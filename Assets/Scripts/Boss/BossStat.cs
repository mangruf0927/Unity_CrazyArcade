using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStat : MonoBehaviour, ISubject
{
    [Header("보스 스피드")]
    public float moveSpeed = 2;

    [Header("보스 물줄기 길이")]
    public int idleAttackPopLength = 14;
    public int attackPopLength = 3;

    [Header("보스 HP")]
    public int maxHP = 100;
    private int curHP;

    public delegate void BossHandler();
    public event BossHandler OnTrapState;

    public List<IObserver> observerList = new List<IObserver>();

    private void Start() 
    {
        curHP = maxHP;    
    }

    // >>
    public int GetCurrentHP()
    {
        return curHP;
    }

    public void DamageUp()
    {
        curHP -= 5;
        
        if(curHP <= 0)
        {
            curHP = 0;
            // OnTrapState?.Invoke();
            Debug.Log("HP = 0");
        }
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
