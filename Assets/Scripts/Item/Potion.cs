using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    public delegate void PotionHandle();
    public event PotionHandle OnPowerUp;

    public ItemTypeEnums itemTypeEnums;
    private bool isGet = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Pop"))
        {
            Destroy(gameObject, 0.1f);
        }
        
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if(!isGet)
            {
                isGet = true;
                OnPowerUp?.Invoke();

                SoundManager.Instance.PlaySFX("GetItem");
                Destroy(gameObject, 0.1f);
            }
        }
    }
}
