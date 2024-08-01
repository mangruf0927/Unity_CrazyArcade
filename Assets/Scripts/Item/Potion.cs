using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    public delegate void PotionHandle();
    public event PotionHandle OnPowerUp;

    public ItemTypeEnums itemTypeEnums;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Pop"))
        {
            Destroy(gameObject, 0.1f);
        }
        
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            OnPowerUp?.Invoke();
            Destroy(gameObject, 0.1f);
        }
    }
}
