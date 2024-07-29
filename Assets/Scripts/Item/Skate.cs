using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skate : MonoBehaviour
{
    public delegate void SkateHandle();
    public event SkateHandle OnSpeedUp;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Pop"))
        {
            Destroy(gameObject, 0.1f);
        }
        
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            OnSpeedUp?.Invoke();
            Destroy(gameObject, 0.1f);
        }
    }
}
