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

    private int _currentHP;
    public int currentHP
    {
        get { return _currentHP; }
        set
        {
            _currentHP = value;
            NotifyObservers(hpObserverList);
        }
    }

    public delegate void BossHandler();
    public event BossHandler OnTrap;

    public List<IObserver> hpObserverList = new List<IObserver>();

    private void Start() 
    {
        currentHP = maxHP;    
    }

    // >>
    public void GetDamage()
    {
        currentHP -= 5;
        
        if(currentHP <= 0)
        {
            currentHP = 0;
            OnTrap?.Invoke();
        }

        // Debug.Log("보스 HP : " + currentHP);
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
