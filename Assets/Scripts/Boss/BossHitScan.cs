using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHitScan : MonoBehaviour, ISubject
{
    public delegate void HitScanHandler();
    public HitScanHandler OnBalloonCollision;
    public HitScanHandler OnPlayerCollision;

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
        }

        // Debug.Log("보스 HP : " + currentHP);
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Pop"))
        {
            OnBalloonCollision?.Invoke();
        }    

        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            OnPlayerCollision?.Invoke();
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
