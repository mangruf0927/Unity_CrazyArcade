using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public Animator animator;
    
    public delegate void BlockHandle(Vector2 pos);
    public event BlockHandle OnCheckPosition; 

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Pop"))
        {
            animator.Play("Pop");

            if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.99f) return;

            OnCheckPosition?.Invoke(transform.position);
            Destroy(gameObject, 0.1f);
        }    
    }
}
