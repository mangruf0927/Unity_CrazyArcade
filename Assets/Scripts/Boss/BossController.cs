using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [Header("보스 상태머신")]
    public BossStateMachine stateMachine;

    private void Update() 
    { 
        if(stateMachine.curState != null)
            stateMachine.curState.Update();
    }

    private void FixedUpdate() 
    {
        if(stateMachine.curState != null)
            stateMachine.curState.FixedUpdate();
    }
}
