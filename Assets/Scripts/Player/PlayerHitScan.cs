using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitScan : MonoBehaviour
{
    public delegate void HitScanHandle();
    public event HitScanHandle OnDamage;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Pop"))
        {
            OnDamage?.Invoke();
        }
    }
}
