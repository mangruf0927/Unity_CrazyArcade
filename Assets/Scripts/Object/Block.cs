using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public Animator animator;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Pop"))
        {
            animator.Play("Pop");

            if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.99f) return;

            Destroy(gameObject, 0.1f);
        }    
    }
}
