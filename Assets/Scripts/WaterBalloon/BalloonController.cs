using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonController : MonoBehaviour
{
    [Header("애니메이터")]
    public Animator animator;

    [Header("물풍선 상태 머신")]
    public BalloonStateMachine stateMachine;

    private void Update() 
    {
        if(stateMachine.curState != null)
            stateMachine.curState.Update(); 

        Debug.Log("물풍선 상태" + stateMachine.curState);
    }

    private void FixedUpdate()
    {
        if(stateMachine.curState != null)
            stateMachine.curState.FixedUpdate();
    }
}