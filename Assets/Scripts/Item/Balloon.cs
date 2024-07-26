using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    public delegate void BalloonHandle();
    public event BalloonHandle OnBalloonUp;

    private void Awake()
    {
        // ItemCenter에 자신을 등록
        ItemCenter itemCenter = FindObjectOfType<ItemCenter>();
        if (itemCenter != null)
        {
            itemCenter.RegisterBalloon(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            OnBalloonUp?.Invoke();
            Destroy(gameObject, 0.1f);
        }
    }
}
